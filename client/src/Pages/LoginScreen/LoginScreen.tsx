import { useFormik } from "formik";
import React, { useState } from "react";
import * as Yup from "yup"; // Import Yup package
import useSignIn from 'react-auth-kit/hooks/useSignIn';
import axios, { AxiosError } from "axios";
import { LoginTokenResponse } from "./LoginScreen.state";


function LoginScreen() {
  const [error, setError] = useState<string | null>(null);
  const signIn = useSignIn();
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
    onSubmit: async (values) => { // Add 'async' keyword to make the function asynchronous
      if (formik.isValid) {
        const token: string | undefined = await retrieveToken(); // Await the retrieveToken() function
        if (!token) {
          return;
        }
      }
    },
    validationSchema: validateSchema,
  });
  async function retrieveToken(): Promise<string | undefined> {
    try {
      const response = await axios.post("http://localhost:5000/api/login", {
        email: formik.values.email,
        password: formik.values.password,
      });
      return response.data.token;
    } catch (e) {
      const error = e as AxiosError;
      console.error(error);
      // setError(error.response?.data);
      if (typeof error.response?.data === "string") {
        setError(error.response.data);
      } else {
        setError("Er is iets misgegaan tijdens het inloggen.");
      }
    }
  }
  return (
    <div className="w-screen h-screen flex justify-center items-center">
      <form onSubmit={formik.handleSubmit} className="flex flex-col">
        {error ? <div className="bg-red-400 text-white p-4 text-center">{error}</div> : null}
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
