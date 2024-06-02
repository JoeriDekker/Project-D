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
    const [waterLevelSettings, setWaterLevelSettings] = React.useState<UserResponse["waterlevelsettings"] | null>(null);
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
      console.log("User data:", res.data); // Log the user data received from the API
      setUser(res.data);
      setWaterLevelSettings(res.data.waterlevelsettings);
      console.log("Water level settings:", res.data.waterLevelSettings);
    }
    fetchUser();
  }, [authHeader]);

    async function handleWaterLevelSettingsChange(values: {
        poleheight: string;
        idealheight: string;
    }) {
        try {
            const res = await axios.put(process.env.REACT_APP_API_URL + "/api/waterlevelsettings", values, {
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
    }

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
                        width="1/3"
                        onChange={waterlevelFormik.handleChange}
                        value={waterlevelFormik.values.poleheight}
                        name="houseNumber"
                        error={waterlevelFormik.errors.poleheight}
                    />
                    </div>

                    <div className="flex dir-col w-full gap-5 px-5 pt-5">
                        <Input
                            label={t("waterlevel.ideal")}
                            placeholder={`${user?.waterlevelsettings?.idealHeight || ""}`}
                            width="1/3"
                            onChange={waterlevelFormik.handleChange}
                            value={waterlevelFormik.values.idealheight}
                            name="street"
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
