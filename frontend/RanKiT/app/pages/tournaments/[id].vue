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
            // Limpiar contenido previo si existe (para evitar duplicados al cambiar de tab)
            const embedContainer = document.getElementById('twitch-embed');
            if (embedContainer) embedContainer.innerHTML = '';

            new window.Twitch.Player("twitch-embed", {
                channel: twitchChannel,
                width: "100%",
                height: "100%",
                // IMPORTANTE: 'parent' es obligatorio por políticas de seguridad de Twitch.
                // Agrega 'localhost' para desarrollo y tu dominio real para producción.
                parent: ["localhost"] 
            });
        }
    }

    // Verificar si el script ya existe para no cargarlo doble
    if (!document.getElementById('twitch-embed-script')) {
        const script = document.createElement('script');
        script.setAttribute('id', 'twitch-embed-script');
        script.setAttribute('src', 'https://player.twitch.tv/js/embed/v1.js');
        script.onload = initPlayer; // Ejecutar init al terminar de cargar
        document.head.appendChild(script);
    } else {
        // Si ya existe (ej. al volver de otra página), inicializar directamente
        // Esperamos un poco para asegurar que window.Twitch esté disponible
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
                        <span class="bg-gray-800 text-gray-300 px-2 py-0.5 rounded text-[10px] font-bold uppercase tracking-wider flex items-center gap-1">Valorant</span>
                    </div>
                    <h1 class="text-4xl md:text-6xl font-display font-bold text-white mb-2 text-transparent bg-clip-text bg-gradient-to-r from-white to-gray-400">NEON CITY CUP</h1>
                    <p class="text-gray-400 max-w-xl text-sm md:text-base">El torneo amateur más grande de la región. Demuestra tu habilidad táctica.</p>
                </div>
                
                <div class="flex gap-4 text-center">
                    <div class="glass-panel p-3 rounded-lg min-w-[100px]">
                        <div class="text-xs text-gray-500 uppercase font-bold">Prize Pool</div>
                        <div class="text-xl font-display font-bold text-brand-cyan">$10,000 MXN</div>
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
        <!-- VISTA DETALLES -->
        <div v-show="activeTab === 'detalles'" class="grid grid-cols-1 lg:grid-cols-3 gap-12 animate-fade-in">
             <div class="lg:col-span-2 space-y-12">
                 <section>
                    <h2 class="font-display text-2xl font-bold mb-6 flex items-center gap-2"><i class="fas fa-trophy text-yellow-500"></i> Campeones Anteriores</h2>
                    <div class="grid grid-cols-1 md:grid-cols-3 gap-4">
                        <!-- Winner Card -->
                        <div class="glass-panel p-6 rounded-xl relative border-t-4 border-yellow-500 bg-gradient-to-b from-yellow-900/10 to-transparent text-center">
                            <div class="absolute -top-3 left-1/2 -translate-x-1/2 bg-yellow-500 text-black px-4 py-1 rounded-full text-xs font-bold shadow-lg"><i class="fas fa-crown"></i> CAMPEÓN</div>
                            <img src="https://i.pravatar.cc/150?img=11" class="w-20 h-20 rounded-full border-4 border-yellow-500 mb-3 mx-auto">
                            <h3 class="font-bold text-xl text-white">Crimson Kings</h3>
                            <div class="mt-3 text-lg font-mono font-bold text-white">$5,000</div>
                        </div>
                    </div>
                 </section>
             </div>
             
             <!-- Sidebar Inscripción -->
             <aside class="space-y-8">
                 <div class="glass-panel p-6 rounded-2xl border border-brand-cyan/30">
                     <div class="text-center mb-6">
                         <div class="inline-block bg-green-500/20 text-green-400 text-xs font-bold px-3 py-1 rounded-full mb-3 animate-pulse">INSCRIPCIONES ABIERTAS</div>
                         <h3 class="text-xl font-bold text-white mb-2">¿Listo para competir?</h3>
                         <p class="text-sm text-gray-400">Quedan <span class="text-white font-bold">16 cupos</span> disponibles.</p>
                     </div>
                     <button class="w-full bg-brand-cyan hover:bg-cyan-400 text-black font-bold py-3 rounded-xl transition flex items-center justify-center gap-2">
                         <i class="fas fa-pen-fancy"></i> Inscribir Equipo
                     </button>
                 </div>
             </aside>
        </div>

        <!-- VISTA COMUNIDAD -->
        <div v-show="activeTab === 'comunidad'" class="animate-fade-in">
             <div class="grid grid-cols-1 lg:grid-cols-12 gap-8">
                 <div class="lg:col-span-8 space-y-8">
                     
                    <!-- Stream Embed REAL (Twitch JS API) -->
                     <div class="glass-panel p-1 rounded-xl overflow-hidden shadow-2xl shadow-purple-900/20">
                         <div class="bg-black/50 p-2 flex justify-between items-center border-b border-gray-800 mb-1">
                             <div class="flex items-center gap-2">
                                 <div class="w-2 h-2 rounded-full bg-red-600 animate-pulse"></div>
                                 <span class="text-xs font-bold text-white uppercase tracking-wider">Transmisión Oficial</span>
                             </div>
                             <span class="text-xs text-gray-500"><i class="fas fa-eye"></i> En Vivo</span>
                         </div>
                         
                         <!-- Contenedor del Player JS -->
                         <div class="aspect-video bg-black w-full relative">
                             <!-- Aquí es donde la API de JS inyectará el iframe -->
                             <div id="twitch-embed" class="w-full h-full absolute inset-0"></div>
                         </div>
                     </div>

                 </div>
                 
                 <!-- Chat Sidebar -->
                 <div class="lg:col-span-4">
                     <div class="glass-panel rounded-xl overflow-hidden h-[500px]">
                        <!-- Iframe del Chat de Twitch -->
                        <iframe
                            id="chat_embed"
                            :src="`https://www.twitch.tv/embed/${twitchChannel}/chat?parent=localhost`"
                            height="100%"
                            width="100%"
                            class="w-full h-full"
                        ></iframe>
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