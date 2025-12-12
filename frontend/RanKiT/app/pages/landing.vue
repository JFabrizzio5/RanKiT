<script setup>
import { ref } from 'vue'

useHead({
  title: 'RankIT - Comunidad & Clips',
  link: [
    { rel: 'stylesheet', href: 'https://fonts.googleapis.com/css2?family=Chakra+Petch:wght@400;600;700&family=Inter:wght@300;400;600;700&display=swap' },
    { rel: 'stylesheet', href: 'https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css' }
  ]
})

// Estado para Modales
const showLogin = ref(false)
const showRegister = ref(false)

// Mock Data para Live Users
const liveUsers = [
  { name: 'xCryBaby', img: 'https://i.pravatar.cc/150?img=11', isStaff: false },
  { name: 'Admin', img: 'https://i.pravatar.cc/150?img=68', isStaff: true },
  { name: 'FakerFaker', img: 'https://i.pravatar.cc/150?img=3', isStaff: false },
]

// Mock Data para Clips
const clips = [
  { id: 1, title: 'Clutch 1v4 imposible! 🤯', user: '@Slayer99', views: '1.2k', likes: 450, time: '0:45', img: 'https://images.unsplash.com/photo-1624138784181-2999e4253b85?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80' },
  { id: 2, title: 'Anuncio del Torneo 🏆', user: '@RankIT_Official', views: '10k', likes: 2500, time: '1:20', isStaffPick: true, img: 'https://images.unsplash.com/photo-1511512578047-dfb367046420?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80' },
  { id: 3, title: 'Setup Tour 2025 💻', user: '@TechGeek', views: '5k', likes: 89, time: '3:10', img: 'https://images.unsplash.com/photo-1593305841991-05c29736ce87?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80' },
  { id: 4, title: 'Funny Glitch in Valo 🤣', user: '@CasualPlayer', views: '320', likes: 320, time: '0:15', img: 'https://images.unsplash.com/photo-1616588589676-60b30c3c1681?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80' },
]
</script>

