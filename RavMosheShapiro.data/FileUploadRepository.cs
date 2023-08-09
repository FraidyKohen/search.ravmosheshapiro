using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RavMosheShapiro.data
{
    public class FileUploadRepository
    {
        private readonly string _connectionString;
        public FileUploadRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetShiurTextFromFile(string fileName)
        {
            string path = Path.GetFullPath("Uploads/");
            Stream stream = File.OpenRead(path + fileName);
            using (WordDocument document = new WordDocument(stream, FormatType.Automatic))
            {
                return document.GetText();
            }
        }
        public List<string> GetLinesFromText(string rawShiur)
        {
            List<string> lines = new List<string>();
            string shiurContent = "";
            string line = "";
            for (int i = 0; i < rawShiur.Length; i++)
            {
                if (rawShiur[i] != '\n')
                {
                    line = line + rawShiur[i];
                }
                else
                {
                    if (line.Contains("*** START OF THIS SHIUR ***"))
                    {
                        shiurContent = rawShiur.Substring(i, rawShiur.Length - i - 210);
                        lines.Add(shiurContent);
                        break;
                    }
                    lines.Add(line);
                    line = "";
                }
            }
            return lines;
        }

        public string GetPartFromString(string line)
        {
            var parts = line.Split(':');
            if (parts.Length < 2)
            {
                return default;
            }
            if (parts.Length > 2)
            {
                string part = "";
                for (int i = 1; i < parts.Length; i++)
                {
                    part = part + parts[i];
                }
                return part;
            }
            return parts[1];
        }
        public Shiur GetShiurFromLines(List<string> lines)
        {
            Shiur shiur = new Shiur();
            shiur.DateJewishEnglishCombined = GetPartFromString(lines[0]);
            shiur.DateJewishHebrewCombined = GetPartFromString(lines[1]);
            shiur.DateSecular = TryDateParse(GetPartFromString(lines[2]));
            shiur.Issue = TryParse(GetPartFromString(lines[3]));
            shiur.IssueHebrew = GetPartFromString(lines[4]);
            shiur.MonthJewishEnglish = GetPartFromString(lines[5]);
            shiur.MonthJewishHebrew = GetPartFromString(lines[6]);
            shiur.ParshahCalc = GetPartFromString(lines[7]);
            shiur.ParshahEnglish = GetPartFromString(lines[8]);
            shiur.ParshahYearHebreInHebrew = GetPartFromString(lines[9]);
            shiur.Recording = GetPartFromString(lines[10]);
            shiur.SeferName = GetPartFromString(lines[11]);
            shiur.SeferOrder = TryParse(GetPartFromString(lines[12]));
            shiur.SeferOrderHebrew = GetPartFromString(lines[13]);
            shiur.TitleCalc = GetPartFromString(lines[14]);
            shiur.TitleEnglish = GetPartFromString(lines[15]);
            shiur.TitleHebrew = GetPartFromString(lines[16]);
            shiur.Topic = GetPartFromString(lines[17]);
            shiur.Type = GetPartFromString(lines[18]);
            shiur.Version = GetPartFromString(lines[19]);
            shiur.Volume = TryParse(GetPartFromString(lines[20]));
            shiur.VolumeIssue = TryParse(GetPartFromString(lines[21]));
            shiur.YearJewishEnglish = TryParse(GetPartFromString(lines[22]));
            shiur.YearJewishHebrew = GetPartFromString(lines[23]);
            if (lines[25].Length > 8)
            {
                shiur.ShiurContent = (lines[25]);
                return shiur;
            }
            if (lines[26].Length > 8)
            {
                shiur.ShiurContent = (lines[26]);
                return shiur;
            }
            if (lines[27].Length > 8)
            {
                shiur.ShiurContent = (lines[27]);
                return shiur;
            }
            if (lines[28].Length > 8)
            {
                shiur.ShiurContent = (lines[28]);
                return shiur;
            }
            return shiur;
        }

        public int AddShiur(Shiur shiur)
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            string statement = "insert into shiurim (DateJewishEnglishCombined, DateJewishHebrewCombined, DateSecular, Issue, IssueHebrew, MonthJewishEnglish, MonthJewishHebrew, ParshahCalc, ParshahEnglish, ParshahYearHebreInHebrew, Recording, SeferName, SeferOrder, SeferOrderHebrew, TitleCalc, TitleEnglish, TitleHebrew, Topic, Type, version, volume, VolumeIssue, YearJewishEnglish, YearJewishHebrew, ShiurContent) " +
                " Values (@DateJewishEnglishCombined, @DateJewishHebrewCombined, @DateSecular, @Issue, @IssueHebrew, @MonthJewishEnglish, @MonthJewishHebrew, @ParshahCalc, @ParshahEnglish, @ParshahYearHebreInHebrew, @Recording, @SeferName, SeferOrder, SeferOrderHebrew, @TitleCalc, @TitleEnglish, @TitleHebrew, @Topic, @Type, @Version, @Volume, @VolumeIssue, @YearJewishEnglish, @YearJewishHebrew, @ShiurContent);";
            var command = new MySqlCommand(statement, connection);
            command.Parameters.AddWithValue("@DateJewishEnglishCombined", shiur.DateJewishEnglishCombined);
            command.Parameters.AddWithValue("@DateJewishHebrewCombined", shiur.DateJewishHebrewCombined);
            command.Parameters.AddWithValue("@DateSecular", shiur.DateSecular);
            command.Parameters.AddWithValue("@Issue", shiur.Issue);
            command.Parameters.AddWithValue("@IssueHebrew", shiur.IssueHebrew);
            command.Parameters.AddWithValue("@MonthJewishEnglish", shiur.MonthJewishEnglish);
            command.Parameters.AddWithValue("@MonthJewishHebrew", shiur.MonthJewishHebrew);
            command.Parameters.AddWithValue("@ParshahCalc", shiur.ParshahCalc);
            command.Parameters.AddWithValue("@ParshahEnglish", shiur.ParshahEnglish);
            command.Parameters.AddWithValue("@ParshahYearHebreInHebrew", shiur.ParshahYearHebreInHebrew);
            command.Parameters.AddWithValue("@Recording", shiur.Recording);
            command.Parameters.AddWithValue("@SeferName", shiur.SeferName);
            command.Parameters.AddWithValue("@SeferOrder", shiur.SeferOrder);
            command.Parameters.AddWithValue("@TitleCalc", shiur.TitleCalc);
            command.Parameters.AddWithValue("@TitleEnglish", shiur.TitleEnglish);
            command.Parameters.AddWithValue("@TitleHebrew", shiur.TitleHebrew);
            command.Parameters.AddWithValue("@Topic", shiur.Topic);
            command.Parameters.AddWithValue("@Type", shiur.Type);
            command.Parameters.AddWithValue("@Version", shiur.Version);
            command.Parameters.AddWithValue("@Volume", shiur.Volume);
            command.Parameters.AddWithValue("@VolumeIssue", shiur.VolumeIssue);
            command.Parameters.AddWithValue("@YearJewishEnglish", shiur.YearJewishEnglish);
            command.Parameters.AddWithValue("@YearJewishHebrew", shiur.YearJewishHebrew);
            command.Parameters.AddWithValue("@ShiurContent", shiur.ShiurContent);
            command.ExecuteNonQuery();
            var statement2 = "Select * from shiurim order by (shiurId) desc limit 1";
            var command2 = new MySqlCommand(statement2, connection);
            var reader = command2.ExecuteReader();
            List<SearchedShiur> lastShiur = new List<SearchedShiur>();
            while (reader.Read())
            {
                lastShiur.Add(new SearchedShiur
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
                });
            }
connection.Close();
            int lastShiurId = lastShiur[0].ShiurId;
            return lastShiurId;
        }

        public int TryParse(string s)
        {
            try
            {
                int x = int.Parse(s);
                return x;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public DateTime TryDateParse(string s)
        {
            try
            {
                DateTime date = DateTime.Parse(s);
                return date;
            }
            catch (Exception)
            {
                return DateTime.Parse("1/1/1111");
            }
        }

        public List<SearchedShiur> DivideShiur(SearchedShiur originalShiur)
        {
            List<SearchedShiur> dividedShiur = new List<SearchedShiur>();
            string paragraph = "";
            int index = 0;
            for (int i = 0; i < originalShiur.ShiurContent.Length; i++)
            {
                if (originalShiur.ShiurContent[i] != '\n')
                {
                    paragraph = paragraph + originalShiur.ShiurContent[i];
                }
                else
                {
                    index++;
                    if (index == 10)
                    {
                        dividedShiur.Add(new SearchedShiur
                        {
                            ShiurId = originalShiur.ShiurId,
                            DateJewishEnglishCombined = originalShiur.DateJewishEnglishCombined,
                            Issue = originalShiur.Issue,
                            ParshahEnglish = originalShiur.ParshahEnglish,
                            ParshahYearHebreInHebrew = originalShiur.ParshahYearHebreInHebrew,
                            TitleCalc = originalShiur.TitleCalc,
                            Version = originalShiur.Version,
                            Volume = originalShiur.Volume,
                            Year = originalShiur.Year,
                            ShiurContent = paragraph
                        });
                        index = 0;
                        paragraph = "";

                    }
                }
            }
            return dividedShiur;
        }

        public void AddShiurParagraphs(SearchedShiur shiur)
        {
            List<SearchedShiur> shiurParagraphs = DivideShiur(shiur);
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            foreach (var s in shiurParagraphs)
            {
                string statement = "insert into shiurimDivided (ShiurId, DateJewishEnglishCombined, Issue, ParshahEnglish, ParshahYearHebreInHebrew, TitleCalc, version, volume, YearJewishEnglish, ShiurContent) " +
            " Values (@ShiurId, @DateJewishEnglishCombined, @Issue, @ParshahEnglish, @ParshahYearHebreInHebrew, @TitleCalc, @version, @volume, @YearJewishEnglish, @ShiurContent)";
                var command = new MySqlCommand(statement, connection);
                command.Parameters.AddWithValue("@ShiurId", shiur.ShiurId);
                command.Parameters.AddWithValue("@DateJewishEnglishCombined", shiur.DateJewishEnglishCombined);
                command.Parameters.AddWithValue("@Issue", shiur.Issue);
                command.Parameters.AddWithValue("@ParshahEnglish", shiur.ParshahEnglish);
                command.Parameters.AddWithValue("@ParshahYearHebreInHebrew", shiur.ParshahYearHebreInHebrew);
                command.Parameters.AddWithValue("@TitleCalc", shiur.TitleCalc);
                command.Parameters.AddWithValue("@Version", shiur.Version);
                command.Parameters.AddWithValue("@Volume", shiur.Volume);
                command.Parameters.AddWithValue("@YearJewishEnglish", shiur.Year);
                command.Parameters.AddWithValue("@ShiurContent", shiur.ShiurContent);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            connection.Close();
        }
        public SearchedShiur Upload(string fileName)
        {
            Shiur s = GetShiurFromLines(GetLinesFromText(GetShiurTextFromFile(fileName)));
            int shiurId = AddShiur(s);
            SearchedShiur searchedShiur = new SearchedShiur
            {
                ShiurId = shiurId,
                TitleCalc = s.TitleCalc,
                ParshahEnglish = s.ParshahEnglish,
                ParshahYearHebreInHebrew = s.ParshahYearHebreInHebrew,
                DateJewishEnglishCombined = s.DateJewishEnglishCombined,
                Year = s.YearJewishEnglish,
                Version = s.Version,
                Volume = s.Volume,
                Issue = s.Issue,
                ShiurContent = s.ShiurContent
            };
            return searchedShiur;
        }


    }
}
