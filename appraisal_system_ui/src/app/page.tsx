"use client";
import { useEffect, useState } from "react";
import axios from "axios";
import { useRouter } from "next/navigation";
import { jwtDecode, JwtPayload } from "jwt-decode";
import * as Yup from "yup";
import { Formik, Field, Form, ErrorMessage } from "formik";
import Cookies from "js-cookie";

const baseAPIUrl = process.env.NEXT_PUBLIC_API_URL;

interface CustomJwtPayload extends JwtPayload {
  sub: string;
  unique_name: string;
  roles: string[];
  employee_role_id: string;
  user_permissions: string;
}

const loginSchema = Yup.object().shape({
  email: Yup.string()
    .email("Please enter a valid email address")
    .required("Email is required"),
  password: Yup.string().required("Password is required"),
});

export default function Home() {
  const [error, setError] = useState<string | null>(null);
  const router = useRouter();

  const handleSubmit = async (values: { email: string; password: string }) => {
    try {
      const response = await axios.post(`${baseAPIUrl}/user/login`, values);
      console.log("Login successful", response.data);

      const token:string = response.data.token;

      
      const decodedToken = jwtDecode<CustomJwtPayload>(token);
      
      const userId = decodedToken.sub;
      const username = decodedToken.unique_name;
      const employee_role_id = decodedToken.employee_role_id;
      const roles = decodedToken.roles || [];
      const permissions = JSON.parse(decodedToken.user_permissions || "[]");
      
      Cookies.set("auth_token",token);
      localStorage.setItem("auth_token", token);
      localStorage.setItem("user_id", userId);
      localStorage.setItem("username", username);
      localStorage.setItem("roles", JSON.stringify(roles));
      localStorage.setItem("permissions", JSON.stringify(permissions));
      localStorage.setItem("employee_role_id", employee_role_id);
  

      router.push("/ProgressPoint");
    } catch (err: any) {
      setError(err.response?.data?.error || "Login failed");
    }
  };

  return (
    <div>
      <div className="flex flex-col justify-center items-center h-screen">
        <div className="text-center mb-6">
          <h1 className="text-3xl font-bold text-purple-600">ProgressPoint</h1>
          <p className="text-gray-500">Your path to performance and growth</p>
        </div>
        <div className="bg-white w-[25rem] h-[17rem] rounded-lg drop-shadow-xl shadow-black p-8 max-w-md">
          <Formik
            initialValues={{ email: "", password: "" }}
            validationSchema={loginSchema}
            onSubmit={handleSubmit}
          >
            {({ isSubmitting }) => (
              <Form className="space-y-4  h-[15rem]">
                <div>
                  <label
                    htmlFor="email"
                    className="block text-sm font-medium text-gray-700"
                  >
                    Email
                  </label>
                  <Field
                    id="email"
                    name="email"
                    type="email"
                    className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-purple-500 focus:border-purple-500 sm:text-sm"
                    placeholder="Enter your email"
                  />
                  <ErrorMessage
                    name="email"
                    component="p"
                    className="text-red-600 text-sm"
                  />
                </div>

                <div>
                  <label
                    htmlFor="password"
                    className="block text-sm font-medium text-gray-700"
                  >
                    Password
                  </label>
                  <Field
                    id="password"
                    name="password"
                    type="password"
                    className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-purple-500 focus:border-purple-500 sm:text-sm"
                    placeholder="Enter your password"
                  />
                  <ErrorMessage
                    name="password"
                    component="p"
                    className="text-red-600 text-sm"
                  />
                </div>

                {error && <div className="text-red-600 text-sm">{error}</div>}

                <div>
                  <button
                    type="submit"
                    disabled={isSubmitting}
                    className="w-full bg-gradient-to-r from-purple-500 to-pink-500 hover:from-purple-600 hover:to-pink-600 text-white py-2 px-4 rounded-md shadow-lg font-medium transition duration-300"
                  >
                    Login
                  </button>
                </div>
              </Form>
            )}
          </Formik>
        </div>
      </div>
    </div>
  );
}

