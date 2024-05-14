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
                <nav className="pt-20 pb-20 h-full flex flex-col justify-start">
                    <div className="space-y-3 flex flex-grow flex-col">
                        <label className="px-14 text-sm text-gray-500">{language.Navigation.dashboard}</label>

                        <NavItem icon="Home" text={language.Navigation.overview} link="/home" />
                        <NavItem icon="Home" text={language.Navigation.waterdata} link="/hoi" />
                        <NavItem icon="Home" text={language.Navigation.logbooks} link="/" />
                    </div>

                    <div className="space-y-3 pb-8 flex flex-col">
                        <label className="px-14 text-sm text-gray-500">{language.Navigation.settings}</label>

                        <NavItem icon="Home" text={language.Navigation.neighbourhood} link="/" />
                        <NavItem icon="Home" text={language.Navigation.account} link="/" />
                    </div>

                    <div className="space-y-3 flex flex-col">
                        <label className="px-14 text-sm text-gray-500">{language.Navigation.contact}</label>

                        <NavItem icon="Home" text="06-72984721" link="/" />
                    </div>
                </nav>
            </div>
        </aside>
    );
}

export default Navbar;