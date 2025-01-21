"use client";
import React, { useState, useEffect } from "react";
import { Chart } from "primereact/chart";
import { Dropdown } from "primereact/dropdown";
import axios from "axios";
import TrendsChart from "../components/TrendChart";
import axiosInstance from "@/services/axiosInterceptor";
import ExportButton from "../components/FileExportButton";
import { Button } from "primereact/button";
import { saveAs } from "file-saver";
import { Sidebar } from "primereact/sidebar";
interface AppraisalSummary {
  summaryId: number;
  appraiseeScore: number;
  appraiserScore: number;
  performanceBucket: string;
}

interface AppraiseeAnalysis {
  appraiseeId: number;
  appraiseeName: string;
  appraisalSummary: AppraisalSummary;
}

interface ApiResponse {
  message: string;
  appraiseeAnalysis: AppraiseeAnalysis[];
}

interface QuarterDTO {
  quarterId: number;
  quarterName: string;
}

interface QuarterYearResponse {
  message: string;
  quarterYears: {
    quarter: QuarterDTO[];
    year: number[];
  };
}

interface Trends {
  year: number;
  quarter: string;
  averageAppraiseeScore: number;
  averageAppraiserScore: number;
}

interface TrendsResponse {
  message: string;
  trends: Trends[];
}

interface Role {
  id: number;
  roleName: string;
}

interface Department {
  departmentId: number;
  name: string;
}

