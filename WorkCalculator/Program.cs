using System;
using System.Diagnostics;
using System.Linq;

namespace WorkCalculator
{
    class Program
    {

        static void Main(string[] args)
        {

            bool result = true;
            string message = null;

            message = "Inserisci il numero totale delle ore che hai lavorato questo mese.";
            Console.WriteLine(message);
            string totalHoursInAMoth = Console.ReadLine();
            message = null;


            message = "Inserisci da quante ore è composta una tua giornata..";
            Console.WriteLine(message);
            string hoursInAday = Console.ReadLine();
            message = null;

            message = string.Format("Inserisci quanato costa una giornata lavorativa di {0} ore..", hoursInAday);
            Console.WriteLine(message);
            double payInADay = double.Parse(Console.ReadLine()); //da aggiungere il tryParse 
            message = null;


            Giornata totalDaysInAMonth = null;
            double totalPayInAMotnh = 0;

            try
            {
                //prende tutte le ore di un mese
                result = result && CalculateTotalDaysInMonth(totalHoursInAMoth, hoursInAday, out totalDaysInAMonth, out message);
                Console.WriteLine(message);

                if (result)
                {
                    message = null;
                    result = result && CalculateTotalPayInMonth(totalHoursInAMoth, hoursInAday, payInADay, out totalPayInAMotnh, out message);
                }

                Console.WriteLine(message);



            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name}: {message} is outside the bounds of the array");
            }
            finally
            {
                Console.ReadKey();
            }

        }

        private static bool CalculateTotalPayInMonth(string totalHoursInAMoth, string hoursInAday, double payInADay, out double totalPayInAMotnh, out string message)
        {
            bool result = true;
            message = null;
            totalPayInAMotnh = 0;

            try
            {

                if (totalHoursInAMoth != null)
                {
                    double minInMonth = 0;
                    if (CalculateMinutesInHours(totalHoursInAMoth, out minInMonth, out message))
                    {
                        double minInDay = 0;
                        if (minInMonth != 0 && CalculateMinutesInHours(hoursInAday, out minInDay, out message))
                        {
                            double payInMin = payInADay / minInDay; // pagamento al minuto
                            totalPayInAMotnh = minInMonth * payInMin;
                        }
                        else { result = false; }

                    }
                    if (result)
                    {
                        message = string.Format("lo stipendio calcolato per questo mese è di {0} euro", totalPayInAMotnh.ToString().Substring(0, 7));
                    }

                }
                else
                {
                    message = "Non ci sono giornate da calcolare.";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name}: {message} is outside the bounds of the array");
            }
            return result;
        }

        private static bool CalculateTotalDaysInMonth(string totalHoursInAMoth, string hoursInAday, out Giornata totalDaysInAMonth, out string message)
        {
            bool result = true;
            message = null;
            totalDaysInAMonth = null;
            try
            {

                if (totalHoursInAMoth != null)
                {
                    //calcolo i minuti al mese
                    double minutesInMonth = 0;
                    result = result && CalculateMinutesInHours(totalHoursInAMoth, out minutesInMonth, out message);
                    //calcolo i minuti alla giornata
                    double minutesInHoursDay = 0;
                    result = result && CalculateMinutesInHours(hoursInAday, out minutesInHoursDay, out message);
                    if (result && minutesInHoursDay != 0)
                    {
                        double resultDays = minutesInMonth / minutesInHoursDay;

                        string resultDaysString = resultDays.ToString();
                        string[] arryResultDaysString = resultDaysString != null ? resultDaysString.Split(',', '.') : null;

                        if (arryResultDaysString[0].Length > 0)
                        {
                            double day = double.Parse(arryResultDaysString[0]);
                            totalDaysInAMonth = new Giornata();
                            if (arryResultDaysString[1].Length > 1)
                            {
                                double hours = 0;
                                double minutes = 0;
                                if (CalculateMinuteMinutesAndHours(arryResultDaysString[1], minutesInHoursDay, out hours, out minutes, out message))
                                {
                                    totalDaysInAMonth.ore = hours;
                                    totalDaysInAMonth.minuti = minutes;
                                }
                            }
                            totalDaysInAMonth.giornata = day;

                            message = string.Format("Questo mese hai lavorato {0} giornate {1} ore e {2} minuti(arrotondati)", totalDaysInAMonth.giornata, totalDaysInAMonth.ore, totalDaysInAMonth.minuti);
                        }
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name}: {message} is outside the bounds of the array");
            }
            return result;
        }

        private static bool CalculateMinuteMinutesAndHours(string dayRest, double minutesInHoursDay, out double hours, out double min, out string message)
        {
            message = null;
            hours = 0;
            min = 0;
            try
            {
                if (dayRest != null)
                {
                    //ore restanti
                    dayRest = string.Format("0.{0}", dayRest);
                    double hour = double.Parse(dayRest);
                    double hourRes = hour * minutesInHoursDay;
                    hourRes = hourRes / 60;

                    string[] hourResArray = hourRes.ToString().Split(',', '.');
                    hours = double.Parse(hourResArray[0]);

                    if (hourResArray[1].Length > 1)
                    {
                        min = double.Parse(hourResArray[1].Substring(0, 2));
                    }
                    //minuti restati 
                }

            }
            catch (Exception ex)
            {
                message = string.Format("{0}", ex.Message);
            }
            return true;
        }

        private static bool CalculateMinutesInHours(string totalHoursInAMoth, out double totMinInMonth, out string message)
        {
            bool result = true;
            message = null;
            double minInAMonth = 0;
            totMinInMonth = 0;
            try
            {
                string[] arryTotHours = totalHoursInAMoth != null ? totalHoursInAMoth.Split(',') : null;
                if (arryTotHours[0].Length > 0)
                {
                    double hoursInAMoth = double.Parse(arryTotHours[0]);
                    hoursInAMoth = hoursInAMoth * 60;
                    if (arryTotHours.Length > 1)
                    {
                        minInAMonth = double.Parse(arryTotHours[1]);
                    }
                    totMinInMonth = hoursInAMoth + minInAMonth;
                }
                else
                {
                    message = "Non sono state inserite ore.";
                }
            }
            catch (Exception ex)
            {

                message = string.Format("{0}", ex.Message);
            }
            return result;

        }

        protected class Message
        {
            string type = "";
            string text = "";
            public Message(string type, string text)
            {
                this.type = type;
                this.text = text;
            }
        }

        protected class Giornata
        {
            public double giornata { get; set; }
            public double ore { get; set; }
            public double minuti { get; set; }
        }
    }
}
