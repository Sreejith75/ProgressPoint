'use client';

import React, { useEffect, useState } from 'react';
import { Card } from 'primereact/card';
import { InputNumber } from 'primereact/inputnumber';
import { Button } from 'primereact/button';
import { Toast } from 'primereact/toast';
import axiosInstance from '@/services/axiosInterceptor'; // Assuming axiosInstance is set up correctly.

interface Indicator {
  indicatorId: number;
  indicatorName: string;
  weightage: number;
}

interface Factor {
  factorId: number;
  factorName: string;
  indicators: Indicator[];
}

const IndicatorForm: React.FC = () => {
  const [factors, setFactors] = useState<Factor[]>([]);
  const [selectedIndicators, setSelectedIndicators] = useState<Map<number, number>>(new Map());
  const [loading, setLoading] = useState<boolean>(true);
  const toast = React.useRef<any>(null);

  useEffect(() => {
    // Fetch the factors and indicators from the API
    const fetchData = async () => {
      try {
        const response = await axiosInstance.get('https://localhost:57679/PerformanceIndicators');
        if (response.data && response.data.factors) {
          setFactors(response.data.factors);
        }
      } catch (error) {
        toast.current.show({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to load data.',
          life: 3000,
        });
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const handleWeightageChange = (indicatorId: number, value: number | null) => {
    const updatedSelections = new Map(selectedIndicators);
    if (value !== null) {
      updatedSelections.set(indicatorId, value);
    } else {
      updatedSelections.delete(indicatorId); 
    }
    setSelectedIndicators(updatedSelections);
  };

  const handleSubmit = () => {
    toast.current.show({
      severity: 'success',
      summary: 'Success',
      detail: 'Indicator configuration saved',
      life: 3000,
    });
    console.log('Submitted data:', Array.from(selectedIndicators));
  };

  if (loading) {
    return (
      <div className="p-4">
        <Toast ref={toast} />
        <Card className="mb-6">
          <h3 className="text-xl font-semibold mb-4">Indicator Configuration</h3>
          <p>Loading...</p>
        </Card>
      </div>
    );
  }

  return (
    <div className="p-4">
      <Toast ref={toast} />
      <Card className="mb-6">
        <h3 className="text-xl font-semibold mb-4">Indicator Configuration</h3>

        <div>
          {factors.map((factor) => (
            <div key={factor.factorId} className="mb-6">
              <h4 className="text-lg font-medium mb-3">{factor.factorName}</h4>
              {factor.indicators.map((indicator) => (
                <div key={indicator.indicatorId} className="flex justify-between items-center mb-3 p-3 border border-1 border-gray-300 rounded-lg">
                  <div>
                    <h5 className="text-md font-medium">{indicator.indicatorName}</h5>
                    <span className="text-sm">Current Weightage: {indicator.weightage*100}</span>
                  </div>
                </div>
              ))}
            </div>
          ))}
        </div>

        <Button label="Save Configuration" icon="pi pi-check" onClick={handleSubmit} className="mt-4" />
      </Card>
    </div>
  );
};

export default IndicatorForm;
