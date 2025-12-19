<script setup>
import { ref, onMounted, watch } from 'vue'
import AppLogo from '@/components/AppLogo.vue'

// --- Lógica de Traducción ---
const currentLang = ref('es')

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

// Inicialización
onMounted(() => {
    if (process.client) {
        // Cargar tema guardado o preferencia del sistema
        const savedTheme = localStorage.getItem('theme')
        const systemPrefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches
        
        isDark.value = savedTheme === 'dark' || (!savedTheme && systemPrefersDark)
        updateTheme()
    }
})

// Textos (Diccionario)
const t = {
    es: {
        nav: { tournaments: "Torneos", pricing: "Precios", custom: "Personalizado", partners: "Partners", login: "Ingresar", create: "Crear Cuenta" },
        hero: { badge: "Plataforma de Torneos v2.0", title1: "Gestiona.", title2: "Compite.", title3: "Escala.", desc: "Desde canchas de fútbol 7 hasta arenas de esports. Rankit es el sistema operativo para organizadores que buscan profesionalismo.", btnOrganize: "Organizar Torneo", btnDemo: "Ver Demo" },
        tournaments: { title: "Torneos", titleSub: "En Curso", desc: "Únete a la competencia. Demuestra tu nivel.", viewAll: "Ver Todos", cardCreate: "Crea tu Torneo" },
        pricing: { title: "Elige tu", titleSub: "Nivel", desc: "Planes diseñados para cada etapa de tu organización. Cancela cuando quieras.", period: "mes", btnStart: "Comenzar", btnPro: "Obtener Pro", btnContact: "Contactar", recommended: "Recomendado", planEnterprise: "Empresas", feat: { tournaments: "2 Torneos al mes", brackets: "Brackets Automáticos", unlimited: "Torneos Ilimitados", obs: "OBS Widgets (Streaming)", payments: "Pagos integrados", multiuser: "Multiusuario", whitelabel: "Marca Blanca" } },
        custom: { title: "¿Necesitas algo", titleSub: "a medida?", desc: "Para organizaciones grandes, ligas nacionales o integraciones personalizadas. Cuéntanos tu proyecto.", btnSend: "Enviar Solicitud", feats: ["Desarrollo de Features Exclusivos", "Servidores Dedicados", "Soporte de Ingeniería 24/7"] },
        alliance: { title: "Únete al", titleSub: "Sindicato", techTitle: "Vendedores de Tecnología", creatorTitle: "Creadores de Contenido" }
    },
    en: {
        nav: { tournaments: "Tournaments", pricing: "Pricing", custom: "Custom", partners: "Partners", login: "Login", create: "Sign Up" },
        hero: { badge: "Tournament Platform v2.0", title1: "Manage.", title2: "Compete.", title3: "Scale.", desc: "From soccer fields to esports arenas. Rankit is the operating system for organizers seeking professionalism.", btnOrganize: "Organize Tournament", btnDemo: "Watch Demo" },
        tournaments: { title: "Live", titleSub: "Tournaments", desc: "Join the competition. Show your skills.", viewAll: "View All", cardCreate: "Create Tournament" },
        pricing: { title: "Choose your", titleSub: "Level", desc: "Plans designed for every stage of your organization. Cancel anytime.", period: "mo", btnStart: "Start Now", btnPro: "Get Pro", btnContact: "Contact Sales", recommended: "Recommended", planEnterprise: "Enterprise", feat: { tournaments: "2 Tournaments/mo", brackets: "Auto Brackets", unlimited: "Unlimited Tournaments", obs: "OBS Widgets", payments: "Integrated Payments", multiuser: "Multi-user Access", whitelabel: "White Label" } },
        custom: { title: "Need something", titleSub: "custom?", desc: "For large organizations, national leagues, or custom integrations. Tell us about your project.", btnSend: "Send Request", feats: ["Exclusive Feature Development", "Dedicated Servers", "24/7 Engineering Support"] },
        alliance: { title: "Join the", titleSub: "Syndicate", techTitle: "Tech Sellers", creatorTitle: "Content Creators" }
    }
}

// Configuración de Meta Tags (Fuentes y CSS externo necesario)
useHead({
    title: 'Rankit - The Competitive Ecosystem',
    script: [
        { src: 'https://unpkg.com/@phosphor-icons/web' },
        { src: 'https://cdn.tailwindcss.com' } // Asegurar compatibilidad visual rápida si no hay build step
    ],
    link: [
        { rel: 'stylesheet', href: 'https://fonts.googleapis.com/css2?family=Chakra+Petch:wght@300;400;500;600;700&family=Archivo:wght@300;400;600;800&display=swap' }
    ]
})
</script>

