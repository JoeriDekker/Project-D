import React, { useState, useEffect } from 'react';
import { NavLink, To } from 'react-router-dom';

import Icons from '../../visuals/icons/generalicons';

type Props = {
    // least?: number;
    // most?: number;
    // steps?: number;
    least?: 5;
    most?: 15;
    steps?: 2;
}

function WaterlevelDial(props: Props) {
    const [waterlevel, setWaterlevel] = useState("");

    function stepsLogic() {

    }

    function dialLogic() {
        var waterlevel = 10;
        if (waterlevel < 0) {
            setWaterlevel(`${waterlevel}m`);
        }
        else {
            setWaterlevel(`+${waterlevel}m`);
        }
    }

    useEffect(() => {
        dialLogic();
    }, []);


    return (
        <div className="circulardial w-full h-full flex justify-center items-center">
            <div className="dial flex justify-center items-center bg-white w-60 h-60 rounded-fully border-solid border-4 border-secondaryCol shadow-inset-outline">
                <div className="dialmeter text-5xl font-bold bg-white rounded-fully flex justify-center items-center border-2 border-solid border-secondaryCol w-96 h-96">
                    <p className="dialtext text-5xl font-bold">{waterlevel}</p>
                </div>

            </div>
            <div className="button"></div>
            <div className="button"></div>
        </div>
    );
}

export default WaterlevelDial;