<script setup>
import { ref } from 'vue'

// Datos simulados
const tournaments = ref([
  { 
    id: 'TRN-8821', 
    name: 'Valorant Masters Q1', 
    game: 'Valorant', 
    status: 'En Curso', 
    registered: 48, 
    total: 64, 
    date: '12 Oct, 2025',
    iconColor: 'from-brand-purple to-blue-600',
    initial: 'V',
    gameIcon: 'https://img.icons8.com/color/48/valorant.png'
  },
  { 
    id: 'TRN-9902', 
    name: 'Fortnite Build Battle', 
    game: 'Fortnite', 
    status: 'Registro Abierto', 
    registered: 15, 
    total: 100, 
    date: '20 Oct, 2025',
    iconColor: 'from-yellow-600 to-yellow-400',
    initial: 'F',
    gameIcon: 'https://img.icons8.com/external-tal-revivo-color-tal-revivo/48/external-fortnite-a-free-to-play-battle-royale-game-developed-by-epic-games-logo-color-tal-revivo.png'
  }
])

const filter = ref('Todos')

// Helper para clases de estado
const getStatusClass = (status) => {
  if (status === 'En Curso') return 'bg-green-500/10 text-green-400 border-green-500/20'
  if (status === 'Registro Abierto') return 'bg-yellow-500/10 text-yellow-400 border-yellow-500/20'
  return 'bg-gray-500/10 text-gray-400'
}
</script>

<template>
  <div>
    <!-- Topbar -->
    <header class="h-20 border-b border-gray-800 flex items-center justify-between px-8 bg-[#0B0C15]/90 backdrop-blur-md sticky top-0 z-30">
        <div>
            <h2 class="font-display text-2xl font-bold">Gestión de Torneos</h2>
            <p class="text-gray-500 text-xs">Administra tus competiciones activas y pasadas.</p>
        </div>
        <div class="flex items-center gap-6">
            <button class="bg-gradient-to-r from-brand-purple to-brand-cyan text-white px-5 py-2 rounded-lg font-bold text-xs shadow-lg hover:opacity-90 transition">
                <i class="fas fa-plus"></i> Nuevo Torneo
            </button>
        </div>
    </header>

    <div class="p-8 max-w-7xl mx-auto w-full">
        <div class="glass-panel rounded-2xl overflow-hidden bg-[#151725] border border-white/5">
            <!-- Filtros y Búsqueda -->
            <div class="p-5 border-b border-gray-800 flex flex-col md:flex-row justify-between items-center gap-4">
                <div class="flex gap-2 bg-black/30 p-1 rounded-lg">
                    <button @click="filter = 'Todos'" :class="['px-4 py-1.5 rounded-md text-xs font-bold transition', filter === 'Todos' ? 'bg-gray-700 text-white shadow' : 'text-gray-400 hover:text-white']">Todos</button>
                    <button @click="filter = 'Activos'" :class="['px-4 py-1.5 rounded-md text-xs font-bold transition', filter === 'Activos' ? 'bg-gray-700 text-white shadow' : 'text-gray-400 hover:text-white']">Activos</button>
                    <button @click="filter = 'Borradores'" :class="['px-4 py-1.5 rounded-md text-xs font-bold transition', filter === 'Borradores' ? 'bg-gray-700 text-white shadow' : 'text-gray-400 hover:text-white']">Borradores</button>
                    <button @click="filter = 'Finalizados'" :class="['px-4 py-1.5 rounded-md text-xs font-bold transition', filter === 'Finalizados' ? 'bg-gray-700 text-white shadow' : 'text-gray-400 hover:text-white']">Finalizados</button>
                </div>
                <div class="relative">
                    <i class="fas fa-search absolute left-3 top-2.5 text-gray-500 text-xs"></i>
                    <input type="text" placeholder="Buscar torneo..." class="bg-black/30 border border-gray-700 rounded-lg pl-8 pr-4 py-2 text-xs text-white focus:border-brand-cyan outline-none w-64">
                </div>
            </div>

            <!-- Tabla -->
            <div class="overflow-x-auto">
                <table class="w-full text-left text-gray-400">
                    <thead class="bg-black/40 text-[10px] uppercase font-bold tracking-wider text-gray-500">
                        <tr>
                            <th class="px-6 py-4">Torneo</th>
                            <th class="px-6 py-4">Juego</th>
                            <th class="px-6 py-4">Estado</th>
                            <th class="px-6 py-4">Inscritos</th>
                            <th class="px-6 py-4">Fecha</th>
                            <th class="px-6 py-4 text-right">Acciones</th>
                        </tr>
                    </thead>
                    <tbody class="divide-y divide-gray-800 text-sm">
                        <tr v-for="trn in tournaments" :key="trn.id" class="hover:bg-white/5 transition group">
                            <td class="px-6 py-4">
                                <div class="flex items-center gap-3">
                                    <div :class="`w-10 h-10 rounded bg-gradient-to-br ${trn.iconColor} flex items-center justify-center text-white font-bold`">
                                        {{ trn.initial }}
                                    </div>
                                    <div>
                                        <div class="font-bold text-white group-hover:text-brand-cyan transition">{{ trn.name }}</div>
                                        <div class="text-[10px] text-gray-500">ID: {{ trn.id }}</div>
                                    </div>
                                </div>
                            </td>
                            <td class="px-6 py-4">
                                <span class="flex items-center gap-2">
                                    <img :src="trn.gameIcon" class="w-4 h-4">
                                    {{ trn.game }}
                                </span>
                            </td>
                            <td class="px-6 py-4">
                                <span :class="`border px-2 py-1 rounded text-xs font-bold flex w-fit items-center gap-1 ${getStatusClass(trn.status)}`">
                                    <span v-if="trn.status === 'En Curso'" class="w-1.5 h-1.5 rounded-full bg-green-500 animate-pulse"></span>
                                    {{ trn.status }}
                                </span>
                            </td>
                            <td class="px-6 py-4">
                                <div class="flex items-center gap-2">
                                    <div class="w-16 bg-gray-700 rounded-full h-1.5">
                                        <div :class="`h-1.5 rounded-full ${trn.status === 'En Curso' ? 'bg-brand-cyan' : 'bg-yellow-500'}`" :style="`width: ${(trn.registered / trn.total) * 100}%`"></div>
                                    </div>
                                    <span class="text-xs">{{ trn.registered }}/{{ trn.total }}</span>
                                </div>
                            </td>
                            <td class="px-6 py-4 text-xs">{{ trn.date }}</td>
                            <td class="px-6 py-4 text-right">
                                <NuxtLink to="/admin/settings" class="text-gray-400 hover:text-white mx-1"><i class="fas fa-edit"></i></NuxtLink>
                                <button class="text-gray-400 hover:text-brand-cyan mx-1"><i class="fas fa-share-alt"></i></button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            
            <!-- Paginación -->
            <div class="p-4 border-t border-gray-800 flex justify-between items-center text-xs text-gray-500">
                <span>Mostrando {{ tournaments.length }} torneos</span>
                <div class="flex gap-1">
                    <button class="px-3 py-1 rounded bg-white/5 hover:bg-white/10 text-white">Anterior</button>
                    <button class="px-3 py-1 rounded bg-brand-cyan text-black font-bold">1</button>
                    <button class="px-3 py-1 rounded bg-white/5 hover:bg-white/10 text-white">Siguiente</button>
                </div>
            </div>
        </div>
    </div>
  </div>
</template>