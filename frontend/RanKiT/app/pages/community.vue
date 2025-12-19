<script setup>
import { ref, onMounted } from 'vue'
import AppLogo from '@/components/AppLogo.vue'

// --- Lógica Global ---
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
    }
})

// Estado local
const showLogin = ref(false)
const showRegister = ref(false)

// Datos simulados
const liveUsers = [
  { name: 'xCryBaby', img: 'https://i.pravatar.cc/150?img=11', isStaff: false },
  { name: 'Admin', img: 'https://i.pravatar.cc/150?img=68', isStaff: true },
  { name: 'FakerFaker', img: 'https://i.pravatar.cc/150?img=3', isStaff: false },
  { name: 'NinjaClips', img: 'https://i.pravatar.cc/150?img=33', isStaff: false, isOffline: true }
]

const clips = [
  { id: 1, title: 'Clutch 1v4 imposible! 😱', user: '@ProGamer_MX', avatar: 'https://i.pravatar.cc/150?img=12', views: '1.2k', likes: 450, comments: 22, isLive: true, img: 'https://images.unsplash.com/photo-1542751371-adc38448a05e?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80' },
  { id: 2, title: 'Anuncio del Torneo 🏆', user: '@RankIT_Official', avatar: 'https://i.pravatar.cc/150?img=60', views: '', likes: '2.5k', comments: 150, isStaffPick: true, verified: true, img: 'https://images.unsplash.com/photo-1511512578047-dfb367046420?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80' },
  { id: 3, title: 'Setup Tour 2025 💻', user: '@TechGeek', avatar: 'https://i.pravatar.cc/150?img=8', views: '', likes: 89, comments: 4, img: 'https://images.unsplash.com/photo-1593305841991-05c29736ce87?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80' },
  { id: 4, title: 'Funny Glitch in Valo 🤣', user: '@CasualPlayer', avatar: 'https://i.pravatar.cc/150?img=52', views: '', likes: 320, comments: 12, img: 'https://images.unsplash.com/photo-1616588589676-60b30c3c1681?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80' }
]
</script>

