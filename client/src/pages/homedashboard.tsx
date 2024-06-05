import React, { useEffect, useState } from "react";
import { t } from "i18next";
import Input from "../components/Input/Input"

import Navbar from '../components/navbar/navbar'
import WaterlevelDial from '../components/waterleveldial/waterleveldial'
import Logboek from "../components/logboek/waterpeillogboek";
import WaterLevelVisual from "../components/waterlevelvisual/waterlevelvisual"
import Automatic from "../components/Validation/Automatic";

import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function HomeDashboard() {
    const [dialtoggle, setdialtoggle] = useState(false);
    const [waterlevel, setwaterlevel] = useState(-1.15);
    const waterlevel_perc = 65;

    const paalkop = -2.05;
    const paalkop_perc = 70;

    const ideal = -1.85;
    const ideal_perc = 80;

    function defineNotifcation() {
        // TODO: de amth.random vervangen door de API call bijvoorbeeld: /api/checkWaterStand

        if (waterlevel < ideal) {
            return toast.error("Let op! Je waterpeil is gevaarlijk laag!", {
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
                        <WaterLevelVisual />
                    </div>

                    <div className="bg-gray-100 m-2 p-4 rounded-xl">
                        <Logboek />
                    </div>

                    <div className="bg-gray-100 m-2 p-4 rounded-xl">

                    </div>

                    <div className="flex flex-col justify-between p-8 items-center bg-gray-100 m-2 p-4 rounded-xl">
                        {dialtoggle ? <Automatic /> : <WaterlevelDial water={waterlevel} setwaterlevel={setwaterlevel} />}

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
                </div >
            </div >
        </div >
    );
}

export default HomeDashboard;
