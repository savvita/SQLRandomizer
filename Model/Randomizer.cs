using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SQLRandomizer.Model
{
    internal static class Randomizer
    {
        private static List<string> firstNames = new List<string>() { "John", "Jack", "Vasiliy" };
        private static List<string> lastNames = new List<string>() { "Smith", "Doe", "Petrov" };
        private static List<string> countries = new List<string>() { "Ukraine", "Poland", "Canada" };
        private static List<string> cities = new List<string>() { "Kyiv", "Dnipro", "Toronto" };
        private static List<string> streets = new List<string>() { "prosp. Yavornitskogo", "prosp. Polya", "Main Street" };
        private static List<string> emails = new List<string>() { "username@gmail.com" };
        private static List<string> logins = new List<string>() { "username", "user" };
        private static List<string> passwords = new List<string>() { "qwerty", "11111", "q1w2e3r4" };
        private static List<string> phones = new List<string>() { "056-999-99-99" };
        private static List<string> cells = new List<string>() { "097-999-99-99" };

        private static Random random = new Random();

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

                    if(users == null || users.results == null)
                    {
                        return;
                    }

                    foreach (var user in users.results)
                    {
                        if (user.name != null)
                        {
                            if(user.name.first != null)
                            {
                                firstNames.Add(user.name.first);
                            }
                            if(user.name.last != null)
                            {
                                lastNames.Add(user.name.last);
                            }
                        }

                        if(user.location != null)
                        {
                            if (user.location.country != null)
                            {
                                countries.Add(user.location.country); 
                            }
                            if (user.location.city != null)
                            {
                                cities.Add(user.location.city); 
                            }
                        }

                        if (user.email != null)
                        {
                            emails.Add(user.email); 
                        }

                        if (user.phone != null)
                        {
                            phones.Add(user.phone);
                        }

                        if (user.cell != null)
                        {
                            cells.Add(user.cell);
                        }

                        if (user.login != null)
                        {
                            if (user.login.username != null)
                            {
                                logins.Add(user.login.username);  
                            }
                            if (user.login.password != null)
                            {
                                passwords.Add(user.login.password); 
                            }
                        }
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

        public static string? GetRandomLogin(int maxLength, double nullPercentage = 0)
        {
            return GetRandomString(logins, maxLength, nullPercentage);
        }

        public static string? GetRandomPassword(int maxLength, double nullPercentage = 0)
        {
            return GetRandomString(passwords, maxLength, nullPercentage);
        }

        public static string? GetRandomEmail(int maxLength, double nullPercentage = 0)
        {
            return GetRandomString(emails, maxLength, nullPercentage);
        }

        public static string? GetRandomPhone(int maxLength, double nullPercentage = 0)
        {
            return GetRandomString(phones, maxLength, nullPercentage);
        }

        public static string? GetRandomCell(int maxLength, double nullPercentage = 0)
        {
            return GetRandomString(cells, maxLength, nullPercentage);
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
