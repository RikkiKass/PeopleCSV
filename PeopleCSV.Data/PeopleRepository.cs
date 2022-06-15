using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCSV.Data
{
   public class PeopleRepository
    {
        private string _conn;
        public PeopleRepository(string connectionString)
        {
            _conn = connectionString;
        }
        public List<Person> GetPeople()
        {
            using var context = new PeopleDataContext(_conn);
            return context.People.ToList();
        }
        public void AddPeople(List<Person>people)
        {
            using var context = new PeopleDataContext(_conn);
            context.People.AddRange(people);
            context.SaveChanges();
        }
        public void DeleteAll()
        {
            using var context = new PeopleDataContext(_conn);
            var people = context.People.ToList();
            context.People.RemoveRange(people);
            context.SaveChanges();

        }
    }
}
