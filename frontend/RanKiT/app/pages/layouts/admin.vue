<script setup>
import { ref, onMounted } from 'vue'
import AppLogo from '@/components/AppLogo.vue'

// --- Lógica de Traducción ---
const currentLang = ref('es')

const t = {
  es: {
    role: "Panel Admin",
    nav: {
      summary: "Resumen",
      tournaments: "Mis Torneos",
      tools: "Herramientas",
      streaming: "Streaming",
      system: "Sistema",
      settings: "Configuración"
    },
    user: {
      name: "Cometax Org",
      plan: "Enterprise"
    }
  },
  en: {
    role: "Admin Panel",
    nav: {
      summary: "Overview",
      tournaments: "My Tournaments",
      tools: "Tools",
      streaming: "Streaming",
      system: "System",
      settings: "Settings"
    },
    user: {
      name: "Cometax Org",
      plan: "Enterprise"
    }
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

const updateTheme = () => {
  if (process.client) {
    const html = document.documentElement
    if (isDark.value) {
      html.classList.add('dark')
      localStorage.setItem('theme', 'dark')
    } else {
      html.classList.remove('dark')
      localStorage.setItem('theme', 'light')
    }
  }
}

onMounted(() => {
  if (process.client) {
    const savedTheme = localStorage.getItem('theme')
    const systemPrefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches
    // Por defecto dark si no hay preferencia guardada
    isDark.value = savedTheme === 'dark' || (!savedTheme && systemPrefersDark)
    
    // Forzar actualización inicial si estaba guardado como light
    if (savedTheme === 'light') isDark.value = false;
    
    updateTheme()
  }
})
</script>

<template>
  <div class="flex h-screen overflow-hidden text-sm font-sans transition-colors duration-300
    bg-gray-100 text-gray-900 
    dark:bg-[#050505] dark:text-white selection:bg-[var(--rankit-neon)] selection:text-white">
    
    <!-- Sidebar -->
    <aside class="flex flex-col shrink-0 w-64 z-20 shadow-xl transition-colors duration-300
      bg-white border-r border-gray-200
      dark:bg-[#0a0a0a] dark:border-[#333] dark:shadow-none">
        
        <!-- Logo Header -->
        <div class="flex items-center h-20 px-6 gap-3 transition-colors duration-300
          border-b border-gray-200
          dark:border-[#333]">
             <div class="w-8 h-8 text-black transition-colors dark:text-white">
                <AppLogo />
             </div>
            <div class="flex flex-col">
                <span class="text-lg italic font-bold tracking-wider text-black uppercase transition-colors font-display dark:text-white">RANK<span class="text-[var(--rankit-neon)]">IT</span></span>
                <span class="text-[9px] text-gray-500 uppercase tracking-[0.2em] font-mono">{{ t[currentLang].role }}</span>
            </div>
        </div>
        
        <!-- Navigation Links -->
        <div class="flex-1 py-6 space-y-1 overflow-y-auto">
            <!-- CORRECCIÓN AQUÍ: Usamos exact-active-class en lugar de active-class para el Resumen -->
            <NuxtLink to="/admin"
                class="relative flex items-center px-6 py-3 overflow-hidden text-gray-600 transition border-r-4 border-transparent group hover:text-black hover:bg-gray-100 dark:text-gray-500 dark:hover:text-white dark:hover:bg-white/5" 
                exact-active-class="!text-[var(--rankit-neon)] !border-[var(--rankit-neon)] bg-[var(--rankit-neon)]/5 dark:!text-[var(--rankit-neon)]">
                <i class="mr-3 text-xl transition-transform ph ph-chart-pie-slice group-hover:scale-110"></i>
                <span class="text-xs font-bold tracking-wider uppercase">{{ t[currentLang].nav.summary }}</span>
            </NuxtLink>

            <NuxtLink to="/admin/tournaments" 
                class="relative flex items-center px-6 py-3 overflow-hidden text-gray-600 transition border-r-4 border-transparent group hover:text-black hover:bg-gray-100 dark:text-gray-500 dark:hover:text-white dark:hover:bg-white/5" 
                active-class="!text-[var(--rankit-neon)] !border-[var(--rankit-neon)] bg-[var(--rankit-neon)]/5 dark:!text-[var(--rankit-neon)]">
                <i class="mr-3 text-xl transition-transform ph ph-trophy group-hover:scale-110"></i>
                <span class="text-xs font-bold tracking-wider uppercase">{{ t[currentLang].nav.tournaments }}</span>
            </NuxtLink>
            
            <div class="pt-8 pb-4 px-6 text-[10px] font-bold text-gray-400 dark:text-gray-600 uppercase tracking-[0.3em]">{{ t[currentLang].nav.tools }}</div>
            
            <NuxtLink to="/admin/streaming" 
                class="relative flex items-center px-6 py-3 overflow-hidden text-gray-600 transition border-r-4 border-transparent group hover:text-black hover:bg-gray-100 dark:text-gray-500 dark:hover:text-white dark:hover:bg-white/5" 
                active-class="!text-[var(--rankit-neon)] !border-[var(--rankit-neon)] bg-[var(--rankit-neon)]/5 dark:!text-[var(--rankit-neon)]">
                <i class="mr-3 text-xl transition-transform ph ph-broadcast group-hover:scale-110"></i>
                <span class="text-xs font-bold tracking-wider uppercase">{{ t[currentLang].nav.streaming }}</span>
            </NuxtLink>

             <div class="pt-8 pb-4 px-6 text-[10px] font-bold text-gray-400 dark:text-gray-600 uppercase tracking-[0.3em]">{{ t[currentLang].nav.system }}</div>

             <NuxtLink to="/admin/settings" 
                class="relative flex items-center px-6 py-3 overflow-hidden text-gray-600 transition border-r-4 border-transparent group hover:text-black hover:bg-gray-100 dark:text-gray-500 dark:hover:text-white dark:hover:bg-white/5" 
                active-class="!text-[var(--rankit-neon)] !border-[var(--rankit-neon)] bg-[var(--rankit-neon)]/5 dark:!text-[var(--rankit-neon)]">
                <i class="mr-3 text-xl transition-transform ph ph-gear group-hover:scale-110"></i>
                <span class="text-xs font-bold tracking-wider uppercase">{{ t[currentLang].nav.settings }}</span>
            </NuxtLink>
        </div>
        
        <!-- Controls & User Profile -->
        <div class="p-4 transition-colors duration-300
            border-t border-gray-200 bg-gray-50
            dark:border-[#333] dark:bg-[#080808]">
            
            <!-- Toggles Row -->
            <div class="flex items-center justify-between px-1 mb-4">
                <button @click="toggleTheme" class="p-2 text-gray-500 transition-colors rounded-md hover:bg-gray-200 hover:text-black dark:text-gray-400 dark:hover:bg-white/10 dark:hover:text-white">
                    <i v-if="isDark" class="text-lg ph-fill ph-sun"></i>
                    <i v-else class="text-lg ph-fill ph-moon"></i>
                </button>
                
                <button @click="toggleLanguage" class="flex items-center gap-1 text-[10px] font-bold border px-2 py-1 rounded transition-colors
                    border-gray-300 text-gray-600 hover:border-[var(--rankit-neon)] hover:text-black
                    dark:border-gray-700 dark:text-gray-400 dark:hover:text-white">
                    <i class="text-sm ph ph-translate"></i>
                    <span>{{ currentLang.toUpperCase() }}</span>
                </button>
            </div>

            <div class="flex items-center gap-3">
                <div class="w-10 h-10 overflow-hidden border border-gray-300 rounded dark:border-gray-700">
                    <img src="https://i.pravatar.cc/150?img=60" class="object-cover w-full h-full">
                </div>
                <div>
                    <div class="text-sm font-bold text-black uppercase transition-colors font-display dark:text-white">{{ t[currentLang].user.name }}</div>
                    <div class="text-[9px] text-[var(--rankit-neon)] uppercase tracking-wider font-bold">{{ t[currentLang].user.plan }}</div>
                </div>
                <button class="ml-auto text-gray-400 transition hover:text-red-500"><i class="text-xl ph ph-sign-out"></i></button>
            </div>
        </div>
    </aside>

    <!-- Main Content Area -->
    <main class="flex-1 flex flex-col relative overflow-y-auto transition-colors duration-300
        bg-gray-100 bg-tech-grid-light
        dark:bg-[#050505] dark:bg-tech-grid-dark bg-[length:40px_40px]">
        
        <!-- Gradient Overlay for Dark Mode Depth -->
        <div class="absolute inset-0 pointer-events-none fixed transition-opacity duration-300
            opacity-0 dark:opacity-100
            bg-gradient-to-b from-[#050505]/50 via-[#050505]/80 to-[#050505]"></div>
            
        <NuxtPage class="relative z-10" />
    </main>
  </div>
</template>

<style>
@import url('https://fonts.googleapis.com/css2?family=Chakra+Petch:wght@300;400;500;600;700&family=Archivo:wght@300;400;600;800&display=swap');

:root {
  --rankit-neon: #bf00ff;
}

body {
  font-family: 'Archivo', sans-serif;
}

.font-display {
  font-family: 'Chakra Petch', sans-serif;
}

/* Grids adaptativos */
.bg-tech-grid-dark {
    background-image: linear-gradient(to right, rgba(255, 255, 255, 0.03) 1px, transparent 1px), linear-gradient(to bottom, rgba(255, 255, 255, 0.03) 1px, transparent 1px);
}
.bg-tech-grid-light {
    background-image: linear-gradient(to right, rgba(0, 0, 0, 0.05) 1px, transparent 1px), linear-gradient(to bottom, rgba(0, 0, 0, 0.05) 1px, transparent 1px);
}

/* Scrollbar Adaptativo */
::-webkit-scrollbar { width: 6px; }

/* Dark Scrollbar */
.dark ::-webkit-scrollbar-track { background: #050505; }
.dark ::-webkit-scrollbar-thumb { background: #333; border-radius: 3px; }

/* Light Scrollbar */
html:not(.dark) ::-webkit-scrollbar-track { background: #e5e5e5; }
html:not(.dark) ::-webkit-scrollbar-thumb { background: #a3a3a3; border-radius: 3px; }

::-webkit-scrollbar-thumb:hover { background: var(--rankit-neon); }

/* Global Brutalist Classes for Children (Adaptive) - CORREGIDO */
.brutal-card { 
    transition: all 0.2s cubic-bezier(0.25, 0.46, 0.45, 0.94);
    @apply transition-colors duration-300;
}

/* Estilos Modo Oscuro */
.dark .brutal-card {
    background: #0a0a0a;
    border: 1px solid #333;
    color: white;
}

/* Estilos Modo Claro (CORREGIDO) */
html:not(.dark) .brutal-card {
    background: #ffffff;
    border: 1px solid #e5e5e5;
    box-shadow: 2px 2px 0px rgba(0,0,0,0.05);
    color: #111827; /* Gray-900 */
}

.brutal-card:hover { 
    border-color: var(--rankit-neon); 
    transform: translate(-2px, -2px);
}

.dark .brutal-card:hover {
    box-shadow: 4px 4px 0px var(--rankit-neon);
}

html:not(.dark) .brutal-card:hover {
    box-shadow: 4px 4px 0px var(--rankit-neon);
}

/* Botones */
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
    background-color: white; 
    color: black; 
    box-shadow: 0 0 15px var(--rankit-neon);
}
/* Invertir hover en light mode para alto contraste */
html:not(.dark) .btn-skew:hover {
    background-color: black;
    color: white;
}

.btn-content { transform: skewX(10deg); }

/* Inputs Adaptativos - CORREGIDO */
.brutal-input {
    width: 100%;
    background: transparent;
    border-bottom: 2px solid;
    padding: 0.75rem 0;
    font-family: 'Archivo', sans-serif;
    font-weight: 600;
    outline: none;
    transition: all 0.3s;
    font-size: 0.875rem;
}

.dark .brutal-input {
    border-color: #333;
    color: white;
}
.dark .brutal-input::placeholder { color: #666; }

html:not(.dark) .brutal-input {
    border-color: #e5e5e5;
    color: #111;
}
html:not(.dark) .brutal-input::placeholder { color: #9ca3af; }

.brutal-input:focus {
    border-color: var(--rankit-neon);
    padding-left: 0.5rem;
}
</style>