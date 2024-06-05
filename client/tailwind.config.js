/** @type {import('tailwindcss').Config} */

const secondaryColor = '#A6E1FA';
const plugin = require('tailwindcss/plugin');

module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      height: {
        '96': '96.666667%'
      },
      colors: {
        'base-color': '#161D27',
        'secondary-color': secondaryColor,
        'reds': '#D16666',
        'greens': '#ADD167',
        'background-color': secondaryColor,
      },
      width: {
        '96': '96.666667%',
        '20r': '25em'
      },
      borderRadius: {
        'fully': '50%'
      },
      boxShadow: {
        'inset-outline': 'inset 0 0 20px 2px rgba(0, 0, 0, 0.05)',
      },
      borderColor: {
        'secondaryCol': secondaryColor,
      },
      lineHeight: {
        'none': '0'
      }
    },
  },
  plugins: [
    plugin(function ({ addUtilities }) {
      const newUtilities = {
        '.content-before::before': {
          content: 'attr(data-content-before)',
        },
        '.content-after::after': {
          content: 'attr(data-content-after)',
        },
        '.before\\:bg-blue-500::before': {
          backgroundColor: '#4299e1',
        },
        '.after\\:bg-blue-500::after': {
          backgroundColor: '#4299e1',
        },
        // Add more custom utilities as needed
      };

      addUtilities(newUtilities, {
        variants: ['responsive'], // Specify variants if needed
      });
    })
  ],
}

