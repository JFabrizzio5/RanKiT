<script setup>
import { ref, onMounted, onUnmounted } from 'vue'

definePageMeta({
  layout: 'overlay'
})

// Estado reactivo (simulando datos que vendrían de una API o LocalStorage)
const data = ref({
    team1: "Equipo A",
    team2: "Equipo B",
    score1: 0,
    score2: 0,
    round: "ESPERANDO CONEXIÓN...",
    status: "scheduled",
    mvp: "",
    kda: ""
})

// Simulación de carga de datos (Polling o Event Listener)
let interval
onMounted(() => {
    // Aquí conectarías con tu backend o LocalStorage
    // Por ahora simulamos que carga datos guardados
    const load = () => {
        if (process.client) {
            const stored = localStorage.getItem('tournament_overlay_data')
            if (stored) data.value = JSON.parse(stored)
        }
    }
    
    load()
    window.addEventListener('storage', load)
    interval = setInterval(load, 1000)
})

onUnmounted(() => {
    if (process.client) window.removeEventListener('storage', () => {})
    clearInterval(interval)
})
</script>

<template>
  <div class="w-full h-screen flex flex-col justify-between p-10 font-sans">
    <div class="flex justify-center animate-slide-down">
        <div class="flex items-stretch h-20 shadow-2xl relative filter drop-shadow-lg">
            <div class="absolute top-0 left-0 right-0 h-1 bg-gradient-to-r from-blue-500 via-white to-red-500 z-20"></div>

            <div class="bg-slate-900/90 border border-white/10 backdrop-blur-sm flex items-center pr-4 pl-8 rounded-bl-xl border-b-4 border-blue-600 min-w-[250px] justify-end relative">
                <span class="text-3xl font-bold uppercase text-white tracking-widest mr-4 font-display">{{ data.team1 }}</span>
                <div class="bg-blue-600 w-12 h-full flex items-center justify-center text-3xl font-bold text-white skew-x-[-15deg] absolute right-0 top-0 bottom-0 shadow-lg z-10">
                    <span class="skew-x-[15deg]">{{ data.score1 }}</span>
                </div>
            </div>

            <div class="bg-[#0f172a] px-8 flex flex-col justify-center items-center z-20 border-b-4 border-slate-700 mx-[-10px]">
                <span class="text-yellow-400 font-bold text-xl font-display leading-none uppercase">{{ data.round }}</span>
                <span class="text-[10px] text-slate-400 font-bold tracking-[0.2em] uppercase mt-1">
                    {{ data.status === 'live' ? '🔴 EN VIVO' : 'OFICIAL' }}
                </span>
            </div>

            <div class="bg-slate-900/90 border border-white/10 backdrop-blur-sm flex items-center pl-4 pr-8 rounded-br-xl border-b-4 border-red-600 min-w-[250px] justify-start relative">
                <div class="bg-red-600 w-12 h-full flex items-center justify-center text-3xl font-bold text-white skew-x-[-15deg] absolute left-0 top-0 bottom-0 shadow-lg z-10">
                    <span class="skew-x-[15deg]">{{ data.score2 }}</span>
                </div>
                <span class="text-3xl font-bold uppercase text-white tracking-widest ml-16 font-display">{{ data.team2 }}</span>
            </div>
        </div>
    </div>

    <div v-if="data.mvp" class="flex justify-start pb-10 animate-slide-up">
        <div class="bg-slate-900/90 backdrop-blur-sm rounded-r-xl border-l-8 border-yellow-500 flex items-center pr-10 pl-6 py-4 max-w-lg border border-white/10">
            <div class="flex flex-col">
                <span class="text-yellow-500 text-xs font-bold tracking-[0.4em] uppercase mb-1">Jugador Destacado</span>
                <span class="text-5xl font-bold text-white font-display leading-none uppercase">{{ data.mvp }}</span>
                <div v-if="data.kda" class="mt-2 flex items-center gap-2">
                    <span class="bg-slate-800 text-slate-400 text-xs font-bold px-2 py-1 rounded border border-white/10 uppercase">Estadísticas</span>
                    <span class="text-white font-mono font-bold text-lg">{{ data.kda }}</span>
                </div>
            </div>
        </div>
    </div>
  </div>
</template>

<style scoped>
@keyframes slideDown { from { transform: translateY(-100%); opacity: 0; } to { transform: translateY(0); opacity: 1; } }
@keyframes slideUp { from { transform: translateY(100%); opacity: 0; } to { transform: translateY(0); opacity: 1; } }
.animate-slide-down { animation: slideDown 0.6s cubic-bezier(0.2, 1, 0.3, 1) forwards; }
.animate-slide-up { animation: slideUp 0.6s cubic-bezier(0.2, 1, 0.3, 1) forwards; }
.font-display { font-family: 'Chakra Petch', sans-serif; }
</style>