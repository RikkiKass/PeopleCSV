using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Faker;
using Microsoft.Extensions.Configuration;
using PeopleCSV.Data;
using System.Text;
using System.IO;
using System.Globalization;
using PeopleCSV.Web.Models;

namespace PeopleCSV.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleCsv : ControllerBase
    {
        private string _connectionString;
        public PeopleCsv(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [Route("getpeople")]
        [HttpGet]
        public List<Person> GetPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetPeople();
        }
        [Route("deleteall")]
        [HttpPost]
        public void DeleteAll()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeleteAll();
        }

        [HttpGet]
        [Route("generatecsv/{amount}")]
        public IActionResult GenerateCsv(int amount)
        {
            var people = GetPeople(amount);
            string csv = GetCsv(people);
            byte[] bytes = Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "people.csv");
        }
        [HttpPost]
        [Route("upload")]
        public void Upload(UploadViewModel viewModel)
        {
            int index = viewModel.Base64File.IndexOf(",") + 1;
            string base64 = viewModel.Base64File.Substring(index);
            byte[] bytes = Convert.FromBase64String(base64);
            var people = GetfromCsvBytes(bytes);
            var repo = new PeopleRepository(_connectionString);
            repo.AddPeople(people);
        }
        private List<Person>GetPeople(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ =>
            {
                return new Person
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Age = Faker.RandomNumber.Next(20, 100),
                    Email = Faker.Internet.Email(),
                    Address=Faker.Address.StreetAddress()
                    
                };
            }).ToList();
        }
        private string GetCsv(List<Person> people)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(people);
            return builder.ToString();
        }
        private List<Person> GetfromCsvBytes(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return reader.GetRecords<Person>().ToList();
        }
    }
}
