<script setup>
import { ref } from 'vue'

const widgetUrl = ref('http://localhost:3000/overlay/scoreboard')
const previewMode = ref('scoreboard')

const copyUrl = () => {
  navigator.clipboard.writeText(widgetUrl.value)
}

// Simulaciones
const updateScore = (team) => { console.log(`Score update: ${team}`) }
const refreshPreview = () => {
    const iframe = document.getElementById('preview-frame')
    if(iframe) iframe.src = iframe.src
}
</script>

<template>
  <div>
    <header class="h-20 border-b flex items-center justify-between px-8 backdrop-blur-md sticky top-0 z-30 transition-colors duration-300
        bg-white/80 border-gray-200 
        dark:bg-[#050505]/90 dark:border-[#333]">
        <div>
            <h2 class="text-2xl font-bold tracking-tight text-gray-900 uppercase transition-colors font-display dark:text-white">Streaming <span class="text-[var(--rankit-neon)]">Tools</span></h2>
            <p class="mt-1 font-mono text-xs text-gray-500">OVERLAYS PARA OBS / STREAMLABS.</p>
        </div>
    </header>

    <div class="w-full p-8 mx-auto max-w-7xl">
        <!-- Widget Generator -->
        <div class="relative p-8 mb-8 overflow-hidden brutal-card">
            <div class="absolute top-0 left-0 w-1 h-full bg-[var(--rankit-neon)]"></div>
            
            <div class="flex flex-col items-start justify-between gap-6 mb-8 md:flex-row md:items-center">
                <div>
                    <h3 class="mb-2 text-xl font-bold text-gray-900 uppercase transition-colors font-display dark:text-white">Generador de Overlays</h3>
                    <p class="max-w-md text-xs text-gray-500">Copia la URL generada y agrégala como "Browser Source" en tu software de transmisión. La resolución recomendada es 1920x1080.</p>
                </div>
                
                <div class="flex gap-4">
                    <button @click="previewMode = 'scoreboard'; widgetUrl = 'http://localhost:3000/overlay/scoreboard'" 
                        :class="['px-4 py-2 text-xs font-bold uppercase tracking-wider border transition-all', 
                        previewMode === 'scoreboard' 
                            ? 'bg-black text-white border-black dark:bg-white dark:text-black dark:border-white' 
                            : 'text-gray-500 border-gray-300 hover:border-gray-500 dark:border-[#333]']">
                        Marcador
                    </button>
                    <button @click="previewMode = 'leaderboard'; widgetUrl = 'http://localhost:3000/overlay/laderboard'" 
                        :class="['px-4 py-2 text-xs font-bold uppercase tracking-wider border transition-all', 
                        previewMode === 'leaderboard' 
                            ? 'bg-black text-white border-black dark:bg-white dark:text-black dark:border-white' 
                            : 'text-gray-500 border-gray-300 hover:border-gray-500 dark:border-[#333]']">
                        Tabla
                    </button>
                </div>
            </div>

            <!-- URL Copy Box -->
            <div class="flex items-end gap-4 mb-8">
                <div class="flex-1">
                    <label class="text-[10px] font-bold text-[var(--rankit-neon)] uppercase tracking-widest mb-2 block">Browser Source URL</label>
                    <div class="flex items-center border-b-2 py-2 transition-colors
                        border-gray-200 dark:border-[#333]">
                        <input type="text" v-model="widgetUrl" readonly class="w-full font-mono text-sm text-gray-600 bg-transparent outline-none dark:text-gray-300">
                    </div>
                </div>
                <button @click="copyUrl" class="px-6 py-2 text-xs font-bold tracking-wider uppercase btn-skew shrink-0">
                    <span class="btn-content"><i class="mr-2 ph ph-copy"></i> Copiar</span>
                </button>
            </div>

            <!-- Preview Area -->
            <div class="grid grid-cols-1 gap-8 lg:grid-cols-3">
                <!-- Controls -->
                <div class="space-y-6">
                    <div class="p-6 border transition-colors
                        bg-gray-50 border-gray-200
                        dark:bg-[#050505] dark:border-[#333]">
                        <h4 class="font-bold mb-4 text-xs uppercase tracking-widest text-gray-900 border-b pb-2 transition-colors
                            border-gray-200 dark:text-white dark:border-[#333]">
                            Control Manual <span class="ml-2 text-red-500">● LIVE</span>
                        </h4>
                        
                        <div class="space-y-4" v-if="previewMode === 'scoreboard'">
                            <div class="grid grid-cols-2 gap-4">
                                <button @click="updateScore('Team 1')" class="p-3 text-xs font-bold text-blue-500 uppercase transition border border-blue-200 hover:bg-blue-50 dark:border-blue-900 dark:hover:bg-blue-900/20">
                                    +1 Team A
                                </button>
                                <button @click="updateScore('Team 2')" class="p-3 text-xs font-bold text-red-500 uppercase transition border border-red-200 hover:bg-red-50 dark:border-red-900 dark:hover:bg-red-900/20">
                                    +1 Team B
                                </button>
                            </div>
                            <button class="w-full p-3 border flex items-center justify-between group transition text-xs font-bold uppercase
                                bg-white text-gray-600 border-gray-200 hover:bg-gray-100
                                dark:bg-[#111] dark:text-gray-300 dark:border-[#333] dark:hover:bg-[#222]">
                                <span>Cambiar Ronda</span>
                                <i class="ph ph-arrows-left-right text-[var(--rankit-neon)]"></i>
                            </button>
                            <button class="flex items-center justify-between w-full p-3 text-xs font-bold text-red-500 uppercase transition border border-red-200 hover:bg-red-50 dark:border-red-900/50 dark:hover:bg-red-900/10">
                                <span>Resetear</span>
                                <i class="ph ph-trash"></i>
                            </button>
                        </div>

                        <div v-else class="py-8 font-mono text-xs text-center text-gray-400 uppercase">
                            // Sincronización automática //
                        </div>
                    </div>
                </div>

                <!-- Iframe Preview -->
                <div class="lg:col-span-2 relative h-[350px] border group overflow-hidden transition-colors
                    bg-gray-100 border-gray-200
                    dark:bg-[#050505] dark:border-[#333]">
                    
                    <!-- Background Grid -->
                    <div class="absolute inset-0 bg-[linear-gradient(to_right,rgba(0,0,0,0.05)_1px,transparent_1px),linear-gradient(to_bottom,rgba(0,0,0,0.05)_1px,transparent_1px)] dark:bg-[linear-gradient(to_right,#1f1f1f_1px,transparent_1px),linear-gradient(to_bottom,#1f1f1f_1px,transparent_1px)] bg-[size:20px_20px]"></div>
                    
                    <iframe id="preview-frame" :src="previewMode === 'scoreboard' ? '/overlay/scoreboard' : '/overlay/laderboard'" class="w-full h-full border-none relative z-10 transform scale-[0.8] origin-center"></iframe>

                    <div class="absolute top-0 left-0 bg-white text-black text-[9px] font-bold px-3 py-1 uppercase tracking-widest z-20 shadow-sm border-r border-b border-gray-200">Preview</div>
                    <button @click="refreshPreview" class="absolute z-20 text-gray-400 transition top-2 right-2 hover:text-black dark:hover:text-white">
                        <i class="text-lg ph ph-arrows-clockwise"></i>
                    </button>
                </div>
            </div>
        </div>
    </div>
  </div>
</template>