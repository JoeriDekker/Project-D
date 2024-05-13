import React from 'react';

import Icons from '../../visuals/icons/generalicons';

type Props = {
    text?: string;
    link?: string;
    icon?: string;
}

function NavItem(props: Props) {

    return (
        <a className="flex items-center px-3 py-2 transition-colors duration-300 transform rounded-lg dark:text-gray-200 hover:bg-gray-100 dark:hover:bg-gray-800 dark:hover:text-black-900 hover:text-black-700" href="#">
            <Icons iconName={props.icon} />

            <a href={props.link} className="mx-2 text-sm font-medium text-black">{props.text}</a>
        </a>
    );
}

export default NavItem;