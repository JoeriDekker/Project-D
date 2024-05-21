import React from "react";

import Navbar from '../components/navbar/navbar'
import WaterlevelDial from '../components/waterleveldial/waterleveldial'

function HomeDashboard() {
    return (
        <div className="bg-background-color w-screen h-screen py-5 flex dir-row">
            <Navbar />
            <div className="bg-white w-full h-full rounded-xl mr-5 pb-4">
                <div className="grid grid-cols-2 h-[100%] m-2">
                    <div className="bg-gray-800 m-2 p-4 rounded-xl">
                        <div className="flex flex-row h-[100%]">
                            <div className="flex w-[50%] flex-col pr-8">
                                <div className="flex-1 relative pl-6 flex justify-center items-center">
                                    <span className="mt-1 mr-1 h-4 w-4 bg-red-500 rounded-full"></span>
                                    <p className="text-white">Je waterpeil is NIET goed!</p>
                                </div>

                                <div className="flex-1 relative pl-6 flex justify-center items-center">
                                    <h1 className="text-red-500 text-[340%]">-0.10</h1>
                                </div>

                                <div className="flex-1 relative pl-6 flex flex-col items-center">
                                    <div className="flex justify-center">
                                        <p className="text-white mr-1">Min:</p>
                                        <p className="text-red-500">-0.40</p>
                                    </div>

                                    <div className="flex justify-center">
                                        <p className="text-white mr-1">Max:</p>
                                        <p className="text-green-500">+0.20</p>
                                    </div>
                                </div>
                            </div>
                            <div className="flex flex-col w-[50%] m-5 border-2">
                                <div className="flex flex-1">
                                </div>
                                <div className="flex h-[50%] border-t-2 border-teal-400 bg-teal-400 bg-opacity-30">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="bg-gray-100 m-2 p-4 rounded-xl">Item 2</div>
                    <div className="bg-gray-100 m-2 p-4 rounded-xl">Item 3</div>
                    <div className="bg-gray-100 m-2 p-4 rounded-xl">
                        <WaterlevelDial />
                    </div>
                </div>
            </div>
        </div>
    );
}

export default HomeDashboard;