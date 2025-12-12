<script setup>
import { ref } from 'vue'

definePageMeta({
  layout: 'admin'
})

// Datos simulados
const requests = ref([
    { id: 1, name: 'DarkWarrior_99', rank: 'Diamante II', avatar: 'https://i.pravatar.cc/150?img=12' },
    { id: 2, name: 'Klaudia_FPS', rank: 'Ascendente I', avatar: 'https://i.pravatar.cc/150?img=33' },
    { id: 3, name: 'NoobMaster69', rank: 'Hierro III', avatar: 'https://i.pravatar.cc/150?img=8' }
])

const acceptRequest = (id) => {
    requests.value = requests.value.filter(r => r.id !== id)
    // Aquí llamarías a tu API
}

const rejectRequest = (id) => {
    requests.value = requests.value.filter(r => r.id !== id)
}
</script>

<template>
  <div>
    <header class="h-20 border-b border-gray-800 flex items-center justify-between px-8 bg-[#0B0C15]/90 backdrop-blur-md sticky top-0 z-30">
        <div>
            <h2 class="font-display text-2xl font-bold">Configuración</h2>
            <p class="text-gray-500 text-xs">Administra las reglas y participantes.</p>
        </div>
    </header>

    <div class="p-8 max-w-7xl mx-auto w-full">
         <div class="glass-panel p-4 rounded-xl mb-8 flex flex-col md:flex-row items-center justify-between gap-4 border-l-4 border-brand-purple bg-[#151725] border border-white/5">
            <div class="flex items-center gap-3">
                <div class="bg-brand-purple/20 text-brand-purple p-2 rounded-lg">
                    <i class="fas fa-chess-king text-xl"></i>
                </div>
                <div>
                    <h3 class="font-bold text-white">Seleccionar Torneo</h3>
                    <p class="text-xs text-gray-500">Elige el torneo que deseas configurar.</p>
                </div>
            </div>
            <select class="bg-black border border-gray-700 rounded-lg px-4 py-2 text-white outline-none w-full md:w-64 focus:border-brand-purple text-sm">
                <option>Valorant Masters Q1</option>
                <option>Fortnite Build Battle</option>
                <option>League of Legends Weekly</option>
            </select>
        </div>

        <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
            
            <div class="space-y-8">
                <div class="glass-panel p-6 rounded-2xl bg-[#151725] border border-white/5">
                    <h3 class="font-bold text-lg mb-4 flex items-center gap-2 text-white">
                        <i class="fas fa-edit text-brand-cyan"></i> Información General
                    </h3>
                    <div class="space-y-4">
                        <div>
                            <label class="block text-xs text-gray-500 uppercase font-bold mb-1">Nombre del Torneo</label>
                            <input type="text" value="Valorant Masters Q1" class="w-full bg-black/50 border border-gray-700 rounded p-2.5 text-white focus:border-brand-cyan outline-none transition text-sm">
                        </div>
                        <div class="grid grid-cols-2 gap-4">
                            <div>
                                <label class="block text-xs text-gray-500 uppercase font-bold mb-1">Juego</label>
                                <select class="w-full bg-black/50 border border-gray-700 rounded p-2.5 text-gray-300 outline-none text-sm">
                                    <option>Valorant</option>
                                    <option>Fortnite</option>
                                    <option>League of Legends</option>
                                </select>
                            </div>
                            <div>
                                <label class="block text-xs text-gray-500 uppercase font-bold mb-1">Estado</label>
                                <select class="w-full bg-black/50 border border-gray-700 rounded p-2.5 text-green-400 font-bold outline-none text-sm">
                                    <option class="text-green-400">En Curso</option>
                                    <option class="text-yellow-400">Registro Abierto</option>
                                    <option class="text-gray-400">Finalizado</option>
                                </select>
                            </div>
                        </div>
                        <div class="flex items-center justify-between bg-black/20 p-3 rounded-lg border border-gray-800">
                            <div>
                                <span class="block text-sm font-bold text-white">Torneo Gratuito</span>
                                <span class="text-xs text-gray-500">No cobrar fee de entrada a los jugadores.</span>
                            </div>
                            <div class="relative inline-block w-10 mr-2 align-middle select-none">
                                <input type="checkbox" checked class="absolute block w-5 h-5 rounded-full bg-white border-4 appearance-none cursor-pointer checked:right-0 checked:border-brand-cyan transition-all duration-300 right-5"/>
                                <div class="block overflow-hidden h-5 rounded-full bg-gray-700 cursor-pointer w-10"></div>
                            </div>
                        </div>
                        <button class="w-full bg-brand-cyan/10 hover:bg-brand-cyan/20 text-brand-cyan border border-brand-cyan/50 py-2 rounded font-bold transition text-sm">
                            Guardar Cambios
                        </button>
                    </div>
                </div>

                <div class="glass-panel p-6 rounded-2xl bg-[#151725] border border-white/5">
                    <h3 class="font-bold text-lg mb-4 flex items-center gap-2 text-white">
                        <i class="fas fa-bullhorn text-yellow-500"></i> Enviar Notificación
                    </h3>
                    <div class="space-y-3">
                        <textarea class="w-full bg-black/50 border border-gray-700 rounded p-3 text-white text-sm outline-none focus:border-yellow-500 h-24" placeholder="Escribe un mensaje para los participantes..."></textarea>
                        <div class="flex gap-2">
                            <select class="bg-black/50 border border-gray-700 rounded px-3 py-2 text-xs text-gray-400 outline-none flex-1">
                                <option>A Todos los Participantes</option>
                                <option>Solo Capitanes</option>
                                <option>Solo Staff</option>
                            </select>
                            <button class="bg-yellow-600 hover:bg-yellow-500 text-white px-4 py-2 rounded font-bold text-xs transition">
                                Enviar
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="glass-panel p-6 rounded-2xl h-full bg-[#151725] border border-white/5">
                <div class="flex justify-between items-center mb-6">
                    <h3 class="font-bold text-lg flex items-center gap-2 text-white">
                        <i class="fas fa-user-clock text-orange-500"></i> Solicitudes Pendientes
                        <span class="bg-orange-500/20 text-orange-500 text-xs px-2 py-0.5 rounded-full">{{ requests.length }}</span>
                    </h3>
                    <button class="text-xs text-gray-400 hover:text-white underline">Historial</button>
                </div>

                <div class="space-y-4">
                    <TransitionGroup name="list">
                        <div v-for="req in requests" :key="req.id" class="bg-black/30 p-4 rounded-xl border border-gray-800 flex items-center justify-between group hover:border-gray-600 transition">
                            <div class="flex items-center gap-3">
                                <img :src="req.avatar" class="w-10 h-10 rounded-full border border-gray-700">
                                <div>
                                    <div class="font-bold text-white text-sm">{{ req.name }}</div>
                                    <div class="text-[10px] text-gray-500">Rango: {{ req.rank }}</div>
                                </div>
                            </div>
                            <div class="flex gap-2">
                                <button @click="rejectRequest(req.id)" class="w-8 h-8 rounded bg-red-500/10 text-red-500 hover:bg-red-500 hover:text-white transition flex items-center justify-center">
                                    <i class="fas fa-times"></i>
                                </button>
                                <button @click="acceptRequest(req.id)" class="w-8 h-8 rounded bg-green-500/10 text-green-500 hover:bg-green-500 hover:text-white transition flex items-center justify-center">
                                    <i class="fas fa-check"></i>
                                </button>
                            </div>
                        </div>
                    </TransitionGroup>
                    <div v-if="requests.length === 0" class="text-center py-8 text-gray-500 text-sm">
                        No hay solicitudes pendientes.
                    </div>
                </div>
                
                <div class="mt-6 p-4 bg-blue-900/10 rounded-lg border border-blue-500/20">
                    <h4 class="text-blue-400 font-bold text-xs mb-1"><i class="fas fa-info-circle"></i> Auto-Aceptar</h4>
                    <p class="text-[10px] text-gray-400">Puedes habilitar la aceptación automática si los jugadores cumplen con el rango mínimo requerido.</p>
                    <button class="text-[10px] text-blue-400 underline mt-1">Configurar reglas</button>
                </div>
            </div>
        </div>
    </div>
  </div>
</template>

<style scoped>
.list-move, .list-enter-active, .list-leave-active { transition: all 0.5s ease; }
.list-enter-from, .list-leave-to { opacity: 0; transform: translateX(30px); }
.list-leave-active { position: absolute; }
</style>