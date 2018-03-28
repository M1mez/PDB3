using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PicDB.Layers_DA
{
    /// <summary>
    /// Connection Class which takes commands from the DataAccessLayer. 
    /// This was done for further splitting the real DataAccessLayer from the MockDataAccessLayer,
    /// which inherits from DataAccessLayer.
    /// </summary>
    public class DAL_Conn
    {
        private static SqlConnection Conn = new SqlConnection()
        {
            ConnectionString = PersInfo.ConnString
        };

        private static DAL_Conn _instance;
        private static readonly object Padlock = new object();

        /// <summary>
        /// returns Bool if the program is executed via UnitTest
        /// </summary>
        public static bool IsUnitTest {
            get
            {
                var isUnitTest = AppDomain.CurrentDomain.GetAssemblies().Any(
                    a => a.FullName.ToLowerInvariant().StartsWith("nunit.framework"));
                return isUnitTest;
            }
        }

        private DAL_Conn() {}

        /// <summary>
        /// Singleton Connection Class
        /// </summary>
        public static DAL_Conn Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ?? (_instance = new DAL_Conn());
                }
            }
        }

        private static int _serialId = 1;

        /// <summary>
        /// Get the ID which would be assigned next by the DB.
        /// If the method is called statically by an UnitTest, return 1, then 2...
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static int GetNextId (string tableName)
        {
            if (IsUnitTest) return _serialId++;
            Conn.ConnectionString = PersInfo.ConnString;
            var uspName = "usp_getNextID";
            try
            {
                Console.WriteLine("Calling usp: {0} for table: {1}", uspName, tableName);
                var nextId = -2;

                Conn.Open();
                using (var cmd = new SqlCommand(uspName, Conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TableName", tableName));
                    nextId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                Conn.Close();
                return nextId;
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

        public void OneWaySingleSql(string uspName, string column, string value) =>
            OneWayListSql(uspName, new List<KeyValuePair<string, string>>() {new KeyValuePair<string, string>(column, value)});

        public void OneWayListSql(string uspName, List<KeyValuePair<string, string>> paramList)
        {
            try
            {
                Console.WriteLine("Calling usp: " + uspName);

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

                    using (var reader = cmd.ExecuteReader()) reader.Read();
                }
                Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught in OneWaySql: " + e.Message);
            }
            finally
            {
                Conn.Close();
            }
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
                            dataList.Add(ReadSingleRow(reader));
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
                list.Add($"{record[curr]}");
            }
            return list;
        }
    }
}
