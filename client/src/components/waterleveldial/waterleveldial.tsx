import { useState, useEffect } from 'react';

import Icons from '../../visuals/icons/generalicons';

type Props = {
    least?: number;
    most?: number;
    steps?: number;
    water?: number;
    waterdialvalue: number;
    waterdialstate: (value: any) => void;
    currentwaterstate: (value: any) => void;
}

function WaterlevelDial(props: Props) {
    const [currentDialValue, setCurrentDialValue] = useState(props.water);

    function waterlevelLogic(plus: boolean = false) {
        let tempChangedWaterLevel = currentDialValue;
        if (currentDialValue == null || tempChangedWaterLevel === undefined) {
            return currentDialValue;
        }
        else {
            if (plus) {
                tempChangedWaterLevel += 0.25;
                setCurrentDialValue(tempChangedWaterLevel);
            }
            else {
                tempChangedWaterLevel -= 0.25;
                setCurrentDialValue(tempChangedWaterLevel);
            }
        }
    }

    useEffect(() => {
        setTimeout(() => {
            return props.currentwaterstate(currentDialValue)
        }, 1000)
    })

    return (
        <div className="circulardial h-72 flex justify-center items-center overflow-hidden">
            <div className="relative dial flex justify-center items-center bg-white w-60 h-60 rounded-fully border-solid border-4 border-secondaryCol shadow-inset-outline">
                <div className="flex flex-col dialmeter text-5xl font-bold bg-white rounded-fully flex justify-center items-center border-2 border-solid border-secondaryCol w-96 h-96
                after:content-[''] after:absolute after:w-60 after:h-40 after:rounded-fully after:bg-gray-100 after:top-44 after:z-5">
                    <p className="dialtext text-5xl font-bold">{`${(currentDialValue)?.toFixed(2)} m`}</p>
                    <div className='absolute flex w-36 justify-between mt-12 top-36 z-10'>
                        <button onClick={() => waterlevelLogic(true)} className="flex justify-center items-center button w-12 h-12 bg-white rounded-fully leading-nones p-2 border-2 border-solid border-secondaryCol">
                            <Icons iconName='Plus'></Icons>
                        </button>
                        <button onClick={() => waterlevelLogic(false)} className="flex justify-center items-center button w-12 h-12 bg-white rounded-fully leading-none p-2 border-2 border-solid border-secondaryCol">
                            <Icons iconName='Minus'></Icons>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default WaterlevelDial;