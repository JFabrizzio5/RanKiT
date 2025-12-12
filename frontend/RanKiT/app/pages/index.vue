<script setup>
import { ref } from 'vue'

// Estado para controlar el menú de desarrollo flotante
const showDevMenu = ref(false)

useHead({
  title: 'RankIT by Cometax - Plataforma de Torneos',
  meta: [
    { name: 'viewport', content: 'width=device-width, initial-scale=1.0' }
  ],
  link: [
    { rel: 'stylesheet', href: 'https://fonts.googleapis.com/css2?family=Chakra+Petch:wght@400;600;700&family=Inter:wght@300;400;600&display=swap' },
    { rel: 'stylesheet', href: 'https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css' }
  ],
  bodyAttrs: {
    class: 'bg-[#0B0C15] text-white antialiased overflow-x-hidden'
  }
})
</script>

<template>
  <div class="overflow-x-hidden relative font-sans text-white bg-[#0B0C15]">
    
    <!-- Navbar (Restaurado del concepto + links funcionales de Nuxt) -->
    <nav class="fixed w-full z-50 glass-card border-b border-white/5 top-0 left-0">
        <div class="max-w-7xl mx-auto px-4 h-20 flex justify-between items-center">
            <NuxtLink to="/" class="flex items-center gap-2">
                <i class="fas fa-trophy text-brand-purple text-2xl"></i>
                <span class="font-display font-bold text-3xl tracking-tighter leading-none">RANK<span class="text-brand-cyan">IT</span></span>
            </NuxtLink>

            <div class="hidden md:flex space-x-8 items-center text-sm font-semibold text-gray-300">
                <!-- Links del Concepto -->
                <a href="#juegos" class="hover:text-brand-cyan transition">Torneos</a>
                <a href="#precios" class="hover:text-brand-cyan transition">Planes</a>
                
                <!-- Links de la App Nuxt -->
                <NuxtLink to="/community" class="hover:text-brand-cyan transition">Comunidad</NuxtLink>
                
                <NuxtLink to="/admin" class="bg-gradient-to-r from-brand-purple to-brand-cyan text-white px-6 py-2 rounded-full font-bold shadow-lg shadow-purple-900/50 hover:shadow-cyan-500/50 transition transform hover:scale-105">
                    Dashboard
                </NuxtLink>
            </div>
        </div>
    </nav>

    <!-- Hero Section (Manteniendo la estructura fiel al concepto) -->
    <section class="hero-gradient min-h-screen flex items-center relative overflow-hidden pt-20">
        <!-- Fondos decorativos -->
        <div class="absolute top-20 right-0 w-1/2 h-full bg-[url('https://images.unsplash.com/photo-1538481199705-c710c4e965fc?q=80&w=2665&auto=format&fit=crop')] bg-cover bg-center opacity-10 mix-blend-overlay"></div>
        <div class="absolute -bottom-32 -left-32 w-96 h-96 bg-brand-purple rounded-full blur-[150px] opacity-20"></div>

        <div class="max-w-7xl mx-auto px-4 grid grid-cols-1 lg:grid-cols-2 gap-12 items-center relative z-10">
            <div>
                <div class="inline-flex items-center gap-2 px-3 py-1 rounded-full border border-brand-cyan/30 bg-brand-cyan/10 text-brand-cyan text-xs font-bold mb-6">
                    <svg class="w-4 h-4" viewBox="0 0 100 100" fill="none">
                        <path d="M20 80 L50 50 L80 20" stroke="#06B6D4" stroke-width="12" stroke-linecap="round" />
                        <circle cx="20" cy="80" r="15" fill="#06B6D4" />
                        <path d="M50 50 L80 50 L80 80 L50 80 Z" fill="#7C3AED" />
                    </svg>
                    POWERED BY COMETAX
                </div>
                <h1 class="font-display font-bold text-5xl md:text-7xl mb-6 leading-tight">
                    TU LIGA.<br>
                    TU REGLAS.<br>
                    <span class="text-transparent bg-clip-text bg-gradient-to-r from-brand-cyan to-brand-purple">TU LEGADO.</span>
                </h1>
                <p class="text-gray-400 text-lg mb-8 max-w-lg">
                    La plataforma profesional para organizar torneos de Esports. 
                    Desde brackets automatizados hasta <strong>overlays en tiempo real para tu stream</strong>.
                </p>
                <div class="flex gap-4">
                    <a href="#precios" class="bg-white text-brand-dark font-bold px-8 py-4 rounded-full hover:scale-105 transition transform text-black">
                        Comenzar Gratis
                    </a>
                    <NuxtLink to="/microsite" class="glass-card px-8 py-4 rounded-full font-bold hover:bg-white/10 transition flex items-center gap-2">
                        <i class="fas fa-play-circle text-brand-purple"></i> Ver Demo
                    </NuxtLink>
                </div>
            </div>
            
            <div class="relative hidden lg:block animate-float">
                <div class="relative z-10 grid grid-cols-2 gap-4 transform rotate-6 hover:rotate-0 transition duration-700">
                    <img src="https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Jinx_0.jpg" class="rounded-2xl shadow-2xl border border-brand-purple/30 w-full h-64 object-cover hover:scale-105 transition duration-500">
                    <img src="https://store-images.s-microsoft.com/image/apps.21507.13663857844271189.4c1de202-3961-4c40-a0aa-7f4f1388775a.20ed7782-0eda-4f9d-b421-4cc47492edc6" class="rounded-2xl shadow-2xl border border-brand-cyan/30 w-full h-64 object-cover mt-12 hover:scale-105 transition duration-500">
                </div>
                 <!-- Cometax Logo Grande Fondo (Decorativo) -->
                <svg class="absolute -top-20 -right-20 w-96 h-96 opacity-5 rotate-12" viewBox="0 0 100 100" fill="none">
                    <path d="M20 80 L80 20" stroke="white" stroke-width="2"/>
                    <path d="M50 50 L80 50 L80 80 L50 80 Z" fill="white" />
                </svg>
            </div>
        </div>
    </section>

    <!-- Sección de Juegos (Agregada del concepto) -->
    <section id="juegos" class="py-20 bg-[#0B0C15] relative">
        <div class="max-w-7xl mx-auto px-4">
            <h2 class="font-display text-4xl font-bold mb-12 text-center">Torneos Activos <span class="text-brand-purple">.</span></h2>
            
            <div class="grid grid-cols-1 md:grid-cols-3 lg:grid-cols-4 gap-6">
                <!-- Fortnite -->
                <div class="game-card group relative h-[400px] rounded-2xl overflow-hidden cursor-pointer">
                    <div class="absolute inset-0 bg-gradient-to-t from-black via-transparent to-transparent z-10"></div>
                    <img src="https://assets.nintendo.com/image/upload/ar_16:9,c_lpad,w_1240/b_white/f_auto/q_auto/store/software/switch/70010000010192/bad0a23863b5cca0e9dc7bd023b98b21497e7711806504b589d5eef23de8609a" class="game-cover img-fortnite absolute inset-0 w-full h-full object-cover" alt="Fortnite">
                    <div class="absolute bottom-0 left-0 p-6 z-20 w-full">
                        <h3 class="font-display font-bold text-2xl text-white mb-1">Fortnite</h3>
                        <p class="text-brand-cyan text-sm font-bold mb-4">Battle Royale</p>
                        <div class="flex justify-between items-center border-t border-white/20 pt-4">
                            <span class="text-xs text-gray-300">15 Torneos Activos</span>
                            <i class="fas fa-arrow-right text-white group-hover:translate-x-2 transition"></i>
                        </div>
                    </div>
                </div>

                <!-- League of Legends (Span 2) -->
                <div class="game-card group relative h-[400px] rounded-2xl overflow-hidden cursor-pointer lg:col-span-2">
                    <div class="absolute inset-0 bg-gradient-to-t from-black via-transparent to-transparent z-10"></div>
                    <img src="https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Ahri_27.jpg" class="game-cover absolute inset-0 w-full h-full object-cover" alt="LoL">
                    <div class="absolute top-4 right-4 z-20 bg-brand-purple text-white px-3 py-1 rounded font-bold text-xs shadow-lg">TORNEO SPONSORED</div>
                    <div class="absolute bottom-0 left-0 p-6 z-20 w-full">
                        <h3 class="font-display font-bold text-3xl text-white mb-1">League of Legends</h3>
                        <p class="text-brand-cyan text-sm font-bold mb-4">Summoner's Rift 5v5</p>
                        <div class="flex justify-between items-center border-t border-white/20 pt-4">
                            <span class="text-xs text-gray-300">Premio: $50,000 MXN</span>
                            <NuxtLink to="/microsite" class="bg-white text-black px-4 py-1 rounded-full text-xs font-bold hover:bg-gray-200">Ver Bracket</NuxtLink>
                        </div>
                    </div>
                </div>

                <!-- Valorant -->
                <div class="game-card group relative h-[400px] rounded-2xl overflow-hidden cursor-pointer">
                    <div class="absolute inset-0 bg-gradient-to-t from-black via-transparent to-transparent z-10"></div>
                    <img src="https://store-images.s-microsoft.com/image/apps.21507.13663857844271189.4c1de202-3961-4c40-a0aa-7f4f1388775a.20ed7782-0eda-4f9d-b421-4cc47492edc6" class="game-cover img-valorant absolute inset-0 w-full h-full object-cover" alt="Valorant">
                    <div class="absolute bottom-0 left-0 p-6 z-20 w-full">
                        <h3 class="font-display font-bold text-2xl text-white mb-1">Valorant</h3>
                        <p class="text-brand-cyan text-sm font-bold mb-4">Tactical Shooter</p>
                        <div class="flex justify-between items-center border-t border-white/20 pt-4">
                            <span class="text-xs text-gray-300">Ligas Regionales</span>
                            <i class="fas fa-arrow-right text-white group-hover:translate-x-2 transition"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Sección de Precios (Agregada del concepto) -->
    <section id="precios" class="py-24 bg-[#151725] relative overflow-hidden">
        <!-- Fondo sutil de rejilla -->
        <div class="absolute inset-0 bg-[url('https://www.transparenttextures.com/patterns/carbon-fibre.png')] opacity-5"></div>
        
        <div class="max-w-7xl mx-auto px-4 relative z-10">
            <div class="text-center mb-16">
                <h2 class="font-display text-4xl md:text-5xl font-bold mb-4">Planes para Organizadores</h2>
                <p class="text-gray-400">Escala desde partidas privadas hasta estadios llenos.</p>
            </div>

            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-5 gap-4 items-start">
                
                <!-- HOST UNICO -->
                <div class="glass-card p-6 rounded-2xl hover:bg-white/5 transition border-t border-gray-700">
                    <h3 class="font-display text-lg font-bold text-gray-300">Host Único</h3>
                    <div class="my-4"><span class="text-3xl font-bold">$500</span> <span class="text-xs text-gray-400">MXN / evento</span></div>
                    <p class="text-xs text-gray-500 mb-6 min-h-[40px]">Para eventos casuales de una sola vez.</p>
                    <button class="w-full py-2 border border-gray-600 rounded-lg text-sm hover:bg-gray-700 transition mb-6 font-bold">Seleccionar</button>
                    <ul class="text-xs space-y-3 text-gray-400">
                        <li class="flex gap-2"><i class="fas fa-check text-brand-cyan"></i> 1 Torneo</li>
                        <li class="flex gap-2"><i class="fas fa-check text-brand-cyan"></i> Bracket Automático</li>
                        <li class="flex gap-2"><i class="fas fa-times text-red-500 opacity-50"></i> Sin Micrositio</li>
                    </ul>
                </div>

                <!-- AMATEUR -->
                <div class="glass-card p-6 rounded-2xl border border-brand-cyan/50 relative bg-brand-cyan/5 transform md:-translate-y-2">
                    <div class="absolute top-0 right-0 bg-brand-cyan text-black text-[10px] font-bold px-2 py-1 rounded-bl-lg rounded-tr-lg">POPULAR</div>
                    <h3 class="font-display text-lg font-bold text-white">Amateur</h3>
                    <div class="my-4"><span class="text-3xl font-bold">$300</span> <span class="text-xs text-gray-400">MXN / mes</span></div>
                    <p class="text-xs text-gray-500 mb-6 min-h-[40px]">Para streamers y pequeños organizadores.</p>
                    <button class="w-full py-2 bg-brand-cyan text-black font-bold rounded-lg text-sm hover:bg-cyan-400 transition mb-6 shadow-lg shadow-cyan-500/20">Empezar</button>
                    <ul class="text-xs space-y-3 text-gray-400">
                        <li class="flex gap-2"><i class="fas fa-check text-brand-cyan"></i> Torneos propios</li>
                        <li class="flex gap-2"><i class="fas fa-check text-brand-cyan"></i> &lt; 50 participantes</li>
                        <li class="flex gap-2"><i class="fas fa-times text-red-500 opacity-50"></i> Sin Micrositio</li>
                    </ul>
                </div>

                <!-- PYME -->
                <div class="glass-card p-6 rounded-2xl hover:bg-white/5 transition border-t-2 border-blue-500">
                    <h3 class="font-display text-lg font-bold text-blue-400">Micro / PyME</h3>
                    <div class="my-4"><span class="text-3xl font-bold">$3,000</span> <span class="text-xs text-gray-400">MXN / año</span></div>
                    <p class="text-xs text-gray-500 mb-6 min-h-[40px]">Negocios medianos y Ligas Locales.</p>
                    <button class="w-full py-2 border border-blue-500 text-blue-400 rounded-lg text-sm hover:bg-blue-500/10 transition mb-6 font-bold">Contratar</button>
                    <ul class="text-xs space-y-3 text-gray-400">
                        <li class="flex gap-2"><i class="fas fa-check text-brand-cyan"></i> <strong>Micrositio Propio</strong></li>
                        <li class="flex gap-2"><i class="fas fa-check text-brand-cyan"></i> &gt; 50 participantes</li>
                        <li class="flex gap-2"><i class="fas fa-check text-brand-cyan"></i> Branding Custom</li>
                    </ul>
                </div>

                <!-- GRANDE -->
                <div class="glass-card p-6 rounded-2xl hover:bg-white/5 transition border-t-2 border-purple-500">
                    <h3 class="font-display text-lg font-bold text-purple-400">Empresa Grande</h3>
                    <div class="my-4"><span class="text-3xl font-bold">$10k</span> <span class="text-xs text-gray-400">MXN / año</span></div>
                    <p class="text-xs text-gray-500 mb-6 min-h-[40px]">Marcas establecidas.</p>
                    <button class="w-full py-2 border border-purple-500 text-purple-400 rounded-lg text-sm hover:bg-purple-500/10 transition mb-6 font-bold">Contactar</button>
                    <ul class="text-xs space-y-3 text-gray-400">
                        <li class="flex gap-2"><i class="fas fa-check text-brand-cyan"></i> Micrositio Avanzado</li>
                        <li class="flex gap-2"><i class="fas fa-check text-brand-cyan"></i> Data Analytics</li>
                        <li class="flex gap-2"><i class="fas fa-check text-brand-cyan"></i> Soporte Prioritario</li>
                    </ul>
                </div>

                <!-- TITAN -->
                <div class="glass-card p-6 rounded-2xl hover:bg-white/5 transition border-t-2 border-yellow-500 bg-gradient-to-b from-yellow-900/10 to-transparent">
                    <h3 class="font-display text-lg font-bold text-yellow-400">Titán</h3>
                    <div class="my-4"><span class="text-3xl font-bold">$15k</span> <span class="text-xs text-gray-400">MXN / año</span></div>
                    <p class="text-xs text-gray-500 mb-6 min-h-[40px]">Corporaciones y Eventos Masivos.</p>
                    <button class="w-full py-2 bg-yellow-500 text-black font-bold rounded-lg text-sm hover:bg-yellow-400 transition mb-6 shadow-lg shadow-yellow-500/20">Agendar Demo</button>
                    <ul class="text-xs space-y-3 text-gray-400">
                        <li class="flex gap-2"><i class="fas fa-check text-brand-cyan"></i> <strong>Torneos Ilimitados</strong></li>
                        <li class="flex gap-2"><i class="fas fa-check text-brand-cyan"></i> Integración API total</li>
                        <li class="flex gap-2"><i class="fas fa-check text-brand-cyan"></i> Gestor de Cuenta Dedicado</li>
                    </ul>
                </div>

            </div>
        </div>
    </section>

    <!-- Footer Simple -->
    <footer class="bg-black py-12 border-t border-white/10">
        <div class="max-w-7xl mx-auto px-4 flex flex-col md:flex-row justify-between items-center gap-6">
            <div class="flex items-center gap-3 opacity-70 hover:opacity-100 transition">
                <!-- SVG Cometax en Footer -->
                <svg class="w-8 h-8" viewBox="0 0 100 100" fill="none">
                    <path d="M20 80 L50 50 L80 20" stroke="#06B6D4" stroke-width="8" stroke-linecap="round" />
                    <circle cx="20" cy="80" r="10" fill="#06B6D4" />
                    <path d="M50 50 L80 50 L80 80 L50 80 Z" fill="#7C3AED" />
                </svg>
                <div class="flex flex-col">
                    <span class="font-display font-bold text-xl leading-none">COMETAX</span>
                    <span class="text-[9px] text-gray-500">TECHNOLOGY SOLUTIONS</span>
                </div>
            </div>
            <div class="text-gray-500 text-sm">
                &copy; 2025 RankIT. Una solución de Cometax Technology Solutions.
            </div>
        </div>
    </footer>

    <!-- Botón Menú Desarrollo (Mantengo esto porque es útil para la navegación en la demo) -->
    <button @click="showDevMenu = !showDevMenu" 
            class="fixed bottom-6 right-6 z-[100] w-14 h-14 bg-brand-cyan text-black rounded-full shadow-[0_0_20px_rgba(6,182,212,0.6)] flex items-center justify-center font-bold text-2xl hover:scale-110 transition border-4 border-[#0B0C15]">
        <i class="fas" :class="showDevMenu ? 'fa-times' : 'fa-compass'"></i>
    </button>

    <!-- Menú Desarrollo Flotante -->
    <div v-if="showDevMenu" class="fixed bottom-24 right-6 z-[99] bg-[#151725] border border-brand-cyan/30 p-6 rounded-2xl shadow-2xl w-80 animate-slide-up backdrop-blur-xl">
        <h3 class="font-display font-bold text-lg text-white mb-4 border-b border-gray-700 pb-2 flex items-center gap-2">
            <i class="fas fa-map-signs text-brand-cyan"></i> Mapa del Sitio
        </h3>
        
        <div class="space-y-4 max-h-[60vh] overflow-y-auto pr-2 custom-scroll">
            
            <div>
                <h4 class="text-xs font-bold text-gray-500 uppercase mb-2">Público / Jugador</h4>
                <div class="space-y-2">
                    <NuxtLink to="/" class="block bg-black/40 hover:bg-brand-cyan/20 px-3 py-2 rounded text-sm text-gray-300 hover:text-white transition border border-transparent hover:border-brand-cyan/30">
                        <i class="fas fa-home w-5"></i> Landing Page
                    </NuxtLink>
                    <NuxtLink to="/start" class="block bg-black/40 hover:bg-brand-cyan/20 px-3 py-2 rounded text-sm text-gray-300 hover:text-white transition border border-transparent hover:border-brand-cyan/30">
                        <i class="fas fa-search w-5"></i> Explorar Torneos
                    </NuxtLink>
                    <NuxtLink to="/community" class="block bg-black/40 hover:bg-brand-cyan/20 px-3 py-2 rounded text-sm text-gray-300 hover:text-white transition border border-transparent hover:border-brand-cyan/30">
                        <i class="fas fa-users w-5"></i> Comunidad & Clips
                    </NuxtLink>
                    <NuxtLink to="/profile" class="block bg-black/40 hover:bg-brand-cyan/20 px-3 py-2 rounded text-sm text-gray-300 hover:text-white transition border border-transparent hover:border-brand-cyan/30">
                        <i class="fas fa-user w-5"></i> Perfil Usuario
                    </NuxtLink>
                </div>
            </div>

            <div>
                <h4 class="text-xs font-bold text-gray-500 uppercase mb-2">Competitivo</h4>
                <div class="space-y-2">
                    <NuxtLink to="/tournaments/1" class="block bg-black/40 hover:bg-yellow-500/20 px-3 py-2 rounded text-sm text-gray-300 hover:text-white transition border border-transparent hover:border-yellow-500/30">
                        <i class="fas fa-info-circle w-5"></i> Detalle Torneo
                    </NuxtLink>
                    <NuxtLink to="/lobby/M-9921" class="block bg-black/40 hover:bg-yellow-500/20 px-3 py-2 rounded text-sm text-gray-300 hover:text-white transition border border-transparent hover:border-yellow-500/30">
                        <i class="fas fa-gamepad w-5"></i> Sala de Juego (Lobby)
                    </NuxtLink>
                     <NuxtLink to="/microsite" class="block bg-black/40 hover:bg-yellow-500/20 px-3 py-2 rounded text-sm text-gray-300 hover:text-white transition border border-transparent hover:border-yellow-500/30">
                        <i class="fas fa-sitemap w-5"></i> Micrositio / Brackets
                    </NuxtLink>
                </div>
            </div>

            <div>
                <h4 class="text-xs font-bold text-gray-500 uppercase mb-2">Administrador</h4>
                <div class="space-y-2">
                    <NuxtLink to="/admin" class="block bg-black/40 hover:bg-brand-purple/20 px-3 py-2 rounded text-sm text-gray-300 hover:text-white transition border border-transparent hover:border-brand-purple/30">
                        <i class="fas fa-tachometer-alt w-5"></i> Dashboard Principal
                    </NuxtLink>
                    <NuxtLink to="/admin/tournaments" class="block bg-black/40 hover:bg-brand-purple/20 px-3 py-2 rounded text-sm text-gray-300 hover:text-white transition border border-transparent hover:border-brand-purple/30">
                        <i class="fas fa-list w-5"></i> Lista de Torneos
                    </NuxtLink>
                    <NuxtLink to="/admin/streaming" class="block bg-black/40 hover:bg-brand-purple/20 px-3 py-2 rounded text-sm text-gray-300 hover:text-white transition border border-transparent hover:border-brand-purple/30">
                        <i class="fas fa-video w-5"></i> Herramientas OBS
                    </NuxtLink>
                </div>
            </div>

            <div>
                <h4 class="text-xs font-bold text-gray-500 uppercase mb-2">Overlays (OBS Sources)</h4>
                <div class="space-y-2">
                    <NuxtLink to="/overlay/scoreboard" target="_blank" class="block bg-black/40 hover:bg-red-500/20 px-3 py-2 rounded text-sm text-gray-300 hover:text-white transition border border-transparent hover:border-red-500/30">
                        <i class="fas fa-window-maximize w-5"></i> Scoreboard Widget
                    </NuxtLink>
                    <NuxtLink to="/overlay/laderboard" target="_blank" class="block bg-black/40 hover:bg-red-500/20 px-3 py-2 rounded text-sm text-gray-300 hover:text-white transition border border-transparent hover:border-red-500/30">
                        <i class="fas fa-list-ol w-5"></i> Leaderboard Widget
                    </NuxtLink>
                </div>
            </div>

        </div>
    </div>

  </div>
