<script setup>
import { ref, computed, nextTick, onMounted } from 'vue'

const route = useRoute()
const lobbyId = route.params.id || 'M-9921'

// --- ESTADO DEL JUEGO ---
const gamePhase = ref('veto') // 'veto', 'ready', 'starting'
const turn = ref('team_a') // 'team_a', 'team_b'
const actionType = ref('BAN') // 'BAN', 'PICK'
const timer = ref(30)
const userTeam = 'team_a' // Simulamos que somos el Team Alpha

// --- MAP POOL DATA ---
const maps = ref([
  { id: 1, name: 'Ascent', img: 'https://images.unsplash.com/photo-1542751371-adc38448a05e?q=80&w=600', status: 'available' },
  { id: 2, name: 'Bind', img: 'https://images.unsplash.com/photo-1534438327276-14e5300c3a48?q=80&w=600', status: 'available' },
  { id: 3, name: 'Haven', img: 'https://images.unsplash.com/photo-1511512578047-dfb367046420?q=80&w=600', status: 'available' },
  { id: 4, name: 'Split', img: 'https://images.unsplash.com/photo-1624138784181-2999e4253b85?q=80&w=600', status: 'available' },
  { id: 5, name: 'Lotus', img: 'https://images.unsplash.com/photo-1518546305927-5a555bb7020d?q=80&w=600', status: 'available' },
  { id: 6, name: 'Pearl', img: 'https://images.unsplash.com/photo-1496568816309-51d7c20e3b21?q=80&w=600', status: 'available' },
  { id: 7, name: 'Fracture', img: 'https://images.unsplash.com/photo-1565514020176-db79238b6d3a?q=80&w=600', status: 'available' }
])

// --- CHAT SYSTEM ---
const chatInput = ref('')
const chatContainer = ref(null)
const messages = ref([
  { id: 1, type: 'system', text: 'La sala ha sido creada.' },
  { id: 2, type: 'system', text: 'Fase de Veto iniciada. Team Alpha comienza baneando.' },
  { id: 3, type: 'user', user: 'xSlayer99', team: 'team_a', text: 'Ban Ascent pls, no lo hemos practicado.' },
  { id: 4, type: 'user', user: 'ViperMain', team: 'team_b', text: 'GL HF!' },
])

const sendMessage = () => {
  if (!chatInput.value.trim()) return
  messages.value.push({
    id: Date.now(),
    type: 'user',
    user: 'xSlayer99', // Usuario actual simulado
    team: 'team_a',
    text: chatInput.value
  })
  chatInput.value = ''
  scrollToBottom()
}

const scrollToBottom = async () => {
  await nextTick()
  if (chatContainer.value) chatContainer.value.scrollTop = chatContainer.value.scrollHeight
}

// --- VETO LOGIC ---
const handleMapClick = (mapId) => {
  // Solo permitir interacción si es turno del usuario y el mapa está disponible
  if (turn.value !== userTeam) return 
  
  const mapIndex = maps.value.findIndex(m => m.id === mapId)
  if (maps.value[mapIndex].status !== 'available') return

  // Aplicar acción
  maps.value[mapIndex].status = actionType.value === 'BAN' ? 'banned' : 'picked'
  
  // Agregar mensaje al chat
  messages.value.push({
    id: Date.now(),
    type: 'action',
    text: `${actionType.value === 'BAN' ? '🚫 BANEÓ' : '✅ ELIGIÓ'} ${maps.value[mapIndex].name.toUpperCase()}`,
    team: turn.value
  })

  // Cambiar turno (Lógica simple para demo)
  if (actionType.value === 'BAN') {
      turn.value = turn.value === 'team_a' ? 'team_b' : 'team_a'
      // Si quedan pocos mapas, cambiar a PICK (simulado)
      const availableMaps = maps.value.filter(m => m.status === 'available').length
      if (availableMaps <= 3) actionType.value = 'PICK'
  } else {
      gamePhase.value = 'ready' // Fin del veto
  }

  timer.value = 30 // Reset timer
  scrollToBottom()
}

