<script setup>
import { onMounted } from 'vue'

// Usar layout overlay
definePageMeta({
  layout: 'overlay'
})

// --- SOLUCIÓN DEFINITIVA OBS ---
// Inyectamos estilos críticos directamente en el head para evitar el fondo blanco
// antes de que cargue el JS.
useHead({
  bodyAttrs: {
    style: 'background-color: transparent !important; background: transparent !important; overflow: hidden;'
  },
  htmlAttrs: {
    style: 'background-color: transparent !important; background: transparent !important;'
  }
})

onMounted(() => {
    if (process.client) {
        // Redundancia por seguridad
        document.documentElement.style.setProperty('background-color', 'transparent', 'important');
        document.body.style.setProperty('background-color', 'transparent', 'important');
        const app = document.getElementById('__nuxt');
        if (app) app.style.setProperty('background-color', 'transparent', 'important');
    }
})

const players = [
    { rank: 1, name: 'xSlayer99', team: 'Liquid', logo: 'https://upload.wikimedia.org/wikipedia/en/thumb/f/f4/Team_Liquid_logo_2017.svg/1200px-Team_Liquid_logo_2017.svg.png', k: 24, d: 10, a: 5, score: 345 },
    { rank: 2, name: 'ViperMain', team: 'Sentinels', logo: 'https://upload.wikimedia.org/wikipedia/commons/6/6b/Sentinels_Logo.png', k: 21, d: 12, a: 8, score: 310 },
    { rank: 3, name: 'JettLag', team: 'Liquid', logo: 'https://upload.wikimedia.org/wikipedia/en/thumb/f/f4/Team_Liquid_logo_2017.svg/1200px-Team_Liquid_logo_2017.svg.png', k: 18, d: 15, a: 2, score: 280 },
    { rank: 4, name: 'OpCrutch', team: 'Sentinels', logo: 'https://upload.wikimedia.org/wikipedia/commons/6/6b/Sentinels_Logo.png', k: 15, d: 15, a: 4, score: 250 },
    { rank: 5, name: 'SmokeMid', team: 'Liquid', logo: 'https://upload.wikimedia.org/wikipedia/en/thumb/f/f4/Team_Liquid_logo_2017.svg/1200px-Team_Liquid_logo_2017.svg.png', k: 12, d: 18, a: 12, score: 210 },
    { rank: 6, name: 'HealerOnly', team: 'Liquid', logo: 'https://upload.wikimedia.org/wikipedia/en/thumb/f/f4/Team_Liquid_logo_2017.svg/1200px-Team_Liquid_logo_2017.svg.png', k: 10, d: 12, a: 15, score: 195 },
    { rank: 7, name: 'EntryFrag', team: 'Sentinels', logo: 'https://upload.wikimedia.org/wikipedia/commons/6/6b/Sentinels_Logo.png', k: 9, d: 19, a: 3, score: 180 },
    { rank: 8, name: 'LineupsNerd', team: 'Sentinels', logo: 'https://upload.wikimedia.org/wikipedia/commons/6/6b/Sentinels_Logo.png', k: 8, d: 20, a: 5, score: 150 },
    { rank: 9, name: 'Lurker', team: 'Liquid', logo: 'https://upload.wikimedia.org/wikipedia/en/thumb/f/f4/Team_Liquid_logo_2017.svg/1200px-Team_Liquid_logo_2017.svg.png', k: 5, d: 22, a: 2, score: 120 },
    { rank: 10, name: 'IGL_Pro', team: 'Sentinels', logo: 'https://upload.wikimedia.org/wikipedia/commons/6/6b/Sentinels_Logo.png', k: 4, d: 23, a: 8, score: 110 },
]

const getRankColor = (rank) => {
    if(rank === 1) return 'text-[#FFD700] drop-shadow-[0_2px_0_rgba(0,0,0,1)]'
    if(rank === 2) return 'text-[#C0C0C0] drop-shadow-[0_2px_0_rgba(0,0,0,1)]'
    if(rank === 3) return 'text-[#CD7F32] drop-shadow-[0_2px_0_rgba(0,0,0,1)]'
    return 'text-white'
}
</script>

