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
    date: '12 OCT, 2025',
    initial: 'V',
    color: 'text-[var(--rankit-neon)]'
  },
  { 
    id: 'TRN-9902', 
    name: 'Fortnite Build Battle', 
    game: 'Fortnite', 
    status: 'Registro Abierto', 
    registered: 15, 
    total: 100, 
    date: '20 OCT, 2025',
    initial: 'F',
    color: 'text-yellow-500'
  }
])

const filter = ref('Todos')

const getStatusStyle = (status) => {
  if (status === 'En Curso') return 'border-green-500 text-green-500 animate-pulse'
  if (status === 'Registro Abierto') return 'border-yellow-500 text-yellow-500'
  return 'border-gray-400 text-gray-400 dark:border-gray-600 dark:text-gray-600'
}
</script>

<template>
  <div>
    <!-- Topbar -->
    <header class="h-20 border-b flex items-center justify-between px-8 backdrop-blur-md sticky top-0 z-30 transition-colors duration-300
        bg-white/80 border-gray-200 
        dark:bg-[#050505]/90 dark:border-[#333]">
        <div>
            <h2 class="text-2xl font-bold tracking-tight text-gray-900 uppercase transition-colors font-display dark:text-white">Gestión de <span class="text-[var(--rankit-neon)]">Torneos</span></h2>
            <p class="mt-1 font-mono text-xs text-gray-500">ADMINISTRA TUS COMPETICIONES.</p>
        </div>
        <div class="flex items-center gap-6">
            <button class="px-5 py-2 text-xs font-bold tracking-wider uppercase btn-skew">
                <span class="flex items-center gap-2 btn-content"><i class="ph ph-plus"></i> Nuevo Torneo</span>
            </button>
        </div>
    </header>

    <div class="w-full p-8 mx-auto max-w-7xl">
        <div class="p-0 overflow-hidden brutal-card">
            <!-- Filtros -->
            <div class="p-6 border-b flex flex-col md:flex-row justify-between items-center gap-4 transition-colors
                border-gray-200 dark:border-[#333]">
                <div class="flex gap-4">
                    <button v-for="f in ['Todos', 'Activos', 'Borradores', 'Finalizados']" :key="f"
                        @click="filter = f" 
                        class="text-xs font-bold tracking-wider uppercase transition-colors"
                        :class="filter === f ? 'text-[var(--rankit-neon)] border-b border-[var(--rankit-neon)] pb-1' : 'text-gray-500 hover:text-black dark:text-gray-400 dark:hover:text-white'">
                        {{ f }}
                    </button>
                </div>
                <div class="relative w-full md:w-64">
                    <i class="absolute left-0 text-gray-400 ph ph-magnifying-glass bottom-3"></i>
                    <input type="text" placeholder="BUSCAR TORNEO..." class="pl-6 text-xs brutal-input">
                </div>
            </div>

            <!-- Tabla -->
            <div class="overflow-x-auto">
                <table class="w-full text-left">
                    <thead class="text-[10px] uppercase font-bold tracking-widest border-b transition-colors
                        bg-gray-50 border-gray-200 text-gray-500
                        dark:bg-[#0f0f0f] dark:border-[#333] dark:text-gray-500">
                        <tr>
                            <th class="px-6 py-4">Torneo</th>
                            <th class="px-6 py-4">Juego</th>
                            <th class="px-6 py-4">Estado</th>
                            <th class="px-6 py-4">Inscritos</th>
                            <th class="px-6 py-4">Fecha</th>
                            <th class="px-6 py-4 text-right">Acciones</th>
                        </tr>
                    </thead>
                    <tbody class="divide-y text-sm transition-colors
                        divide-gray-100 dark:divide-[#333]">
                        <tr v-for="trn in tournaments" :key="trn.id" class="transition group
                            hover:bg-gray-50 dark:hover:bg-[#111]">
                            <td class="px-6 py-4">
                                <div class="flex items-center gap-4">
                                    <div class="w-10 h-10 border flex items-center justify-center font-display font-bold text-lg transition-colors
                                        bg-gray-100 border-gray-200 dark:bg-black dark:border-[#333]" :class="trn.color">
                                        {{ trn.initial }}
                                    </div>
                                    <div>
                                        <div class="font-bold uppercase font-display transition group-hover:text-[var(--rankit-neon)]
                                            text-gray-900 dark:text-white">{{ trn.name }}</div>
                                        <div class="text-[10px] text-gray-500 font-mono">ID: {{ trn.id }}</div>
                                    </div>
                                </div>
                            </td>
                            <td class="px-6 py-4">
                                <span class="text-xs font-bold text-gray-700 uppercase transition-colors dark:text-gray-300">{{ trn.game }}</span>
                            </td>
                            <td class="px-6 py-4">
                                <span :class="`border px-2 py-1 text-[10px] font-bold uppercase tracking-wider ${getStatusStyle(trn.status)}`">
                                    {{ trn.status }}
                                </span>
                            </td>
                            <td class="px-6 py-4">
                                <div class="flex items-center gap-3">
                                    <div class="w-24 h-1.5 bg-gray-200 dark:bg-[#333]">
                                        <div :class="`h-1.5 ${trn.status === 'En Curso' ? 'bg-[var(--rankit-neon)]' : 'bg-yellow-500'}`" :style="`width: ${(trn.registered / trn.total) * 100}%`"></div>
                                    </div>
                                    <span class="font-mono text-xs text-gray-500">{{ trn.registered }}/{{ trn.total }}</span>
                                </div>
                            </td>
                            <td class="px-6 py-4 font-mono text-xs text-gray-500">{{ trn.date }}</td>
                            <td class="px-6 py-4 text-right">
                                <NuxtLink to="/admin/settings" class="mx-2 text-gray-400 transition hover:text-black dark:hover:text-white"><i class="text-lg ph ph-pencil-simple"></i></NuxtLink>
                                <button class="text-gray-400 hover:text-[var(--rankit-neon)] mx-2 transition"><i class="text-lg ph ph-share-network"></i></button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            
            <!-- Paginación -->
            <div class="p-4 border-t flex justify-between items-center text-[10px] uppercase font-bold text-gray-500 transition-colors
                border-gray-200 dark:border-[#333]">
                <span>{{ tournaments.length }} RESULTADOS</span>
                <div class="flex gap-2">
                    <button class="hover:text-black dark:hover:text-white disabled:opacity-50" disabled>Anterior</button>
                    <span class="text-black dark:text-white">1</span>
                    <button class="hover:text-black dark:hover:text-white">Siguiente</button>
                </div>
            </div>
        </div>
    </div>
  </div>
</template>