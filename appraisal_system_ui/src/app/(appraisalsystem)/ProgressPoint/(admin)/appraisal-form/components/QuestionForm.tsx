import React, { useState } from 'react';
import axiosInstance from '@/services/axiosInterceptor';
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";


interface Role {
  id: number;
  roleName: string;
}

interface Question {
  id: number;
  text: string;
  factor: string;
  indicator: string;
}

interface RoleBasedFormProps {
  roles: Role[];
  questions: Question[];
  setQuestions: React.Dispatch<React.SetStateAction<Question[]>>;
}

const RoleBasedForm: React.FC<RoleBasedFormProps> = ({
  roles,
  questions,
}) => {
  const [selectedRoleId, setSelectedRoleId] = useState<number | null>(null);
  const [selectedQuestions, setSelectedQuestions] = useState<Set<number>>(new Set());
  const [selectedFactor, setSelectedFactor] = useState<string>('All');
  const [selectedIndicator, setSelectedIndicator] = useState<string>('All');
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const handleQuestionToggle = (questionId: number) => {
    setSelectedQuestions((prev) => {
      const updated = new Set(prev);
      if (updated.has(questionId)) {
        updated.delete(questionId);
      } else {
        updated.add(questionId);
      }
      return updated;
    });
  };


  const handleSaveForm = async () => {
    if (!selectedRoleId) {
      alert('Please select a role.');
      return;
    }

    const selectedData = {
      employeeRoleId: selectedRoleId,
      questionIds: Array.from(selectedQuestions),
    };

    try {
      setLoading(true);
      setError(null);

      const response = await axiosInstance.post(`/Appraisalforms`, selectedData);

      if (response.status === 200) {
        toast.success('Form saved successfully!');
        setSelectedRoleId(null);
        setSelectedQuestions(new Set());
      }
    } catch (error) {
      console.log('Error saving form:', error);
      setError('An error occurred while saving the form.');
    } finally {
      setLoading(false);
    }
  };

  const filteredQuestions = questions.filter((question) => {
    const matchesFactor = selectedFactor === 'All' || question.factor === selectedFactor;
    const matchesIndicator = selectedIndicator === 'All' || question.indicator === selectedIndicator;
    return matchesFactor && matchesIndicator;
  });

  const uniqueFactors = ['All', ...new Set(questions.map((q) => q.factor))];
  const uniqueIndicators = [
    'All',
    ...new Set(
      questions
        .filter((q) => selectedFactor === 'All' || q.factor === selectedFactor)
        .map((q) => q.indicator)
    ),
  ];

  return (
    <div className="flex flex-col gap-6 p-6 z-10 h-screen w-3/2 mb-[4rem]">
      <div className="relative p-6 bg-white shadow rounded-md">
        <h2 className="text-xl font-semibold mb-4">Role-Based Question Form</h2>

        {/* Role Selection */}
        <div className="w-full mb-6">
          <label className="block text-md font-medium mb-2">Select Role</label>
          <select
            value={selectedRoleId || ''}
            onChange={(e) => setSelectedRoleId(Number(e.target.value))}
            className="w-full p-3 border rounded"
          >
            <option value="" disabled>
              Select a role
            </option>
            {roles.map((role) => (
              <option key={role.id} value={role.id}>
                {role.roleName}
              </option>
            ))}
          </select>
        </div>

        {/* Filter by Factor and Indicator */}
        <div className="flex gap-4 mb-6">
          <div className="w-1/2">
            <label className="block text-md font-medium mb-2">Filter by Factor</label>
            <select
              value={selectedFactor}
              onChange={(e) => setSelectedFactor(e.target.value)}
              className="w-full p-3 border rounded"
            >
              {uniqueFactors.map((factor) => (
                <option key={factor} value={factor}>
                  {factor}
                </option>
              ))}
            </select>
          </div>
          <div className="w-1/2">
            <label className="block text-md font-medium mb-2">Filter by Indicator</label>
            <select
              value={selectedIndicator}
              onChange={(e) => setSelectedIndicator(e.target.value)}
              className="w-full p-3 border rounded"
            >
              {uniqueIndicators.map((indicator) => (
                <option key={indicator} value={indicator}>
                  {indicator}
                </option>
              ))}
            </select>
          </div>
        </div>

        {/* Question List */}
        <div className="mb-6">
          <label className="block text-md font-medium mb-2">Select Questions</label>
          <ul className="space-y-2">
            {filteredQuestions.map((question) => (
              <li key={question.id} className="flex items-center space-x-4">
                <input
                  type="checkbox"
                  checked={selectedQuestions.has(question.id)}
                  onChange={() => handleQuestionToggle(question.id)}
                  className="w-5 h-5"  // Increase checkbox size
                />
                <span className="text-sm">{question.text}</span>
              </li>
            ))}
          </ul>
        </div>

        {/* Save Form */}
        <div className="mt-6 flex justify-center pb-[5rem]">
          <button
            onClick={handleSaveForm}
            disabled={loading} // Disable button while loading
            className="bg-blue-600 text-white p-3 rounded-md"
          >
            {loading ? 'Saving...' : 'Save Form'}
          </button>
        </div>

        {/* Error Message */}
        {error && <div className="text-red-500 text-center mt-4">{error}</div>}
      </div>
    </div>
  );
};

export default RoleBasedForm;
