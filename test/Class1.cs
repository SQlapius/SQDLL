using System;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Client;

namespace test
{
    public class Class1
    {
        public OracleDataReader GetUser()
        {
            try
            {
                OracleConnection connection = new OracleConnection("Password= MED ; User ID= MED ; Data Source= (DESCRIPTION= (ADDRESS= (PROTOCOL=TCP) (HOST= 192.168.245.69) (PORT=1521) ) (CONNECT_DATA= (SERVER=dedicated) (SERVICE_NAME=XEPDB1)  )  );");
                OracleCommand command = new OracleCommand
                {
                    CommandText = "SELECT * from GZI_LOV_656",
                    Connection = connection
                };
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();

                return reader;
            }
            catch (Exception e)
            {
                return default;
            }
        }
    }
}
