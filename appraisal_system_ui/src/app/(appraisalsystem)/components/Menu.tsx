import React, { useRef } from "react";
import { Menu } from "primereact/menu";
import { MenuItem } from "primereact/menuitem";
import { Button } from "primereact/button";
import Link from "next/link";
import { FaSignOutAlt } from "react-icons/fa";  // Importing logout icon

interface MenuProps {
  userName: string | null;
}

const CustomMenu = ({ userName }: MenuProps) => {
  const menu = useRef<Menu>(null);

  const items: MenuItem[] = [
    {
      template: (item, options) => (
        <Link href="/ProgressPoint/profile">
          <div className={`${options.className} flex items-center p-3 hover:bg-blue-100 rounded-md`}>
            <i className="pi pi-user mr-2"></i> Profile
          </div>
        </Link>
      ),
    },
    {
      template: (item, options) => (
        <Link href="/settings">
          <div className={`${options.className} flex items-center p-3 hover:bg-blue-100 rounded-md`}>
            <i className="pi pi-cog mr-2"></i> Settings
          </div>
        </Link>
      ),
    },
    {
      // Logout menu item
      template: (item, options) => (
        <div
          className={`${options.className} flex items-center p-3 cursor-pointer text-red-600 rounded-md hover:bg-blue-100`}
          onClick={handleLogout}
        >
          <FaSignOutAlt className="mr-2" /> Logout
        </div>
      ),
    },
  ];

  const handleLogout = () => {
    localStorage.removeItem("auth_token");
    localStorage.removeItem("user_id");
    localStorage.removeItem("username");
    localStorage.removeItem("roles");
    localStorage.removeItem("permissions");
    localStorage.removeItem("employee_role_id");
    localStorage.removeItem("cycle_id");

    // Redirect to login page (you can adjust this route as needed)
    window.location.href = "/"; // Or use Next.js router.push("/login") if preferred
  };

  return (
    <div className="flex items-center space-x-4 border border-gray-300 p-2 rounded-3xl transition-all  duration-300 hover:shadow-lg hover:border-gray-400">
      <a className="flex items-center space-x-2">
        <p className="text-gray-700 text-sm">{userName}</p>
        <div
          className="w-7 h-7 bg-blue-700 rounded-full flex items-center justify-center text-white font-bold cursor-pointer transform transition-transform duration-300 hover:scale-110 hover:bg-blue-800"
          onClick={(event) => menu.current?.toggle(event)}
        >
          {userName ? userName.charAt(0).toUpperCase() : "U"}
        </div>
      </a>
      <Menu model={items} popup ref={menu}  className="p-2"/>
    </div>
  );
};

export default CustomMenu;
