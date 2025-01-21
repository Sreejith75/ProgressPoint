'use client';

import React, { useEffect, useState } from 'react';
import { useForm, Controller, useFieldArray } from 'react-hook-form';
import { InputNumber } from 'primereact/inputnumber';
import { Button } from 'primereact/button';
import { Card } from 'primereact/card';
import { Chart } from 'primereact/chart';

interface PerformanceFactor {
  factorId: number;
  factorName: string;
  weightage: number | null;
}

interface FactorFormProps {
  selectedRole: string;
  performanceFactors: PerformanceFactor[];
  initialWeightages: PerformanceFactor[];
  onSave: (data: PerformanceFactor[]) => void;
}

const FactorForm: React.FC<FactorFormProps> = ({
  selectedRole,
  performanceFactors,
  initialWeightages,
  onSave,
}) => {
  const { control, handleSubmit, watch, reset } = useForm<{ weightages: PerformanceFactor[] }>({
    defaultValues: {
      weightages: initialWeightages,
    },
  });

  const { fields, replace } = useFieldArray({
    control,
    name: 'weightages',
  });

  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  useEffect(() => {
    replace(initialWeightages);
  }, [initialWeightages, replace]);

  const onSubmit = (data: { weightages: PerformanceFactor[] }) => {
    const totalWeightage = data.weightages.reduce((sum, item) => sum + (item.weightage ?? 0), 0);

    if (totalWeightage !== 100) {
      setErrorMessage('Total weightage must equal 100%.');
      return;
    }

    setErrorMessage(null);
    onSave(data.weightages);
    console.log(data);
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <Card className="mb-6">
        <h3 className="text-xl font-semibold mb-4">Assign Weightages for {selectedRole}</h3>
        <div className="space-y-4">
          {fields.map((field, index) => (
            <div key={field.factorId} className="flex justify-between items-center mb-3">
              <span>{field.factorName}</span>
              <Controller
                name={`weightages.${index}.weightage`}
                control={control}
                render={({ field }) => (
                  <div className="flex items-center">
                    <InputNumber
                      value={field.value}
                      onValueChange={(e) => field.onChange(e.value)}
                      min={0}
                      max={100}
                      showButtons
                      className="w-fit text-gray-700"
                    />
                    <span className="ml-2">%</span>
                  </div>
                )}
              />
            </div>
          ))}
        </div>
        {errorMessage && (
          <p className="text-red-500 text-sm mt-2">{errorMessage}</p>
        )}
      </Card>

      <div className="flex justify-end mt-4">
        <Button
          label="Save Configuration"
          icon="pi pi-check"
          type="submit"
          className="p-button-rounded p-button-success bg-blue-500 text-white p-3"
        />
      </div>

      <Card className="mt-8 flex justify-center mb-[2rem]">
        <h3 className="text-xl font-semibold mb-4">Performance Factor Distribution</h3>
        <Chart
          type="pie"
          className="w-[25rem] h-[25rem] flex justify-center"
          data={{
            labels: fields.map((field) => field.factorName),
            datasets: [
              {
                data: watch('weightages').map((w) => w.weightage ?? 0),
                backgroundColor: ['#42A5F5', '#66BB6A', '#FF7043'],
              },
            ],
          }}
          options={{ responsive: true }}
        />
      </Card>
    </form>
  );
};

export default FactorForm;
