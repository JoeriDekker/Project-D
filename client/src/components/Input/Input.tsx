import Icons from "../../visuals/icons/generalicons";
import React, { useState } from "react";

type Props = {
    label?: string;
    placeholder?: string;
    width?: string;
    needed?: boolean;
    neededText?: string;
    type?: React.HTMLInputTypeAttribute | undefined;
    onChange?: React.ChangeEventHandler<HTMLInputElement>;
    value?: string | number | readonly string[] | undefined;
    name?: string;
    error?: string;
}

function Input(props: Props) {
    const [hover, setHover] = useState(false);
    let newWidth = `w-${props.width}`


    if (props.width === null || props.width === undefined)
        newWidth = `w-full`

    // TODO: Make the label htmlFor and input id dynamic as it causes multiple duplicate notices 
    return (
        <div className={newWidth} >
            <div className="flex dir-row gap-1">
                <label htmlFor="default-input" className="block mb-2 text-sm font-medium text-gray-900 opacity-80">{props.label}</label>
                {!props.needed ? (<></>) : (
                    <div className="flex dir-row " onMouseEnter={() => setHover(true)} onMouseLeave={() => setHover(false)}>
                        <p className="text-red-500 text-xl">*</p>
                        <Icons iconName="Question" />
                        {!hover ? <></> : <p className="opacity-80">{props.neededText}</p>}
                    </div>
                )}
            </div>
            <input name={props.name} type={props.type ? props.type : "text"} value={props.value} onChange={props.onChange} id="default-input" className="bg-gray-50 border-2 p-2 border-gray-300 text-gray-900 text-sm rounded-lg focus:outline-[#A6E1FA] text-grey-900 block w-full" placeholder={props.placeholder} />
            <p id={`${props.name}-error`} className="text-red-500">{props.error}</p>
        </div >
    )
}

export default Input;