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
            string payInADay = Console.ReadLine();
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
                    result = result && CalculateTotalPayInMonth(totalDaysInAMonth, hoursInAday, payInADay, out totalPayInAMotnh, out message);
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






        private static bool CalculateTotalPayInMonth(Giornata totalDaysInAMonth, string hoursInAday, string payInADay, out double totalPayInAMotnh, out string message)
        {
            bool result = true;
            message = null;
            totalPayInAMotnh = 0;
            double payInAdayParsed = 0;
            double minInHours = 0;
            double hoursInAdayParsed = 0;
            double totMoneyinMin = 0;

            try
            {

                if (totalDaysInAMonth != null)
                {
                    if (payInADay != null || payInADay != string.Empty)
                    {
                        payInAdayParsed = double.Parse(payInADay);
                        totalPayInAMotnh = totalDaysInAMonth.giornata * payInAdayParsed;

                        if (totalDaysInAMonth.ore != 0)
                        {
                            minInHours = totalDaysInAMonth.ore * 60;
                        }
                        if (totalDaysInAMonth.minuti != 0)
                        {
                            minInHours = minInHours + totalDaysInAMonth.minuti;
                        }

                        hoursInAdayParsed = double.Parse(hoursInAday);

                        double min = hoursInAdayParsed * 60;
                        totMoneyinMin = payInAdayParsed / min;
                        double tot = totMoneyinMin * minInHours;

                        totalPayInAMotnh = totalPayInAMotnh + tot;


                        if (result)
                        {
                            message = string.Format("La tua paga in questo mese è {0} euro.", totalPayInAMotnh.ToString());
                        }
                    }
                    else
                    {
                        message = "Non c'è il prezzo di una giornata";
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
            double hoursParsed, hoursInAdayParsed = 0;
            double hours = 0;
            double hoursInADays = 0;
            double rest = 0;
            double days = 0;
            double minutes = 0;
            string totalHours = null;
            string totMin = null;
            string totalHoursInADay = null;
            string totMinInADay = null;
            double minutesInADay = 0;



            try
            {

                if (totalHoursInAMoth != null)
                {
                    result = result && CalculateHoursAndMinutes(totalHoursInAMoth, out totalHours, out totMin);
                    hours = totalHours != null && double.TryParse(totalHours, out hoursParsed) ? hours = hoursParsed : 0;
                    result = result && CalculateMinutes(totMin, out minutes);

                    result = result && CalculateHoursAndMinutes(hoursInAday, out totalHoursInADay, out totMinInADay);
                    hoursInADays = totalHoursInADay != null && double.TryParse(totalHoursInADay, out hoursInAdayParsed) ? hoursInADays = hoursInAdayParsed : 0;
                    result = result && CalculateMinutes(totMinInADay, out minutesInADay);
                    hoursInADays = (hoursInADays + minutesInADay);


                    if (hours != 0 && hours >= hoursInADays)
                    {
                        rest = hours % hoursInADays;
                        if (rest > 0)
                        {
                            days = hours - rest;
                            days = days / hoursInADays;
                        }
                        else
                        {
                            days = days / hoursInADays;
                        }
                        rest = rest + minutes;
                        string stringRest = rest.ToString();
                        double doublerest = 0;
                        if (double.TryParse(stringRest.Replace('.', ','), out doublerest))
                        {
                            result = result && CalculateHoursAndMinutes(stringRest.Replace('.', ','), out totalHours, out totMin);
                            rest = totalHours != null ? double.Parse(totalHours) : 0;
                            doublerest = totMin != null ? double.Parse(totMin) : 0;
                            double totrest = 0;
                            double restRest = 0;
                            totrest = rest + totrest;
                            if (totrest > hoursInADays)
                            {
                                totrest = totrest % hoursInADays;
                                restRest = totrest - hoursInADays;

                                days = days + totrest;
                                rest = restRest;
                            }
                        }
                        if (result)
                        {
                            totalDaysInAMonth = new Giornata();
                            totalDaysInAMonth.giornata = days;
                            totalDaysInAMonth.ore = rest;
                            totalDaysInAMonth.minuti = doublerest;

                        }
                        message = string.Format("Questo mese hai lavorato {0} giornate {1} ore e {2} minuti", days, rest, doublerest);

                    }
                    else
                    {
                        message = "è stato riscontrato un problema durante il calcolo delle giorante";
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name}: {message} is outside the bounds of the array");
            }
            return result;
        }

        private static bool CalculateHoursAndMinutes(string totalHoursInAMoth, out string totalHours, out string totMin)
        {
            bool result = true;
            totalHours = null;
            totMin = null;
            try
            {
                string[] arryTotHours = totalHoursInAMoth != null ? totalHoursInAMoth.Split(',') : null;

                totalHours = arryTotHours != null && arryTotHours[0] != null ? arryTotHours[0] : null;
                if (arryTotHours.Length > 1)
                {
                    totMin = arryTotHours[1].Length >= 1 ? arryTotHours[1] : null;
                }
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
            return result;

        }

        private static bool CalculateMinutes(string totMin, out double minutes)
        {
            bool result = true;
            minutes = 0;
            double minParsed = 0;
            try
            {
                double var = totMin != null && double.TryParse(totMin, out minParsed) ? minutes = minParsed : 0;
                if (var != 0)
                {

                    if (var >= 0 && var <= 15)
                    {
                        minutes = var + 10;
                    }
                    else if (var > 15 && var <= 30)
                    {
                        minutes = var + 20;
                    }
                    else if (var > 30 && var <= 45)
                    {
                        minutes = var + 30;
                    }
                    else if (var > 45 && var <= 60)
                    {
                        minutes = var + 40;
                    }
                    else
                    {
                        result = false;
                    }
                    if (result)
                    {
                        minutes = minutes / 100;
                    }

                }

            }
            catch (Exception)
            {

                throw;
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
