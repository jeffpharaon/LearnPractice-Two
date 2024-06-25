using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository
{
    public class EditData
    {
        //вывод данных рабочих для их отображения в полях для редактирования
        public Dictionary<string, string> EditWorkes(string selectedItem)
        {
            string query = "SELECT * FROM dbo.Workes WHERE identify = @identify";
            Dictionary<string, string> result = new Dictionary<string, string>();

            using (SqlConnection connection = new SqlConnection(DataBase.lineConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@identify", selectedItem);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result["identify"] = reader["identify"].ToString();
                    result["name"] = reader["name"].ToString();
                    result["departament"] = reader["departament"].ToString();
                    result["post"] = reader["post"].ToString();
                }

                reader.Close();
            }

            return result;
        }

        //сохранение в БД пользовательских изменений рабочих
        public void SavedEditWorkes(string id, string name, string departament, string post, string oldId)
        {
            string query = "UPDATE dbo.Workes SET identify=@identify, name=@name, departament=@departament, post=@post WHERE identify=@oldid";
            
            using (SqlConnection connection = new SqlConnection(DataBase.lineConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@identify", id);
                command.Parameters.AddWithValue("@name",name);
                command.Parameters.AddWithValue("@departament", departament);
                command.Parameters.AddWithValue("@post", post);
                command.Parameters.AddWithValue("@oldid", oldId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //вывод данных ресурсов и складов для их отображения в полях для редактирования
        public Dictionary<string, string> EditWarehouses(string selectedItem)
        {
            string query = "SELECT * FROM dbo.Warehouses WHERE identify = @identify";
            Dictionary<string, string> result = new Dictionary<string, string>();

            using (SqlConnection connection = new SqlConnection(DataBase.lineConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@identify", selectedItem);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result["identify"] = reader["identify"].ToString();
                    result["kp70"] = reader["kp70"].ToString();
                    result["kp80"] = reader["kp80"].ToString();
                    result["kp100"] = reader["kp100"].ToString();
                }

                reader.Close();
            }

            return result;
        }

        //сохранение в БД пользовательских изменений ресурсов и складов
        public void SavedEditWarehouses (string id, string kp70, string kp80, string kp100, string oldId)
        {
            string query = "UPDATE dbo.Warehouses SET identify=@identify, kp70=@kp70, kp80=@kp80, kp100=@kp100 WHERE identify=@oldid";

            using (SqlConnection connection = new SqlConnection(DataBase.lineConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@identify", id);
                command.Parameters.AddWithValue("@kp70", kp70);
                command.Parameters.AddWithValue("@kp80", kp80);
                command.Parameters.AddWithValue("@kp100", kp100);
                command.Parameters.AddWithValue("@oldid", oldId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
