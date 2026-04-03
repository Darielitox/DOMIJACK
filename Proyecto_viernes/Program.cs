using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Win32.SafeHandles;

namespace Proyecto_viernes
{
    class Program
    {
        static string[] mazoCartas = new string[52];
        static int[] valoresCartas = new int[52];
        static int indiceMazo = 0;
        static string[] manoJugador = new string[10];
        static string[] manoDealer = new string[10];
        static int[] valoresManoJugador = new int[10];
        static int[] valoresManoDealer = new int[10];
        static int cartasJugador = 0;
        static int cartasDealer = 0;
        static void Main(String[] args)
        {
            CrearMazo();
            bool salida = false;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(@"
██████╗ ██╗███████╗███╗   ██╗██╗   ██╗███████╗███╗   ██╗██╗██████╗  ██████╗     █████╗ 
██╔══██╗██║██╔════╝████╗  ██║██║   ██║██╔════╝████╗  ██║██║██╔══██╗██╔═══██╗   ██╔══██╗
██████╔╝██║█████╗  ██╔██╗ ██║██║   ██║█████╗  ██╔██╗ ██║██║██║  ██║██║   ██║   ███████║
██╔══██╗██║██╔══╝  ██║╚██╗██║╚██╗ ██╔╝██╔══╝  ██║╚██╗██║██║██║  ██║██║   ██║   ██╔══██║
██████╔╝██║███████╗██║ ╚████║ ╚████╔╝ ███████╗██║ ╚████║██║██████╔╝╚██████╔╝   ██║  ██║
╚═════╝ ╚═╝╚══════╝╚═╝  ╚═══╝  ╚═══╝  ╚══════╝╚═╝  ╚═══╝╚═╝╚═════╝  ╚═════╝    ╚═╝  ╚═╝
");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(@"
        ██████╗   ██████╗ ███╗   ███╗██╗     ██╗ █████╗  ██████╗██╗  ██╗
        ██╔══ ██╗██╔═══██╗████╗ ████║██║     ██║██╔══██╗██╔════╝██║ ██╔╝
        ██║   ██║██║   ██║██╔████╔██║██║     ██║███████║██║     █████╔╝ 
        ██║   ██║██║   ██║██║╚██╔╝██║██║██   ██║██╔══██║██║     ██╔═██╗ 
        ██████╔╝╚ ██████╔╝██║ ╚═╝ ██║██║╚█████╔╝██║  ██║╚██████╗██║  ██╗
        ╚═════╝   ╚═════╝ ╚═╝     ╚═╝╚═╝ ╚════╝ ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝
            ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("                               EL MEJOR BLACKJACK DE RD                                 ");
                Console.WriteLine("========================================================================================");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" 1 - Nueva Partida");
                Console.WriteLine(" 2 - Tutorial");
                Console.WriteLine(" 3 - Salir");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("========================================================================================");
                Console.ResetColor();
                Console.Write("\nSelecciones una opcion: ");

                ConsoleKeyInfo opcion = Console.ReadKey();
                switch (opcion.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        JugarPartida();
                    break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                       MostrarTutorial();
                    break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        Console.WriteLine("Saliendo de DOMIJACK, vuelva pronto!!");
                        salida = true;
                    break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpción no válida. Intenta de nuevo.");
                        Console.ResetColor();
                        Thread.Sleep(500);
                    break;
                }
            } while (!salida);
        }
        static void CrearMazo()
        {
            string[] simbolos = {"♥", "♦", "♣", "♠"};
            string[] nombres = {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"}; 
            int contador = 0;
            for (int i = 0; i < simbolos.Length; i++) 
            {
                for (int j = 0; j < nombres.Length; j++) 
                {
                    mazoCartas[contador] = nombres[j] + simbolos[i]; 
                    if (j == 0)
                    {
                        valoresCartas[contador] = 11;
                    }
                    else if (j >= 9)
                    {
                        valoresCartas[contador] = 10;
                    }
                    else
                    {
                        valoresCartas[contador] = j + 1;
                    }
                    contador++; 
                }
            }
        }
        static void Barajar()
        {
            Random aleatorio = new Random();
            for (int i = mazoCartas.Length - 1; i > 0; i--) 
            {
                int indiceAleatorio = aleatorio.Next(i + 1);
                string temporalCarta = mazoCartas[i]; 
                mazoCartas[i] = mazoCartas[indiceAleatorio];
                mazoCartas[indiceAleatorio] = temporalCarta; 
                int temporalValor = valoresCartas[i];
                valoresCartas[i] = valoresCartas[indiceAleatorio];
                valoresCartas[indiceAleatorio] = temporalValor;
            }
        Console.WriteLine("\nSe barajo el mazo correctamente");
        }
        static void RepartirJugador()
        {
            manoJugador[cartasJugador] = mazoCartas[indiceMazo];
            valoresManoJugador[cartasJugador] = valoresCartas[indiceMazo];
            cartasJugador++;
            indiceMazo++;
        }
        static void RepartirDealer()
        {
            manoDealer[cartasDealer] = mazoCartas[indiceMazo];
            valoresManoDealer[cartasDealer] = valoresCartas[indiceMazo];
            cartasDealer++;
            indiceMazo++;
        }
        static int CalcularPuntaje(int[] valoresMano, int cartasCantidad)
        {
            int total = 0;
            int asesContador = 0;
            for (int i = 0; i < cartasCantidad; i++)
            {
                total += valoresMano[i];
                if (valoresMano[i] == 11)
                {
                    asesContador++;
                }
            }
            while (total > 21 && asesContador > 0)
            {
                total -= 10;
                asesContador--;
            }
            return total;
        }
        static void JugarPartida()
        {
            ReiniciarPartida(); 
            RepartirJugador();
            RepartirDealer();
            RepartirJugador();
            RepartirDealer();
            bool turnoJugador = true;
            while (turnoJugador)
            {
                MostrarMesa(false); 
                Console.WriteLine("\n¿Qué desea hacer? [P] Pedir | [Q] Quedarse");
                ConsoleKeyInfo Tecla = Console.ReadKey(true);
                if (Tecla.Key == ConsoleKey.P)
                {
                    RepartirJugador();
                    if (CalcularPuntaje(valoresManoJugador, cartasJugador) > 21) turnoJugador = false;
                }
                else if (Tecla.Key == ConsoleKey.Q)
                {
                    turnoJugador = false;
                }
            }
            Console.WriteLine("\nEl Dealer revela su carta oculta...");
            Thread.Sleep(1000);
            MostrarMesa(true); 
            TurnoDelDealer();    
            DeterminarGanador();
            Console.WriteLine("\nPresiona una tecla para volver al menú...");
            Console.ReadKey();
        }
        static void ImprimirCarta(string carta)
        {
            if (carta.Contains("♥") || carta.Contains("♦"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.Write($"[{carta}]");
            Console.ResetColor();
        }
        static void MostrarMesa(bool revelarTodo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"  _______________________________________________________________");
            Console.WriteLine(@" /                                                               \");
            Console.WriteLine(@"|          ┌──────────────────────────────────────────┐          |");
            Console.WriteLine(@"|          │             MESA DE DOMIJACK             │          |");
            Console.WriteLine(@"|          └──────────────────────────────────────────┘          |");
            Console.ResetColor();
            Console.Write("|     DEALER: ");
            for (int i = 0; i < cartasDealer; i++)
            {
                if (i == 1 && !revelarTodo)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[??] ");
                    Console.ResetColor();
                }
                else
                {
                    ImprimirCarta(manoDealer[i]);
                }
            }
            string puntosDealer;
            if (revelarTodo)
            {
                puntosDealer = CalcularPuntaje(valoresManoDealer, cartasDealer).ToString();
            }
            else
            {
                puntosDealer = "??";
            }
            Console.WriteLine($" (Puntos: {puntosDealer})".PadLeft(20));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"|                                                                |");
            Console.WriteLine(@"|    -------------------------------------------------------     |");
            Console.WriteLine(@"|                                                                |");
            Console.ResetColor();
            Console.Write("|     TU MANO: ");
            for (int i = 0; i < cartasJugador; i++) ImprimirCarta(manoJugador[i]);
            int puntosJugador = CalcularPuntaje(valoresManoJugador, cartasJugador);
            Console.WriteLine($" (Puntos: {puntosJugador})".PadLeft(18));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@" \_______________________________________________________________/ ");
            Console.ResetColor();
            Console.WriteLine();
        }
        static void TurnoDelDealer()
        {
            Console.WriteLine("\n--- EL DEALER REVELA SU JUGADA ---");
            Thread.Sleep(1000);
            MostrarMesa(true); 
            if (CalcularPuntaje(valoresManoJugador, cartasJugador) <= 21)
            {
                while (CalcularPuntaje(valoresManoDealer, cartasDealer) < 17)
                {
                    Console.WriteLine("\nEl Dealer pide otra carta...");
                    Thread.Sleep(1200); 
                    RepartirDealer();
                    MostrarMesa(true); 
                }
            }
        }
        static void DeterminarGanador()
        {
            int puntosJugador = CalcularPuntaje(valoresManoJugador, cartasJugador);
            int puntosDealer = CalcularPuntaje(valoresManoDealer, cartasDealer);

            Console.WriteLine("\n===========================");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"Puntos Jugador: {puntosJugador}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Puntos Dealer:  {puntosDealer}");
            Console.ResetColor();
            Console.WriteLine("===========================");
            if (puntosJugador == 21 && puntosDealer != 21)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("!DOMIJACK CÑ! Ganaste");
            }
            else if (puntosJugador > 21)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("!TE PASASTE! Gana la casa.");
            }
            else if (puntosDealer > 21 || puntosJugador > puntosDealer)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("!DOMIJACK! Has ganado la mano.");
            }
            else if (puntosJugador < puntosDealer)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El Dealer gana. Inténtalo de nuevo.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("!EMPATE!");
            }
            Console.ResetColor();
        }
        static void ReiniciarPartida()
        {
            indiceMazo = 0;
            for (int i = 0; i < 10; i++)
            {
                manoJugador[i] = "";       
                manoDealer[i] = "";        
                valoresManoJugador[i] = 0; 
                valoresManoDealer[i] = 0; 
            }
            cartasJugador = 0;
            cartasDealer = 0;
            Barajar();
            Console.WriteLine("\nBarajando de nuevo... ¡Mesa lista!");
            Thread.Sleep(1200);
        }
        static void MostrarTutorial()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("======= REGLAS DE DOMIJACK =======");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n1 - OBJETIVO");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("   - Derrotar al Dealer sumando 21 puntos o acercándote más que él.");
            Console.WriteLine("   - Si pasas de 21, pierdes automáticamente.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n2 - VALOR DE LAS FICHAS (CARTAS)");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("   - [2 al 10] : Valen su número numerico.");
            Console.WriteLine("   - [J, Q, K] : Valen 10 puntos cada una.");
            Console.WriteLine("   - [As (A)]  : Vale 11, pero si te pasas de 21, cambia a 1.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n3 - REGLAS DE LA CASA (DEALER)");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("   El Dealer juega bajo un algoritmo estricto:");
            Console.WriteLine("   - Si tiene 16 puntos o menos: ESTÁ OBLIGADO A PEDIR.");
            Console.WriteLine("   - Si tiene 17 puntos o más: SE PLANTA OBLIGATORIAMENTE.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n4 - CONTROLES DEL JUEGO");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("   - [P] PEDIR    -> Recibes una carta");
            Console.WriteLine("   - [Q] QUEDARSE -> Te quedas con tus puntos y dejas jugar al Dealer.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n5 - EL MISTERIO (LA CARTA OCULTA)");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("   - El Dealer siempre tendrá una carta oculta [??] mientras tú juegas.");
            Console.WriteLine("   - Solo la revelará cuando tú decidas 'Quedarte' o pases de 21 puntos.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("Presione cualquier tecla para volver al menu");
            Console.ReadKey(true);
        }
    }
}


