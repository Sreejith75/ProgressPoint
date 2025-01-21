import React, { useState, useEffect } from "react";

interface DateProgressProps {
  startDate: string | Date;
  endDate: string | Date;
}

const DateDifferenceProgressBar: React.FC<DateProgressProps> = ({
  startDate,
  endDate,
}) => {
  const [progressWidth, setProgressWidth] = useState<number>(0);
  const [currentDatePosition, setCurrentDatePosition] = useState<number>(0);
  const [showTooltip, setShowTooltip] = useState<boolean>(false);

  useEffect(() => {
    const start = new Date(startDate).getTime();
    const end = new Date(endDate).getTime();

    const updateProgress = () => {
      const now = new Date().getTime();

      if (start < end) {
        if (now <= start) {
          setProgressWidth(0);
          setCurrentDatePosition(0);
        } else if (now >= end) {
          setProgressWidth(100);
          setCurrentDatePosition(100);
        } else {
          const totalDuration = end - start;
          const elapsedDuration = now - start;
          const progressValue = (elapsedDuration / totalDuration) * 100;
          setProgressWidth(progressValue);
          setCurrentDatePosition(progressValue);
        }
      } else {
        console.error("Invalid date range: startDate must be before endDate");
        setProgressWidth(0);
        setCurrentDatePosition(0);
      }
    };

    updateProgress();

    const interval = setInterval(updateProgress, 24 * 60 * 60 * 1000); // Update daily

    return () => clearInterval(interval);
  }, [startDate, endDate]);

  return (
    <div className="w-[50rem] p-4 mt-[2rem]">
      <div className="relative">
        <div className="relative h-[0.9rem] bg-gray-300 rounded-full">
          <div
            className="absolute h-full bg-green-600 rounded-full transition-all "
            style={{ width: `${progressWidth}%` }}
            onMouseEnter={() => setShowTooltip(true)}
            onMouseLeave={() => setShowTooltip(false)}
          ></div>
          {showTooltip && (
            <span
              className="absolute top-[-2.5rem] transform -translate-x-1/2 text-sm  text-white bg-blue-500 p-2 rounded shadow"
              style={{ left: `${currentDatePosition}%` }}
            >
              {new Date().toLocaleDateString()}
            </span>
          )}
        </div>
      </div>
      <div className="flex justify-between text-sm text-gray-600 mt-4">
        <span>{new Date(startDate).toLocaleDateString()}</span>
        <span>{new Date(endDate).toLocaleDateString()}</span>
      </div>
    </div>
  );
};

export default DateDifferenceProgressBar;
