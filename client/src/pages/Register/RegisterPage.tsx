import React from "react";
import Input from "../../components/Input/Input";
import { t } from "i18next";

function RegisterPage() {
  return (
    <div className="w-screen h-screen flex flex-col flex-wrap justify-center items-center">
      <h1 className="text-2xl inline-block">{t("Register.register")}</h1>
      <form className="lg:w-2/5 sm:w-4/5 mt-5">
        <div className="space-y-5">
          <div>
            <div className="flex space-x-5">
              <div className="flex-1">
                <h3 className="text-m font-medium">
                  {t("Register.firstname")}
                </h3>
                <Input name="firstname" placeholder={t("Register.examplefirstname")} width="" />
              </div>
              <div className="flex-1">
                <h3 className="text-m font-medium">{t("Register.lastname")}</h3>
                <Input
                  name="lastname"
                  placeholder={t("Register.examplelastname")}
                  width="w-1/2"
                />
              </div>
            </div>
          </div>
          <div>
            <h3 className="text-m font-medium">{t("Register.email")}</h3>
            <p className="opacity-40">{t("Register.emailexplain")}</p>
            <Input type="email" name="email" placeholder={t("Register.exampleemail")} width="" />
          </div>
          <div>
            <h3 className="text-m font-medium">{t("Register.password")}</h3>
            <Input name="password" type="password" placeholder={t("Register.password")} width="" />
          </div>
          <div>
            <h3 className="text-m font-medium">
              {t("Register.confirmpassword")}
            </h3>
            <Input
              type="password"
              placeholder={t("Register.conmfirmpassword")}
              width=""
              name="cpassword"
            />
          </div>
          <div className="space-y-5">
            <div className="flex space-x-2">
              <div className="flex-1">
                <h3 className="text-m font-medium">
                  {t("Register.street")}
                </h3>
                <Input name="street" placeholder={t("Register.examplestreetname")} width="" />
              </div>
              <div className="w-1/6">
                <h3 className="text-m font-medium">
                  {t("Register.housenumber")}
                </h3>
                <Input name="housenumber" placeholder={t("Register.examplehousenumber")} />
              </div>
            </div>
            <div className="flex space-x-2">
              <div className="flex-1">
                <h3 className="text-m font-medium">
                  {t("Register.city")}
                </h3>
                <Input name="city" placeholder={t("Register.examplecity")} width="" />
              </div>
              <div className="w-1/6">
                <h3 className="text-m font-medium">
                  {t("Register.zipcode")}
                </h3>
                <Input name="zipcode" placeholder={t("Register.examplezipcode")} />
              </div>
            </div>
          </div>
        </div>
      </form>
    </div>
  );
}

export default RegisterPage;
