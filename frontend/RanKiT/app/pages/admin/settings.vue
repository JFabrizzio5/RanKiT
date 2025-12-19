<script setup>
import { ref } from 'vue'

// Datos simulados (reutilizando para ejemplo visual)
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
  }
])

const filter = ref('Todos')

const getStatusStyle = (status) => {
  if (status === 'En Curso') return 'border-green-500 text-green-500 animate-pulse'
  return 'border-gray-400 text-gray-400 dark:border-gray-600 dark:text-gray-600'
}
</script>

<template>
  <div>
    <!-- Topbar: Fondo blanco en light, oscuro en dark -->
    <header class="h-20 border-b flex items-center justify-between px-8 backdrop-blur-md sticky top-0 z-30 transition-colors duration-300
        bg-white/80 border-gray-200 
        dark:bg-[#050505]/90 dark:border-[#333]">
        <div>
            <!-- Título: Negro en light, Blanco en dark -->
            <h2 class="text-2xl font-bold tracking-tight text-gray-900 uppercase transition-colors font-display dark:text-white">Configuración <span class="text-[var(--rankit-neon)]">General</span></h2>
            <p class="mt-1 font-mono text-xs text-gray-500">AJUSTES DEL SISTEMA.</p>
        </div>
        <div class="flex items-center gap-6">
            <button class="px-5 py-2 text-xs font-bold tracking-wider uppercase btn-skew">
                <span class="flex items-center gap-2 btn-content"><i class="ph ph-floppy-disk"></i> Guardar Cambios</span>
            </button>
        </div>
    </header>

    <div class="w-full p-8 mx-auto max-w-7xl">
        <!-- Card Brutalista: Se adapta al tema (Blanco/Negro) gracias a los estilos globales en admin.vue -->
        <div class="p-0 overflow-hidden brutal-card">
            <div class="p-8 space-y-8">
                <!-- Sección 1 -->
                <div>
                    <h3 class="mb-6 text-lg font-bold text-gray-900 uppercase font-display dark:text-white">Perfil de Organización</h3>
                    <div class="grid grid-cols-1 gap-8 md:grid-cols-2">
                        <div>
                            <label class="block text-[10px] font-bold uppercase tracking-widest mb-2 text-gray-500">Nombre de la Org</label>
                            <input type="text" value="Cometax Org" class="brutal-input">
                        </div>
                        <div>
                            <label class="block text-[10px] font-bold uppercase tracking-widest mb-2 text-gray-500">Email de Contacto</label>
                            <input type="email" value="contacto@cometax.com" class="brutal-input">
                        </div>
                    </div>
                </div>

                <!-- Sección 2: Apariencia -->
                <div class="pt-8 border-t border-gray-200 dark:border-[#333]">
                     <h3 class="mb-6 text-lg font-bold text-gray-900 uppercase font-display dark:text-white">Apariencia</h3>
                     <div class="flex items-center gap-4">
                         <!-- Opción Claro -->
                         <div class="w-32 h-20 bg-white border-2 border-[var(--rankit-neon)] rounded cursor-pointer flex items-center justify-center relative shadow-lg group hover:bg-gray-50">
                             <span class="text-xs font-bold text-black uppercase">Claro</span>
                             <div class="absolute -top-2 -right-2 bg-[var(--rankit-neon)] text-white text-[9px] px-2 py-0.5 rounded-full font-bold">ACTIVO</div>
                         </div>
                         <!-- Opción Oscuro -->
                         <div class="w-32 h-20 bg-[#050505] border border-gray-300 dark:border-gray-600 rounded cursor-pointer flex items-center justify-center hover:border-gray-400 transition-colors">
                             <span class="text-xs font-bold text-gray-500 uppercase dark:text-white">Oscuro</span>
                         </div>
                     </div>
                     <p class="mt-4 text-xs text-gray-500">Selecciona el tema predeterminado para tu panel de administración.</p>
                </div>
            </div>
        </div>
    </div>
  </div>
</template>