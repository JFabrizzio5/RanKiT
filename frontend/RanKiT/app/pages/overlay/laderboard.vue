<script setup>
import { ref, onMounted } from 'vue'

definePageMeta({
  layout: 'overlay'
})

// Datos iniciales
const players = ref([
    { id: 1, name: "SkullCrusher", score: 1500, avatar: "https://i.pravatar.cc/150?img=11" },
    { id: 2, name: "NinjaTacos", score: 1450, avatar: "https://i.pravatar.cc/150?img=12" },
    { id: 3, name: "ViperMain", score: 1320, avatar: "https://i.pravatar.cc/150?img=13" },
    { id: 4, name: "NoobMaster", score: 1200, avatar: "https://i.pravatar.cc/150?img=14" },
    { id: 5, name: "CometaxPro", score: 1100, avatar: "https://i.pravatar.cc/150?img=15" },
])

// Simulación de actualización en tiempo real
onMounted(() => {
    if(process.client) {
        setInterval(() => {
            // Elegir random y sumar puntos
            const randomIndex = Math.floor(Math.random() * players.value.length)
            players.value[randomIndex].score += Math.floor(Math.random() * 50) + 10
            
            // Reordenar
            players.value.sort((a, b) => b.score - a.score)
        }, 2500)
    }
})
</script>

<template>
    <div class="p-10">
        <div class="w-[400px] bg-[#0b0c15]/95 rounded-2xl border-2 border-brand-cyan/30 shadow-2xl p-4 relative overflow-hidden mx-auto">
            <div class="absolute top-0 left-0 right-0 h-1 bg-gradient-to-r from-brand-purple to-brand-cyan"></div>
            
            <div class="flex justify-between items-center mb-4 border-b border-gray-700 pb-2">
                <div class="flex items-center gap-2">
                    <i class="fas fa-trophy text-brand-cyan"></i>
                    <h2 class="font-display font-bold text-white uppercase tracking-wider text-sm">Top Players</h2>
                </div>
                <span class="text-[10px] text-brand-cyan bg-brand-cyan/10 px-2 py-0.5 rounded animate-pulse">LIVE</span>
            </div>

            <div class="space-y-2 min-h-[300px] relative">
                <TransitionGroup name="list">
                    <div v-for="(player, index) in players" :key="player.id" 
                         class="flex items-center justify-between p-2 rounded-lg border-l-4 transition-all duration-500"
                         :class="{
                            'bg-yellow-500/10 border-yellow-500': index === 0,
                            'bg-gray-400/10 border-gray-400': index === 1,
                            'bg-orange-700/10 border-orange-700': index === 2,
                            'bg-white/5 border-transparent': index > 2
                         }"
                    >
                        <div class="flex items-center gap-3">
                            <span class="font-display font-bold w-4 text-center text-sm" 
                                  :class="index < 3 ? 'text-white' : 'text-gray-500'">
                                {{ index + 1 }}
                            </span>
                            <img :src="player.avatar" class="w-8 h-8 rounded-full border border-gray-600">
                            <span class="text-white font-bold text-sm truncate w-24">{{ player.name }}</span>
                        </div>
                        <span class="font-mono text-brand-cyan font-bold">{{ player.score }}</span>
                    </div>
                </TransitionGroup>
            </div>

            <div class="mt-4 text-center">
                <p class="text-[10px] text-gray-500 font-display">POWERED BY RANKIT</p>
            </div>
        </div>
    </div>
</template>

<style scoped>
/* Animaciones de lista estilo Flip */
.list-move,
.list-enter-active,
.list-leave-active {
  transition: all 0.5s ease;
}
.list-enter-from,
.list-leave-to {
  opacity: 0;
  transform: translateX(30px);
}
.list-leave-active {
  position: absolute;
}
</style>