"use client";

import React, { useState, useEffect } from "react";
import { Dropdown } from "primereact/dropdown";
import { Card } from "primereact/card";
import IndicatorForm from "./components/IndicatorForm";
import FactorForm from "./components/FactorForm";
import axiosInstance from "@/services/axiosInterceptor";

interface Factor {
  factorId: number;
  factorName: string;
  weightage: number;
}

interface Department {
  departmentId: number;
  departmentName: string;
  factors: Factor[];
}

const PerformanceFactorConfig: React.FC = () => {
  const [activeTab, setActiveTab] = useState<"form" | "indicator">("form");
  const [selectedRole, setSelectedRole] = useState<string>("");
  const [roles, setRoles] = useState<string[]>([]);
  const [roleFactors, setRoleFactors] = useState<{ [key: string]: Factor[] }>(
    {}
  );
  const [initialWeightages, setInitialWeightages] = useState<Factor[]>([]);

  useEffect(() => {
    const fetchDepartments = async () => {
      try {
        const response = await axiosInstance.get("https://localhost:57679/PerfomanceFactors/departments");
        const data: { departments: Department[] } = response.data;

        const rolesData: string[] = [];
        const roleFactorsData: { [key: string]: Factor[] } = {};

        data.departments.forEach((department) => {
          rolesData.push(department.departmentName);
          roleFactorsData[department.departmentName] = department.factors.map(
            (factor) => ({
              factorId: factor.factorId,
              factorName: factor.factorName,
              weightage: factor.weightage * 100, 
            })
          );
        });

        setRoles(rolesData);
        setRoleFactors(roleFactorsData);
      } catch (error) {
        console.error("Error fetching department data:", error);
      }
    };

    fetchDepartments();
  }, []);

  useEffect(() => {
    if (selectedRole) {
      setInitialWeightages(roleFactors[selectedRole] || []);
    }
  }, [selectedRole, roleFactors]);

  const handleSave = (data: Factor[]) => {
    console.log("Saved data:", data);
    alert("Configuration saved successfully!");
  };

  return (
    <div className="p-6 bg-gray-100">
      <h2 className="text-2xl font-semibold mb-6">
        Performance Factor Configuration
      </h2>

      <div className="flex space-x-4 mb-4 border-b">
        <button
          className={`px-4 py-2 ${
            activeTab === "form" ? "border-b-2 border-blue-600 font-bold" : ""
          }`}
          onClick={() => setActiveTab("form")}
        >
          Performance Factor Config
        </button>
        <button
          className={`px-4 py-2 ${
            activeTab === "indicator"
              ? "border-b-2 border-blue-600 font-bold"
              : ""
          }`}
          onClick={() => setActiveTab("indicator")}
        >
          Indicator Config
        </button>
      </div>

      {activeTab === "form" ? (
        <>
          <Card className="mb-6">
            <h3 className="text-xl font-semibold mb-4">Select Role</h3>
            <Dropdown
              value={selectedRole}
              onChange={(e) => setSelectedRole(e.value)}
              options={roles}
              placeholder="Select Role"
              className="w-[12rem] text-gray-700"
            />
          </Card>
          {selectedRole && (
            <FactorForm
              selectedRole={selectedRole}
              performanceFactors={roleFactors[selectedRole] || []}
              initialWeightages={initialWeightages}
              onSave={() => console.log("")}
            />
          )}
        </>
      ) : (
        <IndicatorForm />
      )}
    </div>
  );
};

export default PerformanceFactorConfig;
