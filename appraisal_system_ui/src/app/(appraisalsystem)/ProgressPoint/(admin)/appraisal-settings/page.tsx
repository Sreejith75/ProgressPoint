"use client";
import React, { useState, useEffect } from "react";
import CycleForm from "./components/CycleForm";
import { useRouter } from "next/navigation";
import axiosInstance from "@/services/axiosInterceptor";

const baseApiUrl = process.env.NEXT_PUBLIC_API_URL;

interface AppraisalCycle {
  id: number;
  quarter: string;
  year: number;
  appraiseeStartDate: string;
  appraiseeEndDate: string;
  appraiserStartDate: string;
  appraiserEndDate: string;
  status: "NotStarted" | "InProgress" | "Completed";
}

const AppraisalSetup: React.FC = () => {
  const [appraisalCycles, setAppraisalCycles] = useState<AppraisalCycle[]>([]);
  const [showForm, setShowForm] = useState(false);
  const router = useRouter();

  useEffect(() => {
    const fetchAppraisalCycles = async () => {
      try {
        const response = await axiosInstance.get(
          `${baseApiUrl}/appraisal-cycles`
        );
        setAppraisalCycles(response.data);
      } catch (error) {
        console.error("Error fetching appraisal cycles:", error);
      }
    };

    fetchAppraisalCycles();
  }, [showForm]);

  const handleCycleAdded = (cycle: AppraisalCycle) => {
    setAppraisalCycles((prev) => [...prev, cycle]);
  };

  const handleManageForms = (cycleId: number) => {
    router.push(`/manage-cycle/${cycleId}`);
  };

  const handleUpdateStatus = async (
    cycleId: number,
    newStatus: AppraisalCycle["status"]
  ) => {
    try {
      const response = await axiosInstance.put(
        `${baseApiUrl}/appraisal-cycles/UpdatStatus/${cycleId}`,
        { status: newStatus }
      );

      setAppraisalCycles((prev) =>
        prev.map((cycle) =>
          cycle.id === cycleId
            ? {
                ...cycle, 
                status: newStatus,
              }
            : cycle
        )
      );
    } catch (error) {
      console.error("Error updating cycle status:", error);
    }
  };

  return (
    <div className="flex flex-col gap-6 p-6">
      <div className="relative p-6 bg-white shadow rounded-md">
        <h2 className="text-lg font-semibold mb-4">Appraisal Cycles</h2>
        <button
          onClick={() => setShowForm(true)}
          className="absolute top-4 right-4 px-4 py-3 bg-blue-600 text-white rounded text-sm"
        >
          Add Cycle
        </button>
        <table className="table-auto w-full text-left border-collapse mt-6">
          <thead>
            <tr>
              <th className="border-b pb-2 text-sm">Quarter</th>
              <th className="border-b pb-2 text-sm">Year</th>
              <th className="border-b pb-2 text-sm">Appraisee Start</th>
              <th className="border-b pb-2 text-sm">Appraisee End</th>
              <th className="border-b pb-2 text-sm">Appraiser Start</th>
              <th className="border-b pb-2 text-sm">Appraiser End</th>
              <th className="border-b pb-2 text-sm">Status</th>
              <th className="border-b pb-2 text-sm">Actions</th>
            </tr>
          </thead>
          <tbody>
            {appraisalCycles.map((cycle) => (
              <tr key={cycle.id}>
                <td className="border-b py-3 text-sm">{cycle.quarter}</td>
                <td className="border-b py-3 text-sm">{cycle.year}</td>
                <td className="border-b py-3 text-sm">
                  {cycle.appraiseeStartDate}
                </td>
                <td className="border-b py-3 text-sm">
                  {cycle.appraiseeEndDate}
                </td>
                <td className="border-b py-3 text-sm">
                  {cycle.appraiserStartDate}
                </td>
                <td className="border-b py-3 text-sm">
                  {cycle.appraiserEndDate}
                </td>
                <td className="border-b py-3 text-sm">{cycle.status}</td>
                <td className="border-b py-3 text-sm">
                  {cycle.status === "NotStarted" && (
                    <button
                      onClick={() => handleUpdateStatus(cycle.id, "InProgress")}
                      className="px-4 py-2 bg-green-500 text-white rounded"
                    >
                      Start Cycle
                    </button>
                  )}
                  {cycle.status === "InProgress" && (
                    <button
                      onClick={() => handleUpdateStatus(cycle.id, "Completed")}
                      className="px-2 py-2 bg-red-500 text-white text-sm rounded"
                    >
                      Stop Cycle
                    </button>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {showForm && (
        <CycleForm
          onClose={() => setShowForm(false)}
          onCycleAdded={handleCycleAdded}
        />
      )}
    </div>
  );
};

export default AppraisalSetup;
