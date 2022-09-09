/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
    safelist: [
        "validation-message",
        "tab-active",
        'text-success',
        'text-error',
        'active',
    ],
    theme: {
        extend: {},
    },
    plugins: [require("daisyui")],
}
