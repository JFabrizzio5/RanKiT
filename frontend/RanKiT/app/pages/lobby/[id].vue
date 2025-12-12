<script setup>
const route = useRoute()
const lobbyId = route.params.id || 'M-9921'
</script>

<template>
  <div class="bg-[#0B0C15] h-screen flex flex-col overflow-hidden font-sans text-white">
    <!-- Top Match Header -->
    <header class="bg-[#151725]/90 border-b border-white/5 h-20 flex items-center justify-between px-6 z-50 shrink-0 backdrop-blur-md">
        <div class="flex items-center gap-4">
            <NuxtLink to="/profile" class="text-gray-500 hover:text-white transition"><i class="fas fa-arrow-left"></i> Salir</NuxtLink>
            <div class="h-8 w-px bg-gray-800"></div>
            <div>
                <h1 class="font-display font-bold text-xl text-white">SALA DE JUEGO <span class="text-gray-500 text-sm">#{{ lobbyId }}</span></h1>
                <p class="text-xs text-brand-cyan uppercase font-bold tracking-widest">RankIT Competitive Series • Season 5</p>
            </div>
        </div>
        
        <div class="bg-black/40 border border-brand-cyan/30 rounded-lg px-6 py-2 flex flex-col items-center">
            <span class="text-[10px] text-brand-cyan font-bold uppercase tracking-widest mb-1">Fase de Veto</span>
            <div class="text-2xl font-mono font-bold text-white flex items-center gap-2">
                00:45 <i class="fas fa-clock text-xs text-gray-500 animate-pulse"></i>
            </div>
        </div>
    </header>

    <main class="flex-1 flex overflow-hidden">
        <!-- Team A -->
        <aside class="w-72 bg-[#0F1019] border-r border-gray-800 flex flex-col overflow-y-auto">
            <div class="p-4 border-b border-gray-800 bg-gradient-to-r from-blue-900/20 to-transparent">
                <h2 class="font-display font-bold text-lg text-blue-400">TEAM ALPHA</h2>
                <div class="text-xs text-gray-400">Total Elo: 8,450</div>
            </div>
            <div class="p-4 space-y-4">
                <!-- Player Card -->
                <div class="bg-[#151725] p-3 rounded-lg border border-blue-500/30 relative group">
                     <div class="absolute -top-2 -right-2 bg-yellow-500 text-black text-[9px] font-bold px-1.5 rounded-sm shadow-lg z-10">CAP</div>
                     <div class="flex items-center gap-3 mb-2">
                        <img src="https://i.pravatar.cc/150?img=12" class="w-10 h-10 rounded border-2 border-brand-cyan">
                        <div>
                            <div class="text-sm font-bold text-white">xSlayer99</div>
                            <div class="text-[10px] font-bold text-red-400">Grandmaster</div>
                        </div>
                    </div>
                </div>
            </div>
        </aside>

        <!-- Center Veto -->
        <section class="flex-1 relative flex flex-col">
             <div class="absolute inset-0 bg-[url('https://www.transparenttextures.com/patterns/carbon-fibre.png')] opacity-10 pointer-events-none"></div>
             <div class="flex-1 flex flex-col items-center justify-center p-8 z-10">
                <div class="text-center mb-8">
                    <h2 class="font-display font-bold text-2xl text-white mb-2">FASE DE VETO DE MAPAS</h2>
                    <p class="text-gray-400 text-sm">Turno de <span class="text-blue-400 font-bold">Team Alpha</span> para <span class="text-red-400 font-bold uppercase">Banear</span></p>
                </div>

                <div class="flex flex-wrap justify-center gap-4 max-w-4xl">
                    <!-- Map Card Active -->
                    <div class="w-40 h-56 bg-gray-900 rounded-lg border-2 border-transparent relative overflow-hidden group cursor-pointer map-card">
                        <img src="https://images.unsplash.com/photo-1511512578047-dfb367046420?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80" class="absolute inset-0 h-full w-full object-cover">
                        <div class="absolute inset-0 bg-red-900/60 opacity-0 group-hover:opacity-100 transition flex flex-col items-center justify-center z-10">
                            <button class="bg-red-500 text-white font-bold px-4 py-2 rounded text-xs shadow-lg uppercase">Banear</button>
                        </div>
                        <div class="absolute bottom-0 w-full p-3 text-center bg-black/60 z-0">
                            <span class="font-display font-bold text-lg text-white tracking-wider">HAVEN</span>
                        </div>
                    </div>
                    <!-- Map Card Picked -->
                    <div class="w-40 h-56 bg-gray-900 rounded-lg border-2 border-green-500 relative overflow-hidden picked">
                        <img src="https://images.unsplash.com/photo-1534438327276-14e5300c3a48?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80" class="absolute inset-0 h-full w-full object-cover grayscale-0">
                        <div class="absolute top-2 right-2 bg-green-500 text-black font-bold px-2 py-0.5 rounded text-[10px] uppercase shadow z-20">Pickeado</div>
                        <div class="absolute bottom-0 w-full p-3 text-center bg-black/60 z-10">
                            <span class="font-display font-bold text-lg text-white tracking-wider">BIND</span>
                        </div>
                    </div>
                </div>
             </div>
        </section>

        <!-- Team B -->
        <aside class="w-72 bg-[#0F1019] border-l border-gray-800 flex flex-col">
            <div class="p-4 border-b border-gray-800 bg-gradient-to-l from-red-900/20 to-transparent text-right">
                <h2 class="font-display font-bold text-lg text-red-400">TEAM OMEGA</h2>
                <div class="text-xs text-gray-400">Total Elo: 8,320</div>
            </div>
            <!-- Chat -->
            <div class="mt-auto flex flex-col h-1/3 border-t border-gray-800">
                <div class="bg-[#151725] p-2 text-xs font-bold text-gray-500 border-b border-gray-800 uppercase tracking-wider">Lobby Chat</div>
                <div class="flex-1 overflow-y-auto p-3 space-y-2">
                    <div class="text-xs"><span class="font-bold text-blue-400">xSlayer99:</span> Ban Ascent pls</div>
                </div>
                <div class="p-2 bg-[#0F1019]">
                    <input type="text" placeholder="Enviar mensaje..." class="w-full bg-black border border-gray-700 rounded px-3 py-2 text-xs text-white outline-none focus:border-brand-cyan">
                </div>
            </div>
        </aside>
    </main>
  </div>
</template>

<style scoped>
.map-card { transition: all 0.3s ease; filter: grayscale(100%); opacity: 0.6; }
.map-card:hover { filter: grayscale(0%); opacity: 1; transform: scale(1.05); border-color: #06B6D4; }
.picked { filter: grayscale(0%); opacity: 1; transform: scale(1.05); box-shadow: 0 0 20px rgba(16, 185, 129, 0.4); }
</style>