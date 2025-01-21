import React, { useState, useEffect } from "react";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import axiosInstance from "@/services/axiosInterceptor";

const baseApiUrl = process.env.NEXT_PUBLIC_API_URL;

interface CycleFormProps {
  onClose: () => void;
  onCycleAdded: (cycle: any) => void;
}

const CycleForm = ({ onClose, onCycleAdded }: CycleFormProps) => {
  const [newCycle, setNewCycle] = useState({
    quarter: 1,
    year: new Date().getFullYear(),
    appraiseeStartDate: "",
    appraiseeEndDate: "",
    appraiserStartDate: "",
    appraiserEndDate: "",
    completionStatus: "Pending",
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [quarters, setQuarters] = useState<{ quarter: string; quarterId: number }[]>([]);

  useEffect(() => {
    const fetchQuarters = async () => {
      try {
        const response = await axiosInstance.get(`${baseApiUrl}/appraisal-cycles/Quarter`);
        if (response.status === 200) {
          setQuarters(response.data.quarters); // Expecting quarters in { quarterId, quarter } format
        }
      } catch (err) {
        setError("Failed to fetch quarters");
      }
    };

    fetchQuarters();
  }, []);

  const handleCreateCycle = async () => {
    if (
      newCycle.quarter &&
      newCycle.year &&
      newCycle.appraiseeStartDate &&
      newCycle.appraiseeEndDate &&
      newCycle.appraiserStartDate &&
      newCycle.appraiserEndDate
    ) {
      setLoading(true);
      setError(null);

      try {
        const response = await axiosInstance.post(`${baseApiUrl}/appraisal-cycles`, newCycle);

        if (response.status === 200) {
          onCycleAdded({
            id: response.data.id,
            ...newCycle,
          });
          toast.success("Appraisal cycle created successfully!");
          onClose();
        }
      } catch (error: any) {
        setError(error.response?.data?.message || "Failed to create cycle");
      } finally {
        setLoading(false);
      }
    }
  };

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
      <div className="bg-white p-6 rounded-lg shadow-lg w-[30rem] h-[27rem]">
        <h3 className="text-lg font-medium text-gray-800 mb-4">Create New Appraisal Cycle</h3>
        {error && <div className="text-red-600 text-sm mb-3">{error}</div>}
        <form className="space-y-4 text-sm">
          {/* Quarter Selection */}
          <div>
            <label htmlFor="quarter" className="block font-medium text-gray-700 mb-1">
              Select Quarter
            </label>
            <select
              id="quarter"
              value={newCycle.quarter}
              onChange={(e) =>
                setNewCycle((prev) => ({ ...prev, quarter: Number(e.target.value) }))
              }
              className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500"
            >
              {quarters.map((quarter) => (
                <option key={quarter.quarterId} value={quarter.quarterId}>
                  {quarter.quarter}
                </option>
              ))}
            </select>
          </div>
          {/* Year Input */}
          <div>
            <label htmlFor="year" className="block font-medium text-gray-700 mb-1">
              Year
            </label>
            <input
              type="number"
              id="year"
              value={newCycle.year}
              onChange={(e) =>
                setNewCycle((prev) => ({ ...prev, year: Number(e.target.value) }))
              }
              className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500"
            />
          </div>
          {/* Appraisee Dates */}
          <div className="grid grid-cols-2 gap-3">
            <div>
              <label
                htmlFor="appraiseeStartDate"
                className="block font-medium text-gray-700 mb-1"
              >
                Appraisee Start Date
              </label>
              <input
                type="date"
                id="appraiseeStartDate"
                value={newCycle.appraiseeStartDate}
                onChange={(e) =>
                  setNewCycle((prev) => ({ ...prev, appraiseeStartDate: e.target.value }))
                }
                className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500"
              />
            </div>
            <div>
              <label
                htmlFor="appraiseeEndDate"
                className="block font-medium text-gray-700 mb-1"
              >
                Appraisee End Date
              </label>
              <input
                type="date"
                id="appraiseeEndDate"
                value={newCycle.appraiseeEndDate}
                onChange={(e) =>
                  setNewCycle((prev) => ({ ...prev, appraiseeEndDate: e.target.value }))
                }
                className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500"
              />
            </div>
          </div>
          {/* Appraiser Dates */}
          <div className="grid grid-cols-2 gap-3">
            <div>
              <label
                htmlFor="appraiserStartDate"
                className="block font-medium text-gray-700 mb-1"
              >
                Appraiser Start Date
              </label>
              <input
                type="date"
                id="appraiserStartDate"
                value={newCycle.appraiserStartDate}
                onChange={(e) =>
                  setNewCycle((prev) => ({ ...prev, appraiserStartDate: e.target.value }))
                }
                className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500"
              />
            </div>
            <div>
              <label
                htmlFor="appraiserEndDate"
                className="block font-medium text-gray-700 mb-1"
              >
                Appraiser End Date
              </label>
              <input
                type="date"
                id="appraiserEndDate"
                value={newCycle.appraiserEndDate}
                onChange={(e) =>
                  setNewCycle((prev) => ({ ...prev, appraiserEndDate: e.target.value }))
                }
                className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500"
              />
            </div>
          </div>
          <div className="flex justify-end gap-3">
            <button
              type="button"
              onClick={onClose}
              className="px-4 py-2 bg-gray-300 text-gray-800 rounded hover:bg-gray-400"
            >
              Cancel
            </button>
            <button
              type="button"
              onClick={handleCreateCycle}
              className={`px-4 py-2 bg-blue-600 text-white rounded ${
                loading ? "opacity-50 cursor-not-allowed" : "hover:bg-blue-700"
              }`}
              disabled={loading}
            >
              {loading ? "Creating..." : "Create"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default CycleForm;