// --- PLAYERS DATA ---
const teamA = [
    { name: 'xSlayer99', avatar: 'https://i.pravatar.cc/150?img=12', role: 'CAP', ready: true },
    { name: 'JettLag', avatar: 'https://i.pravatar.cc/150?img=3', role: '', ready: true },
    { name: 'SmokeMid', avatar: 'https://i.pravatar.cc/150?img=59', role: '', ready: false },
    { name: 'HealerOnly', avatar: 'https://i.pravatar.cc/150?img=44', role: '', ready: true },
    { name: 'Lurker', avatar: 'https://i.pravatar.cc/150?img=60', role: '', ready: true }
]

const teamB = [
    { name: 'ViperMain', avatar: 'https://i.pravatar.cc/150?img=5', role: 'CAP', ready: true },
    { name: 'OpCrutch', avatar: 'https://i.pravatar.cc/150?img=11', role: '', ready: true },
    { name: 'LineupsNerd', avatar: 'https://i.pravatar.cc/150?img=65', role: '', ready: true },
    { name: 'EntryFrag', avatar: 'https://i.pravatar.cc/150?img=33', role: '', ready: true },
    { name: 'IGL_Pro', avatar: 'https://i.pravatar.cc/150?img=68', role: '', ready: false }
]

// Timer Simulation
onMounted(() => {
    setInterval(() => {
        if(timer.value > 0 && gamePhase.value === 'veto') timer.value--
    }, 1000)
})

// Dynamic Background based on Picked Map
const backgroundMap = computed(() => {
    const picked = maps.value.find(m => m.status === 'picked')
    return picked ? picked.img : 'https://www.transparenttextures.com/patterns/carbon-fibre.png'
})
</script>

