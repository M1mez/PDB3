using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PicDB.Classes
{
    public class DAL_Conn
    {
        private static SqlConnection Conn = new SqlConnection();

        public DAL_Conn()
        {
            Conn.ConnectionString =
                PersInfo.ConnString;
        }

        ~DAL_Conn()
        {
        }

        public static int GetNextID (string tableName)
        {
            Conn.ConnectionString = PersInfo.ConnString;
            var uspName = "usp_getNextID";
            try
            {
                Console.WriteLine("Calling usp: {0} for table: {1}", uspName, tableName);
                int nextID = -2;

                Conn.Open();
                using (var cmd = new SqlCommand(uspName, Conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TableName", tableName));
                    nextID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                Conn.Close();
                return nextID;
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught in usp_getNextID: " + e.Message);
            }
            finally
            {
                Conn.Close();
            }
            return -1;
        }
        

        public List<List<string>> UspList(string uspName, List<KeyValuePair<string, string>> paramList)
        {
            Conn.ConnectionString = PersInfo.ConnString;
            try
            {
                Console.WriteLine("Calling usp: " + uspName);
                var dataList = new List<List<string>>();
                Conn.Open();

                using (var cmd = new SqlCommand(uspName, Conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var el in paramList)
                    {
                        if (el.Value != null)
                        {
                            cmd.Parameters.Add(new SqlParameter("@" + el.Key, el.Value));
                        }
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataList.Add(ReadSingleRow((IDataRecord)reader));
                        }
                    }
                }
                
                Console.WriteLine("Query returned " + dataList.Count + " entries!");

                Conn.Close();
                return dataList;
            } catch (Exception e)
            {
                Console.WriteLine("Caught in UspList: " + e.Message);
            } finally
            {
                Conn.Close();
            }
            return new List<List<string>>();
        }


        private static List<string> ReadSingleRow(IDataRecord record)
        {
            var list = new List<string>();
            var columnCount = record.FieldCount;

            for (var curr = 0; curr < columnCount; curr++)
            {
                list.Add(String.Format("{0}", record[curr]));
            }
            return list;
        }

        /*
        private List<KeyValuePair<string, string>> DbConnection(string cmdString)
        {
            var values = new List<KeyValuePair<string, string>>();
            using (var con = new SqlConnection())
            {

                con.ConnectionString =
                    @"Server=DESKTOP-DIN7DPC\SQLEXPRESS;
                    Database=PicDB;
                    Trusted_Connection=True;";

                con.Open();

                var dateTemps = new SqlCommand(cmdString, con);
                try
                {
                    using (SqlDataReader reader = dateTemps.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var date = String.Format("{0}", reader[0]).Substring(0, 10);
                            var temp = String.Format("{0}", reader[1]);
                            values.Add(new KeyValuePair<string, string>(date, temp));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return values;
        }
        */
    }
}
