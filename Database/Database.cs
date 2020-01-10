using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BitMexBot.Database
{
    public class Database
    {
        public SqlConnectionStringBuilder SQLConnectionBuilder = new SqlConnectionStringBuilder();
        public SqlConnection SQLConnection;

        public bool Connect()
        {
            try
            {
                this.SQLConnectionBuilder.DataSource = "localhost";
                this.SQLConnectionBuilder.UserID = "sa";              // update me
                this.SQLConnectionBuilder.Password = "your_password";      // update me
                this.SQLConnectionBuilder.InitialCatalog = "master";

                using  (this.SQLConnection = new SqlConnection(this.SQLConnectionBuilder.ConnectionString))
                {
                    this.SQLConnection.Open();
                    Console.WriteLine("SQL Connected...");
                }

            }
            catch(SqlException e)
            {
                Console.WriteLine("SQL Exception: {0}", e.ToString());
            }


            return true;
        }
    }
}
