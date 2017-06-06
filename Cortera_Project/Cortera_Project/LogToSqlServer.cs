using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;



namespace Cortera_Project
{
    class LogToSqlServer
    {
        public void Log_To_SqlServer_Operation()
        {
            DateTime Start_Date = DateTime.Now;
            DateTime End_Date;
            string Message;


            try
            {
                End_Date = DateTime.Now;
                Message = "Application Executed Sucessfully";
                Console.WriteLine(Message);
                Coonection_insertion(Start_Date, End_Date, Message);

            }

            catch (Exception e)
            {
                Message = "Not Sucessfull";
                End_Date = DateTime.Now;
                Coonection_insertion(Start_Date, End_Date, Message);
                Console.WriteLine(Message);
                Console.WriteLine("Error message is :" + e.Message);
            }
        }

        private static SqlConnection Coonection_insertion(DateTime Start_Date, DateTime End_Date, string Message)
        {
            SqlConnection con = new SqlConnection("Data Source=bg-in-dspdb1;User Id=testuser;Password=testuser;DataBase=TESTDB");
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Akhilesh_events(Start_Date,End_date,Message) VALUES(@parmtr1,@parmtr2,@parmtr3)";

            cmd.Parameters.AddWithValue("@parmtr1", Start_Date);
            cmd.Parameters.AddWithValue("@parmtr2", End_Date);
            cmd.Parameters.AddWithValue("@parmtr3", Message);
            cmd.ExecuteNonQuery();
            con.Close();
            return con;
        }


    }
}
