import React, { useEffect, useState } from 'react';
import axios from 'axios';
import axiosInstance from '@/services/axiosInterceptor';

const baseApiUrl = process.env.NEXT_PUBLIC_API_URL;

interface Question {
  id: number;
  text: string;
  factor: string;
  indicator: string;
}

const QuestionList: React.FC = () => {
  const [questions, setQuestions] = useState<Question[]>([]);

  useEffect(() => {
    const fetchQuestions = async () => {
      try {
        const response = await axiosInstance.get(`${baseApiUrl}/Questions`);
        const formattedQuestions = response.data.map((item: any) => ({
          id: item.questionId,
          text: item.questionText,
          factor: item.indicator.factor.name,
          indicator: item.indicator.indicatorName,
        }));
        setQuestions(formattedQuestions);
      } catch (error) {
        // console.error('Error fetching questions:', error);
      }
    };

    fetchQuestions();
  }, []);

  const handleDeleteQuestion = async (id: number) => {
    try {
      await axiosInstance.put(`${baseApiUrl}/Questions/deactivate/${id}`); 
      const updatedQuestions = questions.filter((question) => question.id !== id);
      setQuestions(updatedQuestions);
      alert('Question deleted successfully!');
    } catch (error) {
      console.error('Error deleting question:', error);
      alert('Failed to delete the question');
    }
  };

  if (questions.length === 0) {
    return <p>No questions available. Add some questions to view them here.</p>;
  }

  return (
    <div>
      <h2 className="text-xl font-semibold mb-4">Question List</h2>
      <ul className="space-y-4">
        {questions.map((question) => (
          <li
            key={question.id}
            className="border p-4 rounded flex justify-between items-center"
          >
            <div>
              <p className="font-medium">{question.text}</p>
              <p className="text-sm text-gray-600">
                Factor: {question.factor} | Indicator: {question.indicator}
              </p>
            </div>
            <button
              onClick={() => handleDeleteQuestion(question.id)}
              className="bg-red-500 text-white px-3 py-1 rounded"
            >
              Delete
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default QuestionList;
