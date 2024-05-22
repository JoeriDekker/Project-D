import { useFormik } from "formik";
import React, { useEffect, useState } from "react";
import * as Yup from "yup"; // Import Yup package
import useSignIn from "react-auth-kit/hooks/useSignIn";
import useIsAuthenticated from "react-auth-kit/hooks/useIsAuthenticated";
import axios, { AxiosError } from "axios";
import { UserResponse } from "./LoginScreen.state";
import { useLocation } from "react-router-dom";
import { t } from "i18next";

function useQuery() {
  const { search } = useLocation();

  return React.useMemo(() => new URLSearchParams(search), [search]);
}

function LoginScreen() {
  const query = useQuery();
  const [error, setError] = useState<string | null>(query.get("error"));
  const [success, setSuccess] = useState<string | null>(null);
  const signIn = useSignIn();
  const isAuthenticated = useIsAuthenticated();
  const validateSchema = Yup.object().shape({
    email: Yup.string()
      .email("Voer een geldig email adres in.")
      .required("Voer een geldig email adres in.")
      .min(8, "Email moet uit minimaal 8 karakters bestaan."),
    password: Yup.string()
      .required("Voer een geldig wachtwoord in.")
      .min(6, "Wachtwoord moet uit minimaal 6 karakters bestaan."),
  });
  const formik = useFormik({
    initialValues: {
      email: "",
      password: "",
    },
    onSubmit: async (values) => {
      // Add 'async' keyword to make the function asynchronous
      if (formik.isValid) {
        const token: string | undefined = await retrieveToken(); // Await the retrieveToken() function
        if (!token) {
          return;
        }
        const userData = await retrieveUser(token as string);
        if (!userData) {
          return;
        }
        const signinres = signIn({
          auth: {
            token: token,
            type: "Bearer",
          },
          userState: {
            id: userData?.id,
            email: userData?.email,
            firstName: userData?.firstName,
            lastName: userData?.lastName,
          },
        });

        if (signinres) {
          window.location.href = "/";
        }
      }
    },
    validationSchema: validateSchema,
  });
  async function retrieveUser(
    token: string
  ): Promise<UserResponse | undefined> {
    try {
      const response = await axios.get(
        process.env.REACT_APP_API_URL + "/api/users",
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      return response.data;
    } catch (e) {
      const error = e as AxiosError;
      console.error(error);
      if (typeof error.response?.data === "string") {
        setError(error.response.data);
      } else {
        setError("Er is iets misgegaan tijdens het inloggen.");
      }
    }
  }
  async function retrieveToken(): Promise<string | undefined> {
    try {
      const response = await axios.post(
        process.env.REACT_APP_API_URL + "/api/login",
        {
          email: formik.values.email,
          password: formik.values.password,
        }
      );
      return response.data.token;
    } catch (e) {
      const error = e as AxiosError;
      console.error(error);
      if (typeof error.response?.data === "string") {
        setError(error.response.data);
      } else {
        setError("Er is iets misgegaan tijdens het inloggen.");
      }
    }
  }
  
  useEffect(() => {
    if (query.get("success") === "verf") {
      setSuccess(t("Login.verificationsuccess"));
      // fade out the success message after 5 seconds
      setTimeout(() => {
        setSuccess(null);
      }, 5000);
    } else if (query.get("error") === "verf") {
      setError(t("Login.verificationerror"));
    }
  }, [query]);

  // If already logged in, redirect to dashboard
  if (isAuthenticated) {
    window.location.href = "/";
  }
  return (
    <div className="w-screen h-screen flex justify-center items-center">
      <form onSubmit={formik.handleSubmit} className="flex flex-col">
        {error ? (
          <div className="bg-red-400 text-white p-4 text-center">{error}</div>
        ) : null}
        {success ? (
          <div className="bg-green-400 text-white p-4 text-center">{success}</div>
        ) : null}
        <label htmlFor="email">Email</label>
        <input
          className="border"
          type="email"
          name="email"
          onChange={formik.handleChange}
          value={formik.values.email}
          id=""
        />
        {formik.errors.email ? <div>{formik.errors.email}</div> : null}
        <label htmlFor="password">Password</label>
        <input
          className="border"
          type="password"
          name="password"
          onChange={formik.handleChange}
          value={formik.values.password}
          id=""
        />
        {formik.errors.password ? <div>{formik.errors.password}</div> : null}
        <button className="bg-blue-500 text-white" type="submit">
          Submit
        </button>
      </form>
    </div>
  );
}

export default LoginScreen;
