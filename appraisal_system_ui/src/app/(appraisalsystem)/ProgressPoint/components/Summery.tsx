import React from "react";
import { AiOutlineClose } from "react-icons/ai";
import { BsCheckCircleFill } from "react-icons/bs"; // Icon for confirmation visuals
import { FaTrophy } from "react-icons/fa"; // Icon for the performance bucket

interface SummaryModalProps {
  isVisible: boolean;
  onClose: () => void;
  onConfirm: () => void;
  message: string;
  finalScore: number | undefined;
  performanceBucket: string | null;
  loading?: boolean;
  description: string | null;
}

const SummaryModal: React.FC<SummaryModalProps> = ({
  isVisible,
  onClose,
  onConfirm,
  message,
  finalScore,
  performanceBucket,
  loading = false,
  description,
}) => {
  if (!isVisible) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-60 flex justify-center items-center z-50">
      <div className="relative bg-gradient-to-b from-white via-gray-100 to-gray-200 rounded-2xl shadow-xl w-[60rem] max-h-[90vh] p-4">
        <button
          className="absolute top-4 right-4 text-gray-500 hover:text-gray-700 transition"
          onClick={onClose}
          aria-label="Close"
        >
          <AiOutlineClose size={24} />
        </button>

        <div className="text-center mb-6">
          <h2 className="text-2xl font-extrabold text-green-600 flex items-center justify-center space-x-2">
            <BsCheckCircleFill size={30} className="text-green-500" />
            <span>Summary Report</span>
          </h2>
          <p className="text-gray-700 text-sm mt-2">{message}</p>
        </div>

        <div className="bg-blue-50 p-6 rounded-md shadow-inner mb-2 flex items-center justify-between">
          <h3 className="text-lg font-semibold text-blue-800 flex items-center space-x-2">
            <FaTrophy size={20} className="text-blue-600" />
            <span>Final Score</span>
          </h3>
          <p className="text-4xl font-bold text-blue-600">{finalScore ?? "N/A"}</p>
        </div>

        <div className="bg-green-50 p-6 rounded-md shadow-inner mb-2">
          <h3 className="text-lg font-semibold text-green-800 flex items-center space-x-2">
            <FaTrophy size={20} className="text-green-600" />
            <span>Performance Bucket</span>
          </h3>
          <p className="text-xl font-bold text-green-700 mt-2">
            {performanceBucket ?? "N/A"}
          </p>
          {description && (
            <p className="text-sm text-gray-700 mt-2">{description}</p>
          )}
        </div>

        <div className="bg-yellow-50 p-5 rounded-md shadow-inner mb-4">
          <h3 className="text-lg font-semibold text-yellow-800 text-center">
            Instructions
          </h3>
          <p className="text-sm text-gray-700 mt-2 text-center">
            Please review the information carefully. Click{" "}
            <strong>Confirm</strong> to finalize your submission, or{" "}
            <strong>Close</strong> to make changes.
          </p>
        </div>

        <div className="flex justify-center space-x-4">
          <button
            className="bg-gray-300 text-gray-800 py-2 px-6 rounded-md hover:bg-gray-400 transition-colors focus:outline-none focus:ring-2 focus:ring-gray-500"
            onClick={onClose}
            disabled={loading}
          >
            Close
          </button>
          <button
            className={`py-2 px-6 rounded-md text-white transition-colors focus:outline-none focus:ring-2 focus:ring-blue-500 ${
              loading
                ? "bg-blue-400 cursor-not-allowed"
                : "bg-blue-600 hover:bg-blue-700"
            }`}
            onClick={onConfirm}
            disabled={loading}
          >
            {loading ? "Submitting..." : "Confirm"}
          </button>
        </div>
      </div>
    </div>
  );
};

export default SummaryModal;
