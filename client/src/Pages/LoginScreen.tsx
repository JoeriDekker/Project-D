import { useFormik } from "formik";
import React from "react";
import * as Yup from "yup"; // Import Yup package

function LoginScreen() {
  const validateSchema = Yup.object().shape({
    email: Yup.string()
      .email("Voer een geldig email adres in.")
      .required("Voer een geldig email adres in.")
      .min(8, "Email moet uit minimaal 8 karakters bestaan."),
    password: Yup.string().required().min(8),
  });
  const formik = useFormik({
    initialValues: {
      email: "",
      password: "",
    },
    onSubmit: (values) => {
      console.log(values);
    },
  });
  return (
    <div className="w-screen h-screen flex justify-center items-center">
      <form></form>
    </div>
  );
}

export default LoginScreen;
