using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavMosheShapiro.data
{
    public class ShiurSearchRepository
    {
        private readonly string _connectionString;
        public ShiurSearchRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public string GetExactSearchPhrase(string searchPhrase)
        {
            return $"\"{searchPhrase}\"";
        }

        public string GetAllWordsSearchPhrase(string searchPhrase, double space)
        {
            List<string> words = searchPhrase.Split(' ').ToList();
            string phrase = "\"";
            foreach (var w in words)
            {
                phrase = phrase + $"+{w}";
            }
            phrase = phrase + $"\" @{space * words.Count}";
            return phrase;
        }


        public SearchedShiur GetShiurFromReader(MySqlDataReader reader)
        {
            return new SearchedShiur
            {
                Id = (int)reader["ShiurId"],
                ShiurContent = (string)reader["ShiurContent"],
                TitleCalc = (string)reader["TitleCalc"],
                DateJewishEnglishCombined = (string)reader["DateJewishEnglishCombined"],
                ParshahYearHebreInHebrew = (string)reader["ParshahYearHebreInHebrew"],
                Volume = (int)reader["Volume"],
                Issue = (int)reader["Issue"],
                Version = (string)reader["Version"],
                ParshahEnglish = (string)reader["ParshahEnglish"],
                Year = (int)reader["YearJewishEnglish"]
            };
        }

public List<SearchedShiur> SearchShiurimExact(string searchPhrase, MySqlConnection connection)
        {
            List<SearchedShiur> exactMatches = new List<SearchedShiur>();
            string statement = "SELECT * FROM shiurimDivided where match (ShiurContent) against (@searchPhrase) order by ShiurId;";
            MySqlCommand command = new MySqlCommand(statement, connection);
            command.Parameters.AddWithValue("@searchPhrase", searchPhrase);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                exactMatches.Add(GetShiurFromReader(reader));
            }
            reader.Close();
            return exactMatches;
        }
        public List<SearchedShiur> SearchShiurimExact(string searchPhrase, MySqlConnection connection, string idList)
        {
            List<SearchedShiur> exactMatches = new List<SearchedShiur>();
            string statement = "SELECT * FROM shiurimDivided where shiurId in (@idList) and match (ShiurContent) against (@searchPhrase) order by shiurId;";
            MySqlCommand command = new MySqlCommand(statement, connection);
            command.Parameters.AddWithValue("@idList", idList);
            command.Parameters.AddWithValue("@searchPhrase", searchPhrase);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                exactMatches.Add(GetShiurFromReader(reader));
            }
            reader.Close();
            return exactMatches;
        }
        public List<SearchedShiur> SearchShiurimAllWords(string searchPhrase, MySqlConnection connection)
        {
            List<SearchedShiur> AllWords = new List<SearchedShiur>();
            string statement = "SELECT * FROM shiurimDivided where match (ShiurContent) against (@searchPhrase in boolean mode);";
            MySqlCommand command = new MySqlCommand(statement, connection);
            command.Parameters.AddWithValue("@searchPhrase", searchPhrase);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                AllWords.Add(GetShiurFromReader(reader));
            }
            reader.Close();
            return AllWords;
        }

        public List<SearchedShiur> SearchShiurimAllWords(string searchPhrase, MySqlConnection connection, string idList)
        {
            List<SearchedShiur> AllWords = new List<SearchedShiur>();
            string statement = "SELECT * FROM shiurimDivided where shiurId in (@idList) and match (ShiurContent) against (@searchPhrase in boolean mode);";
            MySqlCommand command = new MySqlCommand(statement, connection);
            command.Parameters.AddWithValue("@idList", idList);
            command.Parameters.AddWithValue("@searchPhrase", searchPhrase);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                AllWords.Add(GetShiurFromReader(reader));
            }
            reader.Close();
            return AllWords;
        }

        public List<SearchedShiur> SearchShiurimComplete(string searchPhrase, int maxSpace)
        {
            Dictionary<SearchedShiur, int> shiurimForSearch = new Dictionary<SearchedShiur, int>();
            var connection = new MySqlConnection(_connectionString);
            void AddShiurIfNew(SearchedShiur currentShiur)
            {
                if (!shiurimForSearch.ContainsValue(currentShiur.Id))
                {
                    shiurimForSearch.Add(new SearchedShiur
                    {
                        Id= currentShiur.Id,
                        ShiurContent=currentShiur.ShiurContent,
                        TitleCalc=currentShiur.TitleCalc,
                        DateJewishEnglishCombined=currentShiur.DateJewishEnglishCombined,
                        ParshahYearHebreInHebrew=currentShiur.ParshahYearHebreInHebrew,
                        Volume=currentShiur.Volume,
                        Issue=currentShiur.Issue,
                        Version=currentShiur.Version,
                        Year=currentShiur.Year,
                        ParshahEnglish=currentShiur.ParshahEnglish
                    }, currentShiur.Id);
                }
            }
            connection.Open();
            foreach (SearchedShiur currentShiur in SearchShiurimExact(GetExactSearchPhrase(searchPhrase), connection))
            {
                shiurimForSearch.Add(new SearchedShiur
                {
                    Id = currentShiur.Id,
                    ShiurContent = currentShiur.ShiurContent,
                    TitleCalc = currentShiur.TitleCalc,
                    DateJewishEnglishCombined = currentShiur.DateJewishEnglishCombined,
                    ParshahYearHebreInHebrew= currentShiur.ParshahYearHebreInHebrew,
                    Volume = currentShiur.Volume,
                    Issue = currentShiur.Issue,
                    Version=currentShiur.Version,
                    Year=currentShiur.Year,
                    ParshahEnglish=currentShiur.ParshahEnglish
                }, currentShiur.Id);
            }

            for (int i = 1; i < maxSpace; i++)
            {
                foreach (SearchedShiur shiur in SearchShiurimAllWords(GetAllWordsSearchPhrase(searchPhrase, i), connection))
                {
                    AddShiurIfNew(shiur);
                }
            }

            connection.Close();
            return shiurimForSearch.Keys.ToList();
        }

        public string SearchShiurimCompletePreliminary(string searchPhrase, int maxSpace)
        {
            List<int> shiurimForSearch = new List<int>();
            var connection = new MySqlConnection(_connectionString);
            void AddShiurIfNew(SearchedShiur currentShiur)
            {
                if (!shiurimForSearch.Contains(currentShiur.Id))
                {
                    shiurimForSearch.Add(currentShiur.Id);
                }
            }
            connection.Open();
            foreach (SearchedShiur currentShiur in SearchShiurimExact(GetExactSearchPhrase(searchPhrase), connection))
            {
                shiurimForSearch.Add(currentShiur.Id);
            }

            for (int i = 1; i < maxSpace; i++)
            {
                foreach (SearchedShiur shiur in SearchShiurimAllWords(GetAllWordsSearchPhrase(searchPhrase, i), connection))
                {
                    AddShiurIfNew(shiur);
                }
            }

            connection.Close();
            string idList = $"{shiurimForSearch[0]}";
            for (int i = 1; i < shiurimForSearch.Count; i++)
            {
                idList += $", {shiurimForSearch[i]}";
            }
            return idList;
        }


    }
}
