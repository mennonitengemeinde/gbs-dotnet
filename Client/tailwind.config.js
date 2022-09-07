/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
    safelist: [
        "validation-message",
        "tab-active",
    ],
    theme: {
        extend: {},
    },
    plugins: [require("daisyui")],
}
