/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
    theme: {
        extend: {},
    },
    safelist: [
        "tw-hidden",
        "md:tw-block",
    ],
    plugins: [require('@rvxlab/tailwind-plugin-ios-full-height')],
}

