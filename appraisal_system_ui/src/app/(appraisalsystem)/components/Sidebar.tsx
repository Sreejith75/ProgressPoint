"use client";

import React, { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import {
  FaCog,
  FaUser,
  FaChartLine,
  FaRegListAlt,
  FaHistory,
  FaQuestionCircle,
  FaFileAlt,
  FaSignOutAlt,
  FaClipboardList,
  FaHome,
} from "react-icons/fa";
import Link from "next/link";
import Logout from "./Logout";

type MenuItem = {
  permission: string;
  label: string;
  link: string;
  icon: React.JSX.Element;
};

const MENU_CONFIG: MenuItem[] = [
  {
    permission: "APPRAISAL_FORM",
    label: "Appraisal Form",
    link: "/ProgressPoint/appraisal-form",
    icon: <FaFileAlt />,
  },
  // {
  //   permission: "PERFORMANCE_DATA_FILTERING",
  //   label: "Analysis",
  //   link: "/ProgressPoint",
  //   icon: <FaRegListAlt />,
  // },
  {
    permission: "DASH_BOARD",
    label: "Dashboard",
    link: "/ProgressPoint/dash-board",
    icon: <FaRegListAlt />,
  },
  {
    permission: "APPRAISAL_SETTINGS",
    label: "Appraisal Settings",
    link: "/ProgressPoint/appraisal-settings",
    icon: <FaClipboardList />,
  },
  {
    permission: "QUESTION_CONFIG_PANEL",
    label: "Question",
    link: "/ProgressPoint/question-configuration",
    icon: <FaQuestionCircle />,
  },
  {
    permission: "PERFORMANCE_FACTOR_CONFIG",
    label: "Performance Factors",
    link: "/ProgressPoint/performance-factors",
    icon: <FaChartLine />,
  },
  {
    permission: "ROLES_PERMISSION_MANAGEMENT",
    label: "Roles & Permissions",
    link: "/ProgressPoint/RolePermission",
    icon: <FaCog />,
  },
  {
    permission: "USER_MANAGEMENT",
    label: "User Management",
    link: "/ProgressPoint/user-settings",
    icon: <FaUser />,
  },
  {
    permission: "PERFORMANCE_SUMMARY",
    label: "Review",
    link: "/ProgressPoint/AppraiserReview",
    icon: <FaFileAlt />,
  },
  {
    permission: "ASSESSMENT",
    label: "Assessments",
    link: "/ProgressPoint/AppraiseeAssessment",
    icon: <FaRegListAlt />,
  },
  {
    permission: "SUMMARY",
    label: "Summary",
    link: "/ProgressPoint/Summary",
    icon: <FaChartLine />,
  },
  {
    permission: "HISTORY",
    label: "History",
    link: "/ProgressPoint/History",
    icon: <FaHistory />,
  }
];

const Sidebar = () => {
  const [menuItems, setMenuItems] = useState<MenuItem[]>([]);
  const router = useRouter();

  useEffect(() => {
    const fetchPermissions = () => {
      const permissionsCookie = localStorage.getItem("permissions");

      const permissions = permissionsCookie
        ? JSON.parse(permissionsCookie)
        : [];

      const filteredMenu = MENU_CONFIG.filter((item) =>
        permissions.includes(item.permission)
      );
      setMenuItems(filteredMenu);
    };

    fetchPermissions();
  }, []);

 

  return (
    <div className="w-[14rem] bg-blue-900 text-white flex flex-col overflow-auto justify-between ">
      {/* Menu Items */}  
      <nav className="p-2 space-y-3 mt-[1rem] flex flex-col justify-between">
        <a
          href="/ProgressPoint"
          className="flex items-center px-4 py-2 rounded hover:bg-blue-700 text-md"
        >
          <span className="mr-3">{<FaHome />}</span>
          <label htmlFor="Home" className="text-sm">Home</label>
        </a>
        {menuItems.map(({ permission, label, link, icon }) => (
          <Link
            key={permission}
            href={link}
            className="flex items-center px-4 py-2 rounded hover:bg-blue-700 text-sm"
          >
            <span className="mr-3">{icon}</span>
            {label}
          </Link>
        ))}
      </nav>
    </div>
  );
};

export default Sidebar;
