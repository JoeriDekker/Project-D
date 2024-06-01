import React, { useEffect } from "react";
import Navbar from "../../components/navbar/navbar";
import * as Yup from "yup";
import { t } from "i18next";
import { AnyButton2, LinkLessButton } from "../../components/Button/AnyButton";
import axios, { AxiosError } from "axios";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";
import { useFormik } from "formik";
import { UserResponse } from "../LoginScreen/LoginScreen.state";
import Input from "../../components/Input/Input";

function WaterLevelSettings() {
    const [connected, setConnected] = React.useState(false);
    const [user, setUser] = React.useState<UserResponse | null>(null);
    const authHeader = useAuthHeader();

    // -------- Address Formik --------
    const addressValidationScheme = Yup.object().shape({
        street: Yup.string()
            .required("Address.streetreq")
            .min(3, "Address.streetlength"),
        houseNumber: Yup.string()
            .required("Address.housenumberreq")
            .min(1, "Address.housenumberlength")
            .max(5, "Address.housenumberlength"),
        zip: Yup.string()
            .required("Address.zipreq")
            .length(6, "Address.zipformat")
            .matches(/\d\d\d\d[A-Za-z]+/, "Address.zipformat"),
        city: Yup.string()
            .required("Address.cityreq")
            .min(3, "Address.citylength")
            .max(30, "Address.citylength"),
        password: Yup.string()
            .required("Register.passwordreq")
            .max(250, "Register.passwordmaxlength"),
    });
    const addressFormik = useFormik({
        initialValues: {
            street: "",
            houseNumber: "",
            zip: "",
            city: "",
            password: "",
        },
        onSubmit: async (values) => handleAddressChange(values),
        validationSchema: addressValidationScheme,
    });

  useEffect(() => {
    async function fetchUser() {
      const res = await axios.get(
        process.env.REACT_APP_API_URL + "/api/users",
        {
          headers: {
            Authorization: authHeader,
          },
        }
      );
      setUser(res.data);
    }
    fetchUser();
  }, [authHeader]);

    async function handleAddressChange(values: {
        street: string;
        houseNumber: string;
        zip: string;
        city: string;
        password: string;
    }) {
        try {
            const res = await axios.put(process.env.REACT_APP_API_URL + "/api/address", values, {
                headers: {
                    Authorization: authHeader,
                },
            });
            if (res.status === 200) {
                alert("Address updated successfully");
            }
        } catch (e) {
            const error = e as AxiosError;
            console.error(error);
        }
    }

    // -------- MiniPC Formik --------

    const controlPCFormik = useFormik({
        initialValues: {
            uuid: "",
            key: "",
        },
        onSubmit: async (values) => handleControlPCChange(values),
        validationSchema: Yup.object().shape({
            uuid: Yup.string().required("UUID is required"),
            key: Yup.string().required("Key is required"),
        }),
    });

    async function handleControlPCChange(values: { uuid: string; key: string }) {
        try {
            const res = await axios.put(process.env.REACT_APP_API_URL + "/api/controlPC", values, {
                headers: {
                    Authorization: authHeader,
                },
            });
            if (res.status === 200) {
                alert("controlPC updated successfully");
            }
        } catch (e) {
            const error = e as AxiosError;
            console.error(error);
        }
    }


    useEffect(() => {
        async function fetchUser() {
            const res = await axios.get(
                process.env.REACT_APP_API_URL + "/api/users",
                {
                    headers: {
                        Authorization: authHeader,
                    },
                }
            );
            setUser(res.data);
        }
        fetchUser();
    }, [authHeader]);



    return (
        <div className="w-screen h-screen py-5 flex dir-row max-w-screen bg-backgroundCol overflow-x-hidden">
            <Navbar />
            <div className="ml-80 space-y-10 bg-white w-full h-fit min-h-full rounded-xl mr-5 p-5 flex flex-col">

                {/* Account setting section  */}
                <form onSubmit={addressFormik.handleSubmit}>
                    <h1 className="text-xl font-medium">{t("waterlevel.adjust")}</h1>
                    <h2 className="text-l font-medium opacity-40">
                        {t("waterlevel.current")}
                    </h2>
                    <div className="flex dir-col w-full gap-5 px-5 pt-5">
                        <Input
                            label={t("waterlevel.ideal")}
                            placeholder={user?.address.street || ""}
                            width="1/2"
                            onChange={addressFormik.handleChange}
                            value={addressFormik.values.street}
                            name="street"
                            error={addressFormik.errors.street}
                        />
                    </div>
                    <div className="flex dir-row w-full gap-5 px-5 pt-5">
                        <Input
                            label={t("waterlevel.poleheight")}
                            placeholder={user?.address.houseNumber || ""}
                            width="1/2"
                            onChange={addressFormik.handleChange}
                            value={addressFormik.values.houseNumber}
                            name="houseNumber"
                            error={addressFormik.errors.houseNumber}
                        />
                    </div>

                    <div className="mx-5">
                        <LinkLessButton text={t("Login.adjustbutton")} />
                    </div>
                </form>

            </div>
        </div>
    );
}

export default WaterLevelSettings;
