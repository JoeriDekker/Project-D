import React from "react";

import Navbar from '../components/navbar/navbar'

function AnyPage() {
    return (
        <div className="bg-background-color w-screen h-screen py-5 flex dir-row">
            <Navbar />
            <div className="bg-white w-full h-full rounded-xl mr-5"></div>
        </div>
    );
}

export default AnyPage;