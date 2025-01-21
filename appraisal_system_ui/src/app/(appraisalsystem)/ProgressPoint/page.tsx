"use client";
import { useEffect, useState } from "react";
import axiosInstance from "@/services/axiosInterceptor";
import DateProgressBar from "../components/DateProgressBar";
import { Card } from "primereact/card";
import { Message } from "primereact/message";
import {
  FaUsers,
  FaCheckCircle,
  FaHourglass,
  FaRegClock,
} from "react-icons/fa";

const baseAPIUrl = process.env.NEXT_PUBLIC_API_URL;

interface AppraisalCycle {
  id: number;
  quarter: string;
  year: number;
  appraiseeStartDate: string | Date;
  appraiseeEndDate: string | Date;
  appraiserStartDate: string;
  appraiserEndDate: string;
  status: string;
}

interface CycleDetails {
  totalEmployeeCount: number;
  completedEmployeeCount: number;
  pendingEmployeeCount: number;
  underReviewEmployeeCount: number;
  notStartedEmployeeCount: number;
}

export default function Home() {
  const [userName, setUserName] = useState<string | null>(null);
  const [roles, setRoles] = useState<string[] | null>(null);
  const [appraisalCycle, setAppraisalCycle] = useState<AppraisalCycle | null>(
    null
  );
  const [cycleDetails, setCycleDetails] = useState<CycleDetails | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setUserName(localStorage.getItem("username"));
    const storedRoles = localStorage.getItem("roles");
    if (storedRoles) {
      const parsedRoles = JSON.parse(storedRoles);
      setRoles(parsedRoles);

      fetchAppraisees();
      if (parsedRoles.includes("Admin") || parsedRoles.includes("Reviewer")) {
        fetchCycleDetails();
      }
    }
  }, []);

  const fetchAppraisees = () => {
    axiosInstance
      .get(`/appraisal-cycles/active`)
      .then((response) => {
        setAppraisalCycle(response.data);
        setLoading(false);
        if (response.data) localStorage.setItem("cycle_id", response.data.id);
      })
      .catch((err) => {
        setError("Failed to load active appraisal cycle.");
        setLoading(false);
      });
  };

  const fetchCycleDetails = () => {
    axiosInstance
      .get("/Appraisal-Cycles/Details")
      .then((response) => {
        setCycleDetails(response.data.cycleDetails);
      })
      .catch((err) => {
        setError("Failed to load cycle details.");
      });
  };

  if (loading) {
    return (
      <div className="flex justify-center items-center h-screen">
        <div className="animate-spin rounded-full h-16 w-16 border-t-4 border-blue-500"></div>
      </div>
    );
  }

  return (
    <div className="bg-white p-8 w-full mx-auto mt-2 mb-19 shadow-xl rounded-lg h-fit">
      <h2 className="text-2xl font-semibold text-gray-800 mb-1 text-center">
        Welcome, {userName}
      </h2>

      {/* Appraisal Cycle Info */}
      {appraisalCycle ? (
        <Card className="mb-2 shadow-lg border-0 flex justify-center">
          <h3 className="text-xl font-semibold text-gray-800 flex justify-center">
            Appraisal Cycle
          </h3>
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 mt-2 items-center justify-center">
            <div>
              <p className="text-gray-500 flex justify-center">Quarter</p>
              <p className="text-lg text-gray-800 flex justify-center">
                {appraisalCycle.quarter}
              </p>
            </div>
            <div>
              <p className="text-gray-500 flex justify-center">Year</p>
              <p className="text-lg text-gray-800 flex justify-center">
                {appraisalCycle.year}
              </p>
            </div>
            <div>
              <p className="text-gray-500 flex justify-center">Status</p>
              <p className="text-lg text-gray-800 flex justify-center">
                <span
                  className={`inline-block px-3 py-1 rounded-full justify-center text-sm ${
                    appraisalCycle.status === "InProgress"
                      ? "bg-green-100 text-green-600"
                      : "bg-gray-100 text-gray-600"
                  }`}
                >
                  {appraisalCycle.status}
                </span>
              </p>
            </div>
          </div>
        </Card>
      ) : (
        <Message severity="info" text="Appraisal Cycle has not started yet!" />
      )}

      {/* Show Cycle Details for Admin */}
      {(roles?.includes("Admin") || roles?.includes("Reviewer")) &&
      cycleDetails ? (
        <div className="grid grid-cols-2 sm:grid-cols-2 lg:grid-cols-4 xl:grid-cols-4 gap-6 mt-2 ">
          <Card className="shadow-xl border-0 flex items-center h-[13rem] justify-center p-6">
            <FaUsers size={40} className="text-blue-500 mb-4 ml-8 flex justify-center" />
            <h3 className="text-lg font-semibold text-gray-800 flex justify-center">
              Appraisee's
            </h3>
            <p className="text-2xl text-gray-900 flex justify-center">
              {cycleDetails.totalEmployeeCount}
            </p>
          </Card>

          <Card className="shadow-xl border-0 flex items-center h-[13rem] justify-center p-6">
            <FaCheckCircle size={40} className="text-green-500 ml-7 mb-4" />
            <h3 className="text-lg font-semibold text-gray-800 flex justify-center">Completed</h3>
            <p className="text-2xl text-gray-900 flex justify-center">
              {cycleDetails.completedEmployeeCount}
            </p>
          </Card>

          <Card className="shadow-xl border-0 flex items-center h-[13rem] justify-center p-6">
            <FaHourglass size={40} className="text-yellow-500 mb-4 ml-10 " />
            <h3 className="text-lg font-semibold text-gray-800 flex justify-center">
              Under Review
            </h3>
            <p className="text-2xl text-gray-900 flex justify-center">
              {cycleDetails.underReviewEmployeeCount}
            </p>
          </Card>

          <Card className="shadow-xl border-0 flex items-center h-[13rem] justify-center p-6">
            <FaRegClock size={40} className="text-gray-500 mb-4 ml-4 " />
            <h3 className="text-lg font-semibold text-gray-800 flex justify-center">Pending</h3>
            <p className="text-2xl text-gray-900 flex justify-center">
              {cycleDetails.pendingEmployeeCount}
            </p>
          </Card>
        </div>
      ) : null}

      {/* Error Message */}
      {error && <Message severity="error" text={error} />}
    </div>
  );
}
