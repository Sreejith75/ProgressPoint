"use client";

import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import axiosInstance from "@/services/axiosInterceptor";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { NotificationModal } from "./components/AssessmentNotification";
import AssessmentInstructionsModal from "./components/ConfirmAssessment";
import AssessmentForm from "./components/AssessmentForm";

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

const AppraisalAssessmentPage: React.FC = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [formId, setFormId] = useState<number | null>(null);
  const [employeeId, setEmployeeId] = useState<number | null>(null);
  const [feedbackId, setFeedbackId] = useState<number>();
  const [cycleId, setCycleId] = useState<number | null>(null);
  const [roleId, setRoleId] = useState<number | null>(null);
  const [notificationModal, setNotificationModal] = useState(false);
  const [confirmationModal, setConfirmationModal] = useState(false);
  const [feedbackStatus, setFeedbackStatus] = useState<string>();

  const {
    control,
    register,
    handleSubmit,
    setValue,
    getValues,
    formState: { errors },
  } = useForm<FormValues>({
    defaultValues: { factors: [] },
  });

  useEffect(() => {
    const storedCycleId = Number(localStorage.getItem("cycle_id")) || null;
    const storedRoleId =
      Number(localStorage.getItem("employee_role_id")) || null;
    const storedEmployeeId = Number(localStorage.getItem("user_id")) || null;
    if (storedCycleId && storedRoleId && storedEmployeeId) {
      setCycleId(storedCycleId);
      setRoleId(storedRoleId);
      setEmployeeId(storedEmployeeId);
      checkExistingAppraisalFeedback(
        storedEmployeeId,
        storedCycleId,
        storedRoleId
      );
    } else {
      setError("Assessment not started. Please check back later.");
    }
  }, [confirmationModal, axiosInstance]);

  const checkExistingAppraisalFeedback = async (
    employeeId: number,
    cycleId: number,
    roleId: number
  ) => {
    try {
      setLoading(true);
      const response = await axiosInstance.get(
        `${baseAPIUrl}/appraisal-feedback/existing-feedback/${employeeId}/${cycleId}`
      );

      if (response.data.status === true) {
        const feedbackDetails = response.data.feedbackDetails;
        fetchQuestions(cycleId, roleId, employeeId, feedbackDetails);
        setFeedbackId(response.data.feedbackId);
        setFeedbackStatus(response.data.feedbackStatus);
      } else {
        // No feedback exists; fetch questions normally
        const questionsAvailable = await fetchQuestions(
          cycleId,
          roleId,
          employeeId
        );
        if (questionsAvailable) {
          setNotificationModal(true);
        } else {
          setError("Assessment not started yet!");
          setNotificationModal(false);
        }
      }
    } catch (error) {
      toast.error("An error occurred while checking existing feedback.");
    } finally {
      setLoading(false);
    }
  };

  const onConfirmCreateFeedback = async (
    cycleId: number,
    roleId: number,
    employeeId: number
  ) => {
    try {
      const response = await axiosInstance.post(
        `${baseAPIUrl}/appraisal-feedback`,
        {
          cycleId,
          employeeId,
        }
      );

      if (response.status == 200) {
        setConfirmationModal(false);
        setNotificationModal(false);
        fetchQuestions(cycleId, roleId, employeeId);
      } else {
        setNotificationModal(true);
        toast.error("Unexpected response:", response);
      }
    } catch (error) {
      console.error("Error creating appraisal feedback:", error);
    }
  };

  const fetchQuestions = async (
    cycleId: number,
    roleId: number,
    employeeId: number,
    feedbackDetails?: any[]
  ): Promise<boolean> => {
    try {
      setLoading(true);
      const response = await axiosInstance.get(
        `${baseAPIUrl}/appraisal-forms/${roleId}/${employeeId}/questions`
      );

      if (response.data && response.status === 200) {
        setFormId(response.data.formId);
        const groupedFactors = response.data.factors.map((factor: any) => ({
          factorId: factor.factorId,
          factorName: factor.factorName,
          indicators: factor.indicators.map((indicator: any) => ({
            indicatorId: indicator.indicatorId,
            indicatorName: indicator.indicatorName,
            weightage: indicator.weightage,
            questions: indicator.questions.map((q: any) => {
              // Check if questionId exists in feedbackDetails
              const existingFeedback = feedbackDetails?.find(
                (feedback: any) => feedback.questionId === q.questionId
              );

              return {
                id: q.questionId,
                text: q.questionText,
                rating: existingFeedback?.appraiseeRating || 0,
                comment: existingFeedback?.appraiseeComment || "",
                artifact: existingFeedback?.artifact || null,
              };
            }),
          })),
        }));

        setValue("factors", groupedFactors);
        return true;
      } else {
        setError("No questions found.");
        return false;
      }
    } catch (error) {
      toast.info("Assessment not started yet!");
      return false;
    } finally {
      setLoading(false);
    }
  };

  const handleFormSubmit = async (data: FormValues) => {
    if (!formId || !cycleId || !employeeId) {
      toast.error("Missing form, cycle, or employee details.");
      return;
    }

    const requestPayload = {
      employeeId: employeeId,
      formId: formId,
      cycleId: cycleId,
      feedbackDetails: [] as {
        questionId: number;
        appraiseeRating: number;
        appraiseeComment: string;
        artifact: File | null;
      }[],
    };

    data.factors.forEach((factor) => {
      factor.indicators.forEach((indicator) => {
        indicator.questions.forEach((q) => {
          const feedbackDetail = {
            questionId: q.id,
            appraiseeRating: q.rating,
            appraiseeComment: q.comment || "",
            artifact: null,
          };

          requestPayload.feedbackDetails.push(feedbackDetail);
        });
      });
    });

    sendRequest(requestPayload);
  };

  const sendRequest = async (payload: any) => {
    try {
      const response = await axiosInstance.put(
        `${baseAPIUrl}/appraisal-feedback/update`,
        payload,
        {
          headers: { "Content-Type": "application/json" },
        }
      );
      if (response.status === 200) {
        toast.success("Assessment updated successfully.");
      } else {
        toast.error("Failed to update assessment. Please try again.");
      }
    } catch (error) {
      toast.info("Feedbacks already submitted !\n Confirm your submission");
    }
  };

  return (
    <>
      {notificationModal && (
        <NotificationModal
          isOpen={notificationModal}
          onStartAssessment={() => setConfirmationModal(true)}
        />
      )}

      {confirmationModal && (
        <AssessmentInstructionsModal
          isVisible={confirmationModal}
          onCancel={() => setConfirmationModal(false)}
          onConfirm={() =>
            onConfirmCreateFeedback(
              Number(cycleId),
              Number(roleId),
              Number(employeeId)
            )
          }
        />
      )}

      {!notificationModal && !confirmationModal && (
        <div className="bg-white rounded-lg shadow-md max-w-fit mx-auto mt-2 mb-20">
          {loading && <p className="text-center">Loading questions...</p>}
          {error && (
            <p className="text-center text-slate-500 p-6 flex mt-[10rem]">
              {error}
            </p>
          )}

          {/* Logic to check feedbackDetails and feedbackStatus */}
          {!loading &&
            !error &&
            (!feedbackId || feedbackStatus !== "Pending") && (
              <p className="text-center text-slate-500 p-6 flex mt-[10rem]">
                Assessment is not started yet !
              </p>
            )}

          {/* Existing logic for when feedbackDetails exist */}
          {!loading && !error && feedbackId && feedbackStatus === "Pending" && (
            <AssessmentForm
              control={control}
              register={register}
              handleSubmit={handleSubmit}
              errors={errors}
              onSubmit={handleFormSubmit}
              factors={getValues("factors")}
              feedbackId={feedbackId}
              
            />
          )}
        </div>
      )}
    </>
  );
};

export default AppraisalAssessmentPage;
