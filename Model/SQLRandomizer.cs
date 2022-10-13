using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SQLRandomizer.Model
{
    internal class SQLRandomizer
    {

        public SQLRandomizer()
        {
        }

        public string GetValues(string sqlQuery, int count)
        {
            if (count <= 0)
            {
                return "";
            }
            Regex reg = new Regex(@"\s+");
            sqlQuery = reg.Replace(sqlQuery.ToLower(), " ");

            string[] tables = GetTableQueries(sqlQuery);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < tables.Length; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    sb.Append(GetTableInsert(tables[i]) + "\n");
                }
            }

            return sb.ToString();
        }


        private string[] GetTableQueries(string sqlQuery)
        {
            return sqlQuery.Split("create table", StringSplitOptions.RemoveEmptyEntries);
        }

        private string GetTableInsert(string sqlQuery)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"insert into {GetTableName(sqlQuery)} (");

            Dictionary<string, string> columns = GetColumns(sqlQuery);

            foreach (var tuple in columns)
            {
                sb.Append(tuple.Key + ",");
            }

            sb.Remove(sb.Length - 1, 1);

            sb.Append(") values (");

            foreach (var tuple in columns)
            {
                sb.Append((GetColumnValue(tuple.Key, tuple.Value, 0) ?? "null") + ",");
            }

            sb.Remove(sb.Length - 1, 1);

            sb.Append(");");

            return sb.ToString();
        }

        private string GetTableName(string sqlQuery)
        {
            return sqlQuery.Substring(0, sqlQuery.IndexOf('(')).Trim();
        }

        private Dictionary<string, string> GetColumns(string sqlQuery)
        {
            Dictionary<string, string> columns = new Dictionary<string, string>();
            int startIdx = sqlQuery.IndexOf('(');
            string[] columns2 = sqlQuery.Substring(startIdx + 1, sqlQuery.LastIndexOf(')') - startIdx).Trim().Split(',', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < columns2.Length; i++)
            {
                if (columns2[i].Contains("identity"))
                {
                    continue;
                }
                string[] cols = columns2[i].Trim().Split(' ');
                columns.Add(cols[0], cols[1]);
            }

            return columns;
        }

        private string? GetColumnValue(string columnName, string columnType, double nullPercentage)
        {
            string? str = string.Empty;

            if (columnType.Contains("char"))
            {
                str = $"\'{GetRandomStringColumn(columnName, columnType, nullPercentage)}\'";
            }
            else
            {
                str = $"{GetRandomNumberColumn(columnName, columnType, nullPercentage)}";
            }

            return str;
        }

        private double? GetRandomNumberColumn(string columnName, string columnType, double nullPercentage)
        {
            if (columnType.Contains("float") || columnType.Contains("double"))
            {
                return Randomizer.GetRandomNumber(0, float.MaxValue, 2, nullPercentage);
            }

            if (columnName.Contains("age"))
            {
                return Randomizer.GetRandomNumber(0, 100, 0, nullPercentage);
            }

            return Randomizer.GetRandomNumber(0, int.MaxValue - 1, 0, nullPercentage);
        }

        private string? GetRandomStringColumn(string columnName, string columnType, double nullPercentage)
        {
            string? str = string.Empty;
            int startIdx = columnType.IndexOf("(");
            int.TryParse(columnType.Substring(startIdx + 1, columnType.LastIndexOf(")") - startIdx - 1), out int length);

            if (columnName.Contains("name"))
            {
                if (columnName.Contains("first"))
                {
                    str = Randomizer.GetRandomFirstName(length, nullPercentage);
                }

                else if (columnName.Contains("last"))
                {
                    str = Randomizer.GetRandomLastName(length, nullPercentage);
                }

                else
                {
                    str = Randomizer.GetRandomFullName(length, nullPercentage);
                }
            }

            else if (columnName.Contains("country"))
            {
                str = Randomizer.GetRandomCountry(length, nullPercentage);
            }

            else if (columnName.Contains("city"))
            {
                str = Randomizer.GetRandomCity(length, nullPercentage);
            }

            else if (columnName.Contains("street"))
            {
                str = Randomizer.GetRandomStreet(length, nullPercentage);
            }

            else
            {
                str = Randomizer.GetRandomString(length, nullPercentage);
            }

            return str;
        }
    }
}
