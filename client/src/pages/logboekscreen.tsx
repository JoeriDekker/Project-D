import React from "react";

import Navbar from '../components/navbar/navbar'

function LogboekScreen() {

    const LogboekEntry = ({ date, peil, min, max}: { date: string, peil: string, min: string, max: string}) => {
        let peilcolor: string = 'text-green-600';
        let mincolor: string = 'text-green-600';
        let maxcolor: string = 'text-green-600';
        if (peil.includes('-')) {
            peilcolor = 'text-red-600';
        }
        if (min.includes('-')) {
            mincolor = 'text-red-600';
        }
        if (max.includes('-')) {
            maxcolor = 'text-red-600';
        }

        return (
            <div className="grid grid-cols-4 gap-2 mt-2 shadow-md">
                <div className="py-1 px-2 mr-20 rounded-md bg-slate-100"><b>{date}</b></div>
                <div className="mr-6">Peil: <text className={`${peilcolor}`}>{peil}</text></div>
                <div className="mr-6">Min: <text className={`${mincolor}`}>{min}</text></div>
                <div className="mr-6">Max: <text className={`${maxcolor}`}>{max}</text></div>
            </div>
        );
    };

    const data = [
        { date: '08/12', peil: '+0.12m', min: '-0.22m', max: '+0.50m', color: 'text-red-600' },
        { date: '09/12', peil: '-0.13m', min: '-0.21m', max: '+0.40m', color: 'text-red-600' },
        { date: '10/12', peil: '+0.14m', min: '-0.20m', max: '-0.30m', color: 'text-green-600' },
        { date: '11/12', peil: '-0.15m', min: '-0.19m', max: '+0.60m', color: 'text-green-600' },
        { date: '12/12', peil: '-0.16m', min: '-0.18m', max: '-0.80m', color: 'text-green-600' },
        { date: '13/12', peil: '-0.17m', min: '-0.22m', max: '+0.90m', color: 'text-green-600' },
        { date: '14/12', peil: '-0.18m', min: '-0.22m', max: '+0.10m', color: 'text-orange-600' },
        { date: '12/12', peil: '-0.16m', min: '-0.18m', max: '+0.80m', color: 'text-orange-600' },
        { date: '12/12', peil: '-0.16m', min: '-0.18m', max: '+0.80m', color: 'text-orange-600' },
        { date: '15/12', peil: '-0.19m', min: '-0.22m', max: '+0.60m', color: 'text-orange-600' },
    ];

      
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
                    <div className="grid grid-cols-2 gap-2">
                        <div className="flex justify-center bg-amber-200">
                            Div 1
                        </div>
                        <div className=" justify-center">
                            <div className="grid grid-cols-3 gap-2">
                                {/* Select maand */}
                                <div>
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
                                <div>
                                    <select id="countries" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg">
                                        <option selected>Selecteer jaar</option>
                                        <option value="2021">2021</option>
                                        <option value="2022">2022</option>
                                        <option value="2023">2023</option>
                                        <option value="2024">2024</option>
                                        <option value="2025">2025</option>
                                    </select>
                                </div>
                                <div>
                                    <button type="button" className="px-2 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg">
                                        Download svg
                                    </button>
                                </div>
                            </div>
                            <div className="max-h-[300px] overflow-y-auto mt-1">
                                {data.map((entry, index) => <LogboekEntry key={index} {...entry} />)}
                            </div>
                        </div>
                    </div>
                </div> 
            </div>
        </div>
        
        
    </>
    );
}

export default LogboekScreen;