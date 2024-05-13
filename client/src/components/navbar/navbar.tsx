import React from "react";

import NavItem from '../navbar-navitem/navitem'

import { useLanguage } from "../../language/languagecontext";

function Navbar() {
    const language = useLanguage(true);

    return (
        <aside className="flex flex-col w-20r h-full  pt-8 overflow-y-hidden">
            <a href="localhost:3000/#" className="px-14">
                <img className="w-auto h-7" src="https://merakiui.com/images/logo.svg" alt=""></img>
            </a>

            <div className="content-center h-full">
                <nav className="h-full flex flex-col justify-start">
                    <div className="space-y-3 flex-grow">
                        <label className="px-3 text-xs text-black-500 dark:text-gray-400">{language.Navigation.dashboard}</label>

                        <NavItem icon="Home" text="pp" link="gfu" />
                        <NavItem icon="Home" text="sumting" link="gfu" />
                    </div>

                    <div className="space-y-3">
                        <label className="px-3 text-xs text-gray-500 dark:text-gray-400">{language.Navigation.dashboard}</label>

                        <NavItem icon="Home" text="sumting" link="gfu" />
                        <NavItem icon="Home" text="sumting" link="gfu" />
                    </div>

                    <div className="space-y-3 ">
                        <label className="px-3 text-xs text-gray-500 dark:text-gray-400">{language.Navigation.dashboard}</label>

                        <NavItem icon="Home" text="sumting" link="gfu" />
                        <NavItem icon="Home" text="sumting" link="gfu" />
                    </div>
                </nav>
            </div>
        </aside>
    );
}

export default Navbar;