import React, { useEffect, useState } from 'react';
import axiosInstance from '@/services/axiosInterceptor';
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";


interface Form {
  id: number;
  quarter: string;
  year: number;
  employeeRole: string;
  status: string;
}

const FormList: React.FC = () => {
  const [forms, setForms] = useState<Form[]>([]);

  useEffect(() => {
    const fetchForms = async () => {
      try {
        const response = await axiosInstance.get(`/AppraisalForms`);
        setForms(response.data);
      } catch (error) {
        console.error('Error fetching forms:', error);
      }
    };

    fetchForms();
  }, []);

  const getNextStatus = (currentStatus: string): string => {
    if (currentStatus === 'Draft') return 'Active';
    if (currentStatus === 'Active') return 'Archived';
    return 'Draft';
  };

  const handleUpdateStatus = async (id: number, currentStatus: string) => {
    const newStatus = getNextStatus(currentStatus);

    try {
      await axiosInstance.put(`/appraisal-forms/UpdateStatus/${id}`, {
        status: newStatus,
      });
      setForms((prevForms) =>
        prevForms.map((form) =>
          form.id === id ? { ...form, status: newStatus } : form
        )
      );
      toast.success(`Status updated to ${newStatus}!`);
    } catch (error) {
      console.log('Error updating form status:', error);
      toast.error(`Status update failed`);

    }
  };

  return (
    <div className="flex flex-col h-full">
      {/* Header */}
      <div className="bg-gray-100 px-6 py-4 border-b">
        <h2 className="text-md font-semibold text-gray-700">Appraisal Forms</h2>
        <p className="text-sm text-gray-500">
          View and manage all appraisal forms in the current cycle.
        </p>
      </div>

      {/* Form List */}
      <div className="flex-1 overflow-y-auto p-6 w-full">
        <ul className="space-y-4 w-full">
          {forms.map((form) => (
            <li
              key={form.id}
              className="flex justify-between items-center bg-white border shadow-sm rounded-md p-4 w-full"
            >
              <div>
                <p className="font-medium text-gray-800">{form.employeeRole}</p>
                <p className="text-sm text-gray-500">
                  Quarter: {form.quarter} | Year: {form.year}
                </p>
                <p className="text-sm text-gray-500">
                  Status: <span className="font-semibold">{form.status}</span>
                </p>
              </div>
              <button
                onClick={() => handleUpdateStatus(form.id, form.status)}
                className="text-sm w-[8rem] h-[2rem] bg-blue-500 text-white px-3 py-1 rounded hover:bg-blue-600"
              >
                Set to {getNextStatus(form.status)}
              </button>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default FormList;
