import React, { useEffect } from "react";
import axios from "axios";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";
import {WaterLevel} from "./waterlevelvisual.state";

function WaterLevelVisual(){

    const minScale = -5.5;
    const maxScale = -0.5;

    const poleLevel = -2.05;
    const idealLevel = -1.85;

    const authHeader = useAuthHeader();
    const [currentLevel, setCurrentLevel] = React.useState<WaterLevel | null>(null);

    useEffect(() => 
    {
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

    //     async function fetchUser() {
    //         const res = await axios.get(
    //             process.env.REACT_APP_API_URL + "/api/users",
    //             {
    //                 headers: {
    //                     Authorization: authHeader,
    //                 },
    //             }
    //         );
    //         setUser(res.data);
    //         setAddress(res.data.address);
    //     }
    //     fetchUser();
    }, [authHeader]);



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
                <div className="flex-1 relative pl-6 flex justify-center items-center">
                    <span className="mt-1 mr-1 h-4 w-4 bg-red-500 rounded-full"></span>
                    <p className="text-white">Je waterpeil is NIET goed!</p>
                </div>

                {/* current level */}
                <div className="flex-1 relative pl-6 flex justify-center items-center">
                    <h1 className="text-red-500 text-[350%] font-semibold">{currentLevel?.level}</h1>
                </div>

                {/* min and max */}
                <div className="flex-1 relative pl-6 flex flex-col items-center">

                    <div className="flex justify-center">
                        <p className="text-white mr-1">Ideal:</p>
                        <p className="text-green-500">{idealLevel}</p>
                    </div>

                    <div className="flex justify-center">
                        <p className="text-white mr-1">Paalkop:</p>
                        <p className="text-gray-400">{poleLevel}</p>
                    </div>

                </div>
            </div>
            <div className="flex flex-col w-[50%] m-5 border-2 relative">

                {/* min and max lines */}
                <div className="absolute left-[50%] transform -translate-x-[50%] bg-gray-400 w-full h-0.5" style={{ bottom: `${poleLevelPerc}%` }}></div>
                <p className="absolute bottom-[77%] left-[-10%] transform -translate-x-[50%] text-gray-400 " style={{ bottom: `${poleLevelPerc - 3}%` }}>{poleLevel}</p>

                <div className="absolute left-[50%] transform -translate-x-[50%] bg-green-500  w-full h-0.5" style={{ bottom: `${idealLevelPerc}%` }}></div>
                <p className="absolute left-[-10%] transform -translate-x-[50%] text-green-500" style={{ bottom: `${idealLevelPerc - 3}%` }}>{idealLevel}</p>

                {/* pillar */}
                <div className="absolute bottom-0 left-[50%] transform -translate-x-[50%] bg-orange-300 w-[15%]" style={{ height: `${poleLevelPerc}%` }}></div>

                {/* current water level box */}
                <div className="flex flex-1"></div>
                <div className="flex border-t-2 border-teal-400 bg-teal-400 bg-opacity-30 z-10" style={{ height: `${waterLevelPerc}%` }}></div>
                <p className="absolute left-[9%] transform -translate-x-[50%] text-red-500 z-20" style={{ bottom: `${waterLevelPerc - 8}%` }}>{currentLevel?.level}</p>

            </div>
        </div>
    );
}

export default WaterLevelVisual;