import React, { createContext, useContext, useState, ReactNode } from 'react';

type Language = 'en-EN' | 'nl-NL';

type Require = boolean;

interface LanguageContextType {
    language: Language;
    setLanguage: (language: Language) => void;
}

const LanguageContext = createContext<LanguageContextType | undefined>(undefined);

export const LanguageProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [language, setLanguage] = useState<Language>('en-EN');

    return (
        <LanguageContext.Provider value={{ language, setLanguage }}>
            {children}
        </LanguageContext.Provider>
    );
};

export const useLanguage = (props: Require) => {
    const context = useContext(LanguageContext);
    if (!context) {
        throw new Error('useLanguage must be used within a LanguageProvider');
    }

    if (props) {
        return require(`./${context.language}.json`);
    }

    return context;
};

export type { Language };
