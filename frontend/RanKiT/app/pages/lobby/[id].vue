<script setup>
import { ref, computed, nextTick, onMounted } from 'vue'
import AppLogo from '@/components/AppLogo.vue'

const route = useRoute()
const lobbyId = route.params.id || 'M-9921'

// --- Lógica Global (Tema y Lenguaje) ---
const currentLang = ref('es')
const toggleLanguage = () => currentLang.value = currentLang.value === 'es' ? 'en' : 'es'
const isDark = ref(true)
const toggleTheme = () => { isDark.value = !isDark.value; updateTheme() }
const updateTheme = () => {
    if (process.client) {
        document.documentElement.classList.toggle('dark', isDark.value)
        localStorage.setItem('theme', isDark.value ? 'dark' : 'light')
    }
}
onMounted(() => {
    if (process.client) {
        const saved = localStorage.getItem('theme')
        isDark.value = saved === 'dark' || (!saved && window.matchMedia('(prefers-color-scheme: dark)').matches)
        updateTheme()
        
        // Timer simulation
        setInterval(() => {
            if(timer.value > 0 && gamePhase.value === 'veto') timer.value--
        }, 1000)
    }
})

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

// Dynamic Background based on Picked Map
const backgroundMap = computed(() => {
    const picked = maps.value.find(m => m.status === 'picked')
    return picked ? picked.img : 'https://www.transparenttextures.com/patterns/carbon-fibre.png'
})
</script>