<template>
  <div class="bg-[#0B0C15] text-white min-h-screen font-sans">
    
    <!-- Navbar -->
    <nav class="glass-nav fixed w-full z-50 h-16 flex items-center justify-between px-6 lg:px-12">
        <div class="flex items-center gap-2">
            <i class="fas fa-trophy text-brand-purple text-xl"></i>
            <span class="font-display font-bold text-xl tracking-wider">RANK<span class="text-brand-cyan">IT</span></span>
        </div>

        <div class="hidden md:flex items-center gap-8 text-sm font-semibold text-gray-400">
            <NuxtLink to="/start" class="hover:text-white transition">Torneos</NuxtLink>
            <NuxtLink to="/community" class="text-white border-b-2 border-brand-cyan pb-5">Clips & Feed</NuxtLink>
            <a href="#" class="hover:text-white transition">Ranking</a>
        </div>

        <div class="flex items-center gap-4">
            <button class="text-gray-400 hover:text-white"><i class="fas fa-search text-lg"></i></button>
            <div class="h-6 w-px bg-gray-800"></div>
            <button @click="showLogin = true" class="text-gray-300 hover:text-white text-sm font-bold">Log In</button>
            <button @click="showRegister = true" class="bg-brand-cyan hover:bg-cyan-400 text-black px-5 py-2 rounded-full font-bold text-sm transition transform hover:scale-105 shadow-[0_0_15px_rgba(6,182,212,0.4)]">
                Registrarse
            </button>
        </div>
    </nav>

    <!-- Main Content -->
    <main class="pt-24 pb-12 px-4 lg:px-8 max-w-[1600px] mx-auto">
        
        <!-- Live Now Section -->
        <section class="mb-12">
            <div class="flex items-center gap-2 mb-4">
                <div class="w-2 h-2 rounded-full bg-red-500 animate-pulse-fast"></div>
                <h2 class="font-display text-lg font-bold uppercase tracking-wider text-white">Live Ahora</h2>
            </div>
            
            <div class="flex gap-4 overflow-x-auto no-scrollbar pb-4">
                <div v-for="(user, idx) in liveUsers" :key="idx" class="flex flex-col items-center gap-2 cursor-pointer group min-w-[80px]">
                    <div class="relative live-ring p-[2px]">
                        <img :src="user.img" class="w-16 h-16 rounded-full border-2 border-[#0B0C15] group-hover:scale-105 transition">
                        <div class="absolute -bottom-1 left-1/2 -translate-x-1/2 bg-red-600 text-white text-[9px] font-bold px-1.5 rounded-sm">LIVE</div>
                    </div>
                    <span v-if="!user.isStaff" class="text-xs font-semibold text-gray-300 truncate w-20 text-center group-hover:text-white">{{ user.name }}</span>
                    <span v-else class="text-xs font-semibold text-brand-purple truncate w-20 text-center flex items-center justify-center gap-1">
                        <i class="fas fa-shield-alt text-[10px]"></i> Admin
                    </span>
                </div>
            </div>
        </section>

        <!-- Main Grid Layout -->
        <div class="flex flex-col lg:flex-row gap-8">
            
            <!-- Sidebar Filters -->
            <aside class="w-full lg:w-64 flex-shrink-0 space-y-8 hidden lg:block sticky top-24 h-fit">
                <div>
                    <h3 class="font-bold text-gray-500 text-xs uppercase mb-4 tracking-wider">Explorar</h3>
                    <div class="space-y-1">
                        <button class="w-full text-left px-4 py-3 rounded-xl bg-brand-surface text-white font-bold border border-gray-700 flex items-center gap-3">
                            <i class="fas fa-fire text-brand-cyan"></i> Tendencias
                        </button>
                        <button class="w-full text-left px-4 py-3 rounded-xl text-gray-400 hover:bg-white/5 hover:text-white transition flex items-center gap-3">
                            <i class="fas fa-users"></i> Siguiendo
                        </button>
                    </div>
                </div>
                
                <!-- Promo Box -->
                <div class="bg-gradient-to-br from-brand-purple to-blue-900 rounded-2xl p-6 text-center">
                    <h4 class="font-display font-bold text-xl mb-2">¡Sube tus Clips!</h4>
                    <p class="text-xs text-blue-200 mb-4">Comparte tus mejores jugadas y gana RP o Skins.</p>
                    <button class="w-full bg-white text-brand-purple font-bold py-2 rounded-lg text-sm hover:bg-gray-100 transition">Subir Video</button>
                </div>
            </aside>

            <!-- Video Grid -->
            <div class="flex-1">
                <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
                    
                    <div v-for="clip in clips" :key="clip.id" class="video-card relative group aspect-[9/16] bg-gray-900 rounded-2xl overflow-hidden border border-gray-800 hover:border-brand-cyan/50 transition duration-300">
                        <img :src="clip.img" class="absolute inset-0 w-full h-full object-cover opacity-60 group-hover:opacity-40 transition">
                        <div class="absolute inset-0 bg-gradient-to-b from-transparent via-transparent to-black"></div>
                        
                        <div v-if="clip.isStaffPick" class="absolute top-3 left-3 z-10">
                             <span class="bg-brand-purple text-white px-2 py-1 rounded text-[10px] font-bold uppercase tracking-wider flex items-center gap-1 shadow-lg">
                                <i class="fas fa-crown"></i> Staff Pick
                            </span>
                        </div>

                        <div class="play-icon absolute inset-0 flex items-center justify-center opacity-0 transition transform scale-75 duration-300">
                            <div class="w-16 h-16 rounded-full bg-brand-cyan/90 flex items-center justify-center text-black text-2xl pl-1 shadow-[0_0_20px_rgba(6,182,212,0.6)]">
                                <i class="fas fa-play"></i>
                            </div>
                        </div>

                        <div class="absolute bottom-0 left-0 right-0 p-4">
                            <div class="flex items-center gap-3 mb-3">
                                <img src="https://i.pravatar.cc/150?img=12" class="w-10 h-10 rounded-full border border-white">
                                <div class="flex-1 min-w-0">
                                    <div class="text-white font-bold text-sm truncate">{{ clip.title }}</div>
                                    <div class="text-gray-400 text-xs truncate">{{ clip.user }}</div>
                                </div>
                            </div>
                             <div class="flex justify-between items-center text-white/80">
                                <button class="flex items-center gap-1 text-xs hover:text-brand-cyan"><i class="fas fa-heart"></i> {{ clip.likes }}</button>
                                <button class="flex items-center gap-1 text-xs hover:text-brand-cyan"><i class="fas fa-comment"></i> 22</button>
                                <button class="flex items-center gap-1 text-xs hover:text-brand-cyan"><i class="fas fa-share"></i></button>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </main>

    <!-- Modal Registro -->
    <div v-if="showRegister" class="fixed inset-0 z-[100] bg-black/80 backdrop-blur-sm flex items-center justify-center p-4">
        <div class="bg-[#151725] w-full max-w-md rounded-2xl border border-gray-700 shadow-2xl overflow-hidden relative p-8">
            <button @click="showRegister = false" class="absolute top-4 right-4 text-gray-400 hover:text-white"><i class="fas fa-times text-xl"></i></button>
            <div class="text-center mb-6">
                <h2 class="font-display font-bold text-2xl text-white">Únete a <span class="text-brand-cyan">RankIT</span></h2>
                <p class="text-gray-400 text-sm mt-1">Regístrate para subir clips y participar en torneos.</p>
            </div>
            <!-- Formulario simplificado -->
            <input type="text" placeholder="Nombre de Usuario" class="w-full bg-black/50 border border-gray-700 rounded-lg px-4 py-3 text-white text-sm focus:border-brand-cyan outline-none mb-3">
            <button class="w-full bg-brand-cyan hover:bg-cyan-400 text-black font-bold py-3 rounded-lg mt-2 transition shadow-lg shadow-cyan-500/20">Crear Cuenta</button>
        </div>
    </div>

    <!-- Modal Login -->
    <div v-if="showLogin" class="fixed inset-0 z-[100] bg-black/80 backdrop-blur-sm flex items-center justify-center p-4">
        <div class="bg-[#151725] w-full max-w-sm rounded-2xl border border-gray-700 shadow-2xl p-8 relative">
            <button @click="showLogin = false" class="absolute top-4 right-4 text-gray-400 hover:text-white"><i class="fas fa-times text-xl"></i></button>
            <h2 class="font-display font-bold text-2xl text-white mb-6 text-center">Bienvenido</h2>
            <input type="email" placeholder="Email" class="w-full bg-black/50 border border-gray-700 rounded-lg px-4 py-3 text-white text-sm mb-4 outline-none focus:border-brand-cyan">
            <button class="w-full bg-white text-black font-bold py-3 rounded-lg hover:bg-gray-200 transition">Entrar</button>
        </div>
    </div>

  </div>
</template>

<style scoped>
.font-display { font-family: 'Chakra Petch', sans-serif; }
.glass-nav { background: rgba(11, 12, 21, 0.85); backdrop-filter: blur(12px); border-bottom: 1px solid rgba(255,255,255,0.05); }
.video-card:hover .play-icon { opacity: 1; transform: scale(1); }
.story-ring { background: linear-gradient(45deg, #FF0055, #FFCC00); padding: 2px; border-radius: 50%; }
.live-ring { background: linear-gradient(45deg, #ff0000, #ff4d4d); padding: 2px; border-radius: 50%; box-shadow: 0 0 10px rgba(255, 0, 0, 0.5); }
.bg-brand-surface { background-color: #151725; }
.text-brand-purple { color: #7C3AED; }
.text-brand-cyan { color: #06B6D4; }
.animate-pulse-fast { animation: pulse 1.5s cubic-bezier(0.4, 0, 0.6, 1) infinite; }
</style>