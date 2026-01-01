using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using FortniteReplayReader;
using FortniteReplayReader.Models;

namespace FortniteReplayParser
{
    // Clase para acumular datos globales del torneo
    public class GlobalTeamStats
    {
        public string TeamName { get; set; } = "Equipo Desconocido";
        public int TotalKills { get; set; }
        public int TotalPoints { get; set; }
        public int GamesPlayed { get; set; }
        public List<int> Placements { get; set; } = new List<int>();

        // Diccionarios para guardar stats individuales acumuladas
        public Dictionary<string, int> IndividualKills { get; set; } = new Dictionary<string, int>();
        // Nota: Damage y TimeAlive eliminados de la acumulación global porque la librería no los provee

        public double AveragePlacement => Placements.Count > 0 ? Placements.Average() : 0;
    }

    class ProgramDuos
    {
        private const string Extension = ".replay";
        private static Dictionary<string, GlobalTeamStats> TablaGeneral = new Dictionary<string, GlobalTeamStats>();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================");
            Console.WriteLine("    ANALIZADOR DE TORNEOS PRO (DUOS)            ");
            Console.WriteLine("    (Versión Compatible - Kills y Puntos)       ");
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

            // --- CONFIGURACIÓN DE USUARIO ---
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n¿Quieres incluir a los BOTS en el análisis? (S/N): ");
            Console.ResetColor();
            string respBots = Console.ReadLine()?.Trim().ToUpper() ?? "N";
            bool incluirBots = (respBots == "S" || respBots == "SI" || respBots == "Y");

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
                ProcesarPartida(archivo, carpetaSeleccionada, incluirBots);
                contador++;
            }

