// components/ExportButton.tsx
import React from "react";
import { Button } from "primereact/button";  // Import PrimeReact Button

interface ExportButtonProps {
  onExport: () => void;
}

const ExportButton: React.FC<ExportButtonProps> = ({ onExport }) => {
  return (
    <Button
      icon="pi pi-download"  // Use PrimeReact's pi-download icon
      onClick={onExport}
      className="p-button-primary p-button-rounded"
    />
  );
};

export default ExportButton;
