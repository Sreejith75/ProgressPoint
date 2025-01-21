import React, { useState } from "react";
import { useForm, Controller } from "react-hook-form";
import axiosInstance from "@/services/axiosInterceptor";
import { MultiSelect } from "primereact/multiselect"; // Import MultiSelect component
import { Dropdown } from "primereact/dropdown"; // Import Dropdown component
import { toast } from "react-toastify";

interface Role {
  id: number;
  roleName: string;
  roleCode?: string | null;
  hierarchyLevel: number;
  departmentId: number;
}

interface SystemRole {
  id: number;
  systemRoleName: string;
  roleCode?: string | null;
  hierarchyLevel: number;
  departmentId: number;
}

interface Appraiser {
  id: number;
  name: string;
}

interface AddEmployeeModalProps {
  onClose: () => void;
  onEmployeeAdded: (employee: any) => void;
}

const AddEmployeeModal: React.FC<AddEmployeeModalProps> = ({
  onClose,
  onEmployeeAdded,
}) => {
  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm({
    defaultValues: {
      firstName: "",
      lastName: "",
      email: "",
      passwordHash: "",
      employeeRoleId: "",
      systemRoleIds: [],
      phoneNumber: "",
      appraiserId: "",
    },
  });

  const [loading, setLoading] = useState(false);
  const [roles, setRoles] = useState<Role[]>([]);
  const [systemRoles, setSystemRoles] = useState<SystemRole[]>([]);
  const [appraisers, setAppraisers] = useState<Appraiser[]>([]);
  const [rolesLoading, setRolesLoading] = useState(false);
  const [systemRolesLoading, setSystemRolesLoading] = useState(false);
  const [appraisersLoading, setAppraisersLoading] = useState(false);
  const [selectedRoleId, setSelectedRoleId] = useState<number>();

  const onSubmit = async (data: any) => {
    setLoading(true);
    try {
      // Ensure appraiserId is correctly included in the formatted data
      const formattedData = {
        ...data,
        appraiserId: data.appraiserId ? data.appraiserId : 0, // If no appraiser selected, set to 0
      };

      const response = await axiosInstance.post(
        "https://localhost:57679/Employees",
        formattedData
      );

      onEmployeeAdded(response.data);
      onClose();

      if (response.data.id !== 0) {
        toast.success("Employee successfully added");
      }
    } catch (error) {
      console.log("Error creating employee:", error);
    } finally {
      setLoading(false);
    }
  };

  const loadRoles = async () => {
    if (roles.length === 0 && !rolesLoading) {
      setRolesLoading(true);
      try {
        const response = await axiosInstance.get(
          "https://localhost:57679/employeeroles"
        );
        const formattedRoles = response.data.map((role: Role) => ({
          label: role.roleName,
          value: role.id,
        }));
        setRoles(formattedRoles);
      } catch (error) {
        console.log("Error fetching roles:", error);
      } finally {
        setRolesLoading(false);
      }
    }
  };

  const loadSystemRoles = async () => {
    if (systemRoles.length === 0 && !systemRolesLoading) {
      setSystemRolesLoading(true);
      try {
        const response = await axiosInstance.get(
          "https://localhost:57679/system-roles"
        );
        const formattedSystemRoles = response.data.map((role: SystemRole) => ({
          label: role.systemRoleName,
          value: role.id,
        }));
        setSystemRoles(formattedSystemRoles);
      } catch (error) {
        console.log("Error fetching system roles:", error);
      } finally {
        setSystemRolesLoading(false);
      }
    }
  };

  const loadAppraisers = async () => {
    if (!selectedRoleId) {
      toast.info("Please select a role first.");
      return;
    }

    if (appraisers.length === 0 && !appraisersLoading) {
      setAppraisersLoading(true);
      try {
        const response = await axiosInstance.get(
          `https://localhost:57679/Employees/appraisers-list/${selectedRoleId}`
        );
        const formattedAppraisers = response.data.appraisers.map(
          (appraiser: Appraiser) => ({
            label: appraiser.name,
            value: appraiser.id,
          })
        );
        setAppraisers(formattedAppraisers);
      } catch (error) {
        console.log("Error fetching appraisers:", error);
      } finally {
        setAppraisersLoading(false);
      }
    }
  };

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
      <div className="bg-white rounded-lg shadow-md w-[40rem] p-6 mt-[4rem]">
        <h2 className="text-lg font-semibold mb-4 flex justify-center">
          Create Employee
        </h2>
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <Controller
              name="firstName"
              control={control}
              rules={{ required: "First Name is required" }}
              render={({ field }) => (
                <input
                  {...field}
                  placeholder="First Name"
                  className="w-full px-4 py-2 border rounded-lg border-slate-700"
                />
              )}
            />
            <Controller
              name="lastName"
              control={control}
              rules={{ required: "Last Name is required" }}
              render={({ field }) => (
                <input
                  {...field}
                  placeholder="Last Name"
                  className="w-full px-4 py-2 border rounded-lg border-slate-700"
                />
              )}
            />
            <Controller
              name="email"
              control={control}
              rules={{
                required: "Email is required",
                pattern: {
                  value: /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$/,
                  message: "Invalid email format",
                },
              }}
              render={({ field }) => (
                <input
                  {...field}
                  placeholder="Email"
                  className="w-full px-4 py-2 border rounded-lg border-slate-700"
                />
              )}
            />
            <Controller
              name="passwordHash"
              control={control}
              rules={{ required: "Password is required" }}
              render={({ field }) => (
                <input
                  {...field}
                  type="password"
                  placeholder="Password"
                  className="w-full px-4 py-2 border rounded-lg border-slate-700"
                />
              )}
            />
            <Controller
              name="employeeRoleId"
              control={control}
              rules={{ required: "Role ID is required" }}
              render={({ field }) => (
                <Dropdown
                  {...field}
                  value={field.value || ""}
                  options={roles}
                  onChange={(e) => {
                    field.onChange(e.value);
                    setSelectedRoleId(e.value);
                    setAppraisers([]);
                  }}
                  optionLabel="label"
                  placeholder="Select Role"
                  onFocus={loadRoles}
                  className="w-full md:w-14rem border rounded-lg border-slate-700"
                />
              )}
            />
            <Controller
              name="systemRoleIds"
              control={control}
              rules={{ required: "System Role IDs are required" }}
              render={({ field }) => (
                <MultiSelect
                  {...field}
                  value={field.value || []}
                  options={systemRoles}
                  onChange={(e) => field.onChange(e.value)}
                  optionLabel="label"
                  placeholder="Select System Roles"
                  onFocus={loadSystemRoles}
                  display="chip"
                  maxSelectedLabels={3}
                  className="w-full md:w-14rem border rounded-lg border-slate-700"
                />
              )}
            />
            <Controller
              name="phoneNumber"
              control={control}
              rules={{ required: "Phone Number is required" }}
              render={({ field }) => (
                <input
                  {...field}
                  placeholder="Phone Number"
                  className="w-full px-4 py-2 border rounded-lg border-slate-700"
                />
              )}
            />
            <Controller
              name="appraiserId"
              control={control}
              render={({ field }) => (
                <Dropdown
                  {...field}
                  value={field.value} // Ensure this correctly reflects the selected appraiser ID
                  options={[{ label: "No Appraiser", value: 0 }, ...appraisers]} // Appraiser options
                  onChange={(e) => field.onChange(e.value)} // Updates the value in the form state
                  optionLabel="label"
                  placeholder="Select Appraiser"
                  onFocus={loadAppraisers} // Load appraisers when the dropdown is focused
                  className="w-full md:w-14rem border rounded-lg border-slate-700"
                />
              )}
            />
          </div>
          <div className="flex justify-end space-x-4 mt-4">
            <button
              type="button"
              onClick={onClose}
              className="px-4 py-2 bg-gray-300 rounded-lg hover:bg-gray-400"
            >
              Cancel
            </button>
            <button
              type="submit"
              disabled={loading}
              className="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600"
            >
              {loading ? "Adding..." : "Add Employee"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default AddEmployeeModal;
