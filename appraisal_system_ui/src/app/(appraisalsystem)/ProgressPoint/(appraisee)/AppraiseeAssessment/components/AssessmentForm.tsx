import React, { useEffect, useState } from "react";
import { useFieldArray, Controller, UseFormReturn } from "react-hook-form";
import CircularRating from "../../../components/CircularRating";
import axiosInstance from "@/services/axiosInterceptor";
import { toast } from "react-toastify";
import SummaryModal from "../../../components/Summery";
import { number } from "yup";

const baseAPIUrl = process.env.NEXT_PUBLIC_API_URL;

interface Question {
  id: number;
  text: string;
  rating: number;
  comment: string;
  artifact: File | null;
}

interface Indicator {
  indicatorId: number;
  indicatorName: string;
  weightage: number;
  questions: Question[];
}

interface Factor {
  factorId: number;
  factorName: string;
  indicators: Indicator[];
}

interface FormValues {
  factors: Factor[];
}

interface AssessmentFormProps {
  control: UseFormReturn<FormValues>["control"];
  register: UseFormReturn<FormValues>["register"];
  handleSubmit: UseFormReturn<FormValues>["handleSubmit"];
  errors: UseFormReturn<FormValues>["formState"]["errors"];
  onSubmit: (data: FormValues) => void;
  factors: Factor[];
  feedbackId: number | undefined;
}
interface SummaryDetails {
  summeryId: number;
  appraiseeScore: number;
  appraiserScore: number;
  performanceBucket: string | null;
  description:string | null;
}

const AssessmentForm: React.FC<AssessmentFormProps> = ({
  control,
  register,
  handleSubmit,
  errors,
  onSubmit,
  factors,
  feedbackId,
}) => {
  const { fields: factorFields } = useFieldArray({
    control,
    name: "factors",
  });

  const [formData, setFormData] = useState<FormValues | null>(null);
  const [loading, setLoading] = useState(false);
  const [summaryVisible, setSummaryVisible] = useState(false);
  const [summaryDetails, setSummaryDetails] = useState<SummaryDetails | null>(
    null
  );

  const handleFormSubmit = (data: FormValues) => {
    setFormData(data);
    onSubmit(data);
    setSummaryVisible(true);
  };

  useEffect(() => {
    const storedCycleId = Number(localStorage.getItem("cycle_id"));
    const storedEmployeeId = Number(localStorage.getItem("user_id"));
    setTimeout(() => {
      fetchSummary(storedEmployeeId, storedCycleId);
    }, 500);
  }, [summaryVisible, onSubmit]);

  const fetchSummary = async (employeeId: number, cycleId: number) => {
    try {
      const response = await axiosInstance.get(
        `${baseAPIUrl}/AppraisalSummary/${employeeId}/${cycleId}`
      );

      if (response.data) {
        setSummaryDetails(response.data);
      } else {
        toast.error("Failed to fetch summary. No data returned.");
      }
    } catch (error) {
      toast.error("Error fetching summary. Please try again.");
    }
  };

  const confirmSubmission = async () => {
    if (!formData) return;
    setLoading(true);

    try {
      const response = await axiosInstance.put(
        `${baseAPIUrl}/appraisal-feedback/UpdateStatus`,
        {
          feedbackId: feedbackId,
        }
      );

      toast.success(`Feedback submitted successfully!`);
      setSummaryVisible(false);
    } catch (error) {
      toast.error("Failed to submit feedback. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <form onSubmit={handleSubmit(handleFormSubmit)} className="space-y-4 p-6">
        <h1 className="text-2xl font-bold mb-[2rem] text-center border-2 border-slate-300 bg-slate-200 p-6">
          Assessment
        </h1>
        {factorFields.map((factor, factorIndex) => (
          <div key={factor.factorId} className="mb-4">
            <h2 className="text-xl font-bold mb-[2rem] flex justify-center px-5 border-b-2 p-[0.5rem] border-t-2">
              {factor.factorName}
            </h2>
            {factor.indicators.map((indicator, indicatorIndex) => (
              <div key={indicator.indicatorId} className="pl-2 mb-3">
                <h3 className="text-md font-light mb-[1rem] flex justify-center">
                  {indicator.indicatorName}
                </h3>
                {indicator.questions.map((question, questionIndex) => (
                  <div
                    key={question.id}
                    className="p-3 mb-3 bg-gray-50 rounded-md shadow-sm border border-gray-200"
                  >
                    <p className="text-sm mb-2">
                      {questionIndex + 1}. {question.text}
                    </p>

                    <Controller
                      name={`factors.${factorIndex}.indicators.${indicatorIndex}.questions.${questionIndex}.rating`}
                      control={control}
                      rules={{ required: "Rating is required" }}
                      render={({ field }) => (
                        <CircularRating
                          rating={field.value}
                          onChange={field.onChange}
                        />
                      )}
                    />

                    <textarea
                      {...register(
                        `factors.${factorIndex}.indicators.${indicatorIndex}.questions.${questionIndex}.comment`,
                        
                      )}
                      placeholder="Add your comments..."
                      defaultValue={question.comment}
                      className="w-full mt-2 p-2 border rounded-md text-sm"
                    />

                    {errors?.factors?.[factorIndex]?.indicators?.[
                      indicatorIndex
                    ]?.questions?.[questionIndex]?.rating && (
                      <span className="text-red-600 text-sm">
                        {
                          errors.factors[factorIndex].indicators[indicatorIndex]
                            .questions[questionIndex].rating.message
                        }
                      </span>
                    )}

                    {errors?.factors?.[factorIndex]?.indicators?.[
                      indicatorIndex
                    ]?.questions?.[questionIndex]?.comment && (
                      <span className="text-red-600 text-sm">
                        {
                          errors.factors[factorIndex].indicators[indicatorIndex]
                            .questions[questionIndex].comment.message
                        }
                      </span>
                    )}
                  </div>
                ))}
              </div>
            ))}
          </div>
        ))}

        <button
          type="submit"
          className="w-full bg-blue-600 text-white p-2 rounded-md"
        >
          Submit Assessment
        </button>
      </form>

      {/* Summary Modal */}
      <SummaryModal
        isVisible={summaryVisible}
        onClose={() => setSummaryVisible(false)}
        message="Please review your feedback before submission."
        finalScore={summaryDetails?.appraiseeScore ?? 0}
        performanceBucket={summaryDetails?.performanceBucket ?? "N/A"}
        onConfirm={confirmSubmission}
        loading={loading}
        description={summaryDetails?.description??"N/A"}
      />
    </div>
  );
};

export default AssessmentForm;