<template>
  <div class="min-h-screen font-sans transition-colors duration-300 main-wrapper"
       :class="isDark ? 'bg-[#050505] text-white' : 'bg-gray-50 text-gray-900'">
    
    <!-- Navbar Unificado -->
    <nav class="fixed z-50 flex items-center justify-between w-full h-20 px-6 transition-colors duration-300 border-b lg:px-12 backdrop-blur-md"
         :class="isDark ? 'bg-[#050505]/95 border-white/10' : 'bg-white/90 border-gray-200'">
        
        <div class="flex items-center gap-8">
            <NuxtLink to="/" class="flex items-center gap-3 cursor-pointer group">
                <div class="w-10 h-10 transition-colors" :class="isDark ? 'text-white' : 'text-black group-hover:text-[var(--rankit-neon)]'">
                    <AppLogo />
                </div>
                <span class="text-3xl italic font-bold tracking-tighter uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">Rankit</span>
            </NuxtLink>

            <div class="items-center hidden gap-6 text-sm font-bold tracking-wider uppercase md:flex" :class="isDark ? 'text-gray-400' : 'text-gray-500'">
                <NuxtLink to="/start" class="hover:text-[var(--rankit-neon)] transition">Torneos</NuxtLink>
                <a href="#" class="text-[var(--rankit-neon)] border-b-2 border-[var(--rankit-neon)] pb-6 -mb-6">Comunidad</a>
                <a href="#" class="hover:text-[var(--rankit-neon)] transition">Ranking</a>
            </div>
        </div>

        <div class="flex items-center gap-4">
            <!-- Toggles -->
            <button @click="toggleLanguage" class="flex items-center gap-1 text-xs font-bold border px-2 py-1 rounded hover:border-[var(--rankit-neon)] transition-colors"
                    :class="isDark ? 'border-gray-700 text-white' : 'border-gray-300 text-black'">
                <i class="text-lg ph ph-translate"></i>
                <span>{{ currentLang.toUpperCase() }}</span>
            </button>
            <button @click="toggleTheme" class="p-2 transition-colors border border-transparent rounded-lg"
                    :class="isDark ? 'text-gray-400 hover:text-[var(--rankit-neon)] hover:border-gray-700' : 'text-gray-500 hover:text-[var(--rankit-neon)] hover:border-gray-300'">
                <i v-if="isDark" class="text-xl ph-fill ph-sun"></i>
                <i v-else class="text-xl ph-fill ph-moon"></i>
            </button>

            <div class="w-px h-6" :class="isDark ? 'bg-gray-800' : 'bg-gray-300'"></div>
            <button @click="showLogin = true" class="text-sm font-bold uppercase tracking-wider hover:text-[var(--rankit-neon)] transition">Log In</button>
            <button @click="showRegister = true" class="px-5 py-2 text-xs font-bold tracking-wider uppercase btn-skew">
                <span class="btn-content">Registrarse</span>
            </button>
        </div>
    </nav>

    <!-- Main Content -->
    <main class="pt-24 pb-12 px-4 lg:px-8 max-w-[1600px] mx-auto">
        
        <!-- Live Now Section -->
        <section class="mb-12">
            <div class="flex items-center gap-2 mb-4">
                <div class="w-2 h-2 bg-red-500 rounded-full animate-pulse-fast"></div>
                <h2 class="text-lg font-bold tracking-wider uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">Live Ahora</h2>
            </div>
            
            <div class="flex gap-4 pb-4 overflow-x-auto no-scrollbar">
                <div v-for="(user, i) in liveUsers" :key="i" 
                     class="flex flex-col items-center gap-2 cursor-pointer group min-w-[80px]"
                     :class="{ 'opacity-70 hover:opacity-100': user.isOffline }">
                    <div :class="user.isOffline ? 'story-ring' : 'relative live-ring'" class="p-[2px]">
                        <img :src="user.img" class="w-16 h-16 transition border-2 rounded-full group-hover:scale-105" :class="isDark ? 'border-[#0B0C15]' : 'border-white'">
                        <div v-if="!user.isOffline" class="absolute -bottom-1 left-1/2 -translate-x-1/2 bg-red-600 text-white text-[9px] font-bold px-1.5 rounded-sm">LIVE</div>
                    </div>
                    <span v-if="!user.isStaff" class="w-20 text-xs font-semibold text-center truncate" :class="isDark ? 'text-gray-300 group-hover:text-white' : 'text-gray-600 group-hover:text-black'">{{ user.name }}</span>
                    <span v-else class="text-xs font-semibold text-[var(--rankit-neon)] truncate w-20 text-center flex items-center justify-center gap-1">
                        <i class="fas fa-shield-alt text-[10px]"></i> {{ user.name }}
                    </span>
                </div>
            </div>
        </section>

        <!-- Main Grid Layout -->
        <div class="flex flex-col gap-8 lg:flex-row">
            
            <!-- Sidebar Filters -->
            <aside class="sticky flex-shrink-0 hidden w-full space-y-8 lg:w-64 lg:block top-24 h-fit">
                <div>
                    <h3 class="mb-4 text-xs font-bold tracking-wider uppercase" :class="isDark ? 'text-gray-500' : 'text-gray-400'">Explorar</h3>
                    <div class="space-y-2">
                        <button class="flex items-center w-full gap-3 px-4 py-3 text-xs font-bold text-left uppercase rounded-none brutal-card"
                                :class="isDark ? 'bg-[#151725] text-white border-gray-800' : 'bg-white text-black border-gray-200'">
                            <i class="fas fa-fire text-[var(--rankit-neon)]"></i> Tendencias
                        </button>
                        <button class="w-full text-left px-4 py-3 hover:pl-6 transition-all rounded text-gray-500 hover:text-[var(--rankit-neon)] flex items-center gap-3 font-bold uppercase text-xs">
                            <i class="fas fa-users"></i> Siguiendo
                        </button>
                    </div>
                </div>

                <div class="p-6 brutal-card border-[var(--rankit-neon)]">
                    <h4 class="mb-2 text-lg font-bold uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">¡Sube tus Clips!</h4>
                    <p class="mb-4 text-xs" :class="isDark ? 'text-gray-400' : 'text-gray-600'">Comparte tus mejores jugadas y gana RP o Skins.</p>
                    <button class="w-full py-2 text-xs font-bold tracking-wider uppercase btn-skew">
                        <span class="btn-content">Subir Video</span>
                    </button>
                </div>
            </aside>

            <!-- Video Grid -->
            <div class="flex-1">
                <div class="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-3">
                    <div v-for="clip in clips" :key="clip.id" 
                         class="brutal-card relative group aspect-[9/16] overflow-hidden transition duration-300 border"
                         :class="isDark ? 'bg-gray-900 border-gray-800' : 'bg-gray-100 border-gray-200'">
                        
                        <img :src="clip.img" class="absolute inset-0 object-cover w-full h-full transition opacity-80 group-hover:opacity-60">
                        <div class="absolute inset-0 bg-gradient-to-b from-transparent via-transparent to-black"></div>
                        
                        <!-- Play Icon -->
                        <div class="absolute inset-0 flex items-center justify-center transition duration-300 transform scale-75 opacity-0 play-icon group-hover:opacity-100 group-hover:scale-100">
                            <div class="w-16 h-16 rounded-full bg-[var(--rankit-neon)] flex items-center justify-center text-black text-2xl pl-1 shadow-[0_0_20px_var(--rankit-neon)]">
                                <i class="fas fa-play"></i>
                            </div>
                        </div>

                        <!-- Info Overlay -->
                        <div class="absolute bottom-0 left-0 right-0 p-4">
                            <div class="flex items-center gap-3 mb-3">
                                <div class="relative">
                                    <img :src="clip.avatar" class="w-10 h-10 border-2 rounded-full" :class="clip.verified ? 'border-[var(--rankit-neon)]' : 'border-white'">
                                </div>
                                <div class="flex-1 min-w-0">
                                    <div class="text-sm font-bold text-white uppercase truncate font-display">{{ clip.title }}</div>
                                    <div class="text-xs truncate" :class="clip.verified ? 'text-[var(--rankit-neon)] font-bold' : 'text-gray-300'">
                                        {{ clip.user }} <i v-if="clip.verified" class="fas fa-check-circle"></i>
                                    </div>
                                </div>
                            </div>
                            <div class="flex items-center justify-between font-mono text-xs text-white/80">
                                <span class="flex items-center gap-1"><i class="fas fa-heart text-[var(--rankit-neon)]"></i> {{ clip.likes }}</span>
                                <span class="flex items-center gap-1"><i class="fas fa-comment"></i> {{ clip.comments }}</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
  </div>
