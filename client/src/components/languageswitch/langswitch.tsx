import React from 'react';
import { useTranslation } from 'react-i18next';

const LanguageSwitcher: React.FC = () => {
    const { i18n } = useTranslation();

    const changeLanguage = (language: string) => {
        i18n.changeLanguage(language);
    };

    return (
        <div className="flex items-center space-x-4 p-4">
            <button
                className={`px-4 py-2 rounded ${i18n.language === 'en' ? 'bg-blue-500 text-white' : 'bg-gray-200'}`}
                onClick={() => changeLanguage('en')}
            >
                English
            </button>
            <button
                className={`px-4 py-2 rounded ${i18n.language === 'nl' ? 'bg-blue-500 text-white' : 'bg-gray-200'}`}
                onClick={() => changeLanguage('nl')}
            >
                Nederlands
            </button>
        </div>
    );
};

export default LanguageSwitcher;