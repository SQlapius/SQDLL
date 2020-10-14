using System;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using static GZIDAL001.Config;

namespace GZIDAL001.Patient
{
    public class PatientService
    {
        HttpClient _httpClient;

        public PatientService()
        {
            _httpClient = new HttpClient();
        }

        //async public Task<Root> GetPatient(int vesId, int patId, bool Force = false)
        //{
        //    Debug.WriteLine("hit");
        //    try
        //    {
        //        var data = new Dictionary<string, int?>
        //        {
        //            { "vesId", vesId},
        //            { "patId", patId },
        //        };

        //        //var test = new HttpClient();
        //        var response = await
        //             _httpClient.PostAsync("https://fatum.sqlapius.net/ords/api/zi-v0/patient",
        //             new StringContent(JsonConvert.SerializeObject(data)));
        //        var responseString = await response.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<Root>(responseString);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //        //return "errror";
        //    }
        //}

        public async Task<DataSet> GetUserAsync(string value, int vesId)
        {
            DataSet dataset = new DataSet();

            try
            {
                using OracleConnection connection = new OracleConnection(DB_CONNECTION_STRING);

                OracleCommand cmd = new OracleCommand
                {
                    Connection = connection,
                    CommandText = "med_zi_sq.ZoekPat",
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("p_zoek", OracleDbType.Varchar2).Value = value; // Input id
                cmd.Parameters.Add("p_vesId", OracleDbType.Int32).Value = vesId; // Input id
                cmd.Parameters.Add("u_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("u_Status", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                try
                {
                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    OracleDataAdapter da = new OracleDataAdapter(cmd);

                    return await Task.Run(() =>
                     {
                         da.Fill(dataset);
                         return dataset;
                     }); 

                }
                catch (Exception ex)
                {
                    new Exception(ex.ToString());
                    return default;
                }
                finally
                { 
                    connection.Close();
                    dataset.Dispose();
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.ToString());
                return default;
            }
        }
    }
}