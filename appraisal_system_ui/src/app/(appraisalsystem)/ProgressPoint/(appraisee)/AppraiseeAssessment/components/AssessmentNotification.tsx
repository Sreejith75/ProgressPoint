"use client";
import React, { useState } from "react";

interface NotificationModalProps {
  isOpen: boolean;
  onStartAssessment: () => void;
}

export const NotificationModal: React.FC<NotificationModalProps> = ({
  isOpen,
  onStartAssessment,
}) => {
  if (!isOpen) return null;

  return (
    <div className="flex items-center justify-center mt-[5rem]">
      <div className="bg-white rounded-lg p-6 w-[30rem] shadow-lg ">
        <h2 className="text-xl font-bold mb-4 flex justify-center">Assessment Notification</h2>
        <p className="mb-4 text-sm">The assessment has started. Please click below to proceed.</p>

        <div className="flex justify-center gap-3">
          <button
            className="px-2 py-2 bg-green-600 text-white rounded-md hover:bg-green-700 text-sm"
            onClick={onStartAssessment}
          >
            Start Assessment
          </button>
        </div>
      </div>
    </div>
  );
};