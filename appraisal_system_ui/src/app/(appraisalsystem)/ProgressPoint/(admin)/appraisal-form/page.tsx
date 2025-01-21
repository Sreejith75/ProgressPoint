'use client'
import React, { useState, useEffect } from 'react';
import axios from 'axios';

// Components
import QuestionList from './components/FormsList';
import RoleBasedForm from './components/QuestionForm';
import axiosInstance from '@/services/axiosInterceptor';

const baseApiUrl=process.env.NEXT_PUBLIC_API_URL;

interface AppraisalCycle {
  id: number;
  quarter: string;
  year: number;
}

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

const Page: React.FC = () => {
  const [activeTab, setActiveTab] = useState<'form' | 'list'>('form');

  const [roles, setRoles] = useState<Role[]>([]);
  const [questions, setQuestions] = useState<Question[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const rolesResponse = await axiosInstance.get(`${baseApiUrl}/employeeroles`);
        setRoles(rolesResponse.data);

        const questionsResponse = await axiosInstance.get(`${baseApiUrl}/Questions`);
        const formattedQuestions = questionsResponse.data.map((item: any) => ({
          id: item.questionId,
          text: item.questionText,
          factor: item.indicator.factor.name,
          indicator: item.indicator.indicatorName,
        }));
        setQuestions(formattedQuestions);
      } catch (error) {
        console.log('Error fetching data:', error);
      }
    };

    fetchData();
  }, []);

  const toggleTab = (tab: 'form' | 'list') => {
    setActiveTab(tab);
  };

  return (
    <div className="p-2 z-30 w-full ">
      <h1 className="text-xl font-bold mb-4">Role-Based Question Form</h1>

      {/* Tab Navigation */}
      <div className="flex space-x-4 mb-4 border-b">
        <button
          className={`px-4 py-2 ${activeTab === 'form' ? 'border-b-2 border-blue-600 font-bold' : ''}`}
          onClick={() => toggleTab('form')}
        >
          Add Questions
        </button>
        <button
          className={`px-4 py-2 ${activeTab === 'list' ? 'border-b-2 border-blue-600 font-bold' : ''}`}
          onClick={() => toggleTab('list')}
        >
          List Forms
        </button>
      </div>

      {/* Render Active Panel */}
      {activeTab === 'form' ? (
        <RoleBasedForm
          roles={roles}
          questions={questions}
          setQuestions={setQuestions}
        />
      ) : (
        <QuestionList/>
      )}
    </div>
  );
};

export default Page;
