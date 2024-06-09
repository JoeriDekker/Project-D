import React, { useEffect, useState, FC } from "react";

import Modal from "../components/Modal/Modal";
import Navbar from '../components/navbar/navbar'
import Logboek from "../components/logboek/waterpeillogboek";
import WaterLevelVisual from "../components/waterlevelvisual/waterlevelvisual"
import { WaterLevel } from "../components/waterlevelvisual/waterlevelvisual.state";

import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import WaterStorage from "../components/waterstorage/waterstorage";

interface HomeProps {
    hasWelcomeBeenShown: boolean;
    setWelcomeState: React.Dispatch<React.SetStateAction<boolean>>;
}


const HomeDashboard: FC<HomeProps> = ({ hasWelcomeBeenShown, setWelcomeState }) => {
    const [currentLevel, setCurrentLevel] = useState<WaterLevel | null>(null);

    /*
    This data is real data from the openweatherAPI.
    TODO: implement that it gets the forecast of today at X time
    */
    const [modalState, setModalState] = useState("hidden");

    const weatherForecast = {
        "$id": "36",
        "stationid": 6344,
        "stationname": "Meetstation Rotterdam",
        "lat": 51.95,
        "lon": 4.45,
        "regio": "Rotterdam",
        "timestamp": "2024-05-23T15:00:00",
        "weatherdescription": "Zwaar bewolkt",
        "iconurl": "https://www.buienradar.nl/resources/images/icons/weather/30x30/c.png",
        "fullIconUrl": "https://www.buienradar.nl/resources/images/icons/weather/96x96/C.png",
        "graphUrl": "https://www.buienradar.nl/nederland/weerbericht/weergrafieken/c",
        "winddirection": "WZW",
        "airpressure": 1014.5,
        "temperature": 18.3,
        "groundtemperature": 20.9,
        "feeltemperature": 18.3,
        "visibility": 28600.0,
        "windgusts": 11.2,
        "windspeed": 5.6,
        "windspeedBft": 4,
        "humidity": 67.0,
        "precipitation": 0.0, // neerslag
        "sunpower": 615.0,
        "rainFallLast24Hour": 0.0,
        "rainFallLastHour": 0.0,
        "winddirectiondegrees": 255
    }

    const waterlevel = (currentLevel?.level) ? parseFloat(currentLevel.level) : null; // check if the value is not null otherwise show the level
    const waterlevel_perc = 65;

    const paalkop = -2.05;
    const paalkop_perc = 70;

    const ideal = -1.85;
    const ideal_perc = 80;

    function defineNotifcation() {
        // TODO: de amth.random vervangen door de API call bijvoorbeeld: /api/checkWaterStand
        if (hasWelcomeBeenShown) {
            return;
        }

        if(!currentLevel) {
            return toast.error("Er is iets mis gegaan, probeer het later opnieuw", {
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

        if (!waterlevel || waterlevel < ideal) {

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

    useEffect(() => {
        if (!hasWelcomeBeenShown) {
            setWelcomeState(true);
            setModalState("")
        }
        defineNotifcation();
    })

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
                        <WaterLevelVisual currentLevel={currentLevel} setCurrentLevel={setCurrentLevel}/>
                    </div>

                    <div className="bg-gray-100 m-2 p-4 rounded-xl">
                        <Logboek />
                    </div>

                    <div className="bg-gray-100 m-2 p-4 rounded-xl">
                        <WaterStorage />
                    </div>

                    <div className="bg-gray-100 m-2 p-4 rounded-xl">

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
