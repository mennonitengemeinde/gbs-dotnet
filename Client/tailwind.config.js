/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
    safelist: [
        "validation-message",
        "tab-active",
        'text-success',
        'text-error',
        'active',
        'alert-info',
        'alert-success',
        'alert-warning',
        'alert-error',
        'duration-300',
        'translate-x-0',
        'opacity-100',
        'translate-x-full',
        'opacity-0',
        'delay-100',
    ],
    theme: {
        extend: {},
    },
    plugins: [require("daisyui")],
}
