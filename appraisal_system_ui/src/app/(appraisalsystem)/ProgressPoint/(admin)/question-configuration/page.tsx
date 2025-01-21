'use client';
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import QuestionList from './components/QuestionList';
import { QuestionForm } from './components/QuestionForm';
import axiosInstance from '@/services/axiosInterceptor';

const baseApriUrl = process.env.NEXT_PUBLIC_API_URL;

interface PerformanceIndicator {
  indicatorId: number;
  indicatorName: string;
}

interface PerformanceFactor {
  id: number;
  name: string;
  indicators: PerformanceIndicator[];
}

const QuestionConfigPanel: React.FC = () => {
  const [activeTab, setActiveTab] = useState<'config' | 'list'>('config');
  
  // States for performance factors and questions
  const [performanceFactors, setPerformanceFactors] = useState<PerformanceFactor[]>([]); // Initialize with empty array
  const [questions, setQuestions] = useState<string[]>([]);

  // Loading states
  const [loadingPerformanceFactors, setLoadingPerformanceFactors] = useState(true);
  const [loadingQuestions, setLoadingQuestions] = useState(true);

  // Fetch performance factors and questions from APIs
  useEffect(() => {
    const fetchPerformanceFactors = async () => {
      try {
        const response = await axiosInstance.get(`${baseApriUrl}/PerfomanceFactors`);
        // Check if the response has data and it's the correct structure
        if (Array.isArray(response.data)) {
          setPerformanceFactors(response.data);
        } else {
          console.error('Invalid data structure for performance factors:', response.data);
        }
        setLoadingPerformanceFactors(false);
      } catch (error) {
        setLoadingPerformanceFactors(false);
      }
    };

    const fetchQuestions = async () => {
      try {
        const response = await axiosInstance.get(`${baseApriUrl}/Questions`);
        // Check if the response contains the questions data
        if (Array.isArray(response.data)) {
          setQuestions(response.data);
        } else {
          console.error('Invalid data structure for questions:', response.data);
        }
        setLoadingQuestions(false);
      } catch (error) {
        setLoadingQuestions(false);
      }
    };

    fetchPerformanceFactors();
    fetchQuestions();
  }, []);

  const toggleTab = (tab: 'config' | 'list') => setActiveTab(tab);

  // Show loading indicator or actual data
  if (loadingPerformanceFactors) {
    return <div>Loading...</div>;
  }

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-4">Question Configuration</h1>

      {/* Tab Navigation */}
      <div className="flex space-x-4 mb-6 border-b">
        <button
          className={`px-4 py-2 ${activeTab === 'config' ? 'border-b-2 border-blue-600 font-bold' : ''}`}
          onClick={() => toggleTab('config')}
        >
          Configure Questions
        </button>
        <button
          className={`px-4 py-2 ${activeTab === 'list' ? 'border-b-2 border-blue-600 font-bold' : ''}`}
          onClick={() => toggleTab('list')}
        >
          Question List
        </button>
      </div>

      {/* Render Config or List Panel */}
      {activeTab === 'config' ? (
        <QuestionForm 
          performanceFactors={performanceFactors} 
          setQuestions={setQuestions} 
        />
      ) : (
        <QuestionList />
      )}
    </div>
  );
};

export default QuestionConfigPanel;