</template>

<style scoped>
/* Estilos comunes */
:root { --rankit-neon: #bf00ff; }
.story-ring { background: linear-gradient(45deg, #FF0055, #FFCC00); padding: 2px; border-radius: 50%; }
.live-ring { background: linear-gradient(45deg, #ff0000, #ff4d4d); padding: 2px; border-radius: 50%; box-shadow: 0 0 10px rgba(255, 0, 0, 0.5); }
.animate-pulse-fast { animation: pulse 1.5s cubic-bezier(0.4, 0, 0.6, 1) infinite; }
.no-scrollbar::-webkit-scrollbar { display: none; }
.no-scrollbar { -ms-overflow-style: none; scrollbar-width: none; }

/* Brutal Styles */
.brutal-card { 
    transition: all 0.2s cubic-bezier(0.25, 0.46, 0.45, 0.94);
    border: 1px solid;
}
.main-wrapper.bg-\[\#050505\] .brutal-card { border-color: #333; }
.main-wrapper:not(.bg-\[\#050505\]) .brutal-card { border-color: #e5e5e5; box-shadow: 4px 4px 0px #00000010; }
.brutal-card:hover { border-color: var(--rankit-neon); transform: translate(-4px, -4px); }
.main-wrapper.bg-\[\#050505\] .brutal-card:hover { box-shadow: 6px 6px 0px var(--rankit-neon); }
.main-wrapper:not(.bg-\[\#050505\]) .brutal-card:hover { box-shadow: 6px 6px 0px var(--rankit-neon), 6px 6px 0px 2px black; }

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
.btn-skew:hover { background-color: white; color: black; box-shadow: 0 0 15px var(--rankit-neon); }
.btn-content { transform: skewX(10deg); }
</style>