<template>
  <div class="relative flex flex-col h-screen overflow-hidden font-sans transition-all duration-300"
       :class="isDark ? 'bg-[#0B0C15] text-white' : 'bg-gray-100 text-gray-900'">
    
    <!-- Immersive Background -->
    <div class="absolute inset-0 z-0 transition-opacity duration-1000 pointer-events-none" 
         :class="gamePhase === 'ready' ? 'opacity-20' : 'opacity-5'">
         <img v-if="gamePhase === 'ready'" :src="backgroundMap" class="object-cover w-full h-full grayscale">
         <div v-else class="w-full h-full bg-[length:40px_40px]" :class="isDark ? 'bg-tech-grid-dark' : 'bg-tech-grid-light'"></div>
    </div>
    
    <!-- Gradient Overlay (Dark Mode Only) -->
    <div class="absolute inset-0 z-0 pointer-events-none bg-gradient-to-b"
         :class="isDark ? 'from-[#0B0C15]/90 via-[#0B0C15]/80 to-[#0B0C15]/90' : 'from-gray-100/90 via-gray-100/50 to-gray-100/90'"></div>

    <!-- Header / Navbar Unificado -->
    <header class="relative z-50 flex items-center justify-between h-20 px-6 transition-colors duration-300 border-b shrink-0 backdrop-blur-md"
            :class="isDark ? 'border-white/5 bg-[#0B0C15]/80' : 'border-gray-200 bg-white/80'">
        
        <div class="flex items-center gap-4">
            <NuxtLink to="/" class="flex items-center gap-2 group">
                <div class="w-8 h-8 transition-colors" :class="isDark ? 'text-white' : 'text-black'">
                    <AppLogo />
                </div>
            </NuxtLink>
            
            <div class="w-px h-8" :class="isDark ? 'bg-gray-800' : 'bg-gray-300'"></div>
            
            <NuxtLink to="/profile" class="flex items-center gap-2 text-xs font-bold tracking-wider uppercase transition"
                      :class="isDark ? 'text-gray-500 hover:text-white' : 'text-gray-500 hover:text-black'">
                <i class="fas fa-arrow-left"></i> Salir
            </NuxtLink>

            <div class="hidden w-px h-8 md:block" :class="isDark ? 'bg-gray-800' : 'bg-gray-300'"></div>
            
            <div class="hidden md:block">
                <h1 class="flex items-center gap-2 text-xl font-bold font-display" :class="isDark ? 'text-white' : 'text-black'">
                    LOBBY <span class="text-gray-500">#{{ lobbyId }}</span>
                </h1>
            </div>
        </div>
        
        <!-- Center Game State Widget -->
        <div class="absolute flex flex-col items-center -translate-x-1/2 -translate-y-1/2 left-1/2 top-1/2">
            <div class="border backdrop-blur-md rounded-lg px-8 py-2 flex flex-col items-center min-w-[200px] shadow-xl transition-all duration-300"
                 :class="[
                    timer < 10 && gamePhase === 'veto' ? 'border-red-500/50 shadow-red-900/20' : '',
                    isDark ? 'bg-black/50 border-gray-700' : 'bg-white/80 border-gray-300'
                 ]">
                
                <span class="text-[10px] font-bold uppercase tracking-widest mb-1" 
                      :class="turn === 'team_a' ? 'text-blue-500' : 'text-red-500'">
                    {{ gamePhase === 'veto' ? (turn === 'team_a' ? 'Turno: Team Alpha' : 'Turno: Team Omega') : 'Esperando Inicio' }}
                </span>
                
                <div class="flex items-center gap-2 font-mono text-3xl font-bold leading-none" :class="isDark ? 'text-white' : 'text-black'">
                    <span v-if="gamePhase === 'veto'">00:{{ timer < 10 ? '0'+timer : timer }}</span>
                    <span v-else class="text-green-500">READY</span>
                </div>
            </div>
             <div v-if="gamePhase === 'veto'" class="mt-2 text-[10px] font-bold px-2 py-0.5 rounded border shadow-sm transition-colors"
                  :class="isDark ? 'bg-gray-800 text-white border-gray-600' : 'bg-white text-black border-gray-300'">
                FASE: <span :class="actionType === 'BAN' ? 'text-red-500' : 'text-green-500'">{{ actionType }}</span>
            </div>
        </div>

        <div class="flex items-center gap-3">
             <!-- Toggles -->
            <button @click="toggleLanguage" class="flex items-center gap-1 text-xs font-bold border px-2 py-1 rounded hover:border-[var(--rankit-neon)] transition-colors"
                    :class="isDark ? 'border-gray-700 text-white' : 'border-gray-300 text-black'">
                <span>{{ currentLang.toUpperCase() }}</span>
            </button>
            <button @click="toggleTheme" class="p-2 transition-colors border border-transparent rounded-lg"
                    :class="isDark ? 'text-gray-400 hover:text-[var(--rankit-neon)] hover:border-gray-700' : 'text-gray-500 hover:text-[var(--rankit-neon)] hover:border-gray-300'">
                <i v-if="isDark" class="text-xl ph-fill ph-sun"></i>
                <i v-else class="text-xl ph-fill ph-moon"></i>
            </button>
        </div>
    </header>

    <main class="relative z-10 flex flex-1 overflow-hidden">
        <!-- Sidebar Team A -->
        <aside class="flex flex-col transition-colors duration-300 border-r w-80 backdrop-blur-sm"
               :class="isDark ? 'bg-[#0F1019]/80 border-gray-800' : 'bg-white/80 border-gray-200'">
            
            <div class="p-6 transition-colors border-b bg-gradient-to-r from-blue-500/10 to-transparent"
                 :class="isDark ? 'border-gray-800' : 'border-gray-200'">
                <div class="flex items-start justify-between">
                    <h2 class="text-2xl font-bold text-blue-500 font-display">ALPHA</h2>
                    <span class="px-2 py-1 text-xs font-bold rounded" :class="isDark ? 'text-gray-400 bg-black/30' : 'text-gray-600 bg-gray-200'">DEF</span>
                </div>
                <div class="mt-1 text-xs" :class="isDark ? 'text-gray-400' : 'text-gray-500'">Average Elo: 1,450</div>
            </div>
            
            <div class="flex-1 p-4 space-y-2 overflow-y-auto custom-scroll">
                <div v-for="(player, i) in teamA" :key="i" 
                     class="relative flex items-center gap-3 p-3 overflow-hidden transition border border-transparent rounded-lg hover:border-blue-500/30 group"
                     :class="isDark ? 'bg-[#151725]' : 'bg-gray-50 hover:bg-white'">
                     
                     <div class="absolute top-0 bottom-0 left-0 w-1" :class="player.ready ? 'bg-green-500' : 'bg-gray-400'"></div>
                     
                     <div class="relative">
                        <img :src="player.avatar" class="w-10 h-10 transition border rounded" :class="isDark ? 'border-gray-700 group-hover:border-blue-400' : 'border-gray-300 group-hover:border-blue-500'">
                        <div v-if="player.role === 'CAP'" class="absolute -top-2 -right-2 bg-yellow-500 text-black text-[8px] font-bold px-1.5 py-0.5 rounded shadow-lg z-10">CAP</div>
                     </div>
                     <div class="flex-1">
                        <div class="flex items-center justify-between">
                            <div class="text-sm font-bold transition" :class="[isDark ? 'text-white' : 'text-gray-900', 'group-hover:text-blue-500']">{{ player.name }}</div>
                            <i v-if="player.ready" class="text-xs text-green-500 fas fa-check"></i>
                        </div>
                        <div class="text-[10px] text-gray-500">Duelist Main</div>
                    </div>
                </div>
            </div>
            
             <div class="p-4 transition-colors border-t" :class="isDark ? 'border-gray-800 bg-[#0B0C15]/50' : 'border-gray-200 bg-gray-50'">
                 <button class="relative w-full py-3 overflow-hidden text-sm font-bold tracking-wider uppercase transition shadow-lg group btn-skew"
                         :class="gamePhase === 'ready' ? 'bg-green-600 hover:bg-green-500 text-white' : 'bg-gray-700 text-gray-400 cursor-not-allowed'">
                     <span class="relative z-10 flex items-center justify-center gap-2 btn-content">
                         <i v-if="gamePhase === 'ready'" class="fas fa-play"></i>
                         {{ gamePhase === 'ready' ? 'INICIAR PARTIDA' : 'ESPERANDO VETO' }}
                     </span>
                 </button>
             </div>
        </aside>

        <!-- Center Stage -->
        <section class="relative flex flex-col flex-1">
             <div class="z-10 flex flex-col items-center justify-center flex-1 p-8 overflow-y-auto">
                
                <div class="mb-10 text-center animate-fade-in-up">
                    <h2 class="mb-2 text-4xl font-bold tracking-tight transition-colors font-display drop-shadow-xl" :class="isDark ? 'text-white' : 'text-black'">VETO DE MAPAS</h2>
                    <p class="inline-block px-4 py-2 text-sm transition-colors border rounded-full backdrop-blur"
                       :class="isDark ? 'text-gray-300 bg-black/40 border-white/10' : 'text-gray-600 bg-white/60 border-gray-200'">
                        <span v-if="gamePhase === 'veto'">
                            Turno de <span :class="turn === 'team_a' ? 'text-blue-500' : 'text-red-500'" class="font-bold uppercase">{{ turn === 'team_a' ? 'Team Alpha' : 'Team Omega' }}</span> para 
                            <span :class="actionType === 'BAN' ? 'text-red-500' : 'text-green-500'" class="font-bold uppercase">{{ actionType === 'BAN' ? 'Banear 🚫' : 'Elegir ✅' }}</span>
                        </span>
                        <span v-else class="flex items-center gap-2 font-bold text-green-500 uppercase">
                            <i class="fas fa-check-circle"></i> Veto Completado
                        </span>
                    </p>
                </div>

                <div class="flex flex-wrap justify-center max-w-5xl gap-6">
                    <div v-for="map in maps" :key="map.id" 
                         @click="handleMapClick(map.id)"
                         class="relative p-0 transition-all duration-300 ease-out select-none w-36 h-52 md:w-44 md:h-64 rounded-xl group brutal-card"
                         :class="{
                             'cursor-pointer hover:-translate-y-2 hover:shadow-[0_0_20px_rgba(6,182,212,0.3)]': map.status === 'available' && turn === userTeam,
                             'opacity-50 grayscale scale-95 pointer-events-none': map.status === 'banned',
                             'scale-105 ring-4 ring-green-500 shadow-[0_0_30px_rgba(16,185,129,0.4)] z-10': map.status === 'picked',
                             'opacity-80': map.status === 'available' && turn !== userTeam,
                             'bg-gray-900 border-gray-800': isDark,
                             'bg-white border-gray-200': !isDark
                         }">
                        
                        <div class="absolute inset-0 overflow-hidden rounded-xl">
                            <img :src="map.img" class="object-cover w-full h-full transition duration-700 group-hover:scale-110">
                            <div class="absolute inset-0 bg-gradient-to-t" :class="isDark ? 'from-black via-transparent' : 'from-white via-transparent'"></div>
                            
                            <!-- Banned Overlay -->
                            <div v-if="map.status === 'banned'" class="absolute inset-0 flex flex-col items-center justify-center" :class="isDark ? 'bg-black/60' : 'bg-white/60'">
                                <i class="mb-2 text-4xl text-red-500 fas fa-ban"></i>
                                <span class="text-red-500 font-bold uppercase text-xs tracking-widest border border-red-500 px-2 py-0.5 rounded">Banned</span>
                            </div>

                            <!-- Picked Overlay -->
                            <div v-if="map.status === 'picked'" class="absolute inset-0 flex items-center justify-center" :class="isDark ? 'bg-green-500/20' : 'bg-green-500/10'">
                                <div class="px-3 py-1 text-xs font-bold text-black uppercase transform bg-green-500 rounded shadow-lg -rotate-3">
                                    <i class="mr-1 fas fa-check"></i> Picked
                                </div>
                            </div>
                        </div>

                        <!-- Hover Action Button -->
                        <div v-if="map.status === 'available' && turn === userTeam" 
                             class="absolute inset-0 flex items-center justify-center opacity-0 group-hover:opacity-100 transition duration-200 backdrop-blur-[2px] rounded-xl z-20"
                             :class="isDark ? 'bg-black/40' : 'bg-white/40'">
                            <button class="px-4 py-2 text-xs font-bold uppercase transition transform scale-90 rounded shadow-lg group-hover:scale-100"
                                    :class="actionType === 'BAN' ? 'bg-red-500 text-white' : 'bg-green-500 text-black'">
                                {{ actionType }} MAP
                            </button>
                        </div>

                        <div class="absolute bottom-0 z-20 w-full p-4 text-center">
                            <span class="text-lg font-bold tracking-wider uppercase font-display md:text-xl drop-shadow-md"
                                  :class="isDark ? 'text-white' : 'text-black'">{{ map.name }}</span>
                        </div>
                    </div>
                </div>
             </div>
        </section>

        <!-- Sidebar Team B & Chat -->
        <aside class="flex flex-col transition-colors duration-300 border-l w-80 backdrop-blur-sm"
               :class="isDark ? 'bg-[#0F1019]/80 border-gray-800' : 'bg-white/80 border-gray-200'">
            
            <div class="p-6 text-right transition-colors border-b bg-gradient-to-l from-red-900/10 to-transparent"
                 :class="isDark ? 'border-gray-800' : 'border-gray-200'">
                <div class="flex flex-row-reverse items-start justify-between">
                    <h2 class="text-2xl font-bold text-red-500 font-display">OMEGA</h2>
                    <span class="px-2 py-1 text-xs font-bold rounded" :class="isDark ? 'text-gray-400 bg-black/30' : 'text-gray-600 bg-gray-200'">ATK</span>
                </div>
                <div class="mt-1 text-xs" :class="isDark ? 'text-gray-400' : 'text-gray-500'">Average Elo: 1,420</div>
            </div>

            <div class="flex-shrink-0 p-4 space-y-2">
                <div v-for="(player, i) in teamB" :key="i" class="flex items-center justify-end gap-3 p-2 border border-transparent rounded-lg opacity-80"
                     :class="isDark ? 'bg-[#151725]' : 'bg-gray-50'">
                    <div class="text-right">
                        <div class="text-sm font-bold" :class="isDark ? 'text-gray-300' : 'text-gray-900'">{{ player.name }}</div>
                        <div class="text-[9px] text-gray-500">Controller Main</div>
                    </div>
                    <img :src="player.avatar" class="w-8 h-8 border rounded filter grayscale" :class="isDark ? 'border-gray-700' : 'border-gray-300'">
                </div>
            </div>

            <div class="flex flex-col flex-1 min-h-0 transition-colors border-t"
                 :class="isDark ? 'border-gray-800 bg-black/20' : 'border-gray-200 bg-gray-50'">
                <div class="px-4 py-2 text-[10px] font-bold text-gray-500 border-b uppercase tracking-wider flex justify-between items-center"
                     :class="isDark ? 'bg-[#151725] border-gray-800' : 'bg-white border-gray-200'">
                    <span>Lobby Chat</span>
                    <span class="w-2 h-2 bg-green-500 rounded-full"></span>
                </div>
                
                <div ref="chatContainer" class="flex-1 p-3 space-y-3 overflow-y-auto custom-scroll">
                    <div v-for="msg in messages" :key="msg.id" class="text-xs animate-fade-in">
                        <div v-if="msg.type === 'system'" class="text-yellow-500 font-mono text-[10px] text-center my-2 italic border-y border-yellow-500/20 py-1">
                            >> {{ msg.text }}
                        </div>
                        <div v-else-if="msg.type === 'action'" class="flex items-center justify-center gap-2 my-2 opacity-70">
                             <span class="w-4 h-px bg-gray-400"></span>
                             <span class="font-bold uppercase text-[10px]" :class="msg.team === 'team_a' ? 'text-blue-500' : 'text-red-500'">{{ msg.team === 'team_a' ? 'ALPHA' : 'OMEGA' }}</span>
                             <span class="text-[10px]" :class="isDark ? 'text-gray-400' : 'text-gray-600'">{{ msg.text }}</span>
                             <span class="w-4 h-px bg-gray-400"></span>
                        </div>
                        <div v-else class="break-words">
                            <span class="font-bold cursor-pointer hover:underline" :class="msg.team === 'team_a' ? 'text-blue-500' : 'text-red-500'">
                                {{ msg.user }}:
                            </span>
                            <span class="ml-1" :class="isDark ? 'text-gray-300' : 'text-gray-800'">{{ msg.text }}</span>
                        </div>
                    </div>
                </div>

                <div class="p-3 transition-colors" :class="isDark ? 'bg-[#0F1019]' : 'bg-white'">
                    <div class="relative">
                        <input type="text" 
                               v-model="chatInput" 
                               @keyup.enter="sendMessage"
                               placeholder="Enviar mensaje..." 
                               class="w-full border rounded-lg pl-3 pr-10 py-2.5 text-xs outline-none transition focus:shadow-[0_0_10px_rgba(6,182,212,0.2)]"
                               :class="isDark ? 'bg-black border-gray-700 text-white focus:border-[var(--rankit-neon)]' : 'bg-gray-100 border-gray-300 text-black focus:border-[var(--rankit-neon)]'">
                        <button @click="sendMessage" class="absolute right-2 top-1.5 transition p-1"
                                :class="isDark ? 'text-gray-500 hover:text-white' : 'text-gray-400 hover:text-black'">
                            <i class="text-xs fas fa-paper-plane"></i>
                        </button>
                    </div>
                </div>
            </div>
        </aside>
    </main>
  </div>
