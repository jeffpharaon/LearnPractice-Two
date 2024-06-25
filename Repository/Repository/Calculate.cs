using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Repository
{
    public class Calculate
    {
        //результаты подсчетов ресурсов
        public double resultKp70;
        public double resultKp80;
        public double resultKp100;
        public double resultAll;

        //коллекции для разных типов ресурсов
        public List<string> kp70Calc = new List<string>();
        public List<string> kp80Calc = new List<string>();
        public List<string> kp100Calc = new List<string>();
        public List<string> kpAllCalc = new List<string>();

        public void KP70(string id) //рассчет результатов для kp70
        {
            string query = "SELECT kp70 FROM dbo.Warehouses WHERE identify LIKE '%" + id + "%'";

            using (SqlConnection connection = new SqlConnection(DataBase.lineConnection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    kp70Calc.Add(reader["kp70"].ToString());

                foreach (object result in kp70Calc)
                    resultKp70 = double.Parse((string)result);

                resultKp70 *= 21.066;
            }
        }

        public void KP80(string id) //рассчет результатов для kp80
        {
            string query = "SELECT kp80 FROM dbo.Warehouses WHERE identify LIKE '%" + id + "%'";

            using (SqlConnection connection = new SqlConnection(DataBase.lineConnection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader(); 

                while (reader.Read())
                    kp80Calc.Add(reader["kp80"].ToString());

                foreach (object result in kp80Calc)
                    resultKp80 = double.Parse((string)result);

                resultKp80 *= 15.567;
            }
        }

        public void KP100(string id) //рассчет результатов для kp100
        {
            string query = "SELECT kp100 FROM dbo.Warehouses WHERE identify LIKE '%" + id + "%'";

            using (SqlConnection connection = new SqlConnection(DataBase.lineConnection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    kp100Calc.Add(reader["kp100"].ToString());

                foreach (string result in kp100Calc)
                    resultKp100 = double.Parse((string)result);

                resultKp100 *= 11.23;
            }
        }

        public void AllKP(string id) //рассчет результатов для всех типов ресурсов 
        {
            string query = "SELECT kp70, kp80, kp100 FROM dbo.Warehouses WHERE identify LIKE '%" + id + "%'";

            using (SqlConnection connection = new SqlConnection(DataBase.lineConnection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    kp70Calc.Add(reader["kp70"].ToString());
                    kp80Calc.Add(reader["kp80"].ToString());
                    kp100Calc.Add(reader["kp100"].ToString());
                }

                double kp70 = kp70Calc.Sum(one => double.TryParse(one, out double parsedValue) ? parsedValue : 0);
                double kp80 = kp80Calc.Sum(two => double.TryParse(two, out double parsedValue) ? parsedValue : 0);
                double kp100 = kp100Calc.Sum(three => double.TryParse(three, out double parsedValue) ? parsedValue : 0);

                resultAll = (kp70 * 21.066) + (kp80 * 15.657) + (kp100 * 11.23);
            }
        }
    }
}
