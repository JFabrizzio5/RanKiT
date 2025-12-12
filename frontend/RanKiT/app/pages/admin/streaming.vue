<script setup>
import { ref } from 'vue'

const widgetUrl = ref('http://localhost:3000/overlay/scoreboard')
const previewMode = ref('scoreboard') // scoreboard o leaderboard

const copyUrl = () => {
  navigator.clipboard.writeText(widgetUrl.value)
  alert('URL copiada al portapapeles. Pégala en OBS como "Browser Source".')
}

// Simulaciones de control manual
const updateScore = (team) => {
  alert(`Punto añadido a ${team}. (En producción esto enviaría websocket)`)
}

const refreshPreview = () => {
    const iframe = document.getElementById('preview-frame')
    if(iframe) iframe.src = iframe.src
}
</script>

<template>
  <div>
    <!-- Topbar -->
    <header class="h-20 border-b border-gray-800 flex items-center justify-between px-8 bg-[#0B0C15]/90 backdrop-blur-md sticky top-0 z-30">
        <div>
            <h2 class="font-display text-2xl font-bold">Herramientas de Streaming</h2>
            <p class="text-gray-500 text-xs">Gestiona tus overlays para OBS / Streamlabs.</p>
        </div>
    </header>

    <div class="p-8 max-w-7xl mx-auto w-full">
        <!-- OBS Widget Generator -->
        <div class="glass-panel p-8 rounded-2xl border-t-4 border-brand-cyan mb-8 bg-[#151725] border border-white/5">
            <div class="flex flex-col md:flex-row justify-between items-start md:items-center mb-6 gap-4">
                <div>
                    <h3 class="font-display text-xl font-bold flex items-center gap-2 text-white">
                        <i class="fas fa-layer-group text-brand-purple"></i> Generador de Overlays
                    </h3>
                    <p class="text-gray-400 text-sm mt-1">Genera una URL para usar como "Browser Source" en OBS Studio.</p>
                </div>
                
                <div class="flex gap-2">
                    <button @click="previewMode = 'scoreboard'; widgetUrl = 'http://localhost:3000/overlay/scoreboard'" 
                        :class="['px-3 py-1 rounded text-xs font-bold transition', previewMode === 'scoreboard' ? 'bg-brand-purple text-white' : 'bg-gray-800 text-gray-400']">
                        Marcador
                    </button>
                    <button @click="previewMode = 'leaderboard'; widgetUrl = 'http://localhost:3000/overlay/leaderboard'" 
                        :class="['px-3 py-1 rounded text-xs font-bold transition', previewMode === 'leaderboard' ? 'bg-brand-cyan text-black' : 'bg-gray-800 text-gray-400']">
                        Tabla
                    </button>
                </div>
            </div>

            <!-- URL Copy Box -->
            <div class="bg-black/50 p-4 rounded-lg border border-gray-700 flex items-center gap-4 mb-8">
                <div class="bg-gray-800 px-3 py-1 rounded text-xs font-mono text-gray-400 font-bold">BROWSER SOURCE URL</div>
                <input type="text" v-model="widgetUrl" readonly 
                    class="bg-transparent border-none text-gray-300 flex-1 outline-none font-mono text-sm">
                <button @click="copyUrl" class="bg-brand-purple hover:bg-brand-purple/80 text-white px-4 py-2 rounded font-bold text-sm transition shadow-lg shadow-purple-900/50 flex items-center gap-2">
                    <i class="fas fa-copy"></i> Copiar
                </button>
            </div>

            <!-- Preview Area -->
            <div class="grid grid-cols-1 lg:grid-cols-3 gap-8">
                <!-- Controls -->
                <div class="space-y-6">
                    <div class="bg-gray-900/50 p-4 rounded-xl border border-gray-700">
                        <h4 class="font-bold mb-4 text-sm uppercase text-gray-500">Control Manual (Live)</h4>
                        <p class="text-xs text-gray-400 mb-4">Controla el marcador en tiempo real.</p>
                        
                        <div class="space-y-3" v-if="previewMode === 'scoreboard'">
                            <div class="grid grid-cols-2 gap-2">
                                <button @click="updateScore('Team 1')" class="bg-blue-900/30 hover:bg-blue-900/50 border border-blue-500/30 text-blue-400 p-2 rounded text-xs font-bold transition">
                                    +1 Team A
                                </button>
                                <button @click="updateScore('Team 2')" class="bg-red-900/30 hover:bg-red-900/50 border border-red-500/30 text-red-400 p-2 rounded text-xs font-bold transition">
                                    +1 Team B
                                </button>
                            </div>
                            <button class="w-full bg-gray-800 hover:bg-gray-700 p-3 rounded-lg flex items-center justify-between group transition text-gray-300 text-xs font-bold">
                                <span>Cambiar Ronda</span>
                                <i class="fas fa-exchange-alt text-brand-cyan"></i>
                            </button>
                            <button class="w-full bg-red-900/20 hover:bg-red-900/40 border border-red-900/50 p-3 rounded-lg flex items-center justify-between group transition text-red-400 text-xs font-bold">
                                <span>Resetear Marcador</span>
                                <i class="fas fa-trash"></i>
                            </button>
                        </div>

                        <div v-else class="text-center py-8 text-gray-500 text-xs">
                            Los controles de la tabla son automáticos basado en el puntaje del torneo.
                        </div>
                    </div>
                </div>

                <!-- Iframe Preview -->
                <div class="lg:col-span-2 bg-[url('[https://obsproject.com/assets/images/new_sources.png](https://obsproject.com/assets/images/new_sources.png)')] bg-cover bg-center rounded-xl overflow-hidden border border-gray-700 relative h-[400px] shadow-2xl group">
                    <div class="absolute inset-0 bg-black/60 backdrop-blur-sm"></div>
                    
                    <div class="absolute inset-0 flex items-center justify-center p-4">
                         <iframe id="preview-frame" :src="previewMode === 'scoreboard' ? '/overlay/scoreboard' : '/overlay/leaderboard'" class="w-full h-full border-none bg-transparent rounded-lg transform scale-90"></iframe>
                    </div>

                    <div class="absolute top-2 left-2 bg-red-600 text-white text-[10px] px-2 py-0.5 rounded font-bold animate-pulse z-10">PREVIEW MODE</div>
                    <button @click="refreshPreview" class="absolute top-2 right-2 bg-gray-800 hover:bg-white text-white hover:text-black w-8 h-8 rounded-full flex items-center justify-center transition z-10">
                        <i class="fas fa-sync-alt text-xs"></i>
                    </button>
                </div>
            </div>
        </div>
    </div>
  </div>
</template>