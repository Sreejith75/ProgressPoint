"use client";
import axiosInstance from "@/services/axiosInterceptor";
import axios from "axios";
import React, { useState, useEffect, useCallback } from "react";
import { FaEdit, FaTrash } from "react-icons/fa";

interface Permission {
  id: number;
  permissionName: string;
}

interface Role {
  id: number;
  systemRoleName: string;
  description: string;
  permissions: Permission[];
}

const baseApiUrl = process.env.NEXT_PUBLIC_API_URL;

const page: React.FC = () => {
  const [roles, setRoles] = useState<Role[]>([]);
  const [allPermissions, setAllPermissions] = useState<Permission[]>([]);
  const [editingRole, setEditingRole] = useState<Role | null>(null);
  const [error, setError] = useState<string>("");

  useEffect(() => {
    fetchRoles();
    fetchAllPermissions();
  }, []);

  const fetchAllPermissions = async () => {
    try {
      const response = await axiosInstance.get<Permission[]>(`${baseApiUrl}/permissions`);
      setAllPermissions(response.data);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Error fetching permissions.");
    }
  };

  const fetchRoles = async () => {
    try {
      const response = await axiosInstance.get<Role[]>(`${baseApiUrl}/system-roles`);
      setRoles(response.data);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Error fetching roles.");
    }
  };

  const handleSaveRole = async (role: Role) => {
    try {
      const payload = {
        systemRoleName: role.systemRoleName,
        description: role.description,
        permissions: role.permissions.map((p) => p.id),
      };
  
      if (role.id) {
        await axiosInstance.put(`${baseApiUrl}/system-roles/${role.id}`, payload);
      } else {
        await axiosInstance.post(`${baseApiUrl}/system-roles`, payload);
      }
  
      await fetchRoles();
      setEditingRole(null);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Error saving role.");
    }
  };
  

  const handlePermissionChange = (permissionId: number, checked: boolean) => {
    if (editingRole) {
      const updatedPermissions = checked
        ? [
            ...editingRole.permissions,
            allPermissions.find((p) => p.id === permissionId)!,
          ]
        : editingRole.permissions.filter((p) => p.id !== permissionId);

      setEditingRole({ ...editingRole, permissions: updatedPermissions });
    }
  };

  const handleEditRole = (role: Role) => {
    setEditingRole({ ...role });
  };

  const handleDeleteRole = async (roleId: number) => {
    try {
      await axiosInstance.delete(`${baseApiUrl}/system-roles/${roleId}`);
      setRoles((prev) => prev.filter((role) => role.id !== roleId));
    } catch (err) {
      setError(err instanceof Error ? err.message : "Error deleting role.");
    }
  };

  return (
    <div className="p-6 bg-white rounded shadow">
      <h1 className="text-2xl font-bold mb-4">Role & Permission Configuration</h1>

      {error && (
        <div className="bg-red-500 text-white p-4 mb-4 rounded">
          <strong>Error:</strong> {error}
        </div>
      )}

      <div className="mb-6">
        <button
          className="bg-blue-500 text-white px-4 py-2 rounded"
          onClick={() =>
            setEditingRole({
              id: 0,
              systemRoleName: "",
              description: "",
              permissions: [],
            })
          }
        >
          Create New Role
        </button>
        <table className="w-full mt-4 border border-gray-300 rounded">
          <thead>
            <tr className="bg-gray-200 text-left">
              <th className="py-2 px-4">Role</th>
              <th className="py-2 px-4">Description</th>
              <th className="py-2 px-4">Actions</th>
            </tr>
          </thead>
          <tbody>
            {roles.map((role) => (
              <tr key={role.id} className="border-t hover:bg-gray-50">
                <td className="py-2 px-4">{role.systemRoleName}</td>
                <td className="py-2 px-4">{role.description}</td>
                <td className="py-2 px-4 flex space-x-2">
                  <button
                    className="bg-blue-200 text-blue-500 p-3 rounded shadow hover:bg-blue-200 mr-4"
                    onClick={() => handleEditRole(role)}
                  >
                    <FaEdit />
                  </button>
                  <button
                    className="bg-red-200 text-red-500 p-3 rounded shadow hover:bg-red-200"
                    onClick={() => handleDeleteRole(role.id)}
                  >
                    <FaTrash />
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {editingRole && (
        <div className="p-4 border-t mb-[4rem]">
          <h2 className="text-xl font-bold mb-4">
            {editingRole.id ? "Edit Role" : "Create Role"}
          </h2>
          <form
            onSubmit={(e) => {
              e.preventDefault();
              handleSaveRole(editingRole);
            }}
          >
            <div className="mb-4">
              <label className="block text-sm font-medium">Role Name</label>
              <input
                type="text"
                className="w-full p-2 border rounded"
                value={editingRole.systemRoleName}
                onChange={(e) =>
                  setEditingRole({
                    ...editingRole,
                    systemRoleName: e.target.value,
                  })
                }
                required
              />
            </div>
            <div className="mb-4">
              <label className="block text-sm font-medium">Description</label>
              <textarea
                className="w-full p-2 border rounded"
                value={editingRole.description}
                onChange={(e) =>
                  setEditingRole({
                    ...editingRole,
                    description: e.target.value,
                  })
                }
                required
              />
            </div>
            <div className="mb-4">
              <label className="block text-sm font-medium">Permissions</label>
              <div className="grid grid-cols-2 gap-2 mt-2">
                {allPermissions.map((permission) => (
                  <label key={permission.id} className="flex items-center">
                    <input
                      type="checkbox"
                      className="mr-2"
                      checked={editingRole.permissions.some(
                        (p) => p.id === permission.id
                      )}
                      onChange={(e) =>
                        handlePermissionChange(permission.id, e.target.checked)
                      }
                    />
                    {permission.permissionName}
                  </label>
                ))}
              </div>
            </div>
            <div>
              <button
                type="submit"
                className="bg-green-500 text-white px-4 py-2 rounded mr-2"
              >
                Save
              </button>
              <button
                type="button"
                className="bg-gray-500 text-white px-4 py-2 rounded"
                onClick={() => setEditingRole(null)}
              >
                Cancel
              </button>
            </div>
          </form>
        </div>
      )}
    </div>
  );
};

export default page;
