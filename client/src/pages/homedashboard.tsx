import React, { useEffect, useState, FC } from "react";

import Modal from "../components/Modal/Modal";
import Navbar from '../components/navbar/navbar'
import Logboek from "../components/logboek/waterpeillogboek";

import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

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

    const waterlevel = -2.15;
    const waterlevel_perc = 65;

    const paalkop = -2.05;
    const paalkop_perc = 80;

    const ideal = -1.85;
    const ideal_perc = 70;

    function defineNotifcation() {
        // TODO: de amth.random vervangen door de API call bijvoorbeeld: /api/checkWaterStand
        if (hasWelcomeBeenShown) {
            return;
        }

        if (waterlevel < ideal) {

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
            <div className="bg-white w-full h-full rounded-xl mr-5 pb-4">
                <div className="grid grid-cols-2 grid-rows-2 h-[100%] m-2">

                    {/* water level visual */}
                    <div className="bg-gray-800 m-2 p-4 rounded-xl">
                        <div className="flex flex-row h-[100%]">
                            <div className="flex w-[50%] flex-col pr-8">

                                {/* status header */}
                                <div className="flex-1 relative pl-6 flex justify-center items-center">
                                    <span className="mt-1 mr-1 h-4 w-4 bg-red-500 rounded-full"></span>
                                    <p className="text-white">Je waterpeil is NIET goed!</p>
                                </div>

                                {/* current level */}
                                <div className="flex-1 relative pl-6 flex justify-center items-center">
                                    <h1 className="text-red-500 text-[350%] font-semibold">-2.15</h1>
                                </div>

                                {/* min and max */}
                                <div className="flex-1 relative pl-6 flex flex-col items-center">
                                    <div className="flex justify-center">
                                        <p className="text-white mr-1">Paalkop:</p>
                                        <p className="text-gray-400">{paalkop}</p>
                                    </div>

                                    <div className="flex justify-center">
                                        <p className="text-white mr-1">Ideal:</p>
                                        <p className="text-green-500">{ideal}</p>
                                    </div>
                                </div>
                            </div>
                            <div className="flex flex-col w-[50%] m-5 border-2 relative">

                                {/* min and max lines */}
                                <div className="absolute left-[50%] transform -translate-x-[50%] bg-green-500 w-full h-0.5" style={{ bottom: `${paalkop_perc}%` }}></div>
                                <p className="absolute bottom-[77%] left-[-10%] transform -translate-x-[50%] text-green-500" style={{ bottom: `${paalkop_perc - 3}%` }}>{paalkop}</p>

                                <div className="absolute left-[50%] transform -translate-x-[50%] bg-gray-400 w-full h-0.5" style={{ bottom: `${ideal_perc}%` }}></div>
                                <p className="absolute left-[-10%] transform -translate-x-[50%] text-gray-400" style={{ bottom: `${ideal_perc - 3}%` }}>{ideal}</p>

                                {/* pillar */}
                                <div className="absolute bottom-0 left-[50%] transform -translate-x-[50%] bg-orange-300 w-[15%] h-[70%]"></div>

                                {/* current water level box */}
                                <div className="flex flex-1"></div>
                                <div className="flex border-t-2 border-teal-400 bg-teal-400 bg-opacity-30 z-10" style={{ height: `${waterlevel_perc}%` }}></div>
                                <p className="absolute left-[9%] transform -translate-x-[50%] text-red-500 z-20" style={{ bottom: `${waterlevel_perc - 8}%` }}>{waterlevel}</p>

                            </div>
                        </div>
                    </div>

                    <div className="bg-gray-100 m-2 p-4 rounded-xl">
                        <Logboek />
                    </div>

                    <div className="bg-gray-100 m-2 p-4 rounded-xl">

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
