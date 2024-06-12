import React, { useEffect, useState, FC } from "react";
import axios from "axios";

import Modal from "../components/Modal/Modal";
import Navbar from '../components/navbar/navbar'
import Logboek from "../components/logboek/waterpeillogboek";
import WaterLevelVisual from "../components/waterlevelvisual/waterlevelvisual"
import { WaterLevel } from "../components/waterlevelvisual/waterlevelvisual.state";

import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import WaterStorage from "../components/waterstorage/waterstorage";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";
import { UserResponse } from "./LoginScreen/LoginScreen.state";


interface HomeProps {
    hasWelcomeBeenShown: boolean;
    setWelcomeState: React.Dispatch<React.SetStateAction<boolean>>;
}


const HomeDashboard: FC<HomeProps> = ({ hasWelcomeBeenShown, setWelcomeState }) => {
    const [currentLevel, setCurrentLevel] = useState<WaterLevel | null>(null);
    const authHeader = useAuthHeader();
    const [user, setUser] = React.useState<UserResponse | null>(null);

    /*
    This data is real data from the openweatherAPI.
    TODO: implement that it gets the forecast of today at X time
    */
    const [modalState, setModalState] = useState("hidden");


    // check if the value is not null otherwise show the level
    const waterlevel = currentLevel ? parseFloat(currentLevel.level) : null;
    const ideal = user?.waterLevelSettings.idealHeight;



    function defineNotifcation() {
        if (!currentLevel) {
            return toast.error("Er is iets mis gegaan, probeer het later opnieuw", {
                position: "top-center",
                autoClose: false,
                hideProgressBar: false,
                closeOnClick: false,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                theme: "colored",
            });
        }

        if (!waterlevel || !ideal || waterlevel < ideal) {
            return toast.error("Je waterpeil is laag!", {
                position: "top-center",
                autoClose: false,
                hideProgressBar: false,
                closeOnClick: false,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                theme: "colored",
            });
        }

        setModalState("hidden");

        return toast.success("Welkom terug!", {
            position: "top-center",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: false,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "colored",
        });
    }

    useEffect(() => {
        async function fetchUser() {
            try {
                const res = await axios.get(process.env.REACT_APP_API_URL + "/api/users", {
                    headers: { Authorization: authHeader },
                });
                setUser(res.data);
            } catch (error) {
                console.error("Error fetching user data", error);
            }
        }

        fetchUser();
    }, [authHeader]);

    useEffect(() => {
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
                    setCurrentLevel(res.data[0]);
                } else {
                    console.log("No data available.");
                }
            } catch (error) {
                console.error("Error fetching current level:", error);
            }
        }
        fetchCurrentLevel();
    }, [authHeader]);

    useEffect(() => {
        if (!user || !currentLevel) {
            return;
        }

        const waterlevel = currentLevel ? parseFloat(currentLevel.level) : null;
        const ideal = user?.waterLevelSettings.idealHeight;



        if (!hasWelcomeBeenShown) {
            defineNotifcation();
            setWelcomeState(true);
        }

        if(!waterlevel || !ideal || waterlevel < ideal) {
            setModalState("");
        }

    }, [user, currentLevel]);

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
