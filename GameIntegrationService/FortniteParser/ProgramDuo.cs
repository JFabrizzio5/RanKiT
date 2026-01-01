using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using FortniteReplayReader; 

namespace FortniteReplayParser
{
    // Clase para acumular datos globales del torneo
    public class GlobalTeamStats
    {
        // Corrección CS8618: Inicializamos con valor por defecto
        public string TeamName { get; set; } = "Equipo Desconocido"; 
        public int TotalKills { get; set; }
        public int TotalPoints { get; set; }
        public int GamesPlayed { get; set; }
        public List<int> Placements { get; set; } = new List<int>();

        public double AveragePlacement => Placements.Count > 0 ? Placements.Average() : 0;
    }

    class ProgramDuos
    {
        private const string Extension = ".replay";
        // Diccionario para la Tabla General: Clave = "NombreJugador1+NombreJugador2"
        private static Dictionary<string, GlobalTeamStats> TablaGeneral = new Dictionary<string, GlobalTeamStats>();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================");
            Console.WriteLine("    ANALIZADOR DE TORNEOS (DUOS) - FORTNITE     ");
            Console.WriteLine("================================================");
            Console.ResetColor();

            // 1. Seleccionar la carpeta
            string carpetaSeleccionada = ObtenerCarpetaObjetivo(args);

            if (string.IsNullOrEmpty(carpetaSeleccionada) || !Directory.Exists(carpetaSeleccionada))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[Error] No se ha seleccionado una carpeta válida.");
                Console.ResetColor();
                return;
            }

            // 2. Obtener archivos
            var archivosReplay = Directory.GetFiles(carpetaSeleccionada, $"*{Extension}");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n📂 Carpeta: {new DirectoryInfo(carpetaSeleccionada).Name}");
            Console.WriteLine($"🔍 Encontrados: {archivosReplay.Length} partidas.\n");
            Console.ResetColor();

            // 3. Procesar cada partida
            int contador = 1;
            foreach (var archivo in archivosReplay)
            {
                Console.WriteLine($"\n>>> Procesando partida {contador}/{archivosReplay.Length}: {Path.GetFileName(archivo)}");
                ProcesarPartida(archivo, carpetaSeleccionada);
                contador++;
            }

