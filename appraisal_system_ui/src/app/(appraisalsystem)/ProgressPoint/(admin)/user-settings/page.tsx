'use client'
import React, { useEffect, useState } from "react";
import axiosInstance from "@/services/axiosInterceptor";
import EmployeeTable from "./components/EmployeeTable";
import AddEmployeeModal from "./components/CreateEmployeeModel";

interface Employee {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
  role: string;
  department: string;
  manager: string | null;
}

const EmployeeList: React.FC = () => {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

  useEffect(() => {
    const fetchEmployees = async () => {
      try {
        const response = await axiosInstance.get("https://localhost:57679/Employees");
        setEmployees(response.data.employeeDetails);
      } catch (error) {
        console.error("Error fetching employees:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchEmployees();
  }, []);

  const handleEmployeeAdded = (newEmployee: Employee) => {
    setEmployees((prev) => [...prev, newEmployee]);
  };

  if (loading) {
    return (
      <div className="flex justify-center items-center min-h-screen">
        <p className="text-gray-500">Loading employees...</p>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50 py-10 px-4">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-3xl font-bold text-gray-800">Employee Directory</h1>
        <button
          onClick={() => setIsModalOpen(true)}
          className="flex items-center bg-blue-500 text-white px-4 py-2 rounded-lg shadow hover:bg-blue-600 transition"
        >
          <span className="mr-2">New</span>
          <span className="text-lg font-bold">+</span>
        </button>
      </div>
      <EmployeeTable employees={employees} />
      {isModalOpen && (
        <AddEmployeeModal
          onClose={() => setIsModalOpen(false)}
          onEmployeeAdded={handleEmployeeAdded}
        />
      )}
    </div>
  );
};

export default EmployeeList;
