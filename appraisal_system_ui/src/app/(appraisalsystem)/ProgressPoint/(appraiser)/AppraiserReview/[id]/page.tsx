"use client";

import React, { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import { useForm, Controller, useFieldArray } from "react-hook-form";
import axiosInstance from "@/services/axiosInterceptor";
import { toast } from "react-toastify";
import CircularRating from "../../../components/CircularRating";
import SummaryModal from "../../../components/Summery";

const baseAPIUrl = process.env.NEXT_PUBLIC_API_URL;

interface Question {
  id: number;
  text: string;
  employeeRating: number;
  appraiseeScore:number;
  performanceBucket:string;
  employeeComment: string;
  appraiserRating?: number;
  appraiserComment?: string;
}

interface Indicator {
  indicatorId: number;
  indicatorName: string;
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

interface SummaryDetails {
  summeryId: number;
  appraiseeScore: number;
  appraiserScore: number;
  performanceBucket: string | null;
  description:string | null;
}

const AppraiserReview: React.FC = () => {
  const { id } = useParams();
  const [appraiseeId, setAppraiseeId] = useState<number | null>(null);
  const [appraiseeName, setAppraiseeName] = useState<string | null>(null);
  const [performanceBucket, setPerformanceBucket] = useState<string | null>(
    null
  );
  const [summaryDetails, setSummaryDetails] = useState<SummaryDetails | null>(
    null
  );
  const [finalScore, setFinalScore] = useState<number | null>(null);
  const [feedbackId, setFeedbackId] = useState<number | null>(null);
  const [cycleId, setCycleId] = useState<number | null>(null);
  const [employeeId, setEmployeeId] = useState<number | null>(null);
  const [error, setError] = useState<string>();
  const [appraiserForm, setAppraiserForm] = useState<boolean>(false);
  const [loading, setLoading] = useState(false);
  const [summaryVisible, setSummaryVisible] = useState(false);

  const {
    control,
    register,
    handleSubmit,
    setValue,
    formState: { errors },
  } = useForm<FormValues>({ defaultValues: { factors: [] } });

  const { fields: factorFields, append: appendFactor } = useFieldArray({
    control,
    name: "factors",
  });

  useEffect(() => {
    if (id) {
      setAppraiseeId(Number(id));
    }
  }, [id]);

  useEffect(() => {
    if (appraiseeId === null) return;

    const storedCycleId = Number(localStorage.getItem("cycle_id")) || null;
    setCycleId(storedCycleId);

    const storedEmployeeId = Number(localStorage.getItem("user_id")) || null;
    setEmployeeId(storedEmployeeId);

    const fetchData = async () => {
      try {
        const response = await axiosInstance.get(
          `${baseAPIUrl}/appraisal-feedback/${appraiseeId}/${storedCycleId}`
        );

        const mappedData: FormValues = {
          factors: response.data.factors.map((factor: any) => ({
            factorId: factor.factorId,
            factorName: factor.factorName,
            indicators: factor.indicators.map((indicator: any) => ({
              indicatorId: indicator.indicatorId,
              indicatorName: indicator.indicatorName,
              questions: indicator.question.map((q: any) => ({
                id: q.questId,
                text: q.questionText,
                employeeRating: q.appraiseeRating,
                employeeComment: q.appraiseeComment,
                appraiserRating: q.appraiserRating ?? 0, 
                appraiserComment: q.appraiserComment ?? "",
              })),
            })),
          })),
        };

        // Set additional fields
        setAppraiseeName(response.data.appraiseeName);
        setPerformanceBucket(response.data.performanceBucket);
        setFinalScore(response.data.appraiseeScore);
        setFeedbackId(response.data.feedbackId);

        // Populate the form with the mapped data
        mappedData.factors.forEach((factor) => {
          if (
            !factorFields.find((field) => field.factorId === factor.factorId)
          ) {
            appendFactor(factor);
          }
        });

        setAppraiserForm(true);
        if (response.data.feedbackStatus === "Completed") {
          setAppraiserForm(false);
        }
      } catch (error) {
        setError("Failed to fetch data.");
        setAppraiserForm(false);
      }
    };

    fetchData();
  }, [appraiseeId, appendFactor, factorFields, summaryVisible]);

  const handleFormSubmit = async (data: FormValues) => {
    if (!feedbackId || !cycleId || !appraiseeId) {
      toast.error("Missing form, cycle, or employee details.");
      return;
    }
    setSummaryVisible(true);
    const requestPayload = {
      feedbackId,
      appraiserId: employeeId,
      appraiserFeedbackDetails: data.factors.flatMap((factor) =>
        factor.indicators.flatMap((indicator) =>
          indicator.questions.map((q) => ({
            questionId: q.id,
            appraiserRating: q.appraiserRating || 0,
            appraiserComment: q.appraiserComment || "",
          }))
        )
      ),
    };

    try {
      const response = await axiosInstance.put(
        `${baseAPIUrl}/appraisal-feedback/update/appraiser-feedbacks`,
        requestPayload,
        { headers: { "Content-Type": "application/json" } }
      );

      if (response.status === 200) {
        toast.success("Assessment updated successfully.");
        setTimeout(() => {
          fetchSummary(appraiseeId, cycleId);
        }, 500);
      } else {
        toast.error("Failed to update assessment. Please try again.");
      }
    } catch (error) {
      toast.info("Feedbacks already submitted!\nConfirm your submission.");
    }
  };

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
    <div className="p-6">
      {appraiserForm ? (
        <form
          onSubmit={handleSubmit(handleFormSubmit)}
          className="space-y-4 p-6 border border-black"
        >
          <div className="mb-4 text-center">
            <h1 className="text-2xl font-bold">
              Reviewing  :  {appraiseeName}
            </h1>
            {performanceBucket && finalScore !== null && (
              <p className="text-lg mt-2">
                Performance Bucket: {performanceBucket} | Final Score:{" "}
                {finalScore}
              </p>
            )}
          </div>

          {factorFields.map((factor, factorIndex) => (
            <div key={factor.id} className="mb-4">
              <h3 className="text-xl font-bold mb-4 text-center">
                {factor.factorName}
              </h3>
              {factor.indicators.map((indicator, indicatorIndex) => (
                <div key={indicator.indicatorId} className="mb-3">
                  <h4 className="text-lg mb-2 text-center">
                    {indicator.indicatorName}
                  </h4>
                  {indicator.questions.map((question, questionIndex) => (
                    <div
                      key={question.id}
                      className="p-6 mb-3 bg-gray-50 rounded-md shadow-sm border border-gray-200"
                    >
                      <p className="mt-5 mb-5">{`${questionIndex + 1}. ${
                        question.text
                      }`}</p>
                      <div className="flex gap-10 mb-5">
                        <p>
                          <strong>Rating:</strong> {question.employeeRating}/5
                        </p>
                        <p>
                          <strong>Comment:</strong> {question.employeeComment}
                        </p>
                      </div>
                      <Controller
                        name={`factors.${factorIndex}.indicators.${indicatorIndex}.questions.${questionIndex}.appraiserRating`}
                        control={control}
                        rules={{ required: "Appraiser rating is required" }}
                        render={({ field }) => (
                          <CircularRating
                            rating={field.value ?? 0}
                            onChange={field.onChange}
                          />
                        )}
                      />
                      <textarea
                        {...register(
                          `factors.${factorIndex}.indicators.${indicatorIndex}.questions.${questionIndex}.appraiserComment`,
                          
                        )}
                        placeholder="Add your comments..."
                        className="w-full mt-2 p-2 border rounded-md text-sm"
                        defaultValue={
                          factor.indicators[indicatorIndex].questions[
                            questionIndex
                          ].appraiserComment
                        }
                      />
                      {errors?.factors?.[factorIndex]?.indicators?.[
                        indicatorIndex
                      ]?.questions?.[questionIndex]?.appraiserRating && (
                        <span className="text-red-600">
                          Rating is required for this question.
                        </span>
                      )}
                    </div>
                  ))}
                </div>
              ))}
            </div>
          ))}

          <div className="flex justify-center space-x-4">
            <button
              type="submit"
              className="bg-blue-500 text-white py-2 px-4 rounded-md w-full"
              disabled={loading}
            >
              Submit Feedback
            </button>
          </div>
        </form>
      ) : (
        <p className="text-center text-gray-600">
          No data available for this review.
        </p>
      )}

      {summaryVisible && (
        <SummaryModal
          onClose={() => setSummaryVisible(false)}
          isVisible={summaryVisible}
          onConfirm={confirmSubmission}
          message="Summary"
          performanceBucket={summaryDetails?.performanceBucket ?? "N/A"}
          finalScore={summaryDetails?.appraiserScore ?? 0}
          description={summaryDetails?.description??"N/A"}
        />
      )}
    </div>
  );
};

export default AppraiserReview;
