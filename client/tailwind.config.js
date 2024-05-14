/** @type {import('tailwindcss').Config} */

const secondaryColor = '#A6E1FA';

module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        'base-color': '#161D27',
        'secondary-color': secondaryColor,
        'reds': '#D16666',
        'greens': '#ADD167',
        'background-color': secondaryColor,
      },
      width: {
        '20r': '25em'
      },
    },
  },
  plugins: [],
}

