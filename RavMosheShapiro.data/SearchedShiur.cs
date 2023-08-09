using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavMosheShapiro.data
{
    public class SearchedShiur
    {
        public int Id { get; set; }
        public int ShiurId { get; set; }
        public string Version { get; set; }
        public string ShiurContent { get; set; }
        public string TitleCalc { get; set; }
        public string ParshahEnglish { get; set; }
        public int Year { get; set; }
        public string DateJewishEnglishCombined { get; set; }
        public string ParshahYearHebreInHebrew { get; set; }
        public int Volume { get; set; }
        public int Issue { get; set; }
        public string WordsToDisplay { get; set; }

    }
}
