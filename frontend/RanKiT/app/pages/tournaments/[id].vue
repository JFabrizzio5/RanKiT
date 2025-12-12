<script setup>
import { ref, onMounted } from 'vue'

const activeTab = ref('detalles')
const switchTab = (tab) => activeTab.value = tab

// Canal solicitado (Fijo como pediste)
const twitchChannel = 'Jelty' 

onMounted(() => {
    // Función para inicializar el Player una vez cargado el script
    const initPlayer = () => {
        if (window.Twitch) {
            // Limpiar contenido previo si existe
            const embedContainer = document.getElementById('twitch-embed');
            if (embedContainer) embedContainer.innerHTML = '';

            new window.Twitch.Player("twitch-embed", {
                channel: twitchChannel,
                width: "100%",
                height: "100%",
                parent: ["localhost"] // Cambia esto en producción
            });
        }
    }

    // Cargar script de Twitch si no existe
    if (!document.getElementById('twitch-embed-script')) {
        const script = document.createElement('script');
        script.setAttribute('id', 'twitch-embed-script');
        script.setAttribute('src', 'https://player.twitch.tv/js/embed/v1.js');
        script.onload = initPlayer;
        document.head.appendChild(script);
    } else {
        setTimeout(initPlayer, 500); 
    }
})
</script>

