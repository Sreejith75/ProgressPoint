import React from "react";

interface AssessmentInstructionsProps {
  isVisible: boolean;
  onConfirm: () => void;
  onCancel: () => void;
}

const AssessmentInstructionsModal: React.FC<AssessmentInstructionsProps> = ({
  isVisible,
  onConfirm,
  onCancel,
}) => {
  if (!isVisible) return null;

  

  return (
    <div className="fixed inset-0 bg-gray-800 bg-opacity-50 flex items-center justify-center z-50 ">
      <div className="bg-white p-4 rounded shadow-lg max-w-4xl w-full h-auto overflow-y-auto flex flex-col border border-spacing-10 border-black">
        <h2 className="text-xl font-bold text-gray-800 text-center m-4">Assessment Instructions</h2>
        <p className="mt-4 text-gray-700 text-md leading-relaxed ml-2">
          Welcome to the assessment process! Please carefully read the following instructions before proceeding:
        </p>

        <ol className="list-decimal mt-4 pl-10 text-gray-700 text-sm leading-relaxed">
          <li>
            Each question in the assessment is mandatory. You must answer all questions to successfully complete the process.
          </li>
          <li>
            Use the rating scale from <span className="font-bold">1 to 5</span> to evaluate each statement:
            <ul className="list-disc pl-6 mt-2 text-red-600">
              <li><span className="font-semibold">1</span>: Needs Improvement</li>
              <li><span className="font-semibold">2</span>: Fair</li>
              <li><span className="font-semibold">3</span>: Good</li>
              <li><span className="font-semibold">4</span>: Outstanding</li>
              <li><span className="font-semibold">5</span>: Exceptional​</li>
            </ul>
          </li>
          <li>
            For some questions, you may be required to provide comments or upload supporting documents (artifacts). Be thorough in your responses.
          </li>
          <li>
            Ensure that you review your answers before submitting. Once submitted, changes cannot be made.
          </li>
          <li>
            If you encounter any issues during the assessment, contact the administrator immediately for assistance.
          </li>
        </ol>

        <p className="mt-4 text-gray-700 text-md leading-relaxed ml-2 flex justify-center">
          Please make sure you understand the instructions fully before starting. Click "<span className="text-red-500">Confirm</span>" to begin.
        </p>

        <div className="flex justify-center mt-6 gap-4">
          <button
            onClick={onCancel}
            className="px-4 py-2 bg-gray-300 text-gray-800 rounded hover:bg-gray-400"
          >
            Cancel
          </button>
          <button
            onClick={onConfirm}
            className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-800"
          >
            Confirm
          </button>
        </div>
      </div>
    </div>
  );
};

export default AssessmentInstructionsModal;