<template>
<!-- Wrapper principal que simula el body -->
<div class="main-wrapper min-h-screen transition-colors duration-300 selection:bg-[var(--rankit-neon)] selection:text-white"
     :class="isDark ? 'bg-[#050505] text-white' : 'bg-gray-50 text-gray-900'">

    <!-- Navbar -->
    <nav class="fixed z-50 flex items-center justify-between w-full h-20 px-6 transition-colors duration-300 border-b lg:px-12 backdrop-blur-md"
         :class="isDark ? 'bg-[#050505]/95 border-white/10' : 'bg-white/90 border-gray-200'">
        
        <NuxtLink to="/" class="flex items-center gap-3 cursor-pointer group">
            <div class="w-10 h-10 transition-colors" :class="isDark ? 'text-white' : 'text-black group-hover:text-[var(--rankit-neon)]'">
                <AppLogo />
            </div>
            <span class="text-3xl italic font-bold tracking-tighter uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">Rankit</span>
        </NuxtLink>

        <div class="hidden gap-8 text-sm font-bold tracking-widest uppercase md:flex" :class="isDark ? 'text-gray-400' : 'text-gray-500'">
            <a href="#torneos" class="hover:text-[var(--rankit-neon)] transition">{{ t[currentLang].nav.tournaments }}</a>
            <a href="#pricing" class="hover:text-[var(--rankit-neon)] transition" :class="isDark ? 'text-white' : 'text-black'">{{ t[currentLang].nav.pricing }}</a>
            <a href="#contacto" class="hover:text-[var(--rankit-neon)] transition">{{ t[currentLang].nav.custom }}</a>
            <a href="#partners" class="hover:text-[var(--rankit-neon)] transition text-[var(--rankit-neon)]">{{ t[currentLang].nav.partners }}</a>
        </div>

        <div class="flex items-center gap-4">
            <!-- Language Toggle -->
            <button @click="toggleLanguage" class="flex items-center gap-1 text-xs font-bold border px-2 py-1 rounded hover:border-[var(--rankit-neon)] transition-colors"
                    :class="isDark ? 'border-gray-700 text-white' : 'border-gray-300 text-black'">
                <i class="text-lg ph ph-translate"></i>
                <span>{{ currentLang.toUpperCase() }}</span>
            </button>

            <!-- Theme Toggle -->
            <button @click="toggleTheme" class="p-2 mr-2 transition-colors border border-transparent rounded-lg"
                    :class="isDark ? 'text-gray-400 hover:text-[var(--rankit-neon)] hover:border-gray-700' : 'text-gray-500 hover:text-[var(--rankit-neon)] hover:border-gray-300'">
                <i v-if="isDark" class="text-xl ph-fill ph-sun"></i>
                <i v-else class="text-xl ph-fill ph-moon"></i>
            </button>

            <NuxtLink to="/community" class="hidden sm:block text-sm font-bold uppercase tracking-wider mr-4 hover:text-[var(--rankit-neon)] transition"
                      :class="isDark ? 'text-gray-300' : 'text-gray-600'">
                {{ t[currentLang].nav.login }}
            </NuxtLink>
            
            <NuxtLink to="/start" class="px-6 py-2 text-sm font-bold tracking-wider uppercase btn-skew">
                <span class="btn-content">{{ t[currentLang].nav.create }}</span>
            </NuxtLink>
        </div>
    </nav>

    <!-- Hero Section -->
    <header class="relative min-h-[90vh] flex items-center pt-20 bg-[length:40px_40px]"
            :class="isDark ? 'bg-tech-grid-dark' : 'bg-tech-grid-light'">
        
        <div class="absolute inset-0 pointer-events-none bg-gradient-to-b"
             :class="isDark ? 'from-transparent via-[#050505]/50 to-[#050505]' : 'from-transparent via-gray-50/80 to-gray-50'"></div>
        
        <div class="relative z-10 grid items-center w-full grid-cols-1 gap-16 px-6 mx-auto max-w-7xl lg:grid-cols-2">
            <div>
                <!-- CometaX Badge -->
                <div class="flex items-center gap-2 mb-6 transition-opacity cursor-default opacity-70 hover:opacity-100 group">
                    <span class="text-[10px] font-bold uppercase tracking-widest text-gray-500">Powered by</span>
                    <img src="https://raw.githubusercontent.com/JFabrizzio5/CometaX/bbeb654b90e817236d9d64009b33618065fbba91/image_2025-12-16_083018257-removebg-preview%20(1).png" class="w-auto h-6 transition-transform group-hover:scale-105" :class="isDark ? 'invert' : ''" alt="CometaX Logo">
                </div>

                <div class="flex items-center gap-4 mb-6">
                    <div class="w-16 h-16 shrink-0" :class="isDark ? 'text-white' : 'text-black'">
                        <AppLogo />
                    </div>

                    <div class="inline-flex items-center gap-2 border border-[var(--rankit-neon)] text-[var(--rankit-neon)] px-3 py-1 text-xs font-bold uppercase tracking-widest bg-[var(--rankit-neon)]/5 h-fit">
                        <span class="w-2 h-2 bg-[var(--rankit-neon)] rounded-full animate-pulse"></span>
                        <span>{{ t[currentLang].hero.badge }}</span>
                    </div>
                </div>

                <h1 class="font-display font-black text-6xl md:text-8xl leading-[0.9] mb-6 uppercase" :class="isDark ? 'text-white' : 'text-black'">
                    <span>{{ t[currentLang].hero.title1 }}</span><br>
                    <span>{{ t[currentLang].hero.title2 }}</span><br>
                    <span class="text-transparent bg-clip-text bg-gradient-to-r from-[var(--rankit-neon)]" :class="isDark ? 'to-white' : 'to-black'">{{ t[currentLang].hero.title3 }}</span>
                </h1>
                <p class="text-xl mb-8 max-w-lg font-light border-l-4 border-[var(--rankit-neon)] pl-6" :class="isDark ? 'text-gray-400' : 'text-gray-600'">
                    {{ t[currentLang].hero.desc }}
                </p>
                <div class="flex flex-col gap-4 sm:flex-row">
                    <NuxtLink to="/admin" class="px-10 py-4 text-lg font-bold tracking-wider uppercase btn-skew">
                        <span class="btn-content">{{ t[currentLang].hero.btnOrganize }}</span>
                    </NuxtLink>
                    <button class="px-8 py-4 text-lg font-bold tracking-wider uppercase btn-skew-outline" :class="isDark ? 'text-white' : 'text-black'">
                        <span class="flex items-center gap-3 btn-content">
                            <i class="text-2xl ph ph-play-circle"></i> <span>{{ t[currentLang].hero.btnDemo }}</span>
                        </span>
                    </button>
                </div>
            </div>

            <!-- Hero Visuals (Cards Flotantes) -->
            <div class="relative hidden lg:block h-[600px]">
                <div class="absolute z-10 p-3 transition duration-500 border shadow-xl top-10 right-10 w-72 hover:scale-105 brutal-card">
                    <div class="relative h-40 mb-3 overflow-hidden" :class="isDark ? 'bg-gray-800' : 'bg-gray-200'">
                        <img src="https://images.unsplash.com/photo-1522770179533-24471fcdba45?q=80&w=800" class="object-cover w-full h-full grayscale opacity-80 mix-blend-multiply dark:mix-blend-normal">
                        <div class="absolute top-2 left-2 bg-black text-white text-[10px] font-bold px-2 py-1 uppercase">Fútbol 7</div>
                    </div>
                    <div class="flex items-end justify-between">
                        <div>
                            <h3 class="text-lg font-bold uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">Night League</h3>
                            <p class="text-[10px] text-gray-500 font-mono">FINALES • HOY 20:00</p>
                        </div>
                        <div class="text-xl font-mono font-bold text-[var(--rankit-neon)]">2 - 1</div>
                    </div>
                </div>

                <div class="absolute top-40 right-48 w-80 p-3 z-20 border border-[var(--rankit-neon)] shadow-[0_10px_40px_rgba(0,0,0,0.1)] dark:shadow-[0_0_50px_rgba(0,0,0,0.8)]" 
                     :class="isDark ? 'bg-[#111]' : 'bg-white'">
                    <div class="relative h-48 mb-3 overflow-hidden" :class="isDark ? 'bg-gray-800' : 'bg-gray-200'">
                        <img src="https://images.unsplash.com/photo-1542751371-adc38448a05e?q=80&w=800" class="object-cover w-full h-full opacity-90 dark:opacity-80">
                        <div class="absolute top-2 right-2 bg-red-600 text-white text-[10px] font-bold px-2 py-1 uppercase animate-pulse">Live</div>
                    </div>
                    <div class="flex items-center justify-between mb-2">
                        <div class="flex items-center gap-2">
                            <img src="https://ui-avatars.com/api/?name=T1&background=000&color=fff" class="w-6 h-6 bg-gray-300 rounded dark:bg-gray-800">
                            <span class="text-sm font-bold" :class="isDark ? 'text-white' : 'text-black'">T1</span>
                        </div>
                        <span class="font-mono font-bold text-[var(--rankit-neon)] text-lg">13 - 11</span>
                        <div class="flex items-center gap-2">
                            <span class="text-sm font-bold" :class="isDark ? 'text-white' : 'text-black'">G2</span>
                            <img src="https://ui-avatars.com/api/?name=G2&background=000&color=fff" class="w-6 h-6 bg-gray-300 rounded dark:bg-gray-800">
                        </div>
                    </div>
                    <div class="w-full h-1 overflow-hidden rounded-full" :class="isDark ? 'bg-gray-800' : 'bg-gray-200'">
                        <div class="h-full bg-[var(--rankit-neon)] w-3/4"></div>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <!-- Marquee -->
    <div class="relative py-6 overflow-hidden select-none border-y"
         :class="isDark ? 'bg-black border-white/10' : 'bg-white border-gray-200'">
        
        <div class="absolute top-0 bottom-0 left-0 z-10 w-24 pointer-events-none bg-gradient-to-r to-transparent"
             :class="isDark ? 'from-black' : 'from-white'"></div>
        <div class="absolute top-0 bottom-0 right-0 z-10 w-24 pointer-events-none bg-gradient-to-l to-transparent"
             :class="isDark ? 'from-black' : 'from-white'"></div>

        <div class="flex w-max animate-marquee items-center opacity-80 uppercase font-bold tracking-[0.2em] text-sm text-gray-500 dark:text-gray-400">
            <div class="flex items-center gap-16 px-8" v-for="i in 2" :key="i">
                <span>Valorant</span> <span class="text-[var(--rankit-neon)] text-lg">•</span> 
                <span>Fútbol 7</span> <span class="text-[var(--rankit-neon)] text-lg">•</span> 
                <span>League of Legends</span> <span class="text-[var(--rankit-neon)] text-lg">•</span> 
                <span>Padel</span> <span class="text-[var(--rankit-neon)] text-lg">•</span> 
                <span>Fortnite</span> <span class="text-[var(--rankit-neon)] text-lg">•</span> 
                <span>Basquetbol</span> <span class="text-[var(--rankit-neon)] text-lg">•</span> 
                <span>Call of Duty</span> <span class="text-[var(--rankit-neon)] text-lg">•</span> 
                <span>FIFA/EAFC</span>
                <span class="text-[var(--rankit-neon)] text-lg">•</span>
            </div>
        </div>
    </div>

    <!-- Active Tournaments -->
    <section id="torneos" class="py-24" :class="isDark ? 'bg-[#080808]' : 'bg-gray-50'">
        <div class="px-6 mx-auto max-w-7xl">
            <div class="flex items-end justify-between mb-12">
                <div>
                    <h2 class="mb-2 text-4xl font-black uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">
                        {{ t[currentLang].tournaments.title }} <span class="text-[var(--rankit-neon)]">{{ t[currentLang].tournaments.titleSub }}</span>
                    </h2>
                    <p class="font-mono text-xs" :class="isDark ? 'text-gray-400' : 'text-gray-500'">{{ t[currentLang].tournaments.desc }}</p>
                </div>
                <NuxtLink to="/start" class="hidden md:flex items-center gap-2 text-sm font-bold uppercase tracking-wider hover:text-[var(--rankit-neon)] transition" :class="isDark ? 'text-white' : 'text-black'">
                    {{ t[currentLang].tournaments.viewAll }} <i class="ph ph-arrow-right"></i>
                </NuxtLink>
            </div>

            <div class="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-4">
                <!-- Cards -->
                <NuxtLink to="/tournaments/1" class="block cursor-pointer brutal-card group">
                    <div class="aspect-[4/3] relative overflow-hidden border-b" :class="isDark ? 'border-gray-800' : 'border-gray-200'">
                        <img src="https://images.unsplash.com/photo-1624138784181-2999e4253b85?q=80&w=800" class="object-cover w-full h-full transition duration-500 opacity-90 dark:opacity-60 group-hover:opacity-100 group-hover:scale-110">
                        <div class="absolute top-0 left-0 bg-[var(--rankit-neon)] text-black text-xs font-bold px-3 py-1 uppercase">Esports</div>
                    </div>
                    <div class="p-6">
                        <h3 class="font-display font-bold text-xl uppercase mb-1 group-hover:text-[var(--rankit-neon)] transition" :class="isDark ? 'text-white' : 'text-black'">Valorant Masters</h3>
                        <p class="mb-4 font-mono text-xs text-gray-500">5v5 • PC • $10,000 MXN</p>
                    </div>
                </NuxtLink>

                <div class="cursor-pointer brutal-card group">
                    <div class="aspect-[4/3] relative overflow-hidden border-b" :class="isDark ? 'border-gray-800' : 'border-gray-200'">
                        <img src="https://images.unsplash.com/photo-1579952363873-27f3bade9f55?q=80&w=800" class="object-cover w-full h-full transition duration-500 opacity-90 dark:opacity-60 group-hover:opacity-100 group-hover:scale-110">
                        <div class="absolute top-0 left-0 px-3 py-1 text-xs font-bold text-white uppercase bg-black">Deportes</div>
                    </div>
                    <div class="p-6">
                        <h3 class="font-display font-bold text-xl uppercase mb-1 group-hover:text-[var(--rankit-neon)] transition" :class="isDark ? 'text-white' : 'text-black'">Copa Nocturna</h3>
                        <p class="mb-4 font-mono text-xs text-gray-500">Fut 7 • Cancha Sur • Trofeo</p>
                    </div>
                </div>

                <div class="flex flex-col items-center justify-center p-6 text-center bg-transparent border-gray-300 border-dashed cursor-pointer brutal-card group hover:border-solid" :class="isDark ? 'border-gray-800' : ''">
                    <div class="w-16 h-16 rounded-full flex items-center justify-center mb-4 transition text-gray-500 group-hover:bg-[var(--rankit-neon)] group-hover:text-black" :class="isDark ? 'bg-gray-900' : 'bg-gray-200'">
                        <i class="text-2xl ph ph-plus"></i>
                    </div>
                    <h3 class="text-lg font-bold text-gray-500 uppercase font-display group-hover:text-black dark:group-hover:text-white">{{ t[currentLang].tournaments.cardCreate }}</h3>
                </div>
            </div>
        </div>
    </section>

    <!-- Pricing Section -->
    <section id="pricing" class="relative py-24 overflow-hidden border-t"
             :class="isDark ? 'bg-black border-white/10' : 'bg-white border-gray-200'">
        
        <div class="relative z-10 px-6 mx-auto max-w-7xl">
            <div class="mb-20 text-center">
                <h2 class="mb-4 text-5xl font-black uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">
                    {{ t[currentLang].pricing.title }} <span class="text-[var(--rankit-neon)]">{{ t[currentLang].pricing.titleSub }}</span>
                </h2>
                <p class="max-w-2xl mx-auto font-light" :class="isDark ? 'text-gray-400' : 'text-gray-500'">
                    {{ t[currentLang].pricing.desc }}
                </p>
            </div>

            <div class="grid items-end grid-cols-1 gap-8 md:grid-cols-3">
                <!-- Base -->
                <div class="p-8 brutal-card h-min" :class="isDark ? 'bg-[#080808]' : 'bg-white'">
                    <h3 class="text-2xl font-bold uppercase font-display" :class="isDark ? 'text-gray-300' : 'text-gray-700'">Base</h3>
                    <div class="flex items-baseline mt-4 mb-6">
                        <span class="text-4xl font-black" :class="isDark ? 'text-white' : 'text-black'">$100</span>
                        <span class="ml-2 font-mono text-sm text-gray-500">MXN / <span>{{ t[currentLang].pricing.period }}</span></span>
                    </div>
                    <button class="w-full py-3 mb-8 text-xs font-bold tracking-wider uppercase transition border hover:border-black dark:hover:border-white"
                            :class="isDark ? 'border-gray-700 text-white' : 'border-gray-300 text-black'">
                        {{ t[currentLang].pricing.btnStart }}
                    </button>
                    <ul class="space-y-4 text-sm" :class="isDark ? 'text-gray-300' : 'text-gray-600'">
                        <li class="flex items-center gap-3"><i class="ph ph-check text-[var(--rankit-neon)]"></i> {{ t[currentLang].pricing.feat.tournaments }}</li>
                        <li class="flex items-center gap-3"><i class="ph ph-check text-[var(--rankit-neon)]"></i> {{ t[currentLang].pricing.feat.brackets }}</li>
                    </ul>
                </div>

                <!-- Pro -->
                <div class="brutal-card p-8 border-[var(--rankit-neon)] relative transform md:-translate-y-4 shadow-xl" :class="isDark ? 'bg-[#0a0a0a]' : 'bg-white'">
                    <div class="absolute top-0 left-1/2 -translate-x-1/2 -translate-y-1/2 bg-[var(--rankit-neon)] text-black text-[10px] font-bold px-3 py-1 uppercase tracking-widest">
                        {{ t[currentLang].pricing.recommended }}
                    </div>
                    <h3 class="text-3xl font-bold uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">Gestor Pro</h3>
                    <div class="flex items-baseline mt-4 mb-6">
                        <span class="text-5xl font-black" :class="isDark ? 'text-white' : 'text-black'">$800</span>
                        <span class="ml-2 font-mono text-sm text-gray-500">MXN / <span>{{ t[currentLang].pricing.period }}</span></span>
                    </div>
                    <button class="w-full bg-[var(--rankit-neon)] hover:bg-black hover:text-white dark:hover:bg-white dark:hover:text-black text-black font-bold py-4 uppercase text-xs tracking-wider transition mb-8 shadow-[0_0_20px_var(--rankit-neon)]">
                        {{ t[currentLang].pricing.btnPro }}
                    </button>
                    <ul class="space-y-4 text-sm" :class="isDark ? 'text-white' : 'text-gray-800'">
                        <li class="flex items-center gap-3"><i class="ph ph-check-circle text-[var(--rankit-neon)] text-lg"></i> {{ t[currentLang].pricing.feat.unlimited }}</li>
                        <li class="flex items-center gap-3"><i class="ph ph-check-circle text-[var(--rankit-neon)] text-lg"></i> {{ t[currentLang].pricing.feat.obs }}</li>
                        <li class="flex items-center gap-3"><i class="ph ph-check-circle text-[var(--rankit-neon)] text-lg"></i> {{ t[currentLang].pricing.feat.payments }}</li>
                    </ul>
                </div>

                <!-- Enterprise -->
                <div class="p-8 brutal-card h-min" :class="isDark ? 'bg-[#080808]' : 'bg-white'">
                    <h3 class="text-2xl font-bold uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">{{ t[currentLang].pricing.planEnterprise }}</h3>
                    <div class="flex items-baseline mt-4 mb-6">
                        <span class="text-4xl font-black" :class="isDark ? 'text-white' : 'text-black'">$5,000</span>
                        <span class="ml-2 font-mono text-sm text-gray-500">MXN / <span>{{ t[currentLang].pricing.period }}</span></span>
                    </div>
                    <button class="w-full py-3 mb-8 text-xs font-bold tracking-wider uppercase transition border hover:border-black dark:hover:border-white"
                            :class="isDark ? 'border-gray-700 text-white' : 'border-gray-300 text-black'">
                        {{ t[currentLang].pricing.btnContact }}
                    </button>
                    <ul class="space-y-4 text-sm" :class="isDark ? 'text-gray-300' : 'text-gray-600'">
                        <li class="flex items-center gap-3"><i class="ph ph-check text-[var(--rankit-neon)]"></i> {{ t[currentLang].pricing.feat.multiuser }}</li>
                        <li class="flex items-center gap-3"><i class="ph ph-check text-[var(--rankit-neon)]"></i> {{ t[currentLang].pricing.feat.whitelabel }}</li>
                    </ul>
                </div>
            </div>
        </div>
    </section>

    <!-- Partners / Alliance -->
    <section id="partners" class="py-24 bg-[length:20px_20px] relative border-t"
             :class="isDark ? 'bg-tech-grid-dark border-white/10 bg-[#050505]' : 'bg-tech-grid-light border-gray-200 bg-gray-50'">
        <div class="absolute inset-0 pointer-events-none bg-gradient-to-t via-transparent to-transparent opacity-80"
             :class="isDark ? 'from-black' : 'from-white'"></div>
        <div class="relative z-10 max-w-6xl px-6 mx-auto">
            <div class="mb-16 text-center">
                <span class="text-[var(--rankit-neon)] font-bold tracking-[0.3em] uppercase text-xs mb-3 block">Rankit Alliance</span>
                <h2 class="mb-6 text-4xl font-black uppercase font-display md:text-5xl" :class="isDark ? 'text-white' : 'text-black'">
                    {{ t[currentLang].alliance.title }} <span class="text-transparent bg-clip-text bg-gradient-to-r from-[var(--rankit-neon)] to-purple-600">{{ t[currentLang].alliance.titleSub }}</span>
                </h2>
            </div>

            <div class="grid grid-cols-1 gap-8 md:grid-cols-2">
                <!-- Tech Partner -->
                <div class="p-10 border-l-4 brutal-card border-l-blue-500 group" :class="isDark ? 'bg-[#0c0c0c]' : 'bg-white'">
                    <div class="flex items-center justify-center w-12 h-12 mb-6 text-blue-500 rounded-lg bg-blue-500/10">
                        <i class="text-2xl ph ph-handshake"></i>
                    </div>
                    <h3 class="mb-3 text-2xl font-bold uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">{{ t[currentLang].alliance.techTitle }}</h3>
                    <p class="h-12 mb-8" :class="isDark ? 'text-gray-400' : 'text-gray-500'">
                        ¿Vendes software o hardware a canchas deportivas? Añade Rankit a tu portafolio.
                    </p>
                    <a href="#" class="inline-flex items-center gap-2 text-sm font-bold tracking-wider text-blue-500 uppercase transition-colors group-hover:text-blue-400">
                        Aplicar al Programa <i class="ph ph-arrow-right"></i>
                    </a>
                </div>

                <!-- Creator -->
                <div class="p-10 border-l-4 brutal-card border-l-pink-500 group" :class="isDark ? 'bg-[#0c0c0c]' : 'bg-white'">
                    <div class="flex items-center justify-center w-12 h-12 mb-6 text-pink-500 rounded-lg bg-pink-500/10">
                        <i class="text-2xl ph ph-video-camera"></i>
                    </div>
                    <h3 class="mb-3 text-2xl font-bold uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">{{ t[currentLang].alliance.creatorTitle }}</h3>
                    <p class="h-12 mb-8" :class="isDark ? 'text-gray-400' : 'text-gray-500'">
                        Streamers y YouTubers: Usa Rankit para tus torneos comunitarios.
                    </p>
                    <a href="#" class="inline-flex items-center gap-2 text-sm font-bold tracking-wider text-pink-500 uppercase transition-colors group-hover:text-pink-400">
                        Unirse como Creador <i class="ph ph-arrow-right"></i>
                    </a>
                </div>
            </div>
        </div>
    </section>

    <!-- Charity -->
    <section class="px-6 py-20 border-t" :class="isDark ? 'border-white/10 bg-[#080808]' : 'border-gray-200 bg-white'">
        <div class="max-w-4xl mx-auto text-center">
            <div class="w-16 h-16 bg-[var(--rankit-neon)]/10 rounded-full flex items-center justify-center mx-auto mb-6 shadow-[0_0_30px_var(--rankit-neon)]">
                <i class="ph ph-heart text-[var(--rankit-neon)] text-3xl"></i>
            </div>
            <h2 class="mb-4 text-3xl font-black uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">Competencia con <span class="text-[var(--rankit-neon)]">Causa</span></h2>
            <p class="max-w-2xl mx-auto mb-8 text-base leading-relaxed" :class="isDark ? 'text-gray-400' : 'text-gray-600'">
                El éxito sabe mejor compartido. El <span class="font-bold text-[var(--rankit-neon)]">10% de tu suscripción</span> se va directo a causas sociales vía Fundación CometaX.
            </p>
            <div class="flex flex-wrap items-center justify-center gap-8 transition-all duration-500 opacity-60 grayscale hover:grayscale-0">
                <div class="flex items-center gap-2 font-bold text-gray-500" :class="isDark ? 'hover:text-white' : 'hover:text-black'"><i class="ph ph-plant"></i> GreenFields</div>
                <div class="flex items-center gap-2 font-bold text-gray-500" :class="isDark ? 'hover:text-white' : 'hover:text-black'"><i class="ph ph-graduation-cap"></i> EduSports</div>
            </div>
        </div>
    </section>

    <!-- Contact Form -->
    <section id="contacto" class="py-24 border-t" :class="isDark ? 'bg-[#050505] border-white/10' : 'bg-gray-50 border-gray-200'">
        <div class="grid items-center max-w-5xl grid-cols-1 gap-16 px-6 mx-auto md:grid-cols-2">
            <div>
                <span class="text-[var(--rankit-neon)] font-bold tracking-widest uppercase text-xs mb-2 block">Enterprise</span>
                <h2 class="mb-6 text-4xl font-black uppercase font-display md:text-5xl" :class="isDark ? 'text-white' : 'text-black'">
                    {{ t[currentLang].custom.title }} <span class="text-transparent bg-clip-text bg-gradient-to-r from-[var(--rankit-neon)]" :class="isDark ? 'to-white' : 'to-black'">{{ t[currentLang].custom.titleSub }}</span>
                </h2>
                <p class="mb-8 text-lg" :class="isDark ? 'text-gray-400' : 'text-gray-500'">{{ t[currentLang].custom.desc }}</p>
                <ul class="space-y-4 text-sm" :class="isDark ? 'text-gray-300' : 'text-gray-600'">
                    <li v-for="(feat, i) in t[currentLang].custom.feats" :key="i" class="flex items-center gap-3">
                        <i class="ph ph-check text-[var(--rankit-neon)]"></i> {{ feat }}
                    </li>
                </ul>
            </div>

            <div class="p-8 brutal-card" :class="isDark ? 'bg-[#0a0a0a]' : 'bg-white'">
                <form class="space-y-8">
                    <div>
                        <input type="text" :placeholder="t[currentLang].custom.inputName || 'NOMBRE COMPLETO'" class="brutal-input">
                    </div>
                    <div>
                        <input type="email" :placeholder="t[currentLang].custom.inputEmail || 'CORREO ELECTRONICO'" class="brutal-input">
                    </div>
                    <div>
                        <textarea :placeholder="t[currentLang].custom.inputDetails || 'DETALLES DEL PROYECTO'" rows="3" class="resize-none brutal-input"></textarea>
                    </div>
                    <button type="button" class="w-full py-4 mt-4 text-sm font-bold tracking-wider uppercase btn-skew">
                        <span class="btn-content">{{ t[currentLang].custom.btnSend }}</span>
                    </button>
                </form>
            </div>
        </div>
    </section>

    <!-- Footer -->
    <footer class="py-12 transition-colors border-t"
            :class="isDark ? 'bg-black border-white/10' : 'bg-gray-100 border-gray-200'">
        <div class="flex flex-col items-center justify-between gap-12 px-6 mx-auto max-w-7xl md:flex-row md:gap-6">
            
            <div class="flex items-center gap-4">
                <div class="w-8 h-8" :class="isDark ? 'text-white' : 'text-gray-600'">
                    <AppLogo />
                </div>
                <div class="flex flex-col">
                    <span class="text-xl font-bold uppercase font-display" :class="isDark ? 'text-white' : 'text-black'">Rankit</span>
                    <span class="text-[10px] font-mono uppercase tracking-widest" :class="isDark ? 'text-gray-600' : 'text-gray-500'">Competitive Platform</span>
                </div>
            </div>
            
            <div class="flex flex-col items-center justify-center gap-2 transition-opacity cursor-pointer group hover:opacity-100">
                <span class="text-[10px] font-bold uppercase tracking-[0.2em]" :class="isDark ? 'text-gray-600' : 'text-gray-400'">Powered By</span>
                <div class="flex items-center gap-2">
                    <img src="https://raw.githubusercontent.com/JFabrizzio5/CometaX/bbeb654b90e817236d9d64009b33618065fbba91/image_2025-12-16_083018257-removebg-preview%20(1).png" alt="CometaX Logo" class="w-auto h-8 transition-all duration-500 group-hover:scale-105 opacity-60 group-hover:opacity-100" :class="isDark ? 'invert' : ''">
                    <span class="text-lg font-bold tracking-tight transition-colors group-hover:text-[var(--rankit-neon)]" :class="isDark ? 'text-gray-200' : 'text-gray-800'">CometaX</span>
                </div>
            </div>

            <div class="font-mono text-xs tracking-widest uppercase" :class="isDark ? 'text-gray-700' : 'text-gray-500'">
                © 2025 Rankit System
            </div>
        </div>
    </footer>