            // 4. Generar Tabla Global
            GenerarTablaGlobal(carpetaSeleccionada);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ ¡PROCESO COMPLETO!");
            Console.WriteLine("Revisa la carpeta para ver los reportes detallados.");
            Console.ResetColor();
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        static void ProcesarPartida(string rutaArchivo, string carpetaSalida, bool incluirBots)
        {
            try
            {
                var reader = new ReplayReader();
                var replay = reader.ReadReplay(rutaArchivo);

                if (replay == null || replay.PlayerData == null)
                {
                    Console.WriteLine("   - [WARN] No se pudo leer info de jugadores.");
                    return;
                }

                // FIX: Eliminada la lectura de Header.TimeInMillis que causaba error de compilación.
                // Se establece en 0 ya que esta versión de la librería no expone la duración fácilmente.
                double duracionSegundos = 0;

                // Filtrar lista inicial de jugadores
                var jugadoresParaAnalizar = replay.PlayerData.AsEnumerable();
                if (!incluirBots)
                {
                    jugadoresParaAnalizar = jugadoresParaAnalizar.Where(p => !p.IsBot);
                }

                // --- 1. PROCESAR EQUIPOS ---
                var equipos = jugadoresParaAnalizar
                    .GroupBy(p => p.TeamIndex)
                    .Select(grupo =>
                    {
                        var jugadores = grupo.ToList();

                        // Estadísticas básicas
                        int killsEquipo = (int)jugadores.Sum(p => (long)(p.Kills ?? 0));
                        int posicion = jugadores.Min(p => p.Placement) ?? 0;

                        // Cálculo de Puntos (Ejemplo simple)
                        int puntosPosicion = (posicion > 0 && posicion <= 40) ? (41 - posicion) : 0;
                        int puntosTotal = (killsEquipo * 2) + puntosPosicion;

                        // Nombres (Manejo de nulos agregado para evitar warnings)
                        var nombresOrdenados = jugadores
                            .Select(p =>
                            {
                                string pName = p.PlayerName ?? "Desconocido";
                                return p.IsBot ? $"{pName} [BOT]" : pName;
                            })
                            .OrderBy(n => n)
                            .ToList();

                        string nombreEquipo = string.Join(" & ", nombresOrdenados);
                        if (string.IsNullOrWhiteSpace(nombreEquipo)) nombreEquipo = $"Equipo Desconocido ({grupo.Key})";

                        return new
                        {
                            TeamId = grupo.Key,
                            NombreDisplay = nombreEquipo,
                            Kills = killsEquipo,
                            Placement = posicion,
                            Puntos = puntosTotal,
                            Jugadores = jugadores
                        };
                    })
                    .OrderBy(x => x.Placement)
                    .ToList();

                // A. Generar TXT de Resultados
                string nombreArchivoRes = Path.Combine(carpetaSalida, Path.GetFileNameWithoutExtension(rutaArchivo) + "_Resultados.txt");
                using (StreamWriter sw = new StreamWriter(nombreArchivoRes))
                {
                    sw.WriteLine($"--- RESULTADOS: {Path.GetFileName(rutaArchivo)} ---");
                    sw.WriteLine($"Duración Replay: {(duracionSegundos > 0 ? TimeSpan.FromSeconds(duracionSegundos).ToString(@"mm\:ss") : "N/A")}");
                    sw.WriteLine(new string('-', 100));

                    // Cabecera ajustada a lo que SÍ funciona
                    string header = $"| {"POS",-4} | {"EQUIPO / JUGADORES",-50} | {"KILLS",-6} | {"PTS",-6} |";
                    sw.WriteLine(header);
                    sw.WriteLine(new string('-', 100));

                    foreach (var equipo in equipos)
                    {
                        // Formatear jugadores
                        var detallesJugadores = equipo.Jugadores.Select(j =>
                        {
                            string nombre = (j.IsBot ? "BOT" : j.PlayerName) ?? "Desconocido";
                            return $"{nombre} ({j.Kills ?? 0})";
                        });

                        string displayStr = string.Join(" & ", detallesJugadores);

                        string linea = $"| #{equipo.Placement,-4} | {Shorten(displayStr, 50),-50} | {equipo.Kills,-6} | {equipo.Puntos,-6} |";
                        sw.WriteLine(linea);

                        // --- ACTUALIZAR TABLA GLOBAL ---
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
                        var stats = TablaGeneral[equipo.NombreDisplay];
                        stats.TotalKills += equipo.Kills;
                        stats.TotalPoints += equipo.Puntos;
                        stats.GamesPlayed++;
                        stats.Placements.Add(equipo.Placement);

                        // Acumular stats individuales (Solo Kills)
                        foreach (var jugador in equipo.Jugadores)
                        {
                            // FIX: Asegurar que nombreJugador no sea null para evitar crash en Dictionary
                            string pName = jugador.PlayerName ?? "Desconocido";
                            string nombreJugador = jugador.IsBot ? $"{pName} [BOT]" : pName;

                            int killsJugador = (int)(jugador.Kills ?? 0);

                            if (!stats.IndividualKills.ContainsKey(nombreJugador)) stats.IndividualKills[nombreJugador] = 0;
                            stats.IndividualKills[nombreJugador] += killsJugador;
                        }
                    }
                }
                Console.WriteLine($"   - 📄 Resultados creados: {Path.GetFileName(nombreArchivoRes)}");

                // --- 2. GENERAR CURIOSIDADES DE LA PARTIDA ---
                GenerarCuriosidades(jugadoresParaAnalizar.ToList(), rutaArchivo, carpetaSalida, duracionSegundos);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   - [ERROR] Falló al procesar: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void GenerarCuriosidades(List<PlayerData> jugadores, string rutaArchivo, string carpetaSalida, double duracionTotal)
        {
            if (jugadores == null || jugadores.Count == 0) return;

            string nombreArchivoCurios = Path.Combine(carpetaSalida, Path.GetFileNameWithoutExtension(rutaArchivo) + "_Curiosidades.txt");

            var masKills = jugadores.OrderByDescending(p => p.Kills ?? 0).FirstOrDefault();
            var ganador = jugadores.OrderBy(p => p.Placement ?? 999).FirstOrDefault();

            using (StreamWriter sw = new StreamWriter(nombreArchivoCurios))
            {
                sw.WriteLine("=================================================");
                sw.WriteLine($"   DATOS CURIOSOS: {Path.GetFileName(rutaArchivo)}");
                sw.WriteLine("=================================================\n");

                sw.WriteLine("💀 EL EXTERMINADOR (Más Kills):");
                if (masKills != null)
                {
                    string nombreKills = masKills.PlayerName ?? "Desconocido";
                    sw.WriteLine($"   -> {nombreKills}: {masKills.Kills} bajas. (Top #{masKills.Placement})");
                }

                sw.WriteLine("\n🏆 GANADOR:");
                if (ganador != null)
                {
                    string nombreGanador = ganador.PlayerName ?? "Desconocido";
                    sw.WriteLine($"   -> {nombreGanador} (Top #1)");
                    if (duracionTotal > 0)
                    {
                        sw.WriteLine($"   -> Tiempo Sobrevivido: {TimeSpan.FromSeconds(duracionTotal):mm\\:ss} (Partida Completa)");
                    }
                }

                sw.WriteLine("\n⚠️ NOTA TÉCNICA:");
                sw.WriteLine("   El Daño y el Tiempo exacto de muerte no están disponibles");
                sw.WriteLine("   con la librería actual, por lo que se han omitido.");
            }
            Console.WriteLine($"   - 🎭 Curiosidades creadas.");
        }

        static void GenerarTablaGlobal(string carpetaSalida)
        {
            Console.WriteLine("\nCalculando Tabla General y Top Global...");

            string rutaGlobal = Path.Combine(carpetaSalida, "TABLA_GLOBAL_TORNEO.txt");

            var rankingGlobal = TablaGeneral.Values
                .OrderByDescending(t => t.TotalPoints)
                .ThenByDescending(t => t.TotalKills)
                .ToList();

            var todosJugadores = TablaGeneral.Values.SelectMany(t => t.IndividualKills.Keys.Select(k => new
            {
                Nombre = k,
                Kills = t.IndividualKills[k]
            })).ToList();

            // --- MOSTRAR TOP KILLS EN CONSOLA ---
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n=================================");
            Console.WriteLine("    🔥 TOP 3 KILLERS GLOBAL 🔥   ");
            Console.WriteLine("=================================");
            int rankConsole = 1;
            foreach (var p in todosJugadores.OrderByDescending(x => x.Kills).Take(3))
            {
                Console.WriteLine($" #{rankConsole} {p.Nombre}: {p.Kills} kills");
                rankConsole++;
            }
            Console.ResetColor();
            Console.WriteLine("\n");
            // ------------------------------------

            using (StreamWriter sw = new StreamWriter(rutaGlobal))
            {
                sw.WriteLine("==========================================================================================");
                sw.WriteLine("                          TABLA GENERAL DEL TORNEO (ACUMULADO)                            ");
                sw.WriteLine("==========================================================================================");
                sw.WriteLine($"Total Partidas: {(TablaGeneral.Values.Count > 0 ? TablaGeneral.Values.Max(x => x.GamesPlayed) : 0)}");
                sw.WriteLine(new string('-', 100));

                string header = $"| {"#",-3} | {"EQUIPO / JUGADORES",-50} | {"PTS",-5} | {"KILLS",-5} | {"PROM. TOP",-10} |";
                sw.WriteLine(header);
                sw.WriteLine(new string('-', 100));

                int rank = 1;
                foreach (var team in rankingGlobal)
                {
                    var nombres = string.Join(", ", team.IndividualKills.Keys.Select(k => k.Split(' ')[0]));

                    string linea = $"| {rank,-3} | {Shorten(nombres, 50),-50} | {team.TotalPoints,-5} | {team.TotalKills,-5} | {team.AveragePlacement,-10:F1} |";
                    sw.WriteLine(linea);
                    rank++;
                }

                sw.WriteLine(new string('-', 100));
                sw.WriteLine("\n\n");

                sw.WriteLine("💀 TOP 10 - MÁS KILLS (GLOBAL)");
                sw.WriteLine("(Acumulado de todas las partidas procesadas)");
                sw.WriteLine(new string('-', 50));
                int kRank = 1;
                foreach (var p in todosJugadores.OrderByDescending(x => x.Kills).Take(10))
                {
                    sw.WriteLine($"   #{kRank,-2} {p.Nombre,-20}: {p.Kills} kills.");
                    kRank++;
                }
            }

            Console.WriteLine($"🏆 TABLA GLOBAL GENERADA: {rutaGlobal}");
        }

        // Helper para cortar strings largos
        static string Shorten(string str, int length)
        {
            if (str == null) return "";
            if (str.Length <= length) return str;
            return str.Substring(0, length - 3) + "...";
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
                return Console.ReadLine()?.Trim('"').Trim() ?? string.Empty;
            }
            else
            {
                Console.WriteLine("\nCarpetas detectadas:");
                for (int i = 0; i < carpetasValidas.Count; i++)
                    Console.WriteLine($"[{i + 1}] {new DirectoryInfo(carpetasValidas[i]).Name}");

                Console.Write("Elige número (Enter para la primera): ");
                string? input = Console.ReadLine(); // Nullable string handled
                if (int.TryParse(input, out int sel) && sel > 0 && sel <= carpetasValidas.Count)
                    return carpetasValidas[sel - 1];
                return carpetasValidas[0];
            }
        }
    }
}