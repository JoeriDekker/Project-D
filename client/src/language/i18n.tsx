import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import enTranslation from './en-EN.json';
import nlTranslation from './nl-NL.json';

const resources = {
    en: {
        translation: enTranslation
    },
    nl: {
        translation: nlTranslation
    }
};

i18n
    .use(initReactI18next)
    .init({
        resources,
        lng: 'en', // default language
        interpolation: {
            escapeValue: false
        }
    });

export default i18n;