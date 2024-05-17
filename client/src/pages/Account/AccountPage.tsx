import React, { useEffect } from "react";
import Navbar from "../../components/navbar/navbar";
import * as Yup from "yup"; // Import Yup package
import Input from "../../components/Input/Input";
import { t } from "i18next";
import { AnyButton2, LinkLessButton } from "../../components/Button/AnyButton";
import { UserResponse } from "./AccountPage.state";
import axios, { AxiosError } from "axios";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";
import { useFormik } from "formik";

function AnyPage() {
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
                    <h1 className="text-xl font-medium">{t("Login.adjust")}</h1>
                    <h2 className="text-l font-medium opacity-40">
                        {t("Login.current")}
                    </h2>
                    <div className="flex dir-row w-full gap-5 px-5 pt-5">
                        <Input
                            label={t("Login.street")}
                            placeholder={user?.address.street || ""}
                            onChange={addressFormik.handleChange}
                            value={addressFormik.values.street}
                            name="street"
                            error={addressFormik.errors.street}
                        />

                        <Input
                            label={t("Login.housenum")}
                            placeholder={user?.address.houseNumber || ""}
                            width="1/2"
                            onChange={addressFormik.handleChange}
                            value={addressFormik.values.houseNumber}
                            name="houseNumber"
                            error={addressFormik.errors.houseNumber}
                        />
                    </div>
                    <div className="flex dir-row w-full gap-5 px-5 pt-5">
                        <Input
                            label={t("Login.zipcode")}
                            placeholder={user?.address.zip || ""}
                            width="1/2"
                            onChange={addressFormik.handleChange}
                            value={addressFormik.values.zip}
                            error={addressFormik.errors.zip}
                            name="zip"
                        />
                        <Input
                            label={t("Login.place")}
                            placeholder={user?.address.city || ""}
                            width="full"
                            onChange={addressFormik.handleChange}
                            value={addressFormik.values.city}
                            name="city"
                            error={addressFormik.errors.city}
                        />
                    </div>
                    {/* TODO: Link this do an actual forget page */}
                    <Input
                        label={t("Login.password")}
                        placeholder="***************"
                        width="fit px-5 pt-5"
                        onChange={addressFormik.handleChange}
                        value={addressFormik.values.password}
                        name="password"
                        error={addressFormik.errors.password}
                    />
                    <a href="/wwforgor" className="px-5 underline text-secondaryCol">
                        {t("Login.adjustpass")}
                    </a>

                    <div className="mx-5">
                        <LinkLessButton text={t("Login.adjustbutton")} />
                    </div>
                </form>

                {/* Pc connection section */}
                <form onSubmit={controlPCFormik.handleSubmit}>
                    <h1 className="text-xl font-medium">{t("PC.connection")}</h1>
                    <h2 className="text-l font-medium opacity-40">{t("PC.foundin")}</h2>
                    <div className="space-y-5 px-5 pt-5">
                        <div>
                            <h3 className="text-m font-medium">{t("PC.uuid")}</h3>
                            <p className="opacity-40">{t("PC.uuidexplain")}</p>
                            <Input
                                label="UUID"
                                needed
                                neededText={t("PC.foundon")}
                                placeholder="b678ef40-524f-4890-a40b-efae6e85113e"
                                width="auto max-w-xs"
                                onChange={controlPCFormik.handleChange}
                                value={controlPCFormik.values.uuid}
                                name="password"
                                error={controlPCFormik.errors.uuid}
                            />
                        </div>
                        <div>
                            <h3 className="text-m font-medium">{t("PC.security")}</h3>
                            <p className="opacity-40">{t("PC.securityexplain")}</p>
                            <div className="">
                                <Input
                                    label="Key"
                                    needed
                                    neededText={t("PC.keyon")}
                                    placeholder="***************"
                                    width="auto max-w-xs"
                                    onChange={controlPCFormik.handleChange}
                                    value={controlPCFormik.values.key}
                                    name="password"
                                    error={controlPCFormik.errors.key}
                                />
                            </div>
                        </div>
                        <div>
                            <div className="flex dir-row gap-2 items-center">
                                <h3 className="text-m font-medium">{t("PC.status")}</h3>
                                {connected ? (
                                    <div className="bg-green-500 w-3 h-3 rounded-xl"></div>
                                ) : (
                                    <div className="bg-red-500 w-3 h-3 rounded-xl"></div>
                                )}
                            </div>
                            <p className="opacity-40">{t("PC.statusexplain")}</p>
                        </div>
                        <LinkLessButton text={t("PC.reconnect")} />
                    </div>
                </form>
            </div>
        </div>
    );
}

export default AnyPage;
