import React, { useState } from 'react';
import NavItem from '../navbar-navitem/navitem'
import { useTranslation } from 'react-i18next';
import Icons from '../../visuals/icons/generalicons';
import AnyButton from '../Button/AnyButton';

import useSignOut from 'react-auth-kit/hooks/useSignOut';

// function Dashboard() {
//   const signOut = useSignOut()
//   function handleLogout() {
//     signOut()
//     window.location.href = '/login'
//   }
//     <div>
//       <button className='bg-red-400 p-4 text-white' onClick={handleLogout}>Uitloggen</button>


function Navbar() {
    const { t } = useTranslation();
    const [active, setActive] = useState<boolean>(false);
    const signOut = useSignOut()

    const toggleActive = () => {
        setActive(!active);
    }

    function handleLogout() {
        signOut()
        window.location.href = '/login'
    }

    return (
        <aside className="flex flex-col w-20r min-w-80 h-full pb-8 overflow-y-hidden">
            <a href="/home" className="px-14"> {/* TODO: Add link to homepage */}
                <Icons iconName="Logo" />
            </a>

            <div className="content-center h-full">
                <nav className="pt-20 h-full flex flex-col justify-start">
                    <div className="space-y-2 flex flex-grow flex-col">
                        <label className="px-14 text-sm text-gray-500">{t('Navigation.dashboard')}</label>

                        <NavItem icon="Home" text={t('Navigation.overview')} link="/" />
                        <NavItem icon="Measure" text={t('Navigation.waterdata')} link="/anypage" />
                        <NavItem icon="Logbook" text={t('Navigation.logbooks')} link="/logboek" />
                    </div>

                    <div className="space-y-2 pb-8 flex flex-col">
                        <label className="px-14 text-sm text-gray-500">{t('Navigation.settings')}</label>

                        <NavItem icon="Neighbourhood" text={t('Navigation.neighbourhood')} link="/anypage" />
                        <NavItem icon="Account" text={t('Navigation.account')} link="/account" />
                    </div>

                    <div className="space-y-2 flex flex-col pb-8">
                        <label className="px-14 text-sm text-gray-500">{t('Navigation.contact')}</label>

                        <button onClick={toggleActive} className="flex gap-1 items-center px-14 py-5 transition-all duration-300 transform rounded-lg hover:bg-gradient-to-r hover:from-slate-50 hover:via-slate-50 hover:to-slate-0">
                            <Icons iconName="Call" />
                            06-12345678
                        </button>

                        {active ? <>
                            <div className="pointer-events-none fixed inset-x-0 bottom-0 px-6 pb-6 z-10">
                                <div className="pointer-events-auto ml-72 w-fit rounded-xl bg-white p-6 shadow-lg ring-1 ring-gray-900/10">
                                    <p className="text-sm leading-6 text-gray-900">{t('Contact.contact')}<br /><br />{t('Contact.telephone')}<br />{t('Contact.email')}</p>
                                    <div className="mt-4 flex items-center gap-x-5">
                                        <button onClick={toggleActive} type="button" className="rounded-md bg-gray-900 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-gray-700 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-gray-900">{t('Navigation.close')}</button>
                                    </div>
                                </div>
                            </div>
                        </> : <></>}
                    </div>

                    {/* <AnyButton link="/" text={t('Navigation.logout')} /> */}
                    {/* onClick={handleLogout} */}

                    <button className="ring-offset-background mx-14 focus-visible:ring-ring flex h-10 w-50 items-center justify-center whitespace-nowrap rounded-md bg-black px-4 py-2 text-sm font-medium text-white transition-colors hover:bg-black/70 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50"
                        type="submit" onClick={handleLogout}>
                        {t('Navigation.logout')}
                    </button>
                </nav>
            </div>
        </aside>
    );
}

export default Navbar;