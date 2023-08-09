using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RavMosheShapiro.data;

namespace RavMosheShapiro.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Search : ControllerBase
    {
        private readonly string _connectionString = @"server=localhost;userid=root;password=MySQL2944;database=shiurim";

        [HttpGet]
        [Route("SearchShiurim/{parameters}")]
        public List<SearchedShiur> SearchShiurim(string parameters)
        {
            var repo = new ShiurSearchRepository(_connectionString);
            var bothParameters = parameters.Split('*');
            //var list = repo.SearchShiurimCompletePreliminary(bothParameters[0], int.Parse(bothParameters[1]));
            var result = repo.SearchShiurimComplete(bothParameters[0], int.Parse(bothParameters[1]));
            return result;
        }

        [HttpPost]
        [Route("Upload")]
        public void Upload(FileUpload file)
        {
            string base64 = file.Base64.Substring(file.Base64.IndexOf(",") + 1);
            byte[] imageBytes = Convert.FromBase64String(base64);
            System.IO.File.WriteAllBytes($"uploads/{file.Name}", imageBytes);
            var repo = new FileUploadRepository(_connectionString);
            SearchedShiur s = repo.Upload(file.Name);
            repo.AddShiurParagraphs(s);
        }

    }
}

