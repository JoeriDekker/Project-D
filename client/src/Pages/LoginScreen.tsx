import { useFormik } from "formik";
import React from "react";
import * as Yup from "yup"; // Import Yup package

function LoginScreen() {
  const validateSchema = Yup.object().shape({
    email: Yup.string()
      .email("Voer een geldig email adres in.")
      .required("Voer een geldig email adres in.")
      .min(8, "Email moet uit minimaal 8 karakters bestaan."),
    password: Yup.string()
      .required("Voer een geldig wachtwoord in.")
      .min(8, "Wachtwoord moet uit minimaal 8 karakters bestaan."),
  });
  const formik = useFormik({
    initialValues: {
      email: "",
      password: "",
    },
    onSubmit: (values) => {
      console.log(values);
    },
    validationSchema: validateSchema,
  });
  return (
    <div className="w-screen h-screen flex justify-center items-center">
      <form onSubmit={formik.handleSubmit} className="flex flex-col">
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