<template>
  <!-- 
      IMPORTANTE: bg-transparent explícito en el contenedor raíz 
      y estilo inline con !important para asegurar prioridad sobre Tailwind
  -->
  <div class="flex items-center justify-center w-full min-h-screen p-4 font-sans bg-transparent" style="background: transparent !important;">
    
    <!-- Tarjeta Principal (Esta SÍ tiene fondo) -->
    <div class="w-full max-w-6xl bg-[#09090b] border-2 border-[var(--rankit-neon)] relative overflow-hidden shadow-[0_0_0_1px_rgba(0,0,0,1),0_10px_40px_rgba(0,0,0,0.8)]">
        
        <!-- Fondo Tecnológico (Solo dentro de la tarjeta) -->
        <div class="absolute inset-0 bg-[linear-gradient(to_right,#ffffff05_1px,transparent_1px),linear-gradient(to_bottom,#ffffff05_1px,transparent_1px)] bg-[size:24px_24px]"></div>
        <div class="absolute top-0 right-0 w-[500px] h-[500px] bg-[var(--rankit-neon)]/10 rounded-full blur-[120px] pointer-events-none"></div>
        <div class="absolute bottom-0 left-0 w-[300px] h-[300px] bg-blue-600/10 rounded-full blur-[100px] pointer-events-none"></div>

        <!-- Header -->
        <div class="relative z-10 flex items-end justify-between p-6 bg-black border-b border-white/10">
            <div class="flex items-center gap-4">
                <div class="w-2 h-12 bg-[var(--rankit-neon)] shadow-[0_0_15px_var(--rankit-neon)]"></div>
                <div>
                    <h2 class="font-display font-bold text-[var(--rankit-neon)] text-sm tracking-[0.3em] uppercase mb-1">RankIT Series</h2>
                    <h1 class="text-5xl italic font-black leading-none tracking-tighter text-white uppercase font-display">Leaderboard</h1>
                </div>
            </div>
            <div class="text-right">
                <div class="mb-1 font-mono text-xs font-bold tracking-widest text-gray-400 uppercase">Map Result</div>
                <div class="font-display font-bold text-3xl text-white bg-[#1a1a1a] px-6 py-2 border-l-4 border-blue-500">
                    ASCENT
                </div>
            </div>
        </div>

        <!-- Table -->
        <div class="relative z-10">
            <table class="w-full text-left border-collapse">
                <thead class="bg-[#111] text-gray-400 text-xs font-bold uppercase tracking-widest border-b border-white/10">
                    <tr>
                        <th class="p-4 pl-8">#</th>
                        <th class="p-4">Player</th>
                        <th class="p-4 text-center">Team</th>
                        <th class="p-4 text-center text-green-400">Kills</th>
                        <th class="p-4 text-center text-red-400">Deaths</th>
                        <th class="p-4 text-center text-blue-400">Assists</th>
                        <th class="p-4 pr-8 text-right text-white">Score</th>
                    </tr>
                </thead>
                <tbody class="text-sm font-semibold text-white">
                    <tr v-for="(p, index) in players" :key="p.rank" 
                        class="transition-colors border-b border-white/5"
                        :class="index % 2 === 0 ? 'bg-white/[0.02]' : 'bg-transparent'">
                        
                        <!-- Rank -->
                        <td class="w-20 p-3 pl-8 text-2xl font-black text-center font-display" :class="getRankColor(p.rank)">
                            {{ p.rank }}
                        </td>
                        
                        <!-- Player -->
                        <td class="p-3">
                            <div class="flex items-center gap-4">
                                <div class="w-12 h-12 bg-gray-800 border-2 border-white/10 overflow-hidden relative skew-x-[-10deg]">
                                    <img :src="`https://i.pravatar.cc/150?u=${p.name}`" class="w-full h-full object-cover skew-x-[10deg] scale-110">
                                </div>
                                <span class="text-xl font-bold tracking-wide uppercase font-display">{{ p.name }}</span>
                            </div>
                        </td>
                        
                        <!-- Team -->
                        <td class="p-3 text-center">
                            <div class="flex items-center justify-center gap-2 opacity-80">
                                <img :src="p.logo" class="object-contain w-8 h-8 drop-shadow-md">
                                <span class="text-[10px] font-bold uppercase tracking-wider hidden md:inline-block bg-black/50 px-2 py-1 rounded">{{ p.team }}</span>
                            </div>
                        </td>

                        <!-- Stats -->
                        <td class="p-3 font-mono text-xl font-bold text-center text-green-400">{{ p.k }}</td>
                        <td class="p-3 font-mono text-xl font-bold text-center text-red-400">{{ p.d }}</td>
                        <td class="p-3 font-mono text-xl font-bold text-center text-blue-400">{{ p.a }}</td>
                        
                        <!-- Score -->
                        <td class="p-3 pr-8 text-right">
                            <div class="relative inline-block">
                                <span class="relative z-10 text-3xl font-black text-white font-display">{{ p.score }}</span>
                                <div class="absolute -bottom-1 w-full h-2 bg-[var(--rankit-neon)]/50 -skew-x-12"></div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        
        <!-- Footer -->
        <div class="flex items-center justify-between p-3 px-8 bg-black border-t border-white/10">
            <div class="flex items-center gap-2">
                <span class="w-2 h-2 bg-red-500 rounded-full animate-pulse"></span>
                <span class="text-[10px] text-white font-bold uppercase tracking-[0.2em]">Live Data Feed</span>
            </div>
            <span class="text-[10px] text-gray-600 font-bold uppercase tracking-widest">Powered by RankIT</span>
        </div>
    </div>

  </div>
</template>

<style>
/* REGLA DE ORO PARA OBS:
    Sobrescribir absolutamente todo lo que pueda poner fondo blanco.
    Esto aplica fuera del scope del componente (global).
*/
:root, html, body, #__nuxt, #__layout {
    background-color: transparent !important;
    background: none !important;
}
</style>

<style scoped>
:root { --rankit-neon: #bf00ff; }
.font-display { font-family: 'Chakra Petch', sans-serif; }
</style>