</div>
</template>

<style>
/* --- ESTILOS CRÍTICOS DEL HTML ORIGINAL --- */
:root {
  --rankit-neon: #bf00ff;
}

/* Fuentes */
.font-display {
    font-family: 'Chakra Petch', sans-serif;
}
body {
    font-family: 'Archivo', sans-serif;
}

/* Grids de Fondo (Recreando tailwind.config) */
.bg-tech-grid-dark {
    background-image: linear-gradient(to right, rgba(255, 255, 255, 0.05) 1px, transparent 1px), linear-gradient(to bottom, rgba(255, 255, 255, 0.05) 1px, transparent 1px);
}
.bg-tech-grid-light {
    background-image: linear-gradient(to right, rgba(0, 0, 0, 0.05) 1px, transparent 1px), linear-gradient(to bottom, rgba(0, 0, 0, 0.05) 1px, transparent 1px);
}

/* Animación Marquee */
@keyframes marquee {
    0% { transform: translateX(0); }
    100% { transform: translateX(-50%); }
}
.animate-marquee {
    animation: marquee 30s linear infinite;
}

/* Brutal Card Styles */
.brutal-card { 
    position: relative;
    transition: all 0.2s cubic-bezier(0.25, 0.46, 0.45, 0.94);
    border: 1px solid;
}
.main-wrapper.bg-\[\#050505\] .brutal-card {
    background: #0a0a0a;
    border-color: #333;
}
.main-wrapper:not(.bg-\[\#050505\]) .brutal-card {
    background: #ffffff;
    border-color: #e5e5e5;
    box-shadow: 4px 4px 0px #00000010;
}
.brutal-card:hover { 
    border-color: var(--rankit-neon); 
    transform: translate(-4px, -4px);
}
.main-wrapper.bg-\[\#050505\] .brutal-card:hover {
    box-shadow: 6px 6px 0px var(--rankit-neon);
}
.main-wrapper:not(.bg-\[\#050505\]) .brutal-card:hover {
    box-shadow: 6px 6px 0px var(--rankit-neon), 6px 6px 0px 2px black;
}

/* Botones Sesgados */
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
.main-wrapper:not(.bg-\[\#050505\]) .btn-skew:hover {
    background-color: black;
    color: white;
    box-shadow: 4px 4px 0px rgba(0,0,0,0.2);
}

.btn-skew-outline {
    background: transparent;
    color: currentColor;
    border: 1px solid currentColor;
    transform: skewX(-10deg);
    transition: all 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);
    display: inline-flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;
    position: relative;
    overflow: hidden;
}
.btn-skew-outline::before {
    content: '';
    position: absolute;
    top: 0;
    left: -100%;
    width: 100%;
    height: 100%;
    background: currentColor;
    transition: all 0.3s ease;
    z-index: -1;
    opacity: 0.1;
}
.btn-skew-outline:hover {
    transform: skewX(-10deg) translateY(-2px);
    box-shadow: 4px 4px 0px var(--rankit-neon);
    border-color: var(--rankit-neon);
    color: var(--rankit-neon);
}
.btn-skew-outline:hover::before {
    left: 0;
}
.btn-content { transform: skewX(10deg); }

/* Inputs Brutalistas */
.brutal-input {
    width: 100%;
    background: transparent;
    border-bottom: 2px solid;
    padding: 1rem 0;
    font-family: 'Archivo', sans-serif;
    font-weight: 600;
    outline: none;
    transition: all 0.3s;
}
.main-wrapper.bg-\[\#050505\] .brutal-input {
    color: white;
    border-color: #333;
}
.main-wrapper:not(.bg-\[\#050505\]) .brutal-input {
    color: black;
    border-color: #e5e5e5;
}
.brutal-input:focus {
    border-color: var(--rankit-neon);
    padding-left: 1rem;
}
</style>