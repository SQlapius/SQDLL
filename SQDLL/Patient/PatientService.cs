using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

namespace SQDLL.Patient
{
    public class PatientService
    {
        HttpClient _httpClient;

        public PatientService()
        {
            _httpClient = new HttpClient();
        }

        async public Task<Root> GetPatient(int vesId, int patId, bool Force = false)
        {
            Debug.WriteLine("hit");
            try
            {
                var data = new Dictionary<string, int?>
                {
                    { "vesId", vesId},
                    { "patId", patId },
                };

                //var test = new HttpClient();
                var response = await
                     _httpClient.PostAsync("https://fatum.sqlapius.net/ords/api/zi-v0/patient",
                     new StringContent(JsonConvert.SerializeObject(data)));
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Root>(responseString);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //return "errror";
            }
        }

        public string GetUser(string username)
        {
            if (username != null)
            {
                try
                {
                    using (OracleConnection connection = new OracleConnection(CONNECTION_STRING))
                    {
                        OracleCommand command = new OracleCommand();
                        command.CommandText = "SELECT password,customerId,securityQuestion,securityAnswer,email FROM Users WHERE username LIKE :username";
                        command.Parameters.Add(":username", OracleDbType.NVarchar2).Value = username;
                        command.Connection = connection;
                        connection.Open();
                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string password = reader["password"].ToString();
                            string customerId = reader["customerId"].ToString();
                            string securityQuestion = reader["securityQuestion"].ToString();
                            string securityAnswer = reader["securityAnswer"].ToString();
                            string email = reader["email"].ToString();
                            return "";
                        }
                    }
                }

                catch (Exception e)
                {
                    return "";
                }
            }
        }
    }
}
