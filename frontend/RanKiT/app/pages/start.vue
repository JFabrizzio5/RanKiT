<script setup>
import { ref, onMounted, computed } from 'vue'
import AppLogo from '@/components/AppLogo.vue' // Importamos el logo oficial

// --- Lógica de Traducción ---
const currentLang = ref('es')

const t = {
    es: {
        nav: { home: "Inicio", explore: "Explorar", login: "Ingresar" },
        hero: { 
            title: "ENCUENTRA TU", 
            highlight: "TORNEO", 
            desc: "Busca entre cientos de competiciones activas. Desde amateurs hasta profesionales.",
            placeholder: "BUSCAR POR JUEGO (EJ. VALORANT)...",
            btnSearch: "BUSCAR"
        },
        filters: { all: "Todos", fps: "Shooters", moba: "MOBA", sports: "Deportes", battle: "Battle Royale" },
        card: { entry: "Entrada", join: "Unirse", live: "En Vivo", open: "Abierto" }
    },
    en: {
        nav: { home: "Home", explore: "Explore", login: "Login" },
        hero: { 
            title: "FIND YOUR", 
            highlight: "TOURNAMENT", 
            desc: "Browse through hundreds of active competitions. From amateur to pro levels.",
            placeholder: "SEARCH BY GAME (EX. VALORANT)...",
            btnSearch: "SEARCH"
        },
        filters: { all: "All", fps: "Shooters", moba: "MOBA", sports: "Sports", battle: "Battle Royale" },
        card: { entry: "Entry", join: "Join", live: "Live", open: "Open" }
    }
}

const toggleLanguage = () => {
    currentLang.value = currentLang.value === 'es' ? 'en' : 'es'
}

// --- Lógica de Tema (Dark/Light) ---
const isDark = ref(true)

const toggleTheme = () => {
    isDark.value = !isDark.value
    updateTheme()
}

// FIX: Actualizamos también el color del body para evitar la línea blanca al hacer scroll
const updateTheme = () => {
    if (process.client) {
        const html = document.documentElement
        const body = document.body
        if (isDark.value) {
            html.classList.add('dark')
            body.style.backgroundColor = '#050505' // Color exacto del bg dark
            localStorage.setItem('theme', 'dark')
        } else {
            html.classList.remove('dark')
            body.style.backgroundColor = '#f9fafb' // Color exacto del bg light (gray-50)
            localStorage.setItem('theme', 'light')
        }
    }
}

onMounted(() => {
    if (process.client) {
        const savedTheme = localStorage.getItem('theme')
        const systemPrefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches
        isDark.value = savedTheme === 'dark' || (!savedTheme && systemPrefersDark)
        updateTheme()
    }
})

// --- Mock Data ---
const activeCategory = ref('all')
const tournaments = [
    { id: 1, title: 'Valorant Masters', game: 'Valorant', category: 'fps', image: 'https://images.unsplash.com/photo-1624138784181-2999e4253b85?q=80&w=800', price: '$50', status: 'live', players: '48/64' },
    { id: 2, title: 'Fortnite Zero Build', game: 'Fortnite', category: 'battle', image: 'https://images.unsplash.com/photo-1579373903781-fd5c0c30c4cd?q=80&w=800', price: 'Free', status: 'open', players: '12/100' },
    { id: 3, title: 'League Clash', game: 'LoL', category: 'moba', image: 'https://images.unsplash.com/photo-1542751371-adc38448a05e?q=80&w=800', price: '$20', status: 'open', players: '5/8' },
    { id: 4, title: 'FIFA Night Cup', game: 'EAFC 24', category: 'sports', image: 'https://images.unsplash.com/photo-1606166325683-e6deb697d301?q=80&w=800', price: '$10', status: 'open', players: '20/32' },
    { id: 5, title: 'CS2 Premier', game: 'CS2', category: 'fps', image: 'https://plus.unsplash.com/premium_photo-1685082606913-a41dc9a9b995?q=80&w=800', price: '$100', status: 'live', players: '16/16' },
    { id: 6, title: 'Rocket League Duo', game: 'Rocket League', category: 'sports', image: 'https://images.unsplash.com/photo-1600861194942-f883de0dfe96?q=80&w=800', price: 'Free', status: 'open', players: '8/32' },
]

const filteredTournaments = computed(() => {
    if (activeCategory.value === 'all') return tournaments
    return tournaments.filter(t => t.category === activeCategory.value)
})
</script>

