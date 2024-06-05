import React, { useState, useEffect, useContext } from 'react';
import { NavLink, To } from 'react-router-dom';

import Icons from '../../visuals/icons/generalicons';

type Props = {
    // least?: number;
    // most?: number;
    // steps?: number;
    least?: number;
    most?: number;
    steps?: number;
}

function WaterlevelDial(props: Props) {
    const [waterlevel, setWaterlevel] = useState<number | null>(0);

    function stepsLogic() {

    }

    function dialLogic() {
        var waterlevel = 10;
        if (waterlevel < 0) {
            setWaterlevel(waterlevel);
        }
        else {
            setWaterlevel(waterlevel);
        }
    }

    useEffect(() => {
        dialLogic();
    }, []);


    return (
        <div className="circulardial w-full h-full flex justify-center items-center overflow-hidden">
            <div className="relative dial flex justify-center items-center bg-white w-60 h-60 rounded-fully border-solid border-4 border-secondaryCol shadow-inset-outline">
                <div className="flex flex-col dialmeter text-5xl font-bold bg-white rounded-fully flex justify-center items-center border-2 border-solid border-secondaryCol w-96 h-96
                after:content-[''] after:absolute after:w-60 after:h-40 after:rounded-fully after:bg-gray-100 after:top-44 after:z-5">
                    <p className="dialtext text-5xl font-bold">{waterlevel}</p>
                    <div className='absolute flex w-36 justify-between mt-12 top-36 z-10'>
                        <button className="flex justify-center items-center button w-12 h-12 bg-white rounded-fully leading-nones p-2 border-2 border-solid border-secondaryCol">
                            <Icons iconName='Plus'></Icons>
                        </button>
                        <button className="flex justify-center items-center button w-12 h-12 bg-white rounded-fully leading-none p-2 border-2 border-solid border-secondaryCol">
                            <Icons iconName='Minus'></Icons>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default WaterlevelDial;