</template>

<style scoped>
:root { --rankit-neon: #bf00ff; }
.font-display { font-family: 'Chakra Petch', sans-serif; }
body { font-family: 'Archivo', sans-serif; }

/* Custom Scroll */
.custom-scroll::-webkit-scrollbar { width: 4px; }
.custom-scroll::-webkit-scrollbar-track { background: rgba(0,0,0,0.1); }
.custom-scroll::-webkit-scrollbar-thumb { background: #525252; border-radius: 4px; }
.custom-scroll::-webkit-scrollbar-thumb:hover { background: #737373; }

/* Animations */
.animate-fade-in-up { animation: fadeInUp 0.6s cubic-bezier(0.2, 1, 0.3, 1) forwards; }
.animate-fade-in { animation: fadeIn 0.3s ease-out; }
@keyframes fadeInUp { from { opacity: 0; transform: translateY(20px); } to { opacity: 1; transform: translateY(0); } }
@keyframes fadeIn { from { opacity: 0; } to { opacity: 1; } }

/* Brutal Card (Global Style) */
.brutal-card { 
    transition: all 0.2s cubic-bezier(0.25, 0.46, 0.45, 0.94);
    border: 1px solid;
}
.brutal-card:hover { border-color: var(--rankit-neon); transform: translate(-2px, -2px); }

/* Grids */
.bg-tech-grid-dark { background-image: linear-gradient(to right, rgba(255, 255, 255, 0.05) 1px, transparent 1px), linear-gradient(to bottom, rgba(255, 255, 255, 0.05) 1px, transparent 1px); }
.bg-tech-grid-light { background-image: linear-gradient(to right, rgba(0, 0, 0, 0.05) 1px, transparent 1px), linear-gradient(to bottom, rgba(0, 0, 0, 0.05) 1px, transparent 1px); }

/* Btn Skew */
.btn-skew {
    background-color: var(--rankit-neon);
    color: white;
    transform: skewX(-10deg);
    transition: all 0.2s;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;
}
.btn-skew:hover { 
    background-color: white; color: black; box-shadow: 0 0 15px var(--rankit-neon); 
}
.btn-content { transform: skewX(10deg); }
</style>