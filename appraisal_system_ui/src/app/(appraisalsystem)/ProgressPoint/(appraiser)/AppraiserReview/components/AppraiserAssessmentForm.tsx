// "use client";

// import React, { useEffect, useState } from "react";
// import { useParams } from "next/navigation";
// import { useForm, Controller, useFieldArray } from "react-hook-form";
// import axios from "axios";
// import { toast } from "react-toastify";
// import CircularRating from "../../../components/CircularRating";
// import SummaryModal from "../../../components/Summery";
// import axiosInstance from "@/services/axiosInterceptor";

// const baseAPIUrl = process.env.NEXT_PUBLIC_API_URL;

// interface Question {
//   id: number;
//   text: string;
//   employeeRating: number;
//   employeeComment: string;
//   appraiserRating?: number;
//   appraiserComment?: string;
// }

// interface Indicator {
//   indicatorId: number;
//   indicatorName: string;
//   questions: Question[];
// }

// interface Factor {
//   factorId: number;
//   factorName: string;
//   indicators: Indicator[];
// }

// interface FormValues {
//   factors: Factor[];
// }

// interface AppraiserAssessmentFormProps {
//   onSubmit: (data: FormValues) => void;
//   employeeId: number;
// }

// const AppraiserAssessmentForm: React.FC<AppraiserAssessmentFormProps> = ({
//   onSubmit,
//   employeeId,
// }) => {
//   const {
//     control,
//     register,
//     handleSubmit,
//     setValue,
//     formState: { errors },
//   } = useForm<FormValues>({
//     defaultValues: { factors: [] },
//   });

//   const { fields: factorFields, append: appendFactor } = useFieldArray({
//     control,
//     name: "factors",
//   });

//   const [loading, setLoading] = useState(false);
//   const [summaryVisible, setSummaryVisible] = useState(false);

//   useEffect(() => {
//     const storedCycleId = Number(localStorage.getItem("cycle_id")) || null;
//     const fetchData = async () => {
//       try {
//         const { data } = await axiosInstance.get(
//           `${baseAPIUrl}/appraisal-feedback/${employeeId}/${storedCycleId}`
//         );

//         const mappedData: FormValues = {
//           factors: data.factors.map((factor: any) => ({
//             factorId: factor.factorId,
//             factorName: factor.factorName,
//             indicators: factor.indicators.map((indicator: any) => ({
//               indicatorId: indicator.indicatorId,
//               indicatorName: indicator.indicatorName,
//               questions: indicator.question.map((q: any) => ({
//                 id: q.questId,
//                 text: q.questionText,
//                 employeeRating: q.appraiseeRating,
//                 employeeComment: q.appraiseeComment,
//                 appraiserRating: q.appraiserRating || 0,
//                 appraiserComment: q.appraiserComment || "",
//               })),
//             })),
//           })),
//         };

//         mappedData.factors.forEach((factor) => appendFactor(factor));
//       } catch (error) {
//         toast.error("Failed to fetch data.");
//       }
//     };

//     fetchData();
//   }, [appendFactor, employeeId]);

//   const confirmSubmission = async (data: FormValues) => {
//     setLoading(true);
//     try {
//       await axios.post("your-endpoint-url", data); // Replace with your API endpoint
//       toast.success("Feedback submitted successfully!");
//       setSummaryVisible(false);
//     } catch (error) {
//       toast.error("Failed to submit feedback.");
//     } finally {
//       setLoading(false);
//     }
//   };

//   return (
//     <div>
//       <form onSubmit={handleSubmit(onSubmit)} className="space-y-4 p-6">
//         <h1 className="text-2xl font-bold mb-6 text-center border-2 border-slate-300 bg-slate-200 p-6">
//           Appraiser Assessment for Employee #{employeeId}
//         </h1>
//         {factorFields.map((factor, factorIndex) => (
//           <div key={factor.id} className="mb-4">
//             <h2 className="text-xl font-bold mb-4">{factor.factorName}</h2>
//             {factor.indicators.map((indicator, indicatorIndex) => (
//               <div key={indicator.indicatorId} className="mb-3">
//                 <h3 className="text-lg mb-2">{indicator.indicatorName}</h3>
//                 {indicator.questions.map((question, questionIndex) => (
//                   <div
//                     key={question.id}
//                     className="p-3 mb-3 bg-gray-50 rounded-md shadow-sm border border-gray-200"
//                   >
//                     <p className="mb-2">
//                       {questionIndex + 1}. {question.text}
//                     </p>

//                     <p className="text-gray-600">
//                       <strong>Employee Rating:</strong>{" "}
//                       {question.employeeRating}/5
//                     </p>
//                     <p className="text-gray-600">
//                       <strong>Employee Comment:</strong>{" "}
//                       {question.employeeComment}
//                     </p>

//                     <Controller
//                       name={`factors.${factorIndex}.indicators.${indicatorIndex}.questions.${questionIndex}.appraiserRating`}
//                       control={control}
//                       rules={{ required: "Appraiser rating is required" }}
//                       render={({ field }) => (
//                         <CircularRating
//                           rating={field.value ?? 0}
//                           onChange={field.onChange}
//                         />
//                       )}
//                     />
//                     <textarea
//                       {...register(
//                         `factors.${factorIndex}.indicators.${indicatorIndex}.questions.${questionIndex}.appraiserComment`,
//                         { required: "Appraiser comment is required" }
//                       )}
//                       placeholder="Add your comments..."
//                       className="w-full mt-2 p-2 border rounded-md text-sm"
//                     />
//                     {errors?.factors?.[factorIndex]?.indicators?.[indicatorIndex]?.questions?.[questionIndex]?.appraiserRating && (
//                       <span className="text-red-600 text-sm">
//                         Appraiser rating is required
//                       </span>
//                     )}
//                     {errors?.factors?.[factorIndex]?.indicators?.[indicatorIndex]?.questions?.[questionIndex]?.appraiserComment && (
//                       <span className="text-red-600 text-sm">
//                         Appraiser comment is required
//                       </span>
//                     )}
//                   </div>
//                 ))}
//               </div>
//             ))}
//           </div>
//         ))}
//         <button
//           type="submit"
//           className="w-full bg-blue-600 text-white p-2 rounded-md"
//         >
//           Submit Assessment
//         </button>
//       </form>

//       <SummaryModal
//         isVisible={summaryVisible}
//         onClose={() => setSummaryVisible(false)}
//         message="Please review your feedback before submission."
//         finalScore={undefined}
//         performanceBucket={null}
//         onConfirm={() => handleSubmit(confirmSubmission)}
//         loading={loading}
//       />
//     </div>
//   );
// };

// const AppraiserReview: React.FC = () => {
//   const { id } = useParams();
//   const [employeeId, setEmployeeId] = useState<number>();

//   useEffect(() => {
//     if (id) {
//       setEmployeeId(Number(id)); // Set the employeeId from the params
//     }
//   }, [id]);

//   if (!employeeId) {
//     return <div>Loading...</div>;
//   }

//   const handleFormSubmit = (data: any) => {
//     console.log("Submitted data for Appraisee ID:", employeeId);
//     console.log("Form Data:", data);
//   };

//   return (
//     <div className="p-6">
//       <h1 className="text-2xl font-bold text-center mb-4">
//         Reviewing Appraisee #{employeeId}
//       </h1>
//       <AppraiserAssessmentForm onSubmit={handleFormSubmit} employeeId={employeeId} />
//     </div>
//   );
// };

// export default AppraiserReview;