<template>
<!-- Wrapper Principal con Tema Dinámico y Fondo Tech -->
  <div class="min-h-screen font-sans transition-colors duration-300 bg-[length:40px_40px]"
       :class="[
         isDark ? 'bg-[#050505] text-white bg-tech-grid-dark' : 'bg-gray-50 text-gray-900 bg-tech-grid-light'
       ]">
       
    <!-- Fondo Gradiente Superpuesto -->
    <div class="fixed absolute inset-0 transition-opacity duration-300 pointer-events-none"
         :class="isDark ? 'bg-gradient-to-b from-[#050505]/50 via-[#050505]/80 to-[#050505]' : 'bg-gradient-to-b from-white/50 via-white/80 to-white opacity-50'"></div>
    
    <!-- Navbar (Estilo Idéntico a Landing) -->
    <nav class="fixed top-0 z-50 flex items-center justify-between w-full h-20 px-6 transition-colors duration-300 border-b lg:px-12 backdrop-blur-md"
         :class="isDark ? 'bg-[#050505]/95 border-white/10' : 'bg-white/90 border-gray-200'">
        
        <NuxtLink to="/" class="flex items-center gap-3 cursor-pointer group">
            <div class="w-10 h-10 transition-colors" :class="isDark ? 'text-white' : 'text-black group-hover:text-[var(--rankit-neon)]'">
                <AppLogo />
            </div>
            <span class="text-3xl italic font-bold tracking-tighter uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">Rankit</span>
        </NuxtLink>

        <div class="hidden gap-8 text-sm font-bold tracking-widest uppercase transition-colors md:flex"
             :class="isDark ? 'text-gray-400' : 'text-gray-500'">
            <NuxtLink to="/" class="hover:text-[var(--rankit-neon)] transition">{{ t[currentLang].nav.home }}</NuxtLink>
            <a href="#" class="text-[var(--rankit-neon)] border-b-2 border-[var(--rankit-neon)] pb-6 -mb-6">{{ t[currentLang].nav.explore }}</a>
        </div>

        <div class="flex items-center gap-4">
            <!-- Language Toggle -->
            <button @click="toggleLanguage" class="flex items-center gap-1 text-xs font-bold border px-2 py-1 rounded hover:border-[var(--rankit-neon)] transition-colors"
                    :class="isDark ? 'border-gray-700 text-white' : 'border-gray-300 text-black'">
                <i class="text-lg ph ph-translate"></i>
                <span>{{ currentLang.toUpperCase() }}</span>
            </button>

            <!-- Theme Toggle -->
            <button @click="toggleTheme" class="p-2 transition-colors border border-transparent rounded-lg"
                    :class="isDark ? 'text-gray-400 hover:text-[var(--rankit-neon)] hover:border-gray-700' : 'text-gray-500 hover:text-[var(--rankit-neon)] hover:border-gray-300'">
                <i v-if="isDark" class="text-xl ph-fill ph-sun"></i>
                <i v-else class="text-xl ph-fill ph-moon"></i>
            </button>

            <!-- User Avatar -->
            <div class="w-10 h-10 overflow-hidden border border-gray-300 rounded dark:border-gray-700">
                <img src="https://i.pravatar.cc/150?img=33" class="object-cover w-full h-full">
            </div>
        </div>
    </nav>

    <!-- Main Content -->
    <!-- FIX: Agregamos pb-20 al contenedor padre en lugar de mb-20 al hijo para evitar colapso de márgenes -->
    <div class="relative z-10 px-4 pt-32 pb-20 mx-auto max-w-7xl sm:px-6">
        
        <!-- Search Header -->
        <div class="mb-16 text-center">
             <div class="inline-flex items-center gap-2 mb-4 border border-[var(--rankit-neon)] text-[var(--rankit-neon)] px-3 py-1 text-xs font-bold uppercase tracking-widest bg-[var(--rankit-neon)]/5">
                <span class="w-2 h-2 bg-[var(--rankit-neon)] rounded-full animate-pulse"></span>
                <span>Season 2025</span>
            </div>
             <h1 class="mb-4 text-5xl font-black leading-none uppercase transition-colors font-display md:text-7xl"
                 :class="isDark ? 'text-white' : 'text-gray-900'">
                {{ t[currentLang].hero.title }} <br>
                <span class="text-transparent bg-clip-text bg-gradient-to-r from-[var(--rankit-neon)] to-purple-600">{{ t[currentLang].hero.highlight }}</span>
            </h1>
            <p class="max-w-2xl mx-auto mb-10 text-lg font-light" :class="isDark ? 'text-gray-400' : 'text-gray-600'">{{ t[currentLang].hero.desc }}</p>
            
            <!-- Brutalist Search Bar -->
            <div class="relative max-w-3xl mx-auto">
                <div class="flex items-stretch w-full p-2 transition-all duration-300 border-2"
                     :class="isDark ? 'bg-black border-gray-700 focus-within:border-[var(--rankit-neon)]' : 'bg-white border-gray-300 focus-within:border-black shadow-[4px_4px_0px_rgba(0,0,0,0.1)]'">
                    
                    <div class="flex items-center justify-center pl-4 pr-2">
                         <i class="text-xl ph ph-magnifying-glass" :class="isDark ? 'text-gray-500' : 'text-gray-400'"></i>
                    </div>
                    
                    <input type="text" :placeholder="t[currentLang].hero.placeholder" 
                           class="w-full px-4 py-3 font-mono text-sm font-bold bg-transparent border-none outline-none"
                           :class="isDark ? 'text-white placeholder-gray-600' : 'text-gray-900 placeholder-gray-400'">
                    
                    <button class="px-8 py-2 text-sm font-bold tracking-wider uppercase btn-skew">
                        <span class="btn-content">{{ t[currentLang].hero.btnSearch }}</span>
                    </button>
                </div>
            </div>
        </div>

        <!-- Filters (Brutal Style) -->
        <div class="flex flex-wrap justify-center gap-4 mb-16">
            <button v-for="(label, key) in t[currentLang].filters" :key="key"
                    @click="activeCategory = key"
                    class="px-6 py-3 text-xs font-bold uppercase transition-all border brutal-card hover:translate-y-[-2px]"
                    :class="activeCategory === key 
                        ? 'border-[var(--rankit-neon)] text-[var(--rankit-neon)]' 
                        : (isDark ? 'bg-[#0a0a0a] border-gray-800 text-gray-500 hover:text-white' : 'bg-white border-gray-200 text-gray-500 hover:text-black')">
                <span class="flex items-center gap-2">
                    <i v-if="activeCategory === key" class="ph-fill ph-diamond"></i>
                    {{ label }}
                </span>
            </button>
        </div>

        <!-- Grid Results -->
        <div class="grid grid-cols-1 gap-8 md:grid-cols-2 lg:grid-cols-3">
             <div v-for="tourney in filteredTournaments" :key="tourney.id" 
                  class="flex flex-col h-full cursor-pointer brutal-card group">
                
                <!-- Image Container -->
                <div class="relative h-56 overflow-hidden border-b" :class="isDark ? 'border-gray-800' : 'border-gray-200'">
                    <img :src="tourney.image" class="object-cover w-full h-full transition duration-500 opacity-90 group-hover:opacity-100 group-hover:scale-110">
                    
                    <!-- Status Badges -->
                    <div class="absolute top-0 left-0 p-3">
                        <span v-if="tourney.status === 'live'" class="bg-red-600 text-white text-[10px] font-bold px-2 py-1 uppercase tracking-widest flex items-center gap-2">
                            <span class="w-1.5 h-1.5 bg-white rounded-full animate-pulse"></span> {{ t[currentLang].card.live }}
                        </span>
                        <span v-else class="bg-black text-white text-[10px] font-bold px-2 py-1 uppercase tracking-widest">
                            {{ t[currentLang].card.open }}
                        </span>
                    </div>

                    <div class="absolute bottom-0 right-0 bg-[var(--rankit-neon)] text-black text-[10px] font-bold px-3 py-1 uppercase">
                        {{ tourney.category }}
                    </div>
                </div>

                <!-- Content -->
                <div class="flex flex-col flex-1 p-6">
                    <div class="flex items-start justify-between mb-2">
                        <div>
                            <span class="text-[10px] font-bold uppercase tracking-widest text-gray-500">{{ tourney.game }}</span>
                            <h3 class="mt-1 text-xl font-bold leading-tight uppercase font-display group-hover:text-[var(--rankit-neon)] transition-colors" 
                                :class="isDark ? 'text-white' : 'text-black'">
                                {{ tourney.title }}
                            </h3>
                        </div>
                    </div>

                    <div class="flex items-center justify-between pt-4 mt-auto border-t" 
                         :class="isDark ? 'border-gray-800' : 'border-gray-100'">
                        <div class="flex items-center gap-2 text-xs font-bold text-gray-500">
                            <i class="text-lg ph ph-users"></i> {{ tourney.players }}
                        </div>
                        
                        <div class="flex items-center gap-4">
                            <span class="font-mono text-lg font-bold" 
                                  :class="tourney.price === 'Free' ? 'text-green-500' : (isDark ? 'text-white' : 'text-black')">
                                {{ tourney.price }}
                            </span>
                            <button class="p-2 text-xl transition-colors hover:text-[var(--rankit-neon)]"
                                    :class="isDark ? 'text-white' : 'text-black'">
                                <i class="ph-bold ph-arrow-right"></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
  </div>
</template>

<style scoped>
/* Reutilizamos las clases globales brutalistas de app.vue pero aquí aseguramos consistencia si faltan */
</style>