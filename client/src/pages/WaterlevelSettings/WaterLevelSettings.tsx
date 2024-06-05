import React, { useEffect } from "react";
import Navbar from "../../components/navbar/navbar";
import * as Yup from "yup";
import { t } from "i18next";
import { LinkLessButton } from "../../components/Button/AnyButton";
import axios, { AxiosError } from "axios";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";
import { useFormik } from "formik";
import { UserResponse } from "../LoginScreen/LoginScreen.state";
import Input from "../../components/Input/Input";

function WaterLevelSettings() {
    const [user, setUser] = React.useState<UserResponse | null>(null);
    const authHeader = useAuthHeader();

    // -------- waterlevel Formik --------

    // TODO language errors
    const waterLevelValidationScheme = Yup.object().shape({
        poleheight: Yup.number()
            .min(-5, "Pole height cannot be less than -5")
            .max(5, "Pole height cannot be greater than 5"),
        idealheight: Yup.number()
            .min(-5, "Ideal height cannot be less than -5")
            .max(5, "Ideal height cannot be greater than 5")
    });
    const waterlevelFormik = useFormik({
        initialValues: {
            poleheight: "",
            idealheight: "",
        },
        onSubmit: async (values) => handleWaterLevelSettingsChange(values),
        validationSchema: waterLevelValidationScheme,
    });

    async function handleWaterLevelSettingsChange(values: {
        poleheight: number | string;
        idealheight: number | string;
    }) {
        try {
            if (values.poleheight == "")
            {
                values.poleheight = "0"
            }
            if (values.idealheight == "")
            {
                values.idealheight = "0"
            }
            const res = await axios.put(process.env.REACT_APP_API_URL + "/api/waterlevelsettings", {
                poleheight: values.poleheight,
                idealheight: values.idealheight
            }, {
                headers: {
                    Authorization: authHeader,
                },
            });
            if (res.status === 200) {
                alert("Settings updated successfully");
            }
        } catch (e) {
            const error = e as AxiosError;
            console.error(error);
        }
        values.idealheight = ""
        values.poleheight = ""

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
                
                {/* water level settings section */}
                <form onSubmit={waterlevelFormik.handleSubmit}>
                    <h1 className="text-xl font-medium">{t("waterlevel.adjust")}</h1>
                    <h2 className="text-l font-medium opacity-40">
                        {t("waterlevel.current")}
                    </h2>
                    <div className="flex dir-row w-full gap-5 px-5 pt-5">
                    <Input
                        label={t("waterlevel.poleheight")}
                        placeholder={"" + user?.waterLevelSettings.poleHeight}
                        width="1/3"
                        onChange={waterlevelFormik.handleChange}
                        value={waterlevelFormik.values.poleheight}
                        name="poleheight"
                        error={waterlevelFormik.errors.poleheight}
                    />
                    </div>

                    <div className="flex dir-col w-full gap-5 px-5 pt-5">
                        <Input
                            label={t("waterlevel.ideal")}
                            placeholder={user?.waterLevelSettings?.idealHeight.toString() || ""}
                            width="1/3"
                            onChange={waterlevelFormik.handleChange}
                            value={waterlevelFormik.values.idealheight}
                            name="idealheight"
                            error={waterlevelFormik.errors.idealheight}
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