<template>
  <div class="bg-[#0B0C15] min-h-screen font-sans text-white">
    <TheNavbar />

    <!-- Hero Header -->
    <header class="relative h-[450px] flex items-end pt-20">
        <div class="absolute inset-0 z-0">
            <img src="https://images.unsplash.com/photo-1542751371-adc38448a05e?ixlib=rb-4.0.3&auto=format&fit=crop&w=1920&q=80" class="w-full h-full object-cover opacity-40">
            <div class="absolute inset-0 hero-gradient"></div>
        </div>
        
        <div class="relative z-10 w-full max-w-7xl mx-auto px-6 lg:px-8 flex flex-col gap-6">
            <div class="flex flex-col md:flex-row items-end justify-between gap-6 pb-4">
                <div>
                    <div class="flex items-center gap-3 mb-2">
                        <span class="bg-brand-purple text-white px-2 py-0.5 rounded text-[10px] font-bold uppercase tracking-wider">Temporada 5</span>
                        <span class="bg-gray-800 text-gray-300 px-2 py-0.5 rounded text-[10px] font-bold uppercase tracking-wider flex items-center gap-1">
                            <img src="https://img.icons8.com/color/48/valorant.png" class="w-3 h-3"> Valorant
                        </span>
                    </div>
                    <h1 class="text-4xl md:text-6xl font-display font-bold text-white mb-2 text-transparent bg-clip-text bg-gradient-to-r from-white to-gray-400">NEON CITY CUP</h1>
                    <p class="text-gray-400 max-w-xl text-sm md:text-base">El torneo amateur más grande de la región. Demuestra tu habilidad táctica y compite por el gran premio.</p>
                </div>
                
                <div class="flex gap-4 text-center">
                    <div class="glass-panel p-3 rounded-lg min-w-[100px]">
                        <div class="text-xs text-gray-500 uppercase font-bold">Prize Pool</div>
                        <div class="text-xl font-display font-bold text-brand-cyan">$10,000 MXN</div>
                    </div>
                    <div class="glass-panel p-3 rounded-lg min-w-[100px]">
                        <div class="text-xs text-gray-500 uppercase font-bold">Equipos</div>
                        <div class="text-xl font-display font-bold text-white">16/32</div>
                    </div>
                </div>
            </div>

            <div class="flex border-b border-gray-800">
                <button @click="switchTab('detalles')" :class="['px-6 py-3 text-sm font-bold transition flex items-center gap-2 border-b-3', activeTab === 'detalles' ? 'border-brand-cyan text-white' : 'border-transparent text-gray-400 hover:text-white']">
                    <i class="fas fa-info-circle"></i> Detalles e Inscripción
                </button>
                <button @click="switchTab('comunidad')" :class="['px-6 py-3 text-sm font-bold transition flex items-center gap-2 border-b-3 relative', activeTab === 'comunidad' ? 'border-brand-cyan text-white' : 'border-transparent text-gray-400 hover:text-white']">
                    <i class="fas fa-video"></i> En Vivo & Comunidad
                    <span class="absolute top-2 right-2 flex h-2 w-2">
                        <span class="animate-ping absolute inline-flex h-full w-full rounded-full bg-red-400 opacity-75"></span>
                        <span class="relative inline-flex rounded-full h-2 w-2 bg-red-500"></span>
                    </span>
                </button>
            </div>
        </div>
    </header>

    <main class="max-w-7xl mx-auto px-6 lg:px-8 py-8 min-h-[600px]">
        
        <!-- VISTA 1: DETALLES E INSCRIPCIÓN -->
        <div v-show="activeTab === 'detalles'" class="animate-fade-in">
            <div class="grid grid-cols-1 lg:grid-cols-3 gap-12">
                
                <!-- Columna Izquierda: Hall of Fame & Avisos -->
                <div class="lg:col-span-2 space-y-12">
                    
                    <!-- Rankings Anteriores (Podio Completo) -->
                    <section>
                        <div class="flex items-center justify-between mb-6">
                            <h2 class="font-display text-2xl font-bold flex items-center gap-2">
                                <i class="fas fa-trophy text-yellow-500"></i> Campeones Anteriores
                            </h2>
                            <a href="#" class="text-xs text-gray-500 hover:text-white transition">Ver historial completo</a>
                        </div>
                        
                        <div class="grid grid-cols-1 md:grid-cols-3 gap-4 items-end">
                            <!-- 2nd Place -->
                            <div class="glass-panel p-6 rounded-xl relative border-t-4 border-gray-400 text-center transform hover:-translate-y-2 transition duration-300">
                                <div class="absolute -top-3 left-1/2 -translate-x-1/2 bg-gray-700 text-gray-300 px-3 py-1 rounded-full text-xs font-bold shadow-lg">2nd Place</div>
                                <img src="https://i.pravatar.cc/150?img=60" class="w-16 h-16 rounded-full border-2 border-gray-400 mb-3 mx-auto">
                                <h3 class="font-bold text-white">Team Silver</h3>
                                <div class="mt-3 text-sm font-mono text-gray-400">$2,500</div>
                            </div>
                            
                            <!-- 1st Place -->
                            <div class="glass-panel p-6 rounded-xl relative border-t-4 border-yellow-500 bg-gradient-to-b from-yellow-900/10 to-transparent text-center z-10 md:-translate-y-4 transform hover:-translate-y-6 transition duration-300 shadow-[0_0_30px_rgba(234,179,8,0.1)]">
                                <div class="absolute -top-3 left-1/2 -translate-x-1/2 bg-yellow-500 text-black px-4 py-1 rounded-full text-xs font-bold shadow-lg flex items-center gap-1">
                                    <i class="fas fa-crown"></i> CAMPEÓN
                                </div>
                                <img src="https://i.pravatar.cc/150?img=11" class="w-20 h-20 rounded-full border-4 border-yellow-500 mb-3 mx-auto shadow-[0_0_15px_rgba(251,191,36,0.3)]">
                                <h3 class="font-bold text-xl text-white">Crimson Kings</h3>
                                <div class="mt-3 text-lg font-mono font-bold text-white">$5,000</div>
                            </div>
                            
                            <!-- 3rd Place -->
                            <div class="glass-panel p-6 rounded-xl relative border-t-4 border-orange-700 text-center transform hover:-translate-y-2 transition duration-300">
                                <div class="absolute -top-3 left-1/2 -translate-x-1/2 bg-orange-900 text-orange-200 px-3 py-1 rounded-full text-xs font-bold shadow-lg">3rd Place</div>
                                <img src="https://i.pravatar.cc/150?img=33" class="w-16 h-16 rounded-full border-2 border-orange-700 mb-3 mx-auto">
                                <h3 class="font-bold text-white">Bronze Boys</h3>
                                <div class="mt-3 text-sm font-mono text-gray-400">$1,000</div>
                            </div>
                        </div>
                    </section>

                    <!-- Feed de Avisos -->
                    <section>
                        <h2 class="font-display text-2xl font-bold mb-6 flex items-center gap-2">
                            <i class="fas fa-bullhorn text-brand-cyan"></i> Tablón de Avisos
                        </h2>
                        <div class="space-y-4">
                            <div class="glass-panel p-5 rounded-xl border-l-4 border-l-red-500 flex gap-4 hover:bg-white/5 transition">
                                <div class="w-10 h-10 bg-red-500/20 text-red-500 rounded-full flex items-center justify-center flex-shrink-0"><i class="fas fa-exclamation"></i></div>
                                <div>
                                    <div class="flex items-center gap-2 mb-1">
                                        <span class="text-sm font-bold text-white">Map Pool Actualizado</span>
                                        <span class="text-[10px] text-gray-500 bg-black/30 px-2 py-0.5 rounded">Hace 2 horas</span>
                                    </div>
                                    <p class="text-sm text-gray-400">Fracture ha sido eliminado de la rotación debido a bugs. Será reemplazado por Pearl para esta fase.</p>
                                </div>
                            </div>
                            <div class="glass-panel p-5 rounded-xl border-l-4 border-l-brand-purple flex gap-4 hover:bg-white/5 transition">
                                <div class="w-10 h-10 bg-brand-purple/20 text-brand-purple rounded-full flex items-center justify-center flex-shrink-0"><i class="fas fa-info"></i></div>
                                <div>
                                    <div class="flex items-center gap-2 mb-1">
                                        <span class="text-sm font-bold text-white">Check-in Obligatorio</span>
                                        <span class="text-[10px] text-gray-500 bg-black/30 px-2 py-0.5 rounded">Ayer</span>
                                    </div>
                                    <p class="text-sm text-gray-400">El Check-in abre 60 minutos antes de la partida. Capitanes, no olviden confirmar asistencia.</p>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
                
                <!-- Columna Derecha: Inscripción & Equipos -->
                <aside class="space-y-8">
                    <!-- Inscripción -->
                    <div class="glass-panel p-6 rounded-2xl border border-brand-cyan/30 shadow-[0_0_30px_-10px_rgba(6,182,212,0.15)]">
                        <div class="text-center mb-6">
                            <div class="inline-block bg-green-500/20 text-green-400 text-xs font-bold px-3 py-1 rounded-full mb-3 animate-pulse">
                                <i class="fas fa-circle text-[8px] mr-1"></i> INSCRIPCIONES ABIERTAS
                            </div>
                            <h3 class="text-xl font-bold text-white mb-2">¿Listo para competir?</h3>
                            <p class="text-sm text-gray-400">Quedan <span class="text-white font-bold">16 cupos</span> disponibles.</p>
                        </div>
                        <div class="space-y-3">
                            <button class="w-full bg-brand-cyan hover:bg-cyan-400 text-black font-bold py-3 rounded-xl transition shadow-lg flex items-center justify-center gap-2 transform hover:scale-[1.02]">
                                <i class="fas fa-pen-fancy"></i> Inscribir Equipo
                            </button>
                            <button class="w-full bg-gray-800 hover:bg-gray-700 text-white font-bold py-3 rounded-xl transition flex items-center justify-center gap-2">
                                <i class="fas fa-user-plus"></i> Buscar Equipo
                            </button>
                        </div>
                    </div>
                    
                    <!-- Equipos Inscritos -->
                    <div class="glass-panel p-6 rounded-2xl">
                        <div class="flex items-center justify-between mb-4">
                            <h3 class="font-bold text-white text-sm uppercase">Equipos Inscritos</h3>
                            <i class="fas fa-users text-gray-600"></i>
                        </div>
                        <div class="space-y-3">
                            <div class="flex items-center justify-between bg-black/30 p-3 rounded-lg border border-gray-800 hover:border-brand-purple/50 transition">
                                <div class="flex items-center gap-3">
                                    <div class="w-8 h-8 bg-purple-900 rounded flex items-center justify-center font-bold text-xs">VL</div>
                                    <div class="text-sm font-bold text-gray-200">Violet Lightning</div>
                                </div>
                                <span class="text-[10px] text-green-400 bg-green-900/20 px-2 py-0.5 rounded border border-green-500/20">Verificado</span>
                            </div>
                            <div class="flex items-center justify-between bg-black/30 p-3 rounded-lg border border-gray-800 hover:border-brand-purple/50 transition">
                                <div class="flex items-center gap-3">
                                    <div class="w-8 h-8 bg-blue-900 rounded flex items-center justify-center font-bold text-xs">SK</div>
                                    <div class="text-sm font-bold text-gray-200">Sky High</div>
                                </div>
                                <span class="text-[10px] text-gray-500 bg-gray-800 px-2 py-0.5 rounded">Pendiente</span>
                            </div>
                             <div class="flex items-center justify-between bg-black/30 p-3 rounded-lg border border-gray-800 hover:border-brand-purple/50 transition">
                                <div class="flex items-center gap-3">
                                    <div class="w-8 h-8 bg-red-900 rounded flex items-center justify-center font-bold text-xs">RD</div>
                                    <div class="text-sm font-bold text-gray-200">Red Dragons</div>
                                </div>
                                <span class="text-[10px] text-green-400 bg-green-900/20 px-2 py-0.5 rounded border border-green-500/20">Verificado</span>
                            </div>
                        </div>
                        <button class="w-full mt-4 text-xs text-gray-500 hover:text-white underline">Ver todos los equipos</button>
                    </div>
                </aside>
            </div>
        </div>

        <!-- VISTA 2: COMUNIDAD & FEED -->
        <div v-show="activeTab === 'comunidad'" class="animate-fade-in">
             <div class="grid grid-cols-1 lg:grid-cols-12 gap-8">
                 
                 <!-- Columna Principal: Stream y Clips -->
                 <div class="lg:col-span-8 space-y-8">
                     
                    <!-- Stream Embed (JS API - INTACTO) -->
                     <div class="glass-panel p-1 rounded-xl overflow-hidden shadow-2xl shadow-purple-900/20 border border-brand-purple/30">
                         <div class="bg-black/50 p-2 flex justify-between items-center border-b border-gray-800 mb-1">
                             <div class="flex items-center gap-2">
                                 <div class="w-2 h-2 rounded-full bg-red-600 animate-pulse"></div>
                                 <span class="text-xs font-bold text-white uppercase tracking-wider">Transmisión Oficial</span>
                             </div>
                             <span class="text-xs text-gray-500 flex items-center gap-1"><i class="fas fa-eye"></i> 1,240 Viendo</span>
                         </div>
                         <div class="aspect-video bg-black w-full relative">
                             <!-- Contenedor del Player JS -->
                             <div id="twitch-embed" class="w-full h-full absolute inset-0"></div>
                         </div>
                     </div>

                     <!-- Feed de Clips (Restaurado del concepto) -->
                     <div>
                        <div class="flex items-center justify-between mb-4">
                            <h2 class="font-display text-2xl font-bold flex items-center gap-2">
                                <i class="fas fa-hashtag text-brand-cyan"></i> Clips de la Comunidad
                            </h2>
                            <button class="bg-[#151725] hover:bg-gray-800 text-white px-4 py-2 rounded-lg text-xs font-bold border border-gray-700 transition flex items-center gap-2">
                                <i class="fas fa-upload text-brand-cyan"></i> Subir Clip
                            </button>
                        </div>

                        <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
                            <!-- Clip 1 -->
                            <div class="glass-panel p-2 rounded-xl group hover:border-brand-cyan/50 transition cursor-pointer">
                                <div class="aspect-[9/16] bg-gray-900 rounded-lg overflow-hidden relative mb-2">
                                    <img src="https://images.unsplash.com/photo-1624138784181-2999e4253b85?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80" class="absolute inset-0 w-full h-full object-cover opacity-70 group-hover:opacity-100 transition duration-500">
                                    <div class="absolute inset-0 bg-gradient-to-t from-black/90 to-transparent"></div>
                                    <div class="absolute bottom-2 left-2 right-2">
                                        <p class="text-sm font-bold text-white truncate">Clutch 1v4 imposible! 🤯</p>
                                        <div class="flex justify-between items-center text-xs text-gray-400 mt-1">
                                            <span><i class="fas fa-heart text-red-500"></i> 450</span>
                                            <span>@Slayer99</span>
                                        </div>
                                    </div>
                                    <div class="absolute top-2 right-2 bg-black/60 px-1.5 py-0.5 rounded text-[10px] font-bold text-white">0:45</div>
                                </div>
                            </div>

                            <!-- Clip 2 -->
                            <div class="glass-panel p-2 rounded-xl group hover:border-brand-cyan/50 transition cursor-pointer">
                                <div class="aspect-[9/16] bg-gray-900 rounded-lg overflow-hidden relative mb-2">
                                    <img src="https://images.unsplash.com/photo-1542751371-adc38448a05e?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80" class="absolute inset-0 w-full h-full object-cover opacity-70 group-hover:opacity-100 transition duration-500">
                                    <div class="absolute inset-0 bg-gradient-to-t from-black/90 to-transparent"></div>
                                    <div class="absolute bottom-2 left-2 right-2">
                                        <p class="text-sm font-bold text-white truncate">Final Match Point 🏆</p>
                                        <div class="flex justify-between items-center text-xs text-gray-400 mt-1">
                                            <span><i class="fas fa-heart text-red-500"></i> 1.2k</span>
                                            <span>@OfficialCast</span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Clip 3 -->
                            <div class="glass-panel p-2 rounded-xl group hover:border-brand-cyan/50 transition cursor-pointer">
                                <div class="aspect-[9/16] bg-gray-900 rounded-lg overflow-hidden relative mb-2">
                                    <img src="https://images.unsplash.com/photo-1552820728-8b83bb6b773f?ixlib=rb-4.0.3&auto=format&fit=crop&w=600&q=80" class="absolute inset-0 w-full h-full object-cover opacity-70 group-hover:opacity-100 transition duration-500">
                                    <div class="absolute inset-0 bg-gradient-to-t from-black/90 to-transparent"></div>
                                    <div class="absolute bottom-2 left-2 right-2">
                                        <p class="text-sm font-bold text-white truncate">Funny Fails Compilation</p>
                                        <div class="flex justify-between items-center text-xs text-gray-400 mt-1">
                                            <span><i class="fas fa-heart text-red-500"></i> 89</span>
                                            <span>@NoobMaster</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                     </div>

                 </div>
                 
                 <!-- Columna Derecha: Chat y Socials -->
                 <div class="lg:col-span-4 space-y-6">
                     <!-- Chat Container (INTACTO) -->
                     <div class="glass-panel rounded-xl overflow-hidden h-[500px] border border-gray-700">
                        <iframe
                            id="chat_embed"
                            :src="`https://www.twitch.tv/embed/${twitchChannel}/chat?parent=localhost&darkpopout`"
                            height="100%"
                            width="100%"
                            class="w-full h-full"
                        ></iframe>
                     </div>

                     <!-- Redes Sociales (Agregado del concepto) -->
                     <div class="glass-panel p-4 rounded-xl">
                        <h4 class="font-bold text-sm mb-3 text-white">Síguenos</h4>
                        <div class="flex justify-between gap-2">
                            <button class="flex-1 bg-[#5865F2] hover:opacity-90 py-2 rounded text-white text-xs font-bold flex items-center justify-center gap-2 transition">
                                <i class="fab fa-discord"></i> Discord
                            </button>
                            <button class="flex-1 bg-[#1DA1F2] hover:opacity-90 py-2 rounded text-white text-xs font-bold flex items-center justify-center gap-2 transition">
                                <i class="fab fa-twitter"></i> Twitter
                            </button>
                        </div>
                    </div>
                 </div>
             </div>
        </div>
    </main>
  </div>
</template>

<style scoped>
.glass-panel { background: #151725; border: 1px solid rgba(255,255,255,0.05); }
.hero-gradient { background: linear-gradient(to bottom, rgba(11, 12, 21, 0) 0%, #0B0C15 100%); }
.animate-fade-in { animation: fadeIn 0.5s ease; }
@keyframes fadeIn { from { opacity: 0; transform: translateY(10px); } to { opacity: 1; transform: translateY(0); } }
</style>