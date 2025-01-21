import { useState } from "react";
import axios from "axios";
import { toast } from "react-toastify";
import axiosInstance from "@/services/axiosInterceptor";

interface PerformanceIndicator {
  indicatorId: number;
  indicatorName: string;
}

interface PerformanceFactor {
  id: number;
  name: string;
  indicators: PerformanceIndicator[];
}

interface QuestionFormProps {
  performanceFactors: PerformanceFactor[] | null;
  setQuestions: React.Dispatch<React.SetStateAction<string[]>>;
}

const baseApriUrl=process.env.NEXT_PUBLIC_API_URL;

export const QuestionForm: React.FC<QuestionFormProps> = ({
  performanceFactors,
  setQuestions,
}) => {
  const [selectedFactor, setSelectedFactor] = useState<number | null>(null);
  const [selectedIndicator, setSelectedIndicator] = useState<number | null>(null);
  const [newQuestion, setNewQuestion] = useState<string>("");
  const [feedbackMessage, setFeedbackMessage] = useState<string>("");

  const addQuestion = async () => {
    if (newQuestion.trim() && selectedFactor !== null && selectedIndicator !== null) {
      try {
        const response = await axiosInstance.post(`${baseApriUrl}/Questions`, {
            questionText: newQuestion,
            indicatorId: selectedIndicator,
        });

        if (response.status === 201) {
          setQuestions((prevQuestions) => [...prevQuestions, newQuestion]);
          toast.success("Question added successfully");
          setFeedbackMessage("Question added successfully!");

          setNewQuestion("");
          setSelectedFactor(null);
          setSelectedIndicator(null);
        } else {
          setFeedbackMessage("Failed to add the question. Please try again.");
        }
      } catch (error) {
        console.error("Error adding question:", error);
        setFeedbackMessage("An error occurred. Please try again.");
      }
    } else {
      setFeedbackMessage("Please select a factor, indicator, and enter a question.");
    }
  };

  return (
    <div>
      <h2 className="text-xl mb-4">Add Question to Indicator</h2>

      {/* Select Performance Factor */}
      <div className="mb-4">
        <label className="block text-sm font-medium text-gray-700">Performance Factor</label>
        <select
          value={selectedFactor ?? ""}
          onChange={(e) => setSelectedFactor(Number(e.target.value))}
          className="mt-2 block w-full p-2 border rounded"
        >
          <option value="">Select a Factor</option>
          {performanceFactors &&
            performanceFactors.map((factor) => (
              <option key={factor.id} value={factor.id}>
                {factor.name}
              </option>
            ))}
        </select>
      </div>

      {/* Select Indicator */}
      {selectedFactor !== null && performanceFactors && (
        <div className="mb-4">
          <label className="block text-sm font-medium text-gray-700">Indicator</label>
          <select
            value={selectedIndicator ?? ""}
            onChange={(e) => setSelectedIndicator(Number(e.target.value))}
            className="mt-2 block w-full p-2 border rounded"
          >
            <option value="">Select an Indicator</option>
            {performanceFactors
              .find((factor) => factor.id === selectedFactor)
              ?.indicators.map((indicator) => (
                <option key={indicator.indicatorId} value={indicator.indicatorId}>
                  {indicator.indicatorName}
                </option>
              ))}
          </select>
        </div>
      )}

      {/* New Question Input */}
      {selectedIndicator !== null && (
        <div className="mb-4">
          <label className="block text-sm font-medium text-gray-700">New Question</label>
          <input
            type="text"
            value={newQuestion}
            onChange={(e) => setNewQuestion(e.target.value)}
            className="mt-2 block w-full p-2 border rounded"
            placeholder="Enter a new question"
          />
        </div>
      )}

      <button
        onClick={addQuestion}
        disabled={!newQuestion || !selectedIndicator || selectedFactor === null}
        className="px-6 py-2 bg-blue-600 text-white rounded"
      >
        Add Question
      </button>

      {/* Feedback Message */}
      {feedbackMessage && <div className="mt-4 text-sm text-green-600">{feedbackMessage}</div>}
    </div>
  );
};
