using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebApplication1.Roots;
using Dapper;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ADO_NetCRUDController : ControllerBase
    {
        public string connectionString = "Server=localhost;Port=16172;Database=console_task;username=postgres;Password=axihub;";

        [HttpGet]
        public List<Person> GetDataADO()
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"select * from persons;";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                var result = cmd.ExecuteReader();

                List<Person> list = new List<Person>();

                while (result.Read())
                {
                    list.Add(new Person
                    {
                        Id = (int)result[0],
                        Name = (string)result[1],
                        Age = (int)result[2]
                    });
                }
                return list;
            }
        }

        [HttpGet]
        public List<Person> GetIdDateADO(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"select * from persons where id = {id};";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                var result = cmd.ExecuteReader();

                List<Person> list = new List<Person>();

                while (result.Read())
                {
                    list.Add(new Person
                    {
                        Id = (int)result[0],
                        Name = (string)result[1],
                        Age = (int)result[2]
                    });
                }
                return list;
            }
        }

        [HttpPost]
        public Person InsertDateADO(Person person)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"insert into persons(name, age) values ('{person.Name}', {person.Age});";

                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                var count = cmd.ExecuteNonQuery();

                return person;
            }
        }

        [HttpPut]
        public Person UpdateDatADO(int id, Person person)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"update persons set name = '{person.Name}', age = {person.Age} where id = {id};";

                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                return person;
            }
        }


        [HttpPatch]
        public int UpdatePatchDatADO(int id, string name)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"update persons set name = '{name}' where id = {id};";

                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                return cmd.ExecuteNonQuery();
            }
        }

        [HttpDelete]
        public int DeleteDatADO(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"delete from persons where id = {id};";

                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                var count = cmd.ExecuteNonQuery();

                return count;
            }
        }
    }
}
