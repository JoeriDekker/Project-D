import React from 'react';
import { useLanguage, Language } from '../../language/languagecontext';

const LanguageSwitcher: React.FC = () => {
    const { setLanguage } = useLanguage(false);

    const handleChangeLanguage = (newLanguage: string) => {
        setLanguage(newLanguage as Language);
    };

    return (
        <div>
            <button onClick={() => handleChangeLanguage('en-EN')}>English</button>
            <button onClick={() => handleChangeLanguage('nl-NL')}>Dutch</button>
        </div>
    );
};

export default LanguageSwitcher;