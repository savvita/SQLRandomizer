using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SQLRandomizer.Model
{
    internal static class Randomizer
    {
        private static List<string> firstNames = new List<string>() { "John", "Jack", "Vasiliy" };
        private static List<string> lastNames = new List<string>() { "Smith", "Doe", "Petrov" };
        private static List<string> countries = new List<string>() { "Ukraine", "Poland", "Canada" };
        private static List<string> cities = new List<string>() { "Kyiv", "Dnipro", "Toronto" };
        private static List<string> streets = new List<string>() { "prosp. Yavornitskogo", "prosp. Polya", "Main Street" };
        private static Random random = new Random();

        static Randomizer()
        {
            GetRandomUsersValues();
        }

        private static string? GetRandomString(List<string> strings, int maxLength, double nullPercentage)
        {
            if (random.Next(100) / 100.0 < nullPercentage)
            {
                return null;
            }

            List<string> fitStrings = strings.FindAll(str => str.Length <= maxLength);

            if (fitStrings.Count == 0)
            {
                return null;
            }

            return fitStrings[random.Next(fitStrings.Count)];
        }

        public static void GetRandomUsersValues()
        {
            string url = "https://randomuser.me/api/?results=1000";
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(url);

                if(json != null)
                {
                    var users = JsonSerializer.Deserialize<Users>(json);
                    foreach (var user in users.results)
                    {
                        firstNames.Add(user.name.first);
                        lastNames.Add(user.name.last);
                        countries.Add(user.location.country);
                        cities.Add(user.location.city);
                    }
                }
            }

        }

        private static IEnumerable<IEnumerable<T>> CartesianProduct<T>(IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
            return sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) =>
                    from accseq in accumulator
                    from item in sequence
                    select accseq.Concat(new[] { item })
           );
        }

        public static string? GetRandomFirstName(int maxLength, double nullPercentage = 0)
        {
            return GetRandomString(firstNames, maxLength, nullPercentage);
        }

        public static string? GetRandomLastName(int maxLength, double nullPercentage = 0)
        {
            return GetRandomString(lastNames, maxLength, nullPercentage);
        }

        public static string? GetRandomFullName(int maxLength, double nullPercentage = 0)
        {
            if (random.Next(100) / 100.0 < nullPercentage)
            {
                return null;
            }

            var fullNames = CartesianProduct(new List<List<string>>() { firstNames, lastNames }).ToList();
            List<string> fitStrings = new List<string>();

            foreach (var str in fullNames)
            {
                var str2 = str.ToList<string>();
                string? fullName = str2?.Aggregate((item1, item2) => item1 + " " + item2);

                if (fullName?.Length <= maxLength)
                {
                    fitStrings.Add(fullName);
                }
            }

            if (fitStrings.Count == 0)
            {
                return null;
            }

            return fitStrings[random.Next(fitStrings.Count)];
        }

        public static string? GetRandomCountry(int maxLength, double nullPercentage = 0)
        {
            return GetRandomString(countries, maxLength, nullPercentage);
        }

        public static string? GetRandomCity(int maxLength, double nullPercentage = 0)
        {
            return GetRandomString(cities, maxLength, nullPercentage);
        }

        public static string? GetRandomStreet(int maxLength, double nullPercentage = 0)
        {
            return GetRandomString(streets, maxLength, nullPercentage);
        }

        public static string? GetRandomString(int maxLength, double nullPercentage = 0)
        {
            if (random.Next(100) / 100.0 < nullPercentage)
            {
                return null;
            }

            int length = random.Next(maxLength + 1);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                if (random.Next(10) < 5)
                {
                    sb.Append((char)(random.Next('a', 'z')));
                }
                else
                {
                    sb.Append((char)(random.Next('A', 'Z')));
                }
            }

            return sb.ToString();
        }

        public static double? GetRandomNumber(double min, double max, int decimals, double nullPercentage = 0)
        {
            if (random.Next(100) / 100.0 < nullPercentage)
            {
                return null;
            }

            decimals = decimals > 0 ? decimals : 0;

            return random.Next((int)(min * Math.Pow(10, decimals)), (int)((max + 1) * Math.Pow(10, decimals))) / Math.Pow(10, decimals);
        }
    }
}
