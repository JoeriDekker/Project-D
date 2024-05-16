import React from "react";

import { WaterpeilLogboekState } from "./waterpeillogboek.state";

function WaterpeilLogboek(){

    const WaterpeilLogboekEntry = ({ date, peil, street}: WaterpeilLogboekState) => {
        let peilcolor: string = 'text-green-600';
        if (peil.includes('-')) {
            peilcolor = 'text-red-600';
        }

        return (
            <div className="grid grid-cols-3 gap-2 mt-2 shadow-md text-center">
                <div className="py-1 px-2 mr-20 rounded-md bg-slate-100"><b>{date}</b></div>
                <div className="mr-6">Street: <text>{street}</text></div>
                <div className="mr-6">Peil: <text className={`${peilcolor}`}>{peil}</text></div>
            </div>
        );
    };

    const GetWaterpeilData = () => {
        
    };

    const data = [
        { date: '08/12', peil: '+0.12m', street: 'Street 1' },
        { date: '09/12', peil: '-0.13m', street: 'Street 2' },
        { date: '10/12', peil: '+0.13m', street: 'Street 3' },
        { date: '11/12', peil: '-0.15m', street: 'Street 4' },
        { date: '12/12', peil: '+0.03m', street: 'Street 5' },
        { date: '13/12', peil: '-0.13m', street: 'Street 6' },
    ];

    return (
        <div className=" justify-center">
                            <div className="grid grid-cols-4 ">
                                <div className="col-span-2 font-bold">
                                    Waterpeil Logboek 
                                    <text className="text-left  text-gray-400"> ( {data.length} opnamen )</text>
                                </div>
                                {/* Select maand */}
                                <div className="text-center">
                                    <select id="countries" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg">
                                        <option selected>Selecteer een maand</option>
                                        <option value="01">Januari</option>
                                        <option value="02">Februari</option>
                                        <option value="03">Maart</option>
                                        <option value="04">April</option>
                                        <option value="05">Mei</option>
                                        <option value="06">Juni</option>
                                        <option value="07">Juli</option>
                                        <option value="08">Augustus</option>
                                        <option value="09">September</option>
                                        <option value="10">Oktober</option>
                                        <option value="11">November</option>
                                        <option value="12">December</option>
                                    </select>
                                </div>
                                <div className="text-center font-bold text-gray-600">
                                    {/* current year */}
                                    2024
                                </div>
                                
                            </div>
                            <div className="max-h-[300px] overflow-y-auto mt-1">
                                {data.map((entry, index) => <WaterpeilLogboekEntry key={index} {...entry} />)}
                            </div>
                        </div>
    );
}

export default WaterpeilLogboek;