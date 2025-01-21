'use client'
import React, { useState, useEffect } from 'react';
import axiosInstance from '@/services/axiosInterceptor';

const baseAPIUrl = process.env.NEXT_PUBLIC_API_URL;

interface FeedbackHistoryItem {
  feedbackId: number;
  quarter: string;
  year: number;
  appraiseeScore :number,
  appraiserScore:number,
  performanceBucketName: string,
  assessmentStatus:string
}

interface ApiResponse {
  feedbackHistory: FeedbackHistoryItem[];
  status: string;
  errorMessage?: string;
}

const AppraisalFeedbackHistory: React.FC = () => {
  const [feedbackHistory, setFeedbackHistory] = useState<FeedbackHistoryItem[]>([]);
  const [status, setStatus] = useState<string>('Loading...');
  const [errorMessage, setErrorMessage] = useState<string>('');
  const [isLoading, setIsLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchFeedbackHistory = async () => {
    const storedEmployeeId = Number(localStorage.getItem("user_id")) || null;

      try {
        const response = await axiosInstance.get<ApiResponse>(`${baseAPIUrl}/appraisal-feedback/${storedEmployeeId}`); 
        const data = response.data;

        if (response.status === 200) {
          setFeedbackHistory(data.feedbackHistory || []);
          setStatus(data.status);
        } else {
          setStatus('Error');
          setErrorMessage(data.errorMessage || 'Failed to fetch appraisal history.');
        }
      } catch (error: any) {
        setStatus('Error');
        setErrorMessage(error.response?.data?.errorMessage || 'An unexpected error occurred.');
      } finally {
        setIsLoading(false);
      }
    };

    fetchFeedbackHistory();
  }, []);

  return (
    <div className="max-w-4xl mx-auto p-6 bg-gray-50 rounded-lg shadow-md">
      <h1 className="text-2xl font-bold text-center text-gray-800 mb-6">
        Feedback History
      </h1>

      {isLoading ? (
        <p className="text-center text-gray-600">Loading...</p>
      ) : status === 'Error' ? (
        <div className="bg-red-100 text-red-600 p-4 rounded-md text-center">
          <p>{errorMessage}</p>
        </div>
      ) : (
        <div className="overflow-x-auto">
          <table className="table-auto w-full border-collapse border border-gray-200">
            <thead>
              <tr className="bg-blue-600 text-white">
                <th className="px-4 py-2 border font-normal">Quarter</th>
                <th className="px-4 py-2 border font-normal">Year</th>
                <th className="px-4 py-2 border font-normal">Your Score</th>
                <th className="px-4 py-2 border font-normal">Manager Score</th>
                <th className="px-4 py-2 border font-normal">Performance Bucket</th>
                <th className="px-4 py-2 border font-normal">Assessment Status</th>
              </tr>
            </thead>
            <tbody>
              {feedbackHistory.map((item, index) => (
                <tr
                  key={item.feedbackId}
                  className={`text-center ${
                    index % 2 === 0 ? 'bg-gray-100' : 'bg-white '
                  }`}
                >
                  <td className="px-4 py-2 border text-sm">{item.quarter}</td>
                  <td className="px-4 py-2 border text-sm">{item.year}</td>
                  <td className="px-4 py-2 border text-sm">{item.appraiseeScore}</td>
                  <td className="px-4 py-2 border text-sm">{item.appraiserScore}</td>
                  <td className="px-4 py-2 border text-sm">{item.performanceBucketName}</td>
                  <td className="px-4 py-2 border text-sm">{item.assessmentStatus}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
};

export default AppraisalFeedbackHistory;
