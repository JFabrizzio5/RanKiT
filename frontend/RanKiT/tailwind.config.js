/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./components/**/*.{js,vue,ts}",
    "./layouts/**/*.vue",
    "./pages/**/*.vue",
    "./plugins/**/*.{js,ts}",
    "./app.vue",
    "./error.vue",
    // AGREGA ESTA LÍNEA para que lea tu carpeta 'app':
    "./app/**/*.{js,vue,ts}", 
  ],
  theme: {
    extend: {
      fontFamily: { 
        sans: ['Inter', 'sans-serif'], 
        display: ['Chakra Petch', 'sans-serif'] 
      },
      colors: {
        brand: { 
          dark: '#0B0C15', 
          purple: '#7C3AED', 
          cyan: '#06B6D4', 
          surface: '#151725',
          gold: '#FBBF24',
          red: '#EF4444'
        }
      },
      animation: {
        'float': 'float 6s ease-in-out infinite',
        'pulse-fast': 'pulse 1.5s cubic-bezier(0.4, 0, 0.6, 1) infinite',
      },
      keyframes: {
        float: {
          '0%, 100%': { transform: 'translateY(0)' },
          '50%': { transform: 'translateY(-20px)' },
        }
      }
    },
  },
  plugins: [],
}