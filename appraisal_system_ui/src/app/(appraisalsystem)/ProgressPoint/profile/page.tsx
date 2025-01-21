"use client";
import Link from "next/link";
import React, { useEffect, useState } from "react";
import axios from "axios";
import axiosInstance from "@/services/axiosInterceptor";

const baseAPIUrl = process.env.NEXT_PUBLIC_API_URL;

// Define the UserDetails interface
interface UserDetails {
  name: string;
  email: string;
  phoneNumber: string | null;
  role: string;
  department: string;
  manager: string | null;
  appraisalsCompleted: number;
  averageAppraisalScore: number;
  performanceBucket: string;
  status: boolean;
}

const ProfilePage: React.FC = () => {
  const [userDetails, setUserDetails] = useState<UserDetails | null>(null); // State to store user details
  const [loading, setLoading] = useState<boolean>(true); // State to manage loading
  const [error, setError] = useState<string | null>(null); // State to handle errors

  useEffect(() => {
    // Fetch user details from the API
    const storedEmployeeId = Number(localStorage.getItem("user_id")) || null;
    const fetchUserDetails = async () => {
      try {
        const response = await axiosInstance.get<UserDetails>(
          `/Employees/details/${storedEmployeeId}`
        ); // Replace with your API endpoint
        setUserDetails(response.data);
        setLoading(false);
      } catch (err: any) {
        console.error("Error fetching user details:", err);
        setError("Failed to load user details.");
        setLoading(false);
      }
    };

    fetchUserDetails();
  }, []);

  if (loading) {
    return (
      <div className="flex items-center justify-center h-screen">
        <p>Loading...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="flex items-center justify-center h-screen">
        <p className="text-red-500">{error}</p>
      </div>
    );
  }

  if (!userDetails) {
    return (
      <div className="flex items-center justify-center h-screen">
        <p>No user details found.</p>
      </div>
    );
  }

  return (
    <div className="p-4 bg-gray-100 h-[25rem]">
      <div className="max-w-4xl mx-auto bg-white shadow-lg rounded-lg p-6 h-[28rem]">
        <div className="flex items-center justify-between">
          <h1 className="text-2xl font-bold text-gray-800">Profile</h1>
        </div>

        <div className="flex flex-col md:flex-row items-center mt-6 space-y-4 md:space-y-0 md:space-x-6">
          <div className="w-32 h-32 bg-blue-500 rounded-full flex items-center justify-center text-white text-3xl font-bold">
            {userDetails.name.charAt(0).toUpperCase()}
          </div>

          <div className="flex-1">
            <h2 className="text-xl font-semibold text-gray-800">
              {userDetails.name}
            </h2>
            <p className="text-gray-600">{userDetails.role}</p>
            <p className="text-gray-600">{userDetails.department}</p>
            <p className="text-gray-600">{userDetails.phoneNumber || "N/A"}</p>
          </div>
        </div>

        <div className="mt-8 grid grid-cols-1 md:grid-cols-2 gap-6 h-[10rem]">
          <div className="bg-gray-50 p-4 shadow-md rounded-md">
            <h3 className="text-lg font-semibold text-gray-700">Performance</h3>
            <p className="mt-2 text-gray-600">
              <strong>Performance Bucket:</strong>{" "}
              {userDetails.performanceBucket}
            </p>
            <p className="mt-2 text-gray-600">
              <strong>Status:</strong>{" "}
              {userDetails.status ? "Active" : "Inactive"}
            </p>
          </div>

          <div className="bg-gray-50 p-4 shadow-md rounded-md flex">
            <div>
              <h3 className="text-lg font-semibold text-gray-700">
                Appraisal Summary
              </h3>
              <p className="mt-2 text-gray-600">
                <strong>Completed:</strong> {userDetails.appraisalsCompleted}
              </p>
              <p className="mt-2 text-gray-600">
                <strong>Average Score:</strong>{" "}
                {userDetails.averageAppraisalScore}
              </p>
            </div>
            <Link
              href={"/ProgressPoint/History"}
              className=" bg-blue-500 text-white h-10 w-[8rem] mt-[2rem] ml-[2rem] rounded-md hover:bg-blue-600 flex justify-center items-center"
            >
              View History
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProfilePage;
