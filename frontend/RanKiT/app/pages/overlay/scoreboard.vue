<script setup>
import { ref, onMounted } from 'vue'

// IMPORTANTE: Usar el layout 'overlay' que está limpio
definePageMeta({
  layout: 'overlay'
})

// --- SOLUCIÓN OBS DEFINITIVA ---
// Usamos useHead para inyectar CSS crítico que sobrescriba TODO.
// Esto asegura que html, body y el contenedor de Nuxt sean transparentes.
useHead({
  style: [
    {
      children: `
        html, body, #__nuxt {
          background-color: rgba(0, 0, 0, 0) !important;
          background: transparent !important;
          overflow: hidden !important;
        }
      `
    }
  ]
})

const scoreA = ref(1)
const scoreB = ref(0)
const timer = ref("1:45")
const round = ref(3)

// Datos simulados
const teamA = { name: 'Team Liquid', logo: 'https://upload.wikimedia.org/wikipedia/en/thumb/f/f4/Team_Liquid_logo_2017.svg/1200px-Team_Liquid_logo_2017.svg.png', color: '#0a3250' }
const teamB = { name: 'Sentinels', logo: 'https://upload.wikimedia.org/wikipedia/commons/6/6b/Sentinels_Logo.png', color: '#ce0037' }

onMounted(() => {
    // Simulación del timer
    let t = 105
    setInterval(() => {
        t--
        const m = Math.floor(t / 60)
        const s = t % 60
        timer.value = `${m}:${s < 10 ? '0'+s : s}`
        if(t <= 0) t = 105
    }, 1000)
})
</script>

<template>
  <!-- El contenedor raíz también debe ser explícitamente transparente -->
  <div class="flex justify-center min-h-screen pt-8 overflow-hidden font-sans bg-transparent" style="background-color: transparent !important;">
    
    <div class="relative flex items-stretch h-16 drop-shadow-2xl">
        
        <!-- Team A -->
        <div class="relative flex items-center pl-8 pr-12 overflow-hidden text-white bg-gray-900 border-b-4 border-l-4 skew-box-left" :style="{ borderColor: teamA.color }">
             <div class="flex flex-col items-end mr-4">
                 <span class="text-xl font-bold leading-none tracking-wider uppercase font-display">{{ teamA.name }}</span>
                 <span class="text-[10px] text-gray-400 uppercase font-bold tracking-widest">DEFENDERS</span>
             </div>
             <img :src="teamA.logo" class="object-contain w-10 h-10 drop-shadow-lg">
             <!-- Score A -->
             <div class="absolute top-0 right-0 flex items-center justify-center w-12 h-full border-l bg-black/50 border-white/10">
                 <span class="font-mono text-2xl font-bold text-white">{{ scoreA }}</span>
             </div>
        </div>

        <!-- Center Timer & Round -->
        <div class="bg-black text-white px-6 flex flex-col items-center justify-center border-b-4 border-[var(--rankit-neon)] z-10 relative mx-[-10px] clip-center">
            <span class="font-mono text-2xl font-black leading-none tracking-widest">{{ timer }}</span>
            <span class="text-[10px] font-bold text-[var(--rankit-neon)] uppercase mt-1">Round {{ round }}</span>
        </div>

        <!-- Team B -->
        <div class="relative flex items-center pl-12 pr-8 overflow-hidden text-white bg-gray-900 border-b-4 border-r-4 skew-box-right" :style="{ borderColor: teamB.color }">
             <!-- Score B -->
             <div class="absolute top-0 left-0 flex items-center justify-center w-12 h-full border-r bg-black/50 border-white/10">
                 <span class="font-mono text-2xl font-bold text-white">{{ scoreB }}</span>
             </div>
             <img :src="teamB.logo" class="object-contain w-10 h-10 mr-4 drop-shadow-lg">
             <div class="flex flex-col items-start">
                 <span class="text-xl font-bold leading-none tracking-wider uppercase font-display">{{ teamB.name }}</span>
                 <span class="text-[10px] text-gray-400 uppercase font-bold tracking-widest">ATTACKERS</span>
             </div>
        </div>

    </div>

  </div>
</template>

<style>
/* Reset Global extra por seguridad */
html, body, #__nuxt {
    background-color: transparent !important;
}
</style>

<style scoped>
:root { --rankit-neon: #bf00ff; }
.font-display { font-family: 'Chakra Petch', sans-serif; }

.skew-box-left { transform: skewX(-15deg); }
.skew-box-left > * { transform: skewX(15deg); }

.skew-box-right { transform: skewX(15deg); }
.skew-box-right > * { transform: skewX(-15deg); }

.clip-center { clip-path: polygon(10% 0, 90% 0, 100% 100%, 0% 100%); width: 140px; }
</style>