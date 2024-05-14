import NavItem from '../navbar-navitem/navitem'
import { useLanguage } from "../../language/languagecontext";
import Icons from '../../visuals/icons/generalicons';

function Navbar() {
    const language = useLanguage(true);

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
                        <NavItem icon="Measure" text={language.Navigation.waterdata} link="/hoi" />
                        <NavItem icon="Logbook" text={language.Navigation.logbooks} link="/" />
                    </div>

                    <div className="space-y-2 pb-8 flex flex-col">
                        <label className="px-14 text-sm text-gray-500">{language.Navigation.settings}</label>

                        <NavItem icon="Neighbourhood" text={language.Navigation.neighbourhood} link="/" />
                        <NavItem icon="Account" text={language.Navigation.account} link="/" />
                    </div>

                    <div className="space-y-2 flex flex-col pb-8">
                        <label className="px-14 text-sm text-gray-500">{language.Navigation.contact}</label>

                        <NavItem icon="Call" text="06-72984721" link="/" />
                    </div>

                    <button className="ring-offset-background mx-14 focus-visible:ring-ring flex h-10 w-50 items-center justify-center whitespace-nowrap rounded-md bg-black px-4 py-2 text-sm font-medium text-white transition-colors hover:bg-black/70 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50" type="submit">{language.Navigation.logout}</button>
                </nav>
            </div>
        </aside>
    );
}

export default Navbar;