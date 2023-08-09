using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RavMosheShapiro.data
{
    public class FileUpload
    {
        public string Name { get; set; }
        public string Base64 { get; set; }
    }
}
