import Header from "@/app/(appraisalsystem)/components/Header";
import Sidebar from "@/app/(appraisalsystem)/components/Sidebar";
import React from "react";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";


interface LayoutProps {
  children: React.ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
  
  return (
    <div className="flex flex-col bg-gray-100">
      <ToastContainer position="top-center" autoClose={3000} hideProgressBar={false} theme="colored" />
      <Header />
      {/* Main Content Area */}
      <div className="flex h-screen">
        <Sidebar />
        {/* Page Content */}
        <main className="flex-1 p-6 overflow-y-auto">{children}</main>
      </div>
    </div>
  );
};

export default Layout;
