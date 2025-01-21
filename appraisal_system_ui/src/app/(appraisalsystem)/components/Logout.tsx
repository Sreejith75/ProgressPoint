import { useRouter } from "next/navigation";
import React from "react";
import { FaSignOutAlt } from "react-icons/fa";

const Logout = () => {
  const router = useRouter();
  const handleLogout = () => {
    localStorage.removeItem("auth_token");
    localStorage.removeItem("user_id");
    localStorage.removeItem("username");
    localStorage.removeItem("roles");
    localStorage.removeItem("permissions");
    localStorage.removeItem("employee_role_id");
    localStorage.removeItem("cycle_id");


    router.push("/"); // Redirect to login page
  };
  return (
    <div className="mb-[5rem] flex justify-center">
      <button
        onClick={handleLogout}
        className="text-blue-200 hover:text-white cursor-pointer flex items-center justify-center py-2 bg-blue-800 rounded-md hover:bg-blue-700 w-[15rem]"
      >
        <FaSignOutAlt className="mr-2" />
        Logout
      </button>
    </div>
  );
};

export default Logout;
