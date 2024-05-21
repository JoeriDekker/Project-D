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
  theme: {
    extend: {
      spacing: {
        '40': '10rem',
        '44': '11rem',
        '48': '12rem',
        '52': '13rem',
        '56': '14rem',
        '60': '15rem',
        '64': '16rem',
        '68': '17rem',
        '72': '18rem',
        '76': '19rem',
        '80': '20rem',
      }
    }
  }
}

