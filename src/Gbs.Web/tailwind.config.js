const defaultTheme = require('tailwindcss/defaultTheme')

/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
    theme: {
        extend: {
            fontFamily: {
                'sans': ['"Open Sans"', ...defaultTheme.fontFamily.sans],
                'serif': ['"Merriweather"', ...defaultTheme.fontFamily.serif],
            }
        },
    },
    daisyui: {
        themes: [
            {
                gbstheme: {
                    "primary": "#07114E",
                    "secondary": "#BD9A45",
                    "accent": "#8A7F60",
                    "neutral": "#5A6872",
                    "base-100": "#FAF8F5",
                    "base-200": "#F0EDEA",
                    "base-300": "#E6E3DF",
                    "base-content": "#505050",
                    "info": "#3ABFF8",
                    "success": "#36D399",
                    "warning": "#FBBD23",
                    "error": "#F87272",
                },
            },
        ],
    },
    plugins: [require('daisyui'), require('@rvxlab/tailwind-plugin-ios-full-height')],
}

