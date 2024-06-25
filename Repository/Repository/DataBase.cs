using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository
{
    public class DataBase
    {
        public static string lineConnection = null; //string подключения в БД

        //коллекции данных работников
        public List<string> peopleId = new List<string>(); 
        public List<string> peopleName = new List<string>();
        public List<string> peopleDepartament = new List<string>();
        public List<string> peoplePost = new List<string>();
        public List<string> allInfoPeople = new List<string>();

        //коллекции данных складов
        public List<string> warehouseId = new List<string>();
        public List<string> kp70 = new List<string>();
        public List<string> kp80 = new List<string>();
        public List<string> kp100 = new List<string>();
        public List<string> allInfoWh = new List<string>();

        //метод подключения в БД
        public void Connect(string servername, string dbname)
            => lineConnection = "Data Source=" + servername + ";Initial Catalog=" + dbname + ";Integrated Security=True";

        public void Workes() //метод получения раздельных данных о работнках из БД
        {
            string query = "SELECT identify, name, departament, post FROM dbo.Workes";

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    peopleId.Add(reader["identify"].ToString());
                    peopleName.Add(reader["name"].ToString());
                    peopleDepartament.Add(reader["departament"].ToString());
                    peoplePost.Add(reader["post"].ToString());  
                }

                connection.Close();
            }
        }

        public void Warehouses() //метод получения раздельных данных о складах из БД
        {
            string query = "SELECT identify, kp70, kp80, kp100 FROM dbo.Warehouses";

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    warehouseId.Add(reader["identify"].ToString());
                    kp70.Add(reader["kp70"].ToString());
                    kp80.Add(reader["kp80"].ToString());
                    kp100.Add(reader["kp100"].ToString());
                }

                connection.Close();
            }
        }

        public void AllInfoWorkes() //получение всех данных о работниках из БД
        {
            string query = "SELECT * FROM dbo.Workes";

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string id = reader["identify"].ToString();
                    string name = reader["name"].ToString();
                    string departament = reader["departament"].ToString();
                    string post = reader["post"].ToString();

                    string info = $"{id}, {name}, {departament}, {post}";

                    allInfoPeople.Add(info);
                }

                connection.Close();
            }
        }

        public void AllInfoWarehouses() //получение всех данных о складах из БД
        {
            string query = "SELECT * FROM dbo.Warehouses";

            using (SqlConnection connection = new SqlConnection(lineConnection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string id = reader["identify"].ToString();
                    string kp70 = reader["kp70"].ToString();
                    string kp80 = reader["kp80"].ToString();
                    string kp100 = reader["kp100"].ToString();

                    string info = $"{id}, КП70: {kp70}т, КП80: {kp80}т, КП100: {kp100}т";

                    allInfoWh.Add(info);
                }

                connection.Close();
            }
        }
    }
}
