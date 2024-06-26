import { useState, useEffect } from 'react';

import Icons from '../../visuals/icons/generalicons';

type Props = {
    least?: number;
    most?: number;
    steps?: number;
    water: number;
    waterdialvalue: number;
    waterdialstate: (value: number) => void;
    currentwaterstate: (value: any) => void;
}

function WaterlevelDial(props: Props) {

    const [currentDialValue, setCurrentDialValue] = useState(props.water);

    const {currentwaterstate} = props;

    function waterlevelLogic(plus: boolean = false) {
        let tempChangedWaterLevel = currentDialValue;
        if (currentDialValue == null || tempChangedWaterLevel === undefined) {
            return currentDialValue;
        }
        else {
            if (plus) {
                tempChangedWaterLevel += props.steps ?? 0.15;
                setCurrentDialValue(tempChangedWaterLevel);
            }
            else {
                tempChangedWaterLevel -= props.steps ?? 0.15;
                setCurrentDialValue(tempChangedWaterLevel);
            }
        }
    }

    useEffect(() => {
        if (currentDialValue === 0 || undefined) {
            setCurrentDialValue(props.water as number);
        }
    }, [currentDialValue, props, props.water])

    useEffect(() => {
        setTimeout(() => {

            currentwaterstate(currentDialValue)
        }, 1000)
    }, [currentDialValue, currentwaterstate]);

    function WaterLevelDialToFixed() {
        if (currentDialValue == null || typeof currentDialValue !== 'number') {
            return 0;
        }
        return currentDialValue.toFixed(2);
    }

    return (
        <div className="circulardial h-72 flex justify-center items-center overflow-hidden">
            <div className="relative dial flex justify-center items-center bg-white w-60 h-60 rounded-fully border-solid border-4 border-secondaryCol shadow-inset-outline">
                <div className="flex flex-col dialmeter text-5xl font-bold bg-white rounded-fully justify-center items-center border-2 border-solid border-secondaryCol w-96 h-96
                after:content-[''] after:absolute after:w-60 after:h-40 after:rounded-fully after:bg-gray-100 after:top-44 after:z-5">
                    <p className="dialtext text-5xl font-bold">{`${WaterLevelDialToFixed()} m`}</p>
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