

function LoginAdjust() {

    return (
        <>
            <h1 className="text-xl font-medium">Login gegevens wijzigen</h1>
            <h2 className="text-l font-medium opacity-40">Huidige inlog gegevens</h2>
            <div className="flex dir-row w-full gap-5 px-5" >
                <div className="w-full">
                    <label htmlFor="default-input" className="block mb-2 text-sm font-medium text-gray-900 opacity-40">Straatnaam</label>
                    <input type="text" id="default-input" className="bg-gray-50 border-2 p-2 border-gray-300 text-gray-900 text-sm rounded-lg focus:outline-[#A6E1FA] text-grey-900 text-sm rounded-lg block w-full p-2.5" placeholder="Gortaanse straat" />
                </div>
                <div className="w-1/2">
                    <label htmlFor="default-input" className="block mb-2 text-sm font-medium text-gray-900 opacity-40">Huisnummer</label>
                    <input type="text" id="default-input" className="bg-gray-50 border-2 p-2 border-gray-300 text-gray-900 text-sm rounded-lg focus:outline-[#A6E1FA] text-grey-900 text-sm rounded-lg block w-full p-2.5" placeholder="8" />
                </div>
            </div>
            <div className="flex dir-row w-full gap-5 px-5" >
                <div className="w-1/2">
                    <label htmlFor="default-input" className="block mb-2 text-sm font-medium text-gray-900 opacity-40">Postcode</label>
                    <input type="text" id="default-input" className="bg-gray-50 border-2 p-2 border-gray-300 text-gray-900 text-sm rounded-lg focus:outline-[#A6E1FA] text-grey-900 text-sm rounded-lg block w-full p-2.5" placeholder="3857 KG" />
                </div>
                <div className="w-full">
                    <label htmlFor="default-input" className="block mb-2 text-sm font-medium text-gray-900 opacity-40">Plaats naam</label>
                    <input type="text" id="default-input" className="bg-gray-50 border-2 p-2 border-gray-300 text-gray-900 text-sm rounded-lg focus:outline-[#A6E1FA] text-grey-900 text-sm rounded-lg block w-full p-2.5" placeholder="Gouda" />
                </div>
            </div>
            <div className="w-1/2 px-5">
                <label htmlFor="default-input" className="block mb-2 text-sm font-medium text-gray-900 opacity-40">Wachtwoord</label>
                <input type="text" id="default-input" className="bg-gray-50 border-2 p-2 border-gray-300 text-gray-900 text-sm rounded-lg focus:outline-[#A6E1FA] text-grey-900 text-sm rounded-lg block w-full p-2.5" placeholder="**************" />
            </div>

        </>
    );
}

export default LoginAdjust;