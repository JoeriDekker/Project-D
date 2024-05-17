import React from "react";
import Input from "../../components/Input/Input";
import { t } from "i18next";
import { useFormik } from "formik";
import { LinkLessButton } from "../../components/Button/AnyButton";
import * as Yup from "yup";
import axios, { AxiosError } from "axios";

function RegisterPage() {
  const [error, setError] = React.useState<string | null>(null);
  const validationScheme = Yup.object({
    firstname: Yup.string()
      .required(t("Register.firstnamereq"))
      .min(2, "Register.firstnamemin")
      .max(50, "Register.firstnamemax"),
    lastname: Yup.string()
      .required(t("Register.lastnamereq"))
      .min(2, "Register.lastnamemin")
      .max(50, "Register.lastnamemax"),
    email: Yup.string()
      .required(t("Register.emailreq"))
      .email(t("Register.emailvalid")),
    password: Yup.string()
      .required(t("Register.passwordreq"))
      .min(8, t("Register.passwordmin")).max(250, t("Register.passwordmax"))
      .matches(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,64}$/, t("Register.passwordcomplex")),
    cpassword: Yup.string()
      .required(t("Register.cpasswordreq"))
      .oneOf([Yup.ref("password")], t("Register.cpasswordmatch")),
  });
  const formik = useFormik({
    initialValues: {
      firstname: "",
      lastname: "",
      email: "",
      password: "",
      cpassword: "",
    },
    validateOnChange: true,
    validationSchema: validationScheme,
    onSubmit: async (values) => {
      await sendForm();
    },
  });

  async function sendForm() {
    try {
      const res = await axios.post(process.env.REACT_APP_API_URL + "/api/register", formik.values);
      if (res.status === 200) {
        // Todo: Redirect to success page
      }
    } catch (e) {
      const error = e as AxiosError<{ error?: string }>; // Update the type of the error variable
      if (error.response?.status === 500) {
        setError(t("Register.servererror"));
      } else {
        if (error.response?.data?.error) {
          setError(error.response.data.error);
        }
      }
    }
  }

  return (
    <div className="w-screen h-screen flex flex-col flex-wrap justify-center items-center">
      <h1 className="text-2xl inline-block">{t("Register.register")}</h1>
      {error && <p className="text-red-500">{error}</p>}
      <form onSubmit={formik.handleSubmit} className="lg:w-2/5 sm:w-4/5 mt-5">
        <div className="space-y-5">
          <div>
            <div className="flex space-x-5">
              <div className="flex-1">
                <h3 className="text-m font-medium">
                  {t("Register.firstname")}
                </h3>
                <Input
                  error={formik.errors.firstname}
                  onChange={formik.handleChange}
                  name="firstname"
                  placeholder={t("Register.examplefirstname")}
                  width=""
                />
              </div>
              <div className="flex-1">
                <h3 className="text-m font-medium">{t("Register.lastname")}</h3>
                <Input
                  name="lastname"
                  placeholder={t("Register.examplelastname")}
                  width="w-1/2"
                  error={formik.errors.lastname}
                  onChange={formik.handleChange}
                />
              </div>
            </div>
          </div>
          <div>
            <h3 className="text-m font-medium">{t("Register.email")}</h3>
            <p className="opacity-40">{t("Register.emailexplain")}</p>
            <Input
              type="email"
              name="email"
              placeholder={t("Register.exampleemail")}
              width=""
              error={formik.errors.email}
              onChange={formik.handleChange}
            />
          </div>
          <div>
            <h3 className="text-m font-medium">{t("Register.password")}</h3>
            <Input
              name="password"
              type="password"
              placeholder={t("Register.password")}
              width=""
              error={formik.errors.password}
              onChange={formik.handleChange}
            />
          </div>
          <div>
            <h3 className="text-m font-medium">
              {t("Register.cpassword")}
            </h3>
            <Input
              type="password"
              placeholder={t("Register.cpassword")}
              width=""
              name="cpassword"
              error={formik.errors.cpassword}
              onChange={formik.handleChange}
            />
          </div>
          <LinkLessButton text="Registreren" />
        </div>
      </form>
    </div>
  );
}

export default RegisterPage;
