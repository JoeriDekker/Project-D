import React from 'react';
import { NavLink, To } from 'react-router-dom';

import Icons from '../../visuals/icons/generalicons';

type Props = {
    text?: string;
    link?: To;
    icon?: string;
}

function NavItem(props: Props) {
    const link = props.link || ""; // Set a default value for link if it is undefined

    return (
        <NavLink to={link} className={({ isActive }) => `flex gap-1 items-center px-14 py-5 transition-all duration-300 transform rounded-lg hover:bg-gradient-to-r hover:from-slate-50 hover:via-slate-50 hover:to-slate-0${isActive ? " bg-gradient-to-r from-slate-50 via-slate-50 to-slate-0" : " opacity-60"}`}>
            <Icons iconName={props.icon} />
            {props.text}

        </NavLink>
    );
}

export default NavItem;