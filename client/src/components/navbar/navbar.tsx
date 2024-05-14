import React, { useState } from 'react';
import NavItem from '../navbar-navitem/navitem'
import { useLanguage } from "../../language/languagecontext";
import Icons from '../../visuals/icons/generalicons';

function Navbar() {
    const language = useLanguage(true);
    const [active, setActive] = useState<boolean>(false);

    const toggleActive = () => {
        setActive(!active);
    }

    return (
        <aside className="flex flex-col w-20r h-full pb-8 pt-8 overflow-y-hidden">
            <a href="localhost:3000/#" className="px-14">
                <Icons iconName="Logo" />
            </a>

            <div className="content-center h-full">
                <nav className="pt-20 h-full flex flex-col justify-start">
                    <div className="space-y-2 flex flex-grow flex-col">
                        <label className="px-14 text-sm text-gray-500">{language.Navigation.dashboard}</label>

                        <NavItem icon="Home" text={language.Navigation.overview} link="/home" />
                        <NavItem icon="Measure" text={language.Navigation.waterdata} link="/" />
                        <NavItem icon="Logbook" text={language.Navigation.logbooks} link="/" />
                    </div>

                    <div className="space-y-2 pb-8 flex flex-col">
                        <label className="px-14 text-sm text-gray-500">{language.Navigation.settings}</label>

                        <NavItem icon="Neighbourhood" text={language.Navigation.neighbourhood} link="/" />
                        <NavItem icon="Account" text={language.Navigation.account} link="/" />
                    </div>

                    <div className="space-y-2 flex flex-col pb-8">
                        <label className="px-14 text-sm text-gray-500">{language.Navigation.contact}</label>

                        {/* <button onClick={toggleActive} > hoi </button> */}

                        <button onClick={toggleActive} className="flex gap-1 items-center px-14 py-5 transition-all duration-300 transform rounded-lg hover:bg-gradient-to-r hover:from-slate-50 hover:via-slate-50 hover:to-slate-0">
                            <Icons iconName="Call" />
                            06-12345678
                        </button>

                        {active ? <>
                            <div className="pointer-events-none fixed inset-x-0 bottom-0 px-6 pb-6">
                                <div className="pointer-events-auto ml-72 w-fit rounded-xl bg-white p-6 shadow-lg ring-1 ring-gray-900/10">
                                    <p className="text-sm leading-6 text-gray-900">{language.Contact.contact}<br /><br />{language.Contact.telephone}<br />{language.Contact.email}</p>
                                    <div className="mt-4 flex items-center gap-x-5">
                                        <button onClick={toggleActive} type="button" className="rounded-md bg-gray-900 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-gray-700 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-gray-900">{language.Navigation.close}</button>
                                    </div>
                                </div>
                            </div>
                        </> : <></>}
                    </div>

                    <button className="ring-offset-background mx-14 focus-visible:ring-ring flex h-10 w-50 items-center justify-center whitespace-nowrap rounded-md bg-black px-4 py-2 text-sm font-medium text-white transition-colors hover:bg-black/70 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50" type="submit">{language.Navigation.logout}</button>
                </nav>
            </div>
        </aside>
    );
}

export default Navbar;