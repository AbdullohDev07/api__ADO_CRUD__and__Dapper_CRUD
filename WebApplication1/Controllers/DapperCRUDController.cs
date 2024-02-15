using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebApplication1.Roots;
using Dapper;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DapperCRUDController : ControllerBase
    {
        public string connectionString = "Server=localhost;Port=16172;Database=console_task;username=postgres;Password=axihub;";
        
        [HttpGet]
        public List<Person> GetDataDapper()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                return connection.Query<Person>("select * from persons").ToList();
            }
        }

        [HttpGet]
        public List<Person> GetIdDateDapper(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                return connection.Query<Person>("select * from persons where id = @id;", new { Id = id }).ToList();
            }
        }

        [HttpPost]
        public Person InsertDataDapper(Person person)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Query<Person>("insert into persons(name, age) values (@name, @age);", new { Name = person.Name, Age = person.Age, });

                return person;
            }
        }

        [HttpPut]
        public Person UpdateDataDapper(int id, Person person)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Query<Person>($"update persons set name = @name, age = @age where id = {id};", new { Name = person.Name, Age = person.Age, });

                return person;
            }
        }

        [HttpPatch]
        public int UpdatePatchDataDapper(int id, string name)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                int num = connection.Execute($"update persons set name = @name where id = @id;", new { Id = id, Name = name });

                return num;
            }
        }

        [HttpDelete]
        public int DeleteDataDapper(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                int num = connection.Execute($"delete from persons where id = @id;", new { Id = id});

                return num;
            }
        }
    }
}
