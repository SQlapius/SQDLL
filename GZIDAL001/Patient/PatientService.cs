using System;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using static GZIDAL001.Config;

namespace GZIDAL001.Patient
{
    public static class PatientService
    {
        public static async Task<DataSet> ZoekPatientAsync(string value, int vesId)
        {
            try
            {
                using OracleConnection connection = new OracleConnection(DB_CONNECTION_STRING);
                using DataSet dataSet = new DataSet();

                OracleCommand cmd = new OracleCommand
                {
                    Connection = connection,
                    CommandText = "med_zi_sq.ZoekPat",
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("p_zoek", OracleDbType.Varchar2).Value = value; 
                cmd.Parameters.Add("p_vesId", OracleDbType.Int32).Value = vesId;
                cmd.Parameters.Add("u_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("u_Status", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();

                OracleDataAdapter da = new OracleDataAdapter(cmd);

                return await Task.Run(() =>
                { 
                    da.Fill(dataSet);
                    return dataSet;
                }); 
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}