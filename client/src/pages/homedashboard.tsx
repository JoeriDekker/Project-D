import React, { useEffect, useState, FC } from "react";
import { t } from "i18next";
import Input from "../components/Input/Input"
import Modal from "../components/Modal/Modal";
import Navbar from '../components/navbar/navbar'
import WaterlevelDial from '../components/waterleveldial/waterleveldial'
import Logboek from "../components/logboek/waterpeillogboek";
import WaterLevelVisual from "../components/waterlevelvisual/waterlevelvisual"
import Automatic from "../components/Validation/Automatic";

import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import WaterStorage from "../components/waterstorage/waterstorage";

import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";
import { WaterLevel } from "../components/waterlevelvisual/waterlevelvisual.state";
import { UserResponse } from "./LoginScreen/LoginScreen.state";
import axios from "axios";

interface HomeProps {
    hasWelcomeBeenShown: boolean;
    setWelcomeState: React.Dispatch<React.SetStateAction<boolean>>;
}

const HomeDashboard: FC<HomeProps> = ({ hasWelcomeBeenShown, setWelcomeState }) => {
    /*
    This data is real data from the openweatherAPI.
    TODO: implement that it gets the forecast of today at X time
    */
    const [modalState, setModalState] = useState("hidden");
    const [dialtoggle, setdialtoggle] = useState(false);
    // const [waterlevel, setwaterlevel] = useState(-2.15);

    // const weatherForecast = {
    //     "$id": "36",
    //     "stationid": 6344,
    //     "stationname": "Meetstation Rotterdam",
    //     "lat": 51.95,
    //     "lon": 4.45,
    //     "regio": "Rotterdam",
    //     "timestamp": "2024-05-23T15:00:00",
    //     "weatherdescription": "Zwaar bewolkt",
    //     "iconurl": "https://www.buienradar.nl/resources/images/icons/weather/30x30/c.png",
    //     "fullIconUrl": "https://www.buienradar.nl/resources/images/icons/weather/96x96/C.png",
    //     "graphUrl": "https://www.buienradar.nl/nederland/weerbericht/weergrafieken/c",
    //     "winddirection": "WZW",
    //     "airpressure": 1014.5,
    //     "temperature": 18.3,
    //     "groundtemperature": 20.9,
    //     "feeltemperature": 18.3,
    //     "visibility": 28600.0,
    //     "windgusts": 11.2,
    //     "windspeed": 5.6,
    //     "windspeedBft": 4,
    //     "humidity": 67.0,
    //     "precipitation": 0.0, // neerslag
    //     "sunpower": 615.0,
    //     "rainFallLast24Hour": 0.0,
    //     "rainFallLastHour": 0.0,
    //     "winddirectiondegrees": 255
    // }


    const authHeader = useAuthHeader();
    const [user, setUser] = React.useState<UserResponse | null>(null);
    // const [currentImportLevel, setCurrentImportLevel] = React.useState<WaterLevel | null>(null);
    // console.log(currentImportLevel)
    const [currentLevel, setCurrentLevel] = useState(0);
    const [waterlevelDial, setWaterlevelDial] = useState(currentLevel);

    const poleLevel = user?.waterLevelSettings.poleHeight ?? 0;
    const idealLevel = user?.waterLevelSettings.idealHeight ?? 0;

    const minScale = poleLevel - 4.5;
    const maxScale = poleLevel + 1;

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

        async function fetchCurrentLevel() {
            try {
                const res = await axios.get(
                    process.env.REACT_APP_API_URL + "/api/groundwaterlog",
                    {
                        headers: {
                            Authorization: authHeader,
                        },
                    }
                );

                if (res.data.length > 0) {
                    setCurrentLevel(res.data[0].level);
                } else {
                    console.log("No data available.");
                }
            } catch (error) {
                console.error("Error fetching current level:", error);
            }
        }
        fetchCurrentLevel();

        if (!hasWelcomeBeenShown) {
            setWelcomeState(true);
            setModalState("")
        }
        defineNotifcation();
    }, [authHeader]);

    function defineNotifcation() {
        // TODO: de amth.random vervangen door de API call bijvoorbeeld: /api/checkWaterStand
        if (hasWelcomeBeenShown) {
            return;
        }

        if (currentLevel < idealLevel) {

            return toast.error("Je waterpeil is laag!", {
                position: "top-center",
                autoClose: false,
                hideProgressBar: false,
                closeOnClick: false,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                theme: "colored",
            })
        }

        return toast.success("Welkom terug!", {
            position: "top-center",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "colored",
        });
    }

    return (
        <div className="bg-secondaryCol w-screen h-screen py-5 flex">
            <Navbar />
            <ToastContainer
                position="top-center"
                autoClose={5000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme="light"
            />
            {/* grid setup */}
            <div className="ml-80 bg-white w-full h-full rounded-xl mr-5 pb-4">
                <div className="grid grid-cols-2 grid-rows-2 h-[100%] m-2">

                    {/* water level visual */}
                    <div className="bg-gray-800 m-2 p-4 rounded-xl">
                        {/* <WaterLevelVisual currentLevel={currentLevel} idealLevel={idealLevel} poleLevel={poleLevel} maxScale={maxScale} minScale={minScale} /> */}
                        <WaterLevelVisual currentLevel={currentLevel} idealLevel={idealLevel} poleLevel={poleLevel} maxScale={maxScale} minScale={minScale} />
                    </div>

                    <div className="bg-gray-100 m-2 p-4 rounded-xl">
                        <Logboek />
                    </div>

                    <div className="bg-gray-100 m-2 p-4 rounded-xl">
                        <WaterStorage />
                    </div>

                    <div className="flex flex-col justify-between p-8 items-center bg-gray-100 m-2 p-4 rounded-xl">
                        {dialtoggle ? <Automatic /> : <WaterlevelDial water={currentLevel} waterdialstate={setWaterlevelDial} waterdialvalue={waterlevelDial} currentwaterstate={setCurrentLevel} />}

                        <label>
                            <input
                                type="checkbox"
                                defaultChecked={dialtoggle}
                                onChange={() => setdialtoggle((prevState) => !prevState)}
                            />
                            &nbsp;
                            {t("Water.toggle")}
                        </label>
                    </div>
                </div>
                <Modal
                    header="Oh nee! Je waterpeil is laag🤯"
                    text="Als u geen paalrot wil hebben moet u uw waterpeil verhogen door op de knop op de home pagina te drukken. Als u dit niet doet, ligt de verantwoordelijkheid bij u."
                    buttonText="Begrepen!"
                    hiddenState={modalState}
                />
            </div>
        </div>
    );
}

export default HomeDashboard;
