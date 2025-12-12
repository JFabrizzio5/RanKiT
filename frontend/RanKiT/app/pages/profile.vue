<script setup>
import { ref } from 'vue'

const activeTab = ref('active') // active, history, matches
const showTicketModal = ref(false)

const switchTab = (tab) => {
  activeTab.value = tab
}
</script>

<template>
  <div class="bg-[#0B0C15] min-h-screen font-sans pb-12 text-white">
    
    <div class="match-bar fixed top-0 left-0 w-full h-14 flex items-center justify-between px-6 lg:px-12 z-[60] shadow-lg">
        <div class="flex items-center gap-4">
            <div class="flex items-center gap-2">
                <span class="w-2 h-2 rounded-full bg-red-500 animate-pulse"></span>
                <span class="text-xs font-bold text-indigo-200 uppercase tracking-wider">Partida en Curso</span>
            </div>
            <div class="h-4 w-px bg-indigo-500/50"></div>
            <span class="text-sm font-bold text-white">Neon City Cup - Ronda 3 vs <span class="text-brand-cyan">Team Liquid</span></span>
        </div>
        <div class="flex items-center gap-3">
            <NuxtLink to="/lobby/M-9921" class="bg-indigo-600 hover:bg-indigo-500 text-white px-4 py-1.5 rounded text-xs font-bold transition flex items-center gap-2">
                <i class="fas fa-gamepad"></i> Ir al Lobby
            </NuxtLink>
            <button @click="showTicketModal = !showTicketModal" class="bg-red-500/10 hover:bg-red-500 hover:text-white text-red-400 border border-red-500/30 px-4 py-1.5 rounded text-xs font-bold transition flex items-center gap-2">
                <i class="fas fa-life-ring"></i> Reportar Problema
            </button>
        </div>
    </div>

    <TheNavbar class="!top-14 transition-all duration-300" />

    <main class="max-w-7xl mx-auto px-6 lg:px-8 py-8 grid grid-cols-1 lg:grid-cols-12 gap-8 pt-36">
        
        <aside class="lg:col-span-3 space-y-6">
            <div class="glass-panel rounded-2xl p-6 text-center relative overflow-hidden group">
                <div class="absolute top-0 left-0 w-full h-24 bg-gradient-to-b from-brand-purple/20 to-transparent"></div>
                <div class="relative z-10">
                    <div class="profile-avatar-container mx-auto mb-4 relative flex justify-center items-center">
                        <img 
                            src="https://i.pravatar.cc/150?img=12" 
                            alt="Avatar"
                            class="profile-avatar-img"
                        >
                        <div class="absolute bottom-1 right-1 bg-green-500 w-5 h-5 rounded-full border-2 border-[#151725] animate-pulse"></div>
                    </div>
                    
                    <h1 class="font-display font-bold text-2xl text-white">xSlayer99</h1>
                    <p class="text-gray-400 text-xs mb-4 flex items-center justify-center gap-1">
                        <img src="https://flagcdn.com/w20/mx.png" class="w-4 h-3 rounded-sm" alt="MX Flag"> México • Miembro desde 2023
                    </p>
                    <div class="flex justify-center gap-2 mb-6">
                        <span class="bg-brand-cyan/10 text-brand-cyan px-2 py-1 rounded text-[10px] font-bold uppercase border border-brand-cyan/20">Pro Player</span>
                        <span class="bg-purple-500/10 text-purple-400 px-2 py-1 rounded text-[10px] font-bold uppercase border border-purple-500/20">Capitán</span>
                    </div>
                    <div class="grid grid-cols-2 gap-2 text-left bg-black/30 p-3 rounded-lg border border-gray-700">
                        <div>
                            <div class="text-[10px] text-gray-500 uppercase font-bold">Win Rate</div>
                            <div class="text-green-400 font-mono font-bold">68.5%</div>
                        </div>
                        <div>
                            <div class="text-[10px] text-gray-500 uppercase font-bold">Torneos</div>
                            <div class="text-white font-mono font-bold">14</div>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="glass-panel rounded-2xl p-6">
                <h3 class="font-bold text-white text-sm mb-4 flex items-center justify-between">Ranking por Juego</h3>
                <div class="space-y-4">
                    <div class="bg-black/30 p-3 rounded-lg border border-gray-700">
                        <div class="flex items-center gap-3 mb-2">
                            <img src="https://img.icons8.com/color/48/valorant.png" class="w-8 h-8" alt="Valorant Icon">
                            <div class="flex-1">
                                <div class="text-xs text-gray-400">Valorant</div>
                                <div class="text-sm font-bold text-green-400">Ascendente 1</div>
                            </div>
                        </div>
                        <div class="rank-progress"><div class="rank-fill bg-green-500" style="width: 75%"></div></div>
                    </div>
                     <div class="bg-black/30 p-3 rounded-lg border border-gray-700">
                        <div class="flex items-center gap-3 mb-2">
                            <img src="https://img.icons8.com/color/48/league-of-legends.png" class="w-8 h-8" alt="LoL Icon">
                            <div class="flex-1">
                                <div class="text-xs text-gray-400">League of Legends</div>
                                <div class="text-sm font-bold text-blue-400">Platino IV</div>
                            </div>
                        </div>
                        <div class="rank-progress"><div class="rank-fill bg-blue-500" style="width: 45%"></div></div>
                    </div>
                </div>
            </div>
        </aside>

        <div class="lg:col-span-6 space-y-8">
            <div class="flex gap-6 border-b border-gray-800 pb-1">
                <button @click="switchTab('active')" :class="['font-bold text-sm pb-3 transition', activeTab === 'active' ? 'text-brand-cyan border-b-2 border-brand-cyan' : 'text-gray-500 hover:text-white']">Torneos Activos</button>
                <button @click="switchTab('history')" :class="['font-bold text-sm pb-3 transition', activeTab === 'history' ? 'text-brand-cyan border-b-2 border-brand-cyan' : 'text-gray-500 hover:text-white']">Historial</button>
            </div>

            <div v-show="activeTab === 'active'" class="space-y-4 animate-fade-in">
                 <div class="glass-panel p-5 rounded-xl border-l-4 border-l-green-500 relative overflow-hidden">
                    <div class="flex justify-between items-start mb-4 relative z-10">
                        <div class="flex gap-4">
                            <img src="https://img.icons8.com/color/96/valorant.png" class="w-12 h-12" alt="Valorant">
                            <div>
                                <h3 class="font-display font-bold text-lg text-white">Neon City Cup</h3>
                                <p class="text-xs text-green-400 font-bold flex items-center gap-1">
                                    <span class="w-1.5 h-1.5 bg-green-500 rounded-full animate-pulse"></span> En Curso
                                </p>
                            </div>
                        </div>
                        <div class="text-right">
                            <div class="text-xs text-gray-500 uppercase">Siguiente Match</div>
                            <div class="text-sm font-bold text-white">18:30 PM (Hoy)</div>
                        </div>
                    </div>
                    <div class="bg-gray-800 h-2 rounded-full mb-2 overflow-hidden">
                        <div class="bg-green-500 h-full rounded-full" style="width: 60%"></div>
                    </div>
                    <div class="flex justify-between text-[10px] text-gray-400 mb-4">
                        <span>Fase de Grupos</span>
                        <span>Playoffs</span>
                        <span>Final</span>
                    </div>
                    <div class="flex gap-3">
                        <NuxtLink to="/lobby/M-9921" class="flex-1 bg-green-600 hover:bg-green-500 text-white py-2 rounded-lg text-xs font-bold transition text-center flex items-center justify-center">
                            Ir a Sala de Juego
                        </NuxtLink>
                        <button class="px-4 bg-gray-800 hover:bg-gray-700 text-gray-300 py-2 rounded-lg text-xs font-bold transition border border-gray-700">Ayuda</button>
                    </div>
                </div>
            </div>

            <div v-show="activeTab === 'history'" class="space-y-4 animate-fade-in">
                 <div class="glass-panel p-4 rounded-xl flex items-center justify-between">
                    <div class="flex items-center gap-4">
                         <div class="w-10 h-10 rounded bg-gradient-to-br from-yellow-600 to-yellow-400 flex items-center justify-center text-black font-bold">
                             <i class="fas fa-trophy"></i>
                         </div>
                         <div>
                             <h4 class="font-bold text-white">Summer Championship 2024</h4>
                             <p class="text-xs text-gray-400">Valorant • Finalizado: 15 Ago 2024</p>
                         </div>
                    </div>
                    <div class="text-right">
                        <div class="text-brand-purple font-bold text-sm">Top 4</div>
                        <div class="text-[10px] text-green-400">Premio: $500 MXN</div>
                    </div>
                </div>
            </div>
        </div>

        <aside class="lg:col-span-3 space-y-6">
            <div class="glass-panel p-6 rounded-2xl border border-gray-700/50">
                <h3 class="font-bold text-white text-sm mb-4 flex items-center gap-2">
                    <i class="fas fa-life-ring text-brand-cyan"></i> Centro de Ayuda
                </h3>
                <div class="bg-blue-900/20 border border-blue-500/30 p-3 rounded-lg mb-4 cursor-pointer hover:bg-blue-900/30 transition" @click="showTicketModal = true">
                    <div class="flex justify-between items-start mb-1">
                        <span class="text-xs font-bold text-blue-300">#TKT-8821</span>
                        <span class="bg-blue-500 text-white text-[9px] px-1.5 py-0.5 rounded uppercase font-bold">Abierto</span>
                    </div>
                    <p class="text-xs text-gray-300 font-bold">Reporte de Jugador (AFK)</p>
                </div>
            </div>
        </aside>
    </main>

    <div v-if="showTicketModal" class="fixed inset-0 z-[100]">
        <div class="absolute inset-0 bg-black/60 backdrop-blur-sm" @click="showTicketModal = false"></div>
        <div class="absolute right-0 top-0 h-full w-full max-w-md bg-[#151725] border-l border-gray-700 shadow-2xl ticket-drawer flex flex-col">
            <div class="p-6 border-b border-gray-800 flex justify-between items-center bg-[#0B0C15]">
                <h2 class="font-display font-bold text-xl text-white">Soporte</h2>
                <button @click="showTicketModal = false" class="text-gray-400 hover:text-white"><i class="fas fa-times"></i></button>
            </div>
            <div class="flex-1 p-6 text-gray-400 text-sm">
                <p>Aquí iría el chat de soporte en tiempo real.</p>
            </div>
        </div>
    </div>
  </div>
</template>

<style scoped>
.match-bar { background: linear-gradient(90deg, #1e1b4b 0%, #312e81 100%); border-bottom: 1px solid #4338ca; }
.glass-panel { background: #151725; border: 1px solid rgba(255,255,255,0.05); }
.ticket-drawer { animation: slideIn 0.3s ease-out; }
@keyframes slideIn { from { transform: translateX(100%); } to { transform: translateX(0); } }
.rank-progress { background: rgba(255,255,255,0.1); height: 4px; border-radius: 2px; overflow: hidden; margin-top: 4px;}
.rank-fill { height: 100%; border-radius: 2px; }
.animate-fade-in { animation: fadeIn 0.3s ease; }
@keyframes fadeIn { from { opacity: 0; } to { opacity: 1; } }

.profile-avatar-container {
    width: 96px !important;
    height: 96px !important;
    min-width: 96px !important;
    min-height: 96px !important;
    border-radius: 9999px;
    padding: 0.25rem;
    background: linear-gradient(to right, #06B6D4, #7C3AED);
    flex-shrink: 0;
}

.profile-avatar-img {
    width: 100% !important;
    height: 100% !important;
    border-radius: 9999px;
    border: 4px solid #151725;
    object-fit: cover;
}
</style>