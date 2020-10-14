using System;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using static GZIDAL001.Config;


namespace GZIDAL001.Medicijn
{
    public static class MedicijnService
    {
        public static async Task<DataSet> ZoekMedicijn(string value, int opPrk, int opNaamAtcMemo)
        {
            try
            {
                using OracleConnection connection = new OracleConnection(DB_CONNECTION_STRING);
                using DataSet dataSet = new DataSet();

                OracleCommand cmd = new OracleCommand
                {
                    Connection = connection,
                    CommandText = "med_zi_sq.ZoekMed",
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("p_zoek", OracleDbType.Varchar2).Value = value; 
                cmd.Parameters.Add("p_opPrk", OracleDbType.Int32).Value = opPrk;
                cmd.Parameters.Add("p_opNaamAtcMemo", OracleDbType.Int32).Value = opNaamAtcMemo;
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
