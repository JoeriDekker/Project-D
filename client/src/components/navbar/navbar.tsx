import React, { useState } from 'react';
import NavItem from '../navbar-navitem/navitem'
import { useTranslation } from 'react-i18next';
import Icons from '../../visuals/icons/generalicons';
import AnyButton from '../Button/AnyButton';

function Navbar() {
    const { t } = useTranslation();
    const [active, setActive] = useState<boolean>(false);

    const toggleActive = () => {
        setActive(!active);
    }

    return (
        <aside className="flex fixed flex-col w-20r min-w-80 h-full pb-8 overflow-y-hidden">
            <a href="/home" className="px-14"> {/* TODO: Add link to homepage */}
                <Icons iconName="Logo" />
            </a>

            <div className="content-center h-full">
                <nav className="pt-20 h-full flex flex-col justify-start">
                    <div className="space-y-2 flex flex-grow flex-col">
                        <label className="px-14 text-sm text-gray-500">{t('Navigation.dashboard')}</label>

                        <NavItem icon="Home" text={t('Navigation.overview')} link="/home" />
                        <NavItem icon="Measure" text={t('Navigation.waterdata')} link="/" />
                        <NavItem icon="Logbook" text={t('Navigation.logbooks')} link="/logboek" />
                    </div>

                    <div className="space-y-2 pb-8 flex flex-col">
                        <label className="px-14 text-sm text-gray-500">{t('Navigation.settings')}</label>

                        <NavItem icon="Neighbourhood" text={t('Navigation.neighbourhood')} link="/" />
                        <NavItem icon="Account" text={t('Navigation.account')} link="/account" />
                    </div>

                    <div className="space-y-2 flex flex-col pb-8">
                        <label className="px-14 text-sm text-gray-500">{t('Navigation.contact')}</label>

                        <button onClick={toggleActive} className="flex gap-1 items-center px-14 py-5 transition-all duration-300 transform rounded-lg hover:bg-gradient-to-r hover:from-slate-50 hover:via-slate-50 hover:to-slate-0">
                            <Icons iconName="Call" />
                            06-12345678
                        </button>

                        {active ? <>
                            <div className="pointer-events-none fixed inset-x-0 bottom-0 px-6 pb-6">
                                <div className="pointer-events-auto ml-72 w-fit rounded-xl bg-white p-6 shadow-lg ring-1 ring-gray-900/10">
                                    <p className="text-sm leading-6 text-gray-900">{t('Contact.contact')}<br /><br />{t('Contact.telephone')}<br />{t('Contact.email')}</p>
                                    <div className="mt-4 flex items-center gap-x-5">
                                        <button onClick={toggleActive} type="button" className="rounded-md bg-gray-900 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-gray-700 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-gray-900">{t('Navigation.close')}</button>
                                    </div>
                                </div>
                            </div>
                        </> : <></>}
                    </div>

                    <AnyButton link="/" text={t('Navigation.logout')} />
                </nav>
            </div>
        </aside>
    );
}

export default Navbar;