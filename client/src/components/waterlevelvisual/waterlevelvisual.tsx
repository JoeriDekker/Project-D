import React, { FC, useEffect } from "react";
import axios from "axios";
import { t } from "i18next";

import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";
import {WaterLevel} from "./waterlevelvisual.state";
import { UserResponse } from "./../../pages/LoginScreen/LoginScreen.state";

interface WaterLevelProps {
    currentLevel: WaterLevel | null;
    setCurrentLevel: React.Dispatch<React.SetStateAction<WaterLevel | null>>;
}

const WaterLevelVisual: FC<WaterLevelProps> = ({ currentLevel, setCurrentLevel }) => {
    const authHeader = useAuthHeader();
    const [user, setUser] = React.useState<UserResponse | null>(null);
    const [statusColor, setStatusColor] = React.useState<string | null>(null);
    const [statusText, setStatusText] = React.useState<string | null>(null);
    const [isCurrentLevelFetched, setCurrentLevelFetched] = React.useState<boolean>(false);

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

      const poleLevel = user?.waterLevelSettings.poleHeight ?? 0;
      const idealLevel = user?.waterLevelSettings.idealHeight ?? 0;

      const minScale = poleLevel - 4.5;
      const maxScale = poleLevel + 1;

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
        function setStatus()
        {
            if (typeof currentLevel?.level === 'number')
            {
                if (currentLevel.level >= idealLevel)
                {
                    setStatusColor("#22c55e");
                    setStatusText(t("waterlevel.goodstatus"));
                }
                else
                {
                    setStatusColor("#ef4444");
                    setStatusText(t("waterlevel.badstatus"));
                }
                setCurrentLevelFetched(true);
            }
            else
            {
                if (isCurrentLevelFetched)
                    console.error("Error setting water level status.");
            }
        }
        setStatus();
    }, [currentLevel, idealLevel]);

    function calculatePercentage(value : any, min : number, max : number)
    {
        return (typeof value === 'number' && !isNaN(value)) ? ((value - min) / (max - min)) * 100 : 0;
    }

    const waterLevelPerc = calculatePercentage(currentLevel?.level, minScale, maxScale);
    const poleLevelPerc = calculatePercentage(poleLevel, minScale, maxScale);
    const idealLevelPerc = calculatePercentage(idealLevel, minScale, maxScale);

    return (
        <div className="flex flex-row h-[100%]">
            <div className="flex w-[50%] flex-col pr-8">

                {/* status header */}
                <div className="flex-1 pl-6 flex justify-center items-center">
                    <span className="mt-1 mr-1 h-4 w-4 rounded-full" style={{ backgroundColor: `${statusColor}` }}></span>
                    <p className="text-white">{statusText}</p>
                </div>


                {/* current level */}
                <div className="flex-col pl-6 pb-6 flex justify-center items-center">
                    <h1 className="text-[350%] font-semibold" style={{ color: `${statusColor}` }}>{currentLevel?.level}</h1>
                    <h1 className="text-white text-[350%] font-semibold mt-[-10%]">NAP</h1>
                </div>

                {/* min and max */}
                <div className="flex-1 pl-6 flex flex-col items-center">

                    <div className="flex justify-center">
                        <p className="text-white mr-1">{t("waterlevel.ideal")}</p>
                        <p className="text-green-500">{idealLevel}</p>
                    </div>

                    <div className="flex justify-center">
                        <p className="text-white mr-1">{t("waterlevel.poleheight")}</p>
                        <p className="text-gray-400">{poleLevel}</p>
                    </div>

                </div>
            </div>
            <div className="flex flex-col w-[50%] m-5 border-2 relative">

                {/* min and max lines */}
                {/* <div className="absolute left-[50%] transform -translate-x-[50%] bg-gray-400 w-full h-0.5" style={{ bottom: `${poleLevelPerc}%` }}></div> */}
                {/* <p className="absolute bottom-[77%] left-[-10%] transform -translate-x-[50%] text-gray-400 " style={{ bottom: `${poleLevelPerc - 3}%` }}>{poleLevel}</p> */}

                <div className="absolute left-[50%] transform -translate-x-[50%] bg-green-500  w-full h-0.5" style={{ bottom: `${idealLevelPerc}%` }}></div>
                {/* <p className="absolute left-[-10%] transform -translate-x-[50%] text-green-500" style={{ bottom: `${idealLevelPerc - 3}%` }}>{idealLevel}</p> */}

                {/* pillar */}
                <div className="absolute bottom-0 left-[50%] transform -translate-x-[50%] bg-orange-300 w-[15%]" style={{ height: `${poleLevelPerc}%` }}></div>

                {/* current water level box */}
                <div className="flex flex-1"></div>
                <div className="flex border-t-2 border-teal-400 bg-teal-400 bg-opacity-30 z-10" style={{ height: `${waterLevelPerc}%` }}></div>
                {/* <p className="text-white absolute left-[9%] transform -translate-x-[50%] z-20 bottom-[2%]">{currentLevel?.level}</p> */}

            </div>
        </div>
    );
}

export default WaterLevelVisual;
