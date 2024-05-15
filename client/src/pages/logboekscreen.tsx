import React from "react";

function LogboekScreen() {
    return (
    <>
        <div className="container mx-auto ">
            <h1 className="text-3xl font-bold flex justify-center ">
                Logboek
            </h1>
            <div className="grid grid-cols-2 gap-2">
                <div className="flex justify-center bg-amber-200">
                    Div 1
                </div>
                <div className="flex justify-center">
                    <div className="table-auto">
                        <div className="grid grid-cols-4 gap-2 mt-2 shadow-md">
                            <div><div className="py-1 px-2 mr-20 rounded-md bg-slate-100"><b>08/12</b></div></div>
                            <div><div className="mr-6">Peil: <text className="text-red-600">-0.12m</text></div></div>
                            <div><div className="mr-6">Min: -0.5m</div></div>
                            <div><div className="mr-6">Max: +0.12m</div></div>
                        </div>
                        <div className="grid grid-cols-4 gap-2 mt-2 shadow-md">
                            <div><div className="py-1 px-2 mr-20 rounded-md bg-slate-100"><b>10/12</b></div></div>
                            <div><div className="mr-6">Peil: <text className="text-red-600">-0.12m</text></div></div>
                            <div><div className="mr-6">Min: -0.5m</div></div>
                            <div><div className="mr-6">Max: +0.12m</div></div>
                        </div>
                        <div className="grid grid-cols-4 gap-2 mt-2 shadow-md">
                            <div><div className="py-1 px-2 mr-20 rounded-md bg-slate-100"><b>12/12</b></div></div>
                            <div><div className="mr-6">Peil: <text className="text-red-600">-0.12m</text></div></div>
                            <div><div className="mr-6">Min: -0.5m</div></div>
                            <div><div className="mr-6">Max: +0.12m</div></div>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>
    </>
    );
}

export default LogboekScreen;