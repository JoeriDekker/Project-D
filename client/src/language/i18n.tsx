import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import translationEN from './en-EN.json';
import translationNL from './nl-NL.json';


// Configure i18next
i18n
    .use(initReactI18next) // Pass initReactI18next
    .init({
        lng: 'en', // Set default language
        fallbackLng: 'en', // Set fallback language
        debug: true, // Enable debug mode
        resources: {
            en: {
                translation: translationEN, // English translations
            },
            nl: {
                translation: translationNL, // French translations
            },
            // Add resources for other languages if needed
        },
        interpolation: {
            escapeValue: false, // React already does escaping
        },
    });

export default i18n;