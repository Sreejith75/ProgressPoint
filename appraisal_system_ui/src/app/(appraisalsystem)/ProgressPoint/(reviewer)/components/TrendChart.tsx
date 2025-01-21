// TrendsChart.tsx
import React from 'react';
import { Chart } from 'primereact/chart';

interface Trends {
  year: number;
  quarter: string;
  averageAppraiseeScore: number;
  averageAppraiserScore: number;
}

interface TrendsChartProps {
  trendsData: Trends[];
}

const TrendsChart: React.FC<TrendsChartProps> = ({ trendsData }) => {
  // Chart for Trends
  const trendsChartData = {
    labels: trendsData.map((trend) => `${trend.quarter} ${trend.year}`),
    datasets: [
      {
        label: "Average Appraisee Score",
        data: trendsData.map((trend) => trend.averageAppraiseeScore),
        backgroundColor: "#42A5F5",
        fill: false,
        borderColor: "#42A5F5",
        tension: 0.4,
      },
      {
        label: "Average Appraiser Score",
        data: trendsData.map((trend) => trend.averageAppraiserScore),
        backgroundColor: "#66BB6A",
        fill: false,
        borderColor: "#66BB6A",
        tension: 0.4,
      },
    ],
  };

  return (
    <div className=" mt-[2rem] mb-8 w-full">
      <h4 className="text-2xl font-semibold text-gray-700 mb-4 text-center">Trends Overview</h4>
      <div className="bg-white p-3 rounded-lg shadow-md">
        <Chart type="line" data={trendsChartData} options={{ responsive: true }} className='h-[23rem] flex justify-center'/>
      </div>
    </div>
  );
};

export default TrendsChart;