<template>
  <div class="bg-[#0B0C15] h-screen flex flex-col overflow-hidden font-sans text-white relative transition-all duration-1000">
    <div class="absolute inset-0 z-0 transition-opacity duration-1000" 
         :class="gamePhase === 'ready' ? 'opacity-20' : 'opacity-5'">
         <img v-if="gamePhase === 'ready'" :src="backgroundMap" class="w-full h-full object-cover grayscale">
         <div v-else class="w-full h-full bg-[url('https://www.transparenttextures.com/patterns/carbon-fibre.png')]"></div>
    </div>
    <div class="absolute inset-0 bg-gradient-to-b from-[#0B0C15]/90 via-[#0B0C15]/80 to-[#0B0C15]/90 z-0"></div>

    <header class="h-20 flex items-center justify-between px-6 z-50 shrink-0 border-b border-white/5 backdrop-blur-md relative">
        <div class="flex items-center gap-4">
            <NuxtLink to="/profile" class="text-gray-500 hover:text-white transition flex items-center gap-2 text-xs font-bold uppercase tracking-wider">
                <i class="fas fa-arrow-left"></i> Salir
            </NuxtLink>
            <div class="h-8 w-px bg-gray-800"></div>
            <div>
                <h1 class="font-display font-bold text-xl text-white flex items-center gap-2">
                    LOBBY <span class="text-gray-600">#{{ lobbyId }}</span>
                    <span v-if="gamePhase === 'ready'" class="bg-green-500 text-black text-[10px] px-2 py-0.5 rounded font-bold uppercase animate-pulse">Ready to Start</span>
                </h1>
                <p class="text-[10px] text-brand-cyan uppercase font-bold tracking-[0.2em]">RankIT Competitive Series • Best of 1</p>
            </div>
        </div>
        
        <div class="absolute left-1/2 top-1/2 -translate-x-1/2 -translate-y-1/2 flex flex-col items-center">
            <div class="bg-black/50 border border-gray-700 backdrop-blur-md rounded-lg px-8 py-2 flex flex-col items-center min-w-[200px] shadow-2xl transition-all duration-300"
                 :class="{'border-red-500/50 shadow-red-900/20': timer < 10 && gamePhase === 'veto'}">
                <span class="text-[10px] font-bold uppercase tracking-widest mb-1" 
                      :class="turn === 'team_a' ? 'text-blue-400' : 'text-red-400'">
                    {{ gamePhase === 'veto' ? (turn === 'team_a' ? 'Turno: Team Alpha' : 'Turno: Team Omega') : 'Esperando Inicio' }}
                </span>
                <div class="text-3xl font-mono font-bold text-white flex items-center gap-2 leading-none">
                    <span v-if="gamePhase === 'veto'">00:{{ timer < 10 ? '0'+timer : timer }}</span>
                    <span v-else>READY</span>
                </div>
            </div>
             <div v-if="gamePhase === 'veto'" class="mt-2 text-[10px] font-bold bg-gray-800 px-2 py-0.5 rounded text-white border border-gray-600">
                FASE: <span :class="actionType === 'BAN' ? 'text-red-500' : 'text-green-500'">{{ actionType }}</span>
            </div>
        </div>

        <div class="flex items-center gap-3">
             <button class="w-8 h-8 rounded-full bg-gray-800 hover:bg-gray-700 flex items-center justify-center text-gray-400 transition">
                 <i class="fas fa-cog"></i>
             </button>
             <button class="w-8 h-8 rounded-full bg-gray-800 hover:bg-gray-700 flex items-center justify-center text-gray-400 transition">
                 <i class="fas fa-volume-up"></i>
             </button>
        </div>
    </header>

    <main class="flex-1 flex overflow-hidden relative z-10">
        <aside class="w-80 bg-[#0F1019]/80 border-r border-gray-800 flex flex-col backdrop-blur-sm">
            <div class="p-6 border-b border-gray-800 bg-gradient-to-r from-blue-900/10 to-transparent">
                <div class="flex justify-between items-start">
                    <h2 class="font-display font-bold text-2xl text-blue-400">ALPHA</h2>
                    <span class="text-xs font-bold text-gray-500 bg-black/30 px-2 py-1 rounded">DEF</span>
                </div>
                <div class="text-xs text-gray-400 mt-1">Average Elo: 1,450</div>
            </div>
            <div class="p-4 space-y-2 overflow-y-auto flex-1 custom-scroll">
                <div v-for="(player, i) in teamA" :key="i" class="bg-[#151725] p-3 rounded-lg border border-transparent hover:border-blue-500/30 transition group flex items-center gap-3 relative overflow-hidden">
                     <div class="absolute left-0 top-0 bottom-0 w-1" :class="player.ready ? 'bg-green-500' : 'bg-gray-700'"></div>
                     
                     <div class="relative">
                        <img :src="player.avatar" class="w-10 h-10 rounded border border-gray-700 group-hover:border-blue-400 transition">
                        <div v-if="player.role === 'CAP'" class="absolute -top-2 -right-2 bg-yellow-500 text-black text-[8px] font-bold px-1.5 py-0.5 rounded shadow-lg z-10">CAP</div>
                     </div>
                     <div class="flex-1">
                        <div class="flex justify-between items-center">
                            <div class="text-sm font-bold text-white group-hover:text-blue-300 transition">{{ player.name }}</div>
                            <i v-if="player.ready" class="fas fa-check text-green-500 text-xs"></i>
                        </div>
                        <div class="text-[10px] text-gray-500">Duelist Main</div>
                    </div>
                </div>
            </div>
             <div class="p-4 border-t border-gray-800 bg-[#0B0C15]/50">
                 <button class="w-full py-3 rounded-lg font-bold text-sm uppercase tracking-wider transition shadow-lg relative overflow-hidden group"
                         :class="gamePhase === 'ready' ? 'bg-green-600 hover:bg-green-500 text-white' : 'bg-gray-800 text-gray-500 cursor-not-allowed'">
                     <span class="relative z-10 flex items-center justify-center gap-2">
                         <i v-if="gamePhase === 'ready'" class="fas fa-play"></i>
                         {{ gamePhase === 'ready' ? 'INICIAR PARTIDA' : 'ESPERANDO VETO' }}
                     </span>
                 </button>
             </div>
        </aside>

        <section class="flex-1 relative flex flex-col">
             <div class="flex-1 flex flex-col items-center justify-center p-8 z-10 overflow-y-auto">
                
                <div class="text-center mb-10 animate-fade-in-up">
                    <h2 class="font-display font-bold text-4xl text-white mb-2 drop-shadow-xl tracking-tight">VETO DE MAPAS</h2>
                    <p class="text-gray-300 text-sm bg-black/40 px-4 py-2 rounded-full border border-white/10 backdrop-blur inline-block">
                        <span v-if="gamePhase === 'veto'">
                            Turno de <span :class="turn === 'team_a' ? 'text-blue-400' : 'text-red-400'" class="font-bold uppercase">{{ turn === 'team_a' ? 'Team Alpha' : 'Team Omega' }}</span> para 
                            <span :class="actionType === 'BAN' ? 'text-red-500' : 'text-green-500'" class="font-bold uppercase">{{ actionType === 'BAN' ? 'Banear 🚫' : 'Elegir ✅' }}</span>
                        </span>
                        <span v-else class="text-green-400 font-bold uppercase flex items-center gap-2">
                            <i class="fas fa-check-circle"></i> Veto Completado
                        </span>
                    </p>
                </div>

                <div class="flex flex-wrap justify-center gap-6 max-w-5xl">
                    <div v-for="map in maps" :key="map.id" 
                         @click="handleMapClick(map.id)"
                         class="relative w-36 h-52 md:w-44 md:h-64 rounded-xl transition-all duration-300 ease-out group select-none"
                         :class="{
                             'cursor-pointer hover:-translate-y-2 hover:shadow-[0_0_20px_rgba(6,182,212,0.3)]': map.status === 'available' && turn === userTeam,
                             'opacity-50 grayscale scale-95 pointer-events-none': map.status === 'banned',
                             'scale-105 ring-4 ring-green-500 shadow-[0_0_30px_rgba(16,185,129,0.4)] z-10': map.status === 'picked',
                             'opacity-80': map.status === 'available' && turn !== userTeam
                         }">
                        
                        <div class="absolute inset-0 rounded-xl overflow-hidden bg-gray-900 border border-white/10">
                            <img :src="map.img" class="w-full h-full object-cover transition duration-700 group-hover:scale-110">
                            <div class="absolute inset-0 bg-gradient-to-t from-black via-transparent to-transparent"></div>
                            
                            <div v-if="map.status === 'banned'" class="absolute inset-0 bg-black/60 flex items-center justify-center flex-col">
                                <i class="fas fa-ban text-red-500 text-4xl mb-2"></i>
                                <span class="text-red-500 font-bold uppercase text-xs tracking-widest border border-red-500 px-2 py-0.5 rounded">Banned</span>
                            </div>

                            <div v-if="map.status === 'picked'" class="absolute inset-0 bg-green-500/20 flex items-center justify-center">
                                <div class="bg-green-500 text-black font-bold px-3 py-1 rounded shadow-lg uppercase text-xs transform -rotate-3">
                                    <i class="fas fa-check mr-1"></i> Picked
                                </div>
                            </div>
                        </div>

                        <div v-if="map.status === 'available' && turn === userTeam" 
                             class="absolute inset-0 flex items-center justify-center opacity-0 group-hover:opacity-100 transition duration-200 bg-black/40 backdrop-blur-[2px] rounded-xl z-20">
                            <button :class="['font-bold px-4 py-2 rounded uppercase text-xs shadow-lg transform scale-90 group-hover:scale-100 transition', actionType === 'BAN' ? 'bg-red-500 text-white' : 'bg-green-500 text-black']">
                                {{ actionType }} MAP
                            </button>
                        </div>

                        <div class="absolute bottom-0 w-full p-4 text-center z-20">
                            <span class="font-display font-bold text-lg md:text-xl text-white tracking-wider drop-shadow-md uppercase">{{ map.name }}</span>
                        </div>
                    </div>
                </div>
             </div>
        </section>

        <aside class="w-80 bg-[#0F1019]/80 border-l border-gray-800 flex flex-col backdrop-blur-sm">
            <div class="p-6 border-b border-gray-800 bg-gradient-to-l from-red-900/10 to-transparent text-right">
                <div class="flex justify-between items-start flex-row-reverse">
                    <h2 class="font-display font-bold text-2xl text-red-400">OMEGA</h2>
                    <span class="text-xs font-bold text-gray-500 bg-black/30 px-2 py-1 rounded">ATK</span>
                </div>
                <div class="text-xs text-gray-400 mt-1">Average Elo: 1,420</div>
            </div>

            <div class="p-4 space-y-2 flex-shrink-0">
                <div v-for="(player, i) in teamB" :key="i" class="bg-[#151725] p-2 rounded-lg border border-transparent flex items-center justify-end gap-3 opacity-80">
                    <div class="text-right">
                        <div class="text-sm font-bold text-gray-300">{{ player.name }}</div>
                        <div class="text-[9px] text-gray-500">Controller Main</div>
                    </div>
                    <img :src="player.avatar" class="w-8 h-8 rounded border border-gray-700 filter grayscale">
                </div>
            </div>

            <div class="flex-1 flex flex-col border-t border-gray-800 bg-black/20 min-h-0">
                <div class="bg-[#151725] px-4 py-2 text-[10px] font-bold text-gray-500 border-b border-gray-800 uppercase tracking-wider flex justify-between items-center">
                    <span>Lobby Chat</span>
                    <span class="w-2 h-2 rounded-full bg-green-500"></span>
                </div>
                
                <div ref="chatContainer" class="flex-1 overflow-y-auto p-3 space-y-3 custom-scroll">
                    <div v-for="msg in messages" :key="msg.id" class="text-xs animate-fade-in">
                        <div v-if="msg.type === 'system'" class="text-yellow-500/80 font-mono text-[10px] text-center my-2 italic border-y border-yellow-500/10 py-1">
                            >> {{ msg.text }}
                        </div>
                        <div v-else-if="msg.type === 'action'" class="flex items-center gap-2 justify-center opacity-70 my-2">
                             <span class="h-px w-4 bg-gray-600"></span>
                             <span class="font-bold uppercase text-[10px]" :class="msg.team === 'team_a' ? 'text-blue-400' : 'text-red-400'">{{ msg.team === 'team_a' ? 'ALPHA' : 'OMEGA' }}</span>
                             <span class="text-gray-400 text-[10px]">{{ msg.text }}</span>
                             <span class="h-px w-4 bg-gray-600"></span>
                        </div>
                        <div v-else class="break-words">
                            <span class="font-bold cursor-pointer hover:underline" :class="msg.team === 'team_a' ? 'text-blue-400' : 'text-red-400'">
                                {{ msg.user }}:
                            </span>
                            <span class="text-gray-300 ml-1">{{ msg.text }}</span>
                        </div>
                    </div>
                </div>

                <div class="p-3 bg-[#0F1019]">
                    <div class="relative">
                        <input type="text" 
                               v-model="chatInput" 
                               @keyup.enter="sendMessage"
                               placeholder="Enviar mensaje al lobby..." 
                               class="w-full bg-black border border-gray-700 rounded-lg pl-3 pr-10 py-2.5 text-xs text-white outline-none focus:border-brand-cyan transition focus:shadow-[0_0_10px_rgba(6,182,212,0.2)]">
                        <button @click="sendMessage" class="absolute right-2 top-1.5 text-gray-500 hover:text-white transition p-1">
                            <i class="fas fa-paper-plane text-xs"></i>
                        </button>
                    </div>
                </div>
            </div>
        </aside>
    </main>
  </div>
</template>

<style scoped>
/* Scrollbar */
.custom-scroll::-webkit-scrollbar { width: 4px; }
.custom-scroll::-webkit-scrollbar-track { background: rgba(0,0,0,0.1); }
.custom-scroll::-webkit-scrollbar-thumb { background: #374151; border-radius: 4px; }
.custom-scroll::-webkit-scrollbar-thumb:hover { background: #4B5563; }

/* Animaciones */
.animate-fade-in-up { animation: fadeInUp 0.6s cubic-bezier(0.2, 1, 0.3, 1) forwards; }
.animate-fade-in { animation: fadeIn 0.3s ease-out; }

@keyframes fadeInUp { from { opacity: 0; transform: translateY(20px); } to { opacity: 1; transform: translateY(0); } }
@keyframes fadeIn { from { opacity: 0; } to { opacity: 1; } }

/* Glitch effect on hover for header text maybe? */
</style>