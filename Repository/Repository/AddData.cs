using System.Data.SqlClient;

namespace Repository
{
    public class AddData
    {
        //добавление данных в БД склады и ресурсы 
        public void AddMaterial(string id, string kp70, string kp80, string kp100)
        {
            string query = "INSERT INTO dbo.Warehouses (identify, kp70, kp80, kp100) VALUES (@identify, @kp70, @kp80, @kp100)";

            using (SqlConnection connection = new SqlConnection(DataBase.lineConnection))
            {
                connection.Open();

               using (SqlCommand command = new SqlCommand(query, connection))
               {
                    command.Parameters.AddWithValue("@identify", id);
                    command.Parameters.AddWithValue("@kp70", kp70);
                    command.Parameters.AddWithValue("@kp80", kp80);
                    command.Parameters.AddWithValue("@kp100", kp100);

                    command.ExecuteNonQuery();
               }

                connection.Close();
            }
        }

        //добавление данных в БД рабочие
        public void AddWorkes(string id, string name, string departament, string post)
        {
            string query = "INSERT INTO dbo.Workes (identify, name, departament, post) VALUES (@identify, @name, @departament, @post)";

            using (SqlConnection connection = new SqlConnection(DataBase.lineConnection))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@identify", id);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@departament", departament);
                    command.Parameters.AddWithValue("@post", post);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
