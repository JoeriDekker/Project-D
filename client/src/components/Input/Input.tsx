type Props = {
    label?: string;
    placeholder?: string;
    width?: string;
}

function Input(props: Props) {
    let newWidth = `w-${props.width}`


    if (props.width === null || props.width === undefined)
        newWidth = `w-full`

    return (
        <div className={newWidth} >
            <label htmlFor="default-input" className="block mb-2 text-sm font-medium text-gray-900 opacity-40">{props.label}</label>
            <input type="text" id="default-input" className="bg-gray-50 border-2 p-2 border-gray-300 text-gray-900 text-sm rounded-lg focus:outline-[#A6E1FA] text-grey-900 text-sm rounded-lg block w-full p-2.5" placeholder={props.placeholder} />
        </div >
    )
}

export default Input;