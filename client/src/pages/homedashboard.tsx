import React from "react";

import Navbar from '../components/navbar/navbar'
import Logboek from "../components/logboek/waterpeillogboek";

function HomeDashboard() {
    return (
        <div className="bg-secondaryCol w-screen h-screen py-5 flex dir-row">
            <Navbar />

            {/* grid setup */}
            <div className="bg-white w-full h-full rounded-xl mr-5 pb-4">
                <div className="grid grid-cols-2 h-[100%] m-2">

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
                                        <p className="text-red-500">-2.05</p>
                                    </div>

                                    <div className="flex justify-center">
                                        <p className="text-white mr-1">Ideal:</p>
                                        <p className="text-green-500">-1.85</p>
                                    </div>
                                </div>
                            </div>
                            <div className="flex flex-col w-[50%] m-5 border-2 relative">

                                {/* min and max lines */}
                                <div className="absolute bottom-[80%] left-[50%] transform -translate-x-[50%] bg-green-500 w-full h-0.5"></div>
                                <div className="absolute bottom-[70%] left-[50%] transform -translate-x-[50%] bg-red-500 w-full h-0.5"></div>

                                {/* pillar */}
                                <div className="absolute bottom-0 left-[50%] transform -translate-x-[50%] bg-orange-300 w-[15%] h-[70%]"></div>

                                {/* current water level box */}
                                <div className="flex flex-1"></div>
                                <div className="flex h-[65%] border-t-2 border-teal-400 bg-teal-400 bg-opacity-30 z-10"></div>
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
            </div>
        </div>
    );
}

export default HomeDashboard;