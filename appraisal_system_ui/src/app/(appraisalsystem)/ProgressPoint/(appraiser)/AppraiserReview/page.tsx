"use client";

import React, { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import axiosInstance from "@/services/axiosInterceptor";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Button } from "primereact/button";

const baseAPIUrl = process.env.NEXT_PUBLIC_API_URL;

interface Appraisee {
  appraiseeId: number;
  name: string;
  employeeRole: string;
  feedbackstatus: string;
  appraiseeScore: number;
  appraiserScore: number;
  performanceBucket: string;
}

const AppraiserReviewPage: React.FC = () => {
  const router = useRouter();
  const [appraisees, setAppraisees] = useState<Appraisee[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [storedEmployeeId, setStoredEmployeeId] = useState<number | null>(null);

  useEffect(() => {
    const employeeId = Number(localStorage.getItem("user_id")) || null;
    if (employeeId) {
      setStoredEmployeeId(employeeId);
    } else {
      setError("User ID not found.");
    }
  }, []);

  useEffect(() => {
    if (storedEmployeeId) {
      fetchAppraisees();
    }
  }, [storedEmployeeId]);

  const fetchAppraisees = async () => {
    try {
      setLoading(true);
      const response = await axiosInstance.get(
        `${baseAPIUrl}/Employees/${storedEmployeeId}`
      );
      if (response.data.message === "Success") {
        // Sorting to ensure "UnderReview" rows are at the top
        const sortedAppraisees = response.data.appraiseeList.sort(
          (a: Appraisee, b: Appraisee) =>
            b.feedbackstatus === "UnderReview"
              ? 1
              : a.feedbackstatus === "UnderReview"
              ? -1
              : 0
        );
        setAppraisees(sortedAppraisees);
      } else {
        setError("Failed to fetch appraisee list.");
      }
    } catch (err: any) {
      setError(
        err.response?.data?.message ||
          "An error occurred while fetching appraisee data."
      );
    } finally {
      setLoading(false);
    }
  };

  const handleAppraiseeClick = (id: number) => {
    router.push(`/ProgressPoint/AppraiserReview/${id}`);
  };

  if (loading) {
    return <div className="p-6">Loading...</div>;
  }

  if (error) {
    return <div className="p-6 text-red-600">{error}</div>;
  }

  const actionTemplate = (rowData: Appraisee) => {
    return rowData.feedbackstatus === "UnderReview" ? (
      <Button
        label="Review"
        onClick={() => handleAppraiseeClick(rowData.appraiseeId)}
        className="p-button-primary rounded-md bg-blue-600 text-white p-1 hover:bg-blue-800"
      />
    ) : null;
  };

  return (
    <div className="p-3 space-y-4">
      <h1 className="text-2xl font-bold text-center mb-4">Appraisee List</h1>
      <div className="bg-white shadow rounded-md text-sm p-4">
        <DataTable value={appraisees} paginator rows={10} loading={loading}>
          <Column field="name" header="Name" />
          <Column field="employeeRole" header="Role" />
          <Column field="appraiseeScore" header="Appraisee Score" />
          <Column field="appraiserScore" header="Your Score" />
          <Column field="performanceBucket" header="Performance Bucket" />
          <Column field="feedbackstatus" header="Feedback Status" />
          <Column body={actionTemplate} header="Action" />
        </DataTable>
      </div>
    </div>
  );
};

export default AppraiserReviewPage;
