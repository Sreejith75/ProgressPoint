import React from "react";

interface CircularRatingProps {
  rating: number;
  onChange: (rating: number) => void;
}

const CircularRating: React.FC<CircularRatingProps> = ({ rating, onChange }) => {
  const ratings = [1, 2, 3, 4, 5];
  
  return (
    <div className="flex gap-5 justify-center">
      {ratings.map((rate) => (
        <div
          key={rate}
          onClick={() => onChange(rate)} // Update only the selected rating
          className={`w-7 h-7 p-4 rounded-full flex items-center justify-center cursor-pointer border-2 ${rate <= rating ? "bg-blue-700 text-white" : "bg-gray-200 border-gray-300"}`}
        >
          {rate}
        </div>
      ))}
    </div>
  );
};

export default CircularRating;
