"use client";
import React, { useEffect, useState } from "react";
import Menu from "./Menu";
const Header = () => {
  const[userName,setUserName]=useState<string | null>(null);

  useEffect(()=>{
    const userName=localStorage.getItem("username")
    setUserName(userName);
  },[]);
  
  return (
    <div className="bg-white shadow-md flex justify-between items-center px-6 py-2 relative z-30">
      <h1 className="text-2xl font-bold text-purple-600">ProgressPoint</h1>
      <Menu userName={userName}/>
    </div>
  );
};

export default Header;