const AppraiseeAnalysisPage: React.FC = () => {
  const [analysisData, setAnalysisData] = useState<AppraiseeAnalysis[] | null>(
    null
  );
  const [selectedQuarter, setSelectedQuarter] = useState<number | null>(1);
  const [selectedYear, setSelectedYear] = useState<number | null>(2025);
  const [selectedRole, setSelectedRole] = useState<number | null>(null);
  const [selectedDepartment, setSelectedDepartment] = useState<number | null>(
    null
  );
  const [selectedRoleName, setSelectedRoleName] = useState<string | null>(null);
  const [isFilterModalOpen, setIsFilterModalOpen] = useState(false);

  const [quarterOptions, setQuarterOptions] = useState<
    {
      label: string;
      value: number | null;
    }[]
  >([]);
  const [yearOptions, setYearOptions] = useState<
    {
      label: string;
      value: number | null;
    }[]
  >([]);
  const [roleOptions, setRoleOptions] = useState<
    {
      label: string;
      value: number | null;
    }[]
  >([]);
  const [departmentOptions, setDepartmentOptions] = useState<
    {
      label: string;
      value: number | null;
    }[]
  >([]);
  const [trendsData, setTrendsData] = useState<Trends[]>([]);

  // Fetch quarter, year, role, and department data
  useEffect(() => {
    const fetchQuarterYearData = async () => {
      try {
        const response = await axiosInstance.get<QuarterYearResponse>(
          "https://localhost:57679/quarters-years"
        );
        const currentYear = new Date().getFullYear();
        if (response.data.message === "Success") {
          const quarters = [
            { label: "Select Quarter", value: null },
            ...response.data.quarterYears.quarter.map((quarter) => ({
              label: quarter.quarterName,
              value: quarter.quarterId,
            })),
          ];

          const years = [
            { label: "Select Year", value: null },
            ...response.data.quarterYears.year.map((year) => ({
              label: year.toString(),
              value: year,
            })),
          ];

          setQuarterOptions(quarters);
          setYearOptions(years);

          // Set default selected year as the current year
          const yearExists = years.some(
            (option) => option.value === currentYear
          );
          if (yearExists) {
            setSelectedYear(currentYear); 
          } else if (years.length > 0) {
            setSelectedYear(years[0].value);
          }

          if (quarters.length > 0 && years.length > 0) {
            setSelectedQuarter(quarters[0].value);
          }
        }
      } catch (error) {
        console.error("Error fetching quarter and year data:", error);
      }
    };

    const fetchRoleAndDepartmentData = async () => {
      try {
        // Fetch role data from API
        const rolesResponse = await axiosInstance.get(
          "https://localhost:57679/employeeroles"
        );
        const roles: Role[] = rolesResponse.data;

        // Fetch department data from API
        const departmentsResponse = await axiosInstance.get(
          "https://localhost:57679/Departments"
        );
        const departments: Department[] = departmentsResponse.data.departments;

        // Set role options
        const roleOptions = [
          { label: "Select Role", value: null },
          ...roles.map((role) => ({
            label: role.roleName,
            value: role.id,
          })),
        ];

        // Set department options
        const departmentOptions = [
          { label: "Select Department", value: null },
          ...departments.map((department) => ({
            label: department.name,
            value: department.departmentId,
          })),
        ];

        setRoleOptions(roleOptions);
        setDepartmentOptions(departmentOptions);

        // Default selected role and department
        if (roleOptions.length > 0) setSelectedRole(roleOptions[0].value);
        if (departmentOptions.length > 0)
          setSelectedDepartment(departmentOptions[0].value);
      } catch (error) {
        console.log("Error fetching role and department data:", error);
      }
    };

    fetchQuarterYearData();
    fetchRoleAndDepartmentData();
  }, []);

  // Fetch trends data
  useEffect(() => {
    const fetchTrendsData = async () => {
      try {
        const response = await axios.get<TrendsResponse>(
          "https://localhost:57679/AppraisalSummary/trends"
        );
        if (response.data.message === "Success") {
          setTrendsData(response.data.trends);
        } else {
          setTrendsData([]);
        }
      } catch (error) {
        console.log("Error fetching trends data:", error);
        setTrendsData([]);
      }
    };

    fetchTrendsData();
  }, []);

  // Fetch appraisee analysis data based on selected filters
  useEffect(() => {
    const fetchAppraiseeAnalysis = async () => {
      try {
        const response = await axios.get<ApiResponse>(
          `https://localhost:57679/AppraisalSummary/appraisee-analysis`,
          {
            params: {
              quarter: selectedQuarter,
              year: selectedYear,
              roleId: selectedRole,
              departmentId: selectedDepartment,
            },
          }
        );

        if (response.data.message === "Success") {
          setAnalysisData(response.data.appraiseeAnalysis);
        } else {
          setAnalysisData([]);
        }
      } catch (error) {
        console.error("Error fetching appraisee analysis data:", error);
        setAnalysisData([]);
      }
    };

    fetchAppraiseeAnalysis();
  }, [selectedQuarter, selectedYear, selectedRole, selectedDepartment]);

  const handleExport = async () => {
    try {
      const response = await axiosInstance.get(
        "/AppraisalSummary/Analysis/Export",
        {
          responseType: "arraybuffer",
          params: {
            quarter: selectedQuarter,
            year: selectedYear,
            roleId: selectedRole,
            departmentId: selectedDepartment,
          },
        }
      );

      const blob = new Blob([response.data], { type: "application/csv" });

      saveAs(
        blob,
        `appraisee_analysis_${selectedYear}_${selectedQuarter}_${selectedRole}_${selectedDepartment}.csv`
      );
    } catch (error) {
      console.error("Error exporting CSV:", error);
    }
  };

  // Prepare data for charts
  const bucketData = analysisData?.reduce((acc, item) => {
    const bucket = item.appraisalSummary.performanceBucket;
    acc[bucket] = (acc[bucket] || 0) + 1;
    return acc;
  }, {} as Record<string, number>);

  const bucketLabels = bucketData ? Object.keys(bucketData) : [];
  const bucketCounts = bucketData ? Object.values(bucketData) : [];

  const performanceBarData = {
    labels: bucketLabels,
    datasets: [
      {
        label: "Employee Count",
        data: bucketCounts,
        backgroundColor: "#42A5F5",
      },
    ],
  };

  const performancePieData = {
    labels: bucketLabels,
    datasets: [
      {
        data: bucketCounts,
        backgroundColor: [
          "#FF6384",
          "#36A2EB",
          "#FFCE56",
          "#4CAF50",
          "#9C27B0",
        ],
      },
    ],
  };

  const appraiseeBarData = {
    labels: analysisData?.map((item) => item.appraiseeName) || [],
    datasets: [
      {
        label: "Appraisee Score",
        data:
          analysisData?.map((item) => item.appraisalSummary.appraiseeScore) ||
          [],
        backgroundColor: "#42A5F5",
      },
      {
        label: "Appraiser Score",
        data:
          analysisData?.map((item) => item.appraisalSummary.appraiserScore) ||
          [],
        backgroundColor: "#66BB6A",
      },
    ],
  };

  return (
    <div className="p-6 bg-gray-100 min-h-screen w-full mb-10">
      <div className="flex justify-between items-center mb-6 border-t-2 border-b-2 p-2">
        <span className="text-3xl font-semibold flex justify-center ml-[10rem]">
          Performance Analysis & Trends
        </span>
        <div className="flex space-x-4">
          <Button
            icon="pi pi-download"
            onClick={handleExport}
            style={{ fontSize: "2rem", color: "white" }}
            className="p-button-primary p-button-rounded p-button-lghover:border-2 border-cyan-600 hover:shadow-lg bg-cyan-400 h-[2.8rem] w-[2.8rem]"
          />
          <Button
            icon="pi pi-filter"
            style={{ fontSize: "2rem", color: "white" }}
            className="p-button-primary p-button-rounded p-button-lghover:border-2 border-cyan-600 hover:shadow-lg bg-cyan-400 h-[2.8rem] w-[2.8rem]"
            onClick={() => setIsFilterModalOpen(true)}
          />
        </div>
      </div>

      <div className="flex justify-between mb-6">
        <Sidebar
          visible={isFilterModalOpen}
          position="right"
          onHide={() => setIsFilterModalOpen(false)}
          className="bg-blue-900 text-white font-bold w-[18rem] h-[30rem] rounded-md"
          header="Filter"
        >
          <div className="flex flex-col gap-6 w-full text-white">
            <div>
              <label
                htmlFor="year"
                className="block text-sm font-semibold text-white mb-2"
              >
                Select Year
              </label>
              <Dropdown
                id="year"
                value={selectedYear}
                options={yearOptions}
                onChange={(e) => setSelectedYear(e.value)}
                className="border rounded-lg w-full text-white shadow-sm"
                optionLabel="label"
                optionValue="value"
              />
            </div>
            <div>
              <label
                htmlFor="quarter"
                className="block text-sm font-semibold text-white mb-2"
              >
                Select Quarter
              </label>
              <Dropdown
                id="quarter"
                value={selectedQuarter}
                options={quarterOptions}
                onChange={(e) => setSelectedQuarter(e.value)}
                className="border rounded-lg w-full text-gray-700 shadow-sm"
                optionLabel="label"
                optionValue="value"
              />
            </div>

            <div>
              <label
                htmlFor="role"
                className="block text-sm font-semibold text-white mb-2"
              >
                Select Role
              </label>
              <Dropdown
                id="role"
                value={selectedRole}
                options={roleOptions}
                onChange={(e) => setSelectedRole(e.value)}
                className="border rounded-lg w-full text-gray-700 shadow-sm"
                optionLabel="label"
                optionValue="value"
              />
            </div>

            <div>
              <label
                htmlFor="department"
                className="block text-sm font-semibold text-white mb-2"
              >
                Select Department
              </label>
              <Dropdown
                id="department"
                value={selectedDepartment}
                options={departmentOptions}
                onChange={(e) => setSelectedDepartment(e.value)}
                className="border rounded-lg w-full text-gray-700 shadow-sm"
                optionLabel="label"
                optionValue="value"
              />
            </div>
          </div>
        </Sidebar>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mb-8">
        <div className="bg-white p-4 rounded-lg shadow-sm flex items-center justify-center">
          {bucketData && Object.keys(bucketData).length > 0 ? (
            <Chart
              type="bar"
              data={performanceBarData}
              options={{ responsive: true }}
              className="h-[18rem] flex items-center"
            />
          ) : (
            <div className="flex justify-center items-center py-10">
              <p>No data found</p>
            </div>
          )}
        </div>

        <div className="bg-white p-4 rounded-lg shadow-sm">
          {bucketData && Object.keys(bucketData).length > 0 ? (
            <Chart
              type="pie"
              data={performancePieData}
              options={{ responsive: true }}
              className="h-[18rem] flex justify-center"
            />
          ) : (
            <div className="flex justify-center items-center py-10">
              <p>No data found</p>
            </div>
          )}
        </div>
      </div>

      <div className="bg-white p-4 mt-8 rounded-lg shadow-sm">
        <span className="text-xl font-semibold flex justify-center mb-[1rem]">
          Appraisee Analysis
        </span>
        {analysisData && analysisData.length > 0 ? (
          <Chart
            type="bar"
            data={appraiseeBarData}
            options={{ responsive: true }}
            className="h-[20rem] flex justify-center"
          />
        ) : (
          <div className="flex justify-center items-center py-10">
            <p>No data found</p>
          </div>
        )}
      </div>

      <TrendsChart trendsData={trendsData} />
    </div>
  );
};

export default AppraiseeAnalysisPage;
