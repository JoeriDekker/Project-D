/** @type {import('tailwindcss').Config} */

const secondaryColor = '#A6E1FA';

module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        'baseCol': '#161D27',
        'secondaryCol': secondaryColor,
        'reds': '#D16666',
        'greens': '#ADD167',
        'backgroundCol': secondaryColor,
      },
      width: {
        '20r': '20em'
      },
    },
  },
  plugins: [],
}

