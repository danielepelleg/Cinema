﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WCFServer
{
    abstract class DatabaseHandler
    {
        public static SqlConnection conn = null;
        /*
         * @return the connection to the Database
         */
        public static SqlConnection GetConnection()
        {
            if (conn != null) return conn;
            return Connect();
        }

        /*
         * Connect to the database.
         * @return the connection
         */
        public static SqlConnection Connect()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
                return conn;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Database Connection Error");
            }
            return conn;
        }
    }
}