            // 4. Generar Tabla Global
            GenerarTablaGlobal(carpetaSeleccionada);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ ¡PROCESO COMPLETO!");
            Console.WriteLine("Revisa la carpeta para ver los .txt de cada partida y la TABLA_GLOBAL.");
            Console.ResetColor();
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        static void ProcesarPartida(string rutaArchivo, string carpetaSalida)
        {
            try
            {
                // Instanciamos el lector de replays
                var reader = new ReplayReader();
                var replay = reader.ReadReplay(rutaArchivo);

                if (replay == null || replay.PlayerData == null)
                {
                    Console.WriteLine("   - [WARN] No se pudo leer info de jugadores.");
                    return;
                }

                // Agrupar jugadores por TeamIndex (para identificar Duos)
                var equipos = replay.PlayerData
                    .Where(p => !p.IsBot) // Filtramos bots
                    .GroupBy(p => p.TeamIndex)
                    .Select(grupo => 
                    {
                        var jugadores = grupo.ToList();
                        
                        // --- CORRECCIÓN CRÍTICA (NULL SAFE) ---
                        // Usamos (p.Kills ?? 0) para evitar que falle si Kills es null
                        int killsEquipo = (int)jugadores.Sum(p => (long)(p.Kills ?? 0));
                        
                        // Usamos ?? 0 para evitar fallos si Placement es null
                        int posicion = jugadores.Min(p => p.Placement) ?? 0;
                        
                        // Cálculo de Puntos
                        int puntosPosicion = (posicion > 0 && posicion <= 40) ? (41 - posicion) : 0;
                        int puntosTotal = (killsEquipo * 2) + puntosPosicion;

                        // Crear nombre compuesto
                        var nombresOrdenados = jugadores.Select(p => p.PlayerName).OrderBy(n => n).ToList();
                        string nombreEquipo = string.Join(" & ", nombresOrdenados);
                        if (string.IsNullOrWhiteSpace(nombreEquipo)) nombreEquipo = $"Equipo Desconocido ({grupo.Key})";

                        return new 
                        {
                            TeamId = grupo.Key,
                            NombreDisplay = nombreEquipo,
                            Kills = killsEquipo,
                            Placement = posicion,
                            Puntos = puntosTotal
                        };
                    })
                    .OrderBy(x => x.Placement) // Ordenar por posición en esta partida
                    .ToList();

                // --- A. Generar TXT de ESTA partida ---
                string nombreArchivoTxt = Path.Combine(carpetaSalida, Path.GetFileNameWithoutExtension(rutaArchivo) + "_Resultados.txt");
                
                using (StreamWriter sw = new StreamWriter(nombreArchivoTxt))
                {
                    sw.WriteLine($"--- RESULTADOS: {Path.GetFileName(rutaArchivo)} ---");
                    sw.WriteLine($"Fecha: {DateTime.Now}");
                    sw.WriteLine($"Equipos encontrados: {equipos.Count}");
                    sw.WriteLine(new string('-', 90));
                    sw.WriteLine($"| {"POS",-4} | {"EQUIPO / JUGADORES",-50} | {"KILLS",-6} | {"PTS",-6} |");
                    sw.WriteLine(new string('-', 90));

                    foreach (var equipo in equipos)
                    {
                        sw.WriteLine($"| #{equipo.Placement,-4} | {equipo.NombreDisplay,-50} | {equipo.Kills,-6} | {equipo.Puntos,-6} |");

                        // --- B. Actualizar Tabla Global ---
                        if (!TablaGeneral.ContainsKey(equipo.NombreDisplay))
                        {
                            TablaGeneral[equipo.NombreDisplay] = new GlobalTeamStats 
                            { 
                                TeamName = equipo.NombreDisplay, 
                                TotalKills = 0, 
                                TotalPoints = 0,
                                GamesPlayed = 0
                            };
                        }

                        // Sumar estadísticas
                        var stats = TablaGeneral[equipo.NombreDisplay];
                        stats.TotalKills += equipo.Kills;
                        stats.TotalPoints += equipo.Puntos;
                        stats.GamesPlayed++;
                        stats.Placements.Add(equipo.Placement);
                    }
                }

                Console.WriteLine($"   - 📄 Reporte creado: {Path.GetFileName(nombreArchivoTxt)}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   - [ERROR] Falló al procesar: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void GenerarTablaGlobal(string carpetaSalida)
        {
            Console.WriteLine("\nCalculando Tabla General del Torneo...");

            string rutaGlobal = Path.Combine(carpetaSalida, "TABLA_GLOBAL_TORNEO.txt");
            
            // Ordenar: Mayor puntaje primero, luego más kills, luego mejor promedio de posición
            var rankingGlobal = TablaGeneral.Values
                .OrderByDescending(t => t.TotalPoints)
                .ThenByDescending(t => t.TotalKills)
                .ThenBy(t => t.AveragePlacement)
                .ToList();

            using (StreamWriter sw = new StreamWriter(rutaGlobal))
            {
                sw.WriteLine("==========================================================================");
                sw.WriteLine("                  TABLA GENERAL DEL TORNEO (ACUMULADO)                    ");
                sw.WriteLine("==========================================================================");
                if (TablaGeneral.Values.Count > 0)
                {
                    sw.WriteLine($"Total Partidas Procesadas: {TablaGeneral.Values.Max(x => x.GamesPlayed)} (aprox)");
                }
                else
                {
                    sw.WriteLine("Total Partidas Procesadas: 0");
                }
                sw.WriteLine($"Equipos Totales: {rankingGlobal.Count}");
                sw.WriteLine("");
                sw.WriteLine(new string('-', 100));
                sw.WriteLine($"| {"RANK",-4} | {"EQUIPO",-50} | {"TOTAL PTS",-10} | {"KILLS",-6} | {"PROM. TOP",-10} |");
                sw.WriteLine(new string('-', 100));

                int rank = 1;
                foreach (var team in rankingGlobal)
                {
                    sw.WriteLine($"| #{rank,-4} | {team.TeamName,-50} | {team.TotalPoints,-10} | {team.TotalKills,-6} | #{team.AveragePlacement,-9:F1} |");
                    rank++;
                }
                sw.WriteLine(new string('-', 100));
            }

            Console.WriteLine($"🏆 TABLA GLOBAL GENERADA: {rutaGlobal}");
        }

        static string ObtenerCarpetaObjetivo(string[] args)
        {
            if (args.Length > 0 && Directory.Exists(args[0])) return args[0];

            string directorioRaiz = Directory.GetCurrentDirectory();
            var subDirectorios = Directory.GetDirectories(directorioRaiz);
            var carpetasValidas = subDirectorios.Where(d => Directory.GetFiles(d, $"*{Extension}").Length > 0).ToList();

            if (carpetasValidas.Count == 0)
            {
                Console.WriteLine($"Arrastra la carpeta aquí:");
                // Corrección CS8603: Si es nulo, devuelve string vacío
                return Console.ReadLine()?.Trim('"').Trim() ?? string.Empty;
            }
            else if (carpetasValidas.Count == 1)
            {
                return carpetasValidas[0];
            }
            else
            {
                Console.WriteLine("\nCarpetas encontradas:");
                for (int i = 0; i < carpetasValidas.Count; i++)
                    Console.WriteLine($"[{i + 1}] {new DirectoryInfo(carpetasValidas[i]).Name}");

                Console.Write("Elige número: ");
                if (int.TryParse(Console.ReadLine(), out int sel) && sel > 0 && sel <= carpetasValidas.Count)
                    return carpetasValidas[sel - 1];
                return carpetasValidas[0];
            }
        }
    }
}