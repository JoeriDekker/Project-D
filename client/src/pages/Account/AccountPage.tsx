import React from "react";
import Navbar from "../../components/navbar/navbar";

import LoginAdjust from "../../components/Login/LoginAdjust";

function AnyPage() {
    return (
        <div className="bg-backgroundCol w-screen h-screen py-5 flex dir-row">
            <Navbar />
            <div className="bg-white w-full h-full rounded-xl mr-5 p-5">

                <LoginAdjust />

            </div>
        </div>
    );
}

export default AnyPage;