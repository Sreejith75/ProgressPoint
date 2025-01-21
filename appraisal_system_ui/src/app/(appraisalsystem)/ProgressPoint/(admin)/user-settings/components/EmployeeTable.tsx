// components/EmployeeTable.tsx
import React from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import "primereact/resources/themes/lara-light-indigo/theme.css"; // Adjust theme as needed
import "primereact/resources/primereact.min.css"; // PrimeReact styles
import "primeicons/primeicons.css"; // PrimeIcons for sorting/filtering icons

interface Employee {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
  role: string;
  department: string;
  manager: string | null;
}

interface EmployeeTableProps {
  employees: Employee[];
}

const EmployeeTable: React.FC<EmployeeTableProps> = ({ employees }) => {
  return (
    <div className="card">
      <DataTable
        value={employees}
        paginator
        rows={10}
        emptyMessage="No employees found."
        className="shadow-md rounded-lg"
        style={{height:"100%"}}
      >
        <Column field="id" header="ID" sortable className="text-sm" />
        <Column field="name" header="Name" sortable className="text-sm" />
        <Column field="email" header="Email" sortable className="text-sm" />
        <Column
          field="phoneNumber"
          header="Phone Number"
          sortable
          className="text-sm"
        />
        <Column field="role" header="Role" sortable className="text-sm" />
        <Column field="department" header="Department" sortable className="text-sm" />
        <Column
          field="manager"
          header="Manager"
          sortable
          body={(rowData) => rowData.manager || "N/A"}
          className="text-sm"
        />
      </DataTable>
    </div>
  );
};

export default EmployeeTable;