</template>

<style scoped>
/* Clases personalizadas restauradas del concepto */
.glass-card { 
    background: rgba(21, 23, 37, 0.7); 
    backdrop-filter: blur(12px); 
    border: 1px solid rgba(255, 255, 255, 0.08); 
}
.hero-gradient { 
    background: radial-gradient(circle at top right, #1e1b4b, #0B0C15); 
}
.animate-float {
    animation: float 6s ease-in-out infinite;
}
@keyframes float {
    0%, 100% { transform: translateY(0); }
    50% { transform: translateY(-20px); }
}

/* Efectos de Hover para las Game Cards */
.game-cover { transition: transform 0.5s cubic-bezier(0.4, 0, 0.2, 1); }
.game-card:hover .game-cover { transform: scale(1.05); }

/* Animación del menú */
@keyframes slideUpFade {
    from { opacity: 0; transform: translateY(20px); }
    to { opacity: 1; transform: translateY(0); }
}
.animate-slide-up {
    animation: slideUpFade 0.3s cubic-bezier(0.16, 1, 0.3, 1);
}

/* Scrollbar personalizada para el menú */
.custom-scroll::-webkit-scrollbar { width: 4px; }
.custom-scroll::-webkit-scrollbar-track { background: rgba(0,0,0,0.2); }
.custom-scroll::-webkit-scrollbar-thumb { background: #06B6D4; border-radius: 4px; }

/* Ajustes de posición de imagen */
.img-fortnite { object-position: center top; }
.img-valorant { object-position: center 20%; }
</style>