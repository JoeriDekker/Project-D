import React from "react";
import axios, { AxiosError } from "axios";

import Navbar from '../../components/navbar/navbar'
import Logboek from '../../components/logboek/logboek'
import WaterpeilLogboek from "../../components/logboek/waterpeillogboek";

function LogboekScreen() {
      
    return (
    <>
        <div className="bg-background-color w-screen h-screen py-5 flex dir-row">
            <Navbar />
            <div className="bg-white w-full h-full rounded-xl mr-5">
                <div className="container mx-auto ">
                    <h1 className="text-3xl font-bold flex justify-center ">
                        Logboek
                    </h1>
                    <br />
                    <div className="grid grid-cols-2 gap-10">
                        <WaterpeilLogboek />
                        <Logboek />
                    </div>
                </div> 
            </div>
        </div>
        
        
    </>
    );
}

export default LogboekScreen;