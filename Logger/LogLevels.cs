using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLogger
{
    public static class LogLevels
    {
        public static string Info = "Info"; //všeobecne užitočné informácie (service sa spustil, aplikácia sa spustila, ... )        
        public static string Debug = "Debug"; //diagnosticky dôležité informácie
        public static string Warning = "Warning"; //čokoľvek čo môže spôsobiť chybu aplikácie, ktorá sa ale z toho dokáže spamätať
        public static string Error = "Error"; //chyba, ktorá je fatálna pre aktuálnu operáciu, ale nespôsobí pád aplikácie
        public static string Fatal = "Fatal"; //nazávažnejšie chyby, ktoré spôsobia pád aplikácie alebo servicu 
        public static string Exception = "Exception"; //výnimky
    }
}
