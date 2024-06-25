using System.Data.SqlClient;

namespace Repository
{
    public class DeleteData
    {
        //удаление из базы данных SQL таблицы рабочих
        public void DeleteWorkes(string selectedItem, string[] parts, string nameToDelete)
        {
            string query = "DELETE FROM dbo.Workes WHERE identify = @identify";

            using (SqlConnection connection = new SqlConnection(DataBase.lineConnection))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("identify", nameToDelete);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        //удаление из базы данных SQL таблицы складов и ресурсов
        public void DeleteWarehouses(string selectedItem, string[] parts, string nameToDelete)
        {
            string query = "DELETE FROM dbo.Warehouses WHERE identify = @identify";

            using (SqlConnection connection = new SqlConnection(DataBase.lineConnection))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("identify", nameToDelete);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
