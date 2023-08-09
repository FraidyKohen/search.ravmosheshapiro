using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavMosheShapiro.data
{
    public class Shiur
    {
        public int Id { get; set; }
        public string DateJewishEnglishCombined { get; set; }
        public string DateJewishHebrewCombined { get; set; }
        public DateTime DateSecular { get; set; }
        public int Issue { get; set; }
        public string IssueHebrew { get; set; }
        public string MonthJewishEnglish { get; set; }
        public string MonthJewishHebrew { get; set; }
        public string ParshahCalc { get; set; }
        public string ParshahEnglish { get; set; }
        public string ParshahYearHebreInHebrew { get; set; }
        public string Recording { get; set; }
        public string SeferName { get; set; }
        public int SeferOrder { get; set; }
        public string SeferOrderHebrew { get; set; }
        public string TitleCalc { get; set; }
        public string TitleEnglish { get; set; }
        public string TitleHebrew { get; set; }
        public string Topic { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
        public int Volume { get; set; }
        public int VolumeIssue { get; set; }
        public int YearJewishEnglish { get; set; }
        public string YearJewishHebrew { get; set; }
        public string ShiurContent { get; set; }

    }
}
