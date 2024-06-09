import React from "react";
import axios, { AxiosError } from "axios";

import Navbar from '../../components/navbar/navbar'
import Logboek from '../../components/logboek/logboek'
import WaterpeilLogboek from "../../components/logboek/waterpeillogboek";
import ActionLogboek from "../../components/logboek/actionlogboek";
import WaterStorage from "../../components/waterstorage/waterstorage";

function LogboekScreen() {

    return (
        <>
            <div className="bg-backgroundCol w-screen h-screen py-5 flex dir-row">
                <Navbar />
                <div className="ml-80 bg-white w-full h-full rounded-xl mr-5">
                    <div className="container mx-auto ">
                        <h1 className="text-3xl font-bold flex justify-center ">
                            Logboek
                        </h1>
                        <br />
                        <div className="grid grid-cols-2 grid-rows-2 gap-10  px-5 pt-5">
                            <WaterpeilLogboek />
                            <Logboek />
                            <ActionLogboek />
                            <WaterStorage />
                        </div>
                    </div>
                </div>
            </div>


        </>
    );
}

export default LogboekScreen;