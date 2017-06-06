using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Cortera_Project_2_StringDLL;
using Oracle.DataAccess.Client;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Cortera_Project
{
    class Program
    {
        public static int maxx;
        public static int minn;

        public static DateTime Start_Date = DateTime.Now;
        public static DateTime End_Date;
        public static string Message;


        public static DataTable Akhilesh_Stats_DT = new DataTable();
   
 
        static void Main(string[] args)
        {
            try
            {
                //checking Weather the Data is in the DataBase or not 
                Post_Checking_Of_Data_InDB();

                Akhilesh_Stats_DT.Columns.Add("Stats_Desc", typeof(string));
                Akhilesh_Stats_DT.Columns.Add("Stats_Value", typeof(int));
                Akhilesh_Stats_DT.Columns.Add("Student_Id", typeof(int));
                Akhilesh_Stats_DT.Columns.Add("Sub_Code", typeof(int));

                string csv_filepath = @"C:\Users\akumar\Documents\Visual Studio 2012\Projects\Cortera_Project\Cortera_Project_Files\Akhilesh_student.csv";
                DataTable csvData = GDFCSVFile1(csv_filepath);
                LoadToDB(csvData, "Akhilesh_Student");
                Show_Data(csvData, false);


                Console.WriteLine("\n");
                Console.WriteLine(".........................Student Marks data...................... ");
                Console.WriteLine("\n");

                string csv_filepath1 = @"C:\Users\akumar\Documents\Visual Studio 2012\Projects\Cortera_Project\Cortera_Project_Files\Akhilesh_Marks.csv";
                DataTable csvData1 = GDFCSVFile2(csv_filepath1);
                LoadToDB(csvData1, "Akhilesh_Marks");
                Show_Data(csvData1, true);

                Console.WriteLine();
                Message = "Application Executed Sucessfully";
                Console.WriteLine(Message);
                End_Date = DateTime.Now;
                Coonection_insertion(Start_Date, End_Date, Message, "No error");

                Console.WriteLine("Are you want to flush the db");
                char c = Convert.ToChar(Console.ReadLine());
                if (c == 'y' || c == 'Y')
                    Flushing_DB();
            }
            catch (Exception exp)
            {

                Message = "Not Sucessfull";
                End_Date = DateTime.Now;
                Console.WriteLine(Message);
                Console.WriteLine("Error message is :" + exp.Message);
                Coonection_insertion(Start_Date, End_Date, Message, exp.Message);
                Environment.Exit(1);

            }
        }

        private static void Post_Checking_Of_Data_InDB()
        {
            OracleConnection check_data_con = new OracleConnection(@"Data Source=INSTGU01;User ID=AKUMAR;Password=pass2017");
            check_data_con.Open();
            OracleCommand Check_data_cmd = new OracleCommand("Select Count(*) from Akhilesh_student", check_data_con);
            if (Convert.ToInt32(Check_data_cmd.ExecuteScalar()) > 0) 
                Flushing_DB();
            Check_data_cmd.CommandText = "Select * from Akhilesh_Marks1";
            if (Convert.ToInt32(Check_data_cmd.ExecuteScalar()) > 0) 
                Flushing_DB();
            Check_data_cmd.CommandText = "Select * from Akhilesh_stats";
            if (Convert.ToInt32(Check_data_cmd.ExecuteScalar()) > 0) 
                Flushing_DB();
            check_data_con.Close();
        }

        private static void Calulating_Stats(DataTable csvData)
        {

            Console.WriteLine("\n \n \n \n Calulation Part \n \n Condition A \n \n \n ");

            // Condition A

            int k1, j; k1 = j = 1;
            int[] sum = new int[11];
            foreach (DataRow row in csvData.Rows)
            {
                int id_check = Convert.ToInt32(row["Student_Id"]);
                if (k1 == id_check)
                {
                    sum[j] += Convert.ToInt32(row["Marks"]);
                }
                else
                {
                    k1++; j++;
                    sum[j] += Convert.ToInt32(row["Marks"]);
                }
            }
            int[] avg = new int[10];
            for (int i = 0; i < sum.Length - 1; i++)
            {
                avg[i] += (int)(sum[i + 1] / 3);
            }
            maxx = avg.Max();
            minn = avg.Min();

            Console.WriteLine("Student {1} heaving highest Average Marks is {0}", maxx, Array.IndexOf(avg, maxx));
            Console.WriteLine("Student {1} heaving highest Average Marks is {0}", minn, Array.IndexOf(avg, minn));

            DataRow Condition_A_Record = Akhilesh_Stats_DT.NewRow();
            Condition_A_Record["Stats_Desc"] = "Student heaving highest  Average Marks ";
            Condition_A_Record["Stats_Value"] = maxx;
            Condition_A_Record["Student_Id"] = Array.IndexOf(avg, maxx);
            Condition_A_Record["Sub_Code"] = DBNull.Value;
            Akhilesh_Stats_DT.Rows.Add(Condition_A_Record);
            DataRow Condition_A_Record1 = Akhilesh_Stats_DT.NewRow();
            Condition_A_Record1["Stats_Desc"] = "Student heaving Lowest Average Marks ";
            Condition_A_Record1["Stats_Value"] = minn;
            Condition_A_Record1["Student_Id"] = Array.IndexOf(avg, minn);
            Condition_A_Record1["Sub_Code"] = DBNull.Value;
            Akhilesh_Stats_DT.Rows.Add(Condition_A_Record1);

            // Condition B Calculation

            Console.WriteLine("\n \n \n Condition B \n \n \n ");


            int[] SHMI101 = new int[10];
            int[] SHMI102 = new int[10];
            int[] SHMI103 = new int[10];


            int j2, j1, j3; j2 = j3 = j1 = 0;
            int[] sum1 = new int[10];
            foreach (DataRow row in csvData.Rows)
            {
                int id_check = Convert.ToInt32(row["Sub_Code"]);
                if (101 == id_check)
                {
                    SHMI101[j1] += Convert.ToInt32(row["Marks"]); j1++;

                }
                if (102 == id_check)
                {

                    SHMI102[j2] += Convert.ToInt32(row["Marks"]); j2++;

                }
                if (103 == id_check)
                {
                    SHMI103[j3] += Convert.ToInt32(row["Marks"]); j3++;
                }

            }

            Console.WriteLine("Stundet {1} With highest  Marks in sub 101 {0} ", SHMI101.Max(), Array.IndexOf(SHMI101, SHMI101.Max()));

                DataRow Condition_B_Record = Akhilesh_Stats_DT.NewRow();
                Condition_B_Record["Stats_Desc"] = "Stundet With Higest Marks in sub ";
                Condition_B_Record["Stats_Value"] = SHMI101.Max();
                Condition_B_Record["Student_Id"] = Array.IndexOf(SHMI101,SHMI101.Max());
                Condition_B_Record["Sub_Code"] = 101;
                Akhilesh_Stats_DT.Rows.Add(Condition_B_Record);

                Console.WriteLine("Stundet {1} With highest  Marsk in sub 102 {0} ", SHMI102.Max(), Array.IndexOf(SHMI102, SHMI102.Max()));

            DataRow Condition_B_Record1 = Akhilesh_Stats_DT.NewRow();
            Condition_B_Record1["Stats_Desc"] = "Stundet With Higest Marks in sub ";
            Condition_B_Record1["Stats_Value"] = SHMI102.Max();
            Condition_B_Record1["Student_Id"] = Array.IndexOf(SHMI102, SHMI102.Max());
            Condition_B_Record1["Sub_Code"] = 102;
            Akhilesh_Stats_DT.Rows.Add(Condition_B_Record1);

            Console.WriteLine("Stundet {1} With highest  Marsk in sub 103 {0} ", SHMI103.Max(), Array.IndexOf(SHMI103, SHMI103.Max()));

            DataRow Condition_B_Record2 = Akhilesh_Stats_DT.NewRow();
            Condition_B_Record2["Stats_Desc"] = "Stundet With Higest Marks in sub ";
            Condition_B_Record2["Stats_Value"] = SHMI103.Max();
            Condition_B_Record2["Student_Id"] = Array.IndexOf(SHMI103, SHMI103.Max());
            Condition_B_Record2["Sub_Code"] = 103;
            Akhilesh_Stats_DT.Rows.Add(Condition_B_Record2);

            Console.WriteLine("\n \n \n");
            Console.WriteLine("Stundet {1} With Lowest Marsk in sub 101 {0} ", SHMI101.Min(), Array.IndexOf(SHMI101, SHMI101.Min()));

            DataRow Condition_B_Record3 = Akhilesh_Stats_DT.NewRow();
            Condition_B_Record3["Stats_Desc"] = "Stundet With Lowest Marks in sub ";
            Condition_B_Record3["Stats_Value"] = SHMI101.Min();
            Condition_B_Record3["Student_Id"] = Array.IndexOf(SHMI101, SHMI101.Min());
            Condition_B_Record3["Sub_Code"] = 101;
            Akhilesh_Stats_DT.Rows.Add(Condition_B_Record3);

            Console.WriteLine("Stundet {1} With Lowest Marsk in sub 102 {0} ", SHMI102.Min(), Array.IndexOf(SHMI102, SHMI102.Min()));

            DataRow Condition_B_Record4 = Akhilesh_Stats_DT.NewRow();
            Condition_B_Record4["Stats_Desc"] = "Stundet With Lowest Marks in sub ";
            Condition_B_Record4["Stats_Value"] = SHMI102.Min();
            Condition_B_Record4["Student_Id"] = Array.IndexOf(SHMI102, SHMI102.Min());
            Condition_B_Record4["Sub_Code"] = 102;
            Akhilesh_Stats_DT.Rows.Add(Condition_B_Record4);


            Console.WriteLine("Stundet {1} With Lowest Marsk in sub 103 {0} ", SHMI103.Min(), Array.IndexOf(SHMI103, SHMI103.Min()));

            DataRow Condition_B_Record5 = Akhilesh_Stats_DT.NewRow();
            Condition_B_Record5["Stats_Desc"] = "Stundet With Lowest Marks in sub ";
            Condition_B_Record5["Stats_Value"] = SHMI103.Min();
            Condition_B_Record5["Student_Id"] = Array.IndexOf(SHMI103, SHMI103.Min());
            Condition_B_Record5["Sub_Code"] = 103;
            Akhilesh_Stats_DT.Rows.Add(Condition_B_Record5);

            // condition C calulation   

            Console.WriteLine("\n \n \n Condition c \n \n \n ");

            Console.WriteLine("Average Makrs in Subject 101 is {0}", SHMI101.Average());

            DataRow Condition_c_Record = Akhilesh_Stats_DT.NewRow();
            Condition_c_Record["Stats_Desc"] = "Average Makrs in Subject ";
            Condition_c_Record["Stats_Value"] = SHMI101.Average();
            Condition_c_Record["Student_Id"] = DBNull.Value;
            Condition_c_Record["Sub_Code"] = 101;
            Akhilesh_Stats_DT.Rows.Add(Condition_c_Record);

            Console.WriteLine("Average Makrs in Subject 102 is {0}", SHMI102.Average());

            DataRow Condition_c_Record1 = Akhilesh_Stats_DT.NewRow();
            Condition_c_Record1["Stats_Desc"] = "Average Makrs in Subject ";
            Condition_c_Record1["Stats_Value"] = SHMI102.Average();
            Condition_c_Record1["Student_Id"] = DBNull.Value;
            Condition_c_Record1["Sub_Code"] = 102;
            Akhilesh_Stats_DT.Rows.Add(Condition_c_Record1);

            Console.WriteLine("Average Makrs in Subject 103 is {0}", SHMI103.Average());

            DataRow Condition_c_Record2 = Akhilesh_Stats_DT.NewRow();
            Condition_c_Record2["Stats_Desc"] = "Average Makrs in Subject ";
            Condition_c_Record2["Stats_Value"] = SHMI101.Average();
            Condition_c_Record2["Student_Id"] = DBNull.Value;
            Condition_c_Record2["Sub_Code"] = 103;
            Akhilesh_Stats_DT.Rows.Add(Condition_c_Record2);

            // Condition D Calculation

            Console.WriteLine("\n \n \n Condition D \n \n \n ");
            
            for (int i = 0; i < avg.Length; i++)
            {
                Console.WriteLine("Student {0} heaving Average Marks {1} ", i + 1, avg[i]);
                if (i <= avg.Length - 1)
                {
                        DataRow Condition_D_Record = Akhilesh_Stats_DT.NewRow();
                        Condition_D_Record["Stats_Desc"] = "Average marks of all students in all subjects";
                        Condition_D_Record["Stats_Value"] = avg[i];
                        Condition_D_Record["Student_Id"] = DBNull.Value;
                        Condition_D_Record["Sub_Code"] = DBNull.Value;
                        Akhilesh_Stats_DT.Rows.Add(Condition_D_Record);
                   //    Akhilesh_Stats_DT.Rows.InsertAt(Condition_B_Record, j9);  used to insert at specific index but not required becuase datatable automatically Append the records as u enter in it.
                }

            }

            Console.WriteLine("\n \n \n \n  Main tavle \n \n \n \n");

            foreach (DataRow mainrowss in Akhilesh_Stats_DT.Rows)
            {
                Console.WriteLine(string.Join("\t",mainrowss.ItemArray));
            }
            
            // Sending Data To Oracle DataBase.
            LoadToDB(Akhilesh_Stats_DT, "Akhilesh_Stats");
            
            //DataTable Final Data Uploading to CSV
            Stats_To_CSV(Akhilesh_Stats_DT, @"C:\Users\akumar\Documents\Visual Studio 2012\Projects\Cortera_Project\Cortera_Project_Files\Akhilesh_Stats.csv");

        }

        private static void Stats_To_CSV(DataTable Final_table,String strFilePath)
        {

            StreamWriter sw = new StreamWriter(strFilePath, false);

            int iColCount = Final_table.Columns.Count;

            for (int i = 0; i < iColCount; i++)
            {
                sw.Write(Final_table.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);


            foreach (DataRow dr in Final_table.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }


        private static void LoadToDB(DataTable csvData, string Tbl_Name)
        {
            OracleConnection conntion = new OracleConnection(@"Data Source=INSTGU01;User ID=AKUMAR;Password=pass2017");
            conntion.Open();

            if (Tbl_Name == "Akhilesh_Student")
            {
                using (OracleBulkCopy blk1 = new OracleBulkCopy(conntion))
                {
                    blk1.DestinationTableName = "Akhilesh_student";
                    blk1.WriteToServer(csvData);
                }

                conntion.Close();
            }
            else if (Tbl_Name == "Akhilesh_Marks")
            {
                using (OracleBulkCopy blk1 = new OracleBulkCopy(conntion))
                {
                    blk1.DestinationTableName = "Akhilesh_marks1";
                    blk1.WriteToServer(csvData);
                }

                conntion.Close();
            }
            else if (Tbl_Name == "Akhilesh_Stats")
            {
                using (OracleBulkCopy blk1 = new OracleBulkCopy(conntion))
                {
                    blk1.DestinationTableName = "Akhilesh_stats";
                    blk1.WriteToServer(csvData);
                }

                conntion.Close();
            }

        }

        private static void Show_Data(DataTable csvData, bool Decider)
        {

            if (Decider == false)
            {
                foreach (DataRow row in csvData.Rows)
                {
                    Console.WriteLine(string.Join("\t", row.ItemArray));
                }
            }
            else
            {
                foreach (DataRow row in csvData.Rows)
                {
                    Console.WriteLine(string.Join("\t", row.ItemArray));
                }
                Calulating_Stats(csvData);
            }
        }

        private static DataTable GDFCSVFile1(string csv_file_path)
        {
            DataTable csv_Data = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFileds = csvReader.ReadFields();

                    int Column_Spliter = 0;
                    foreach (string column in colFileds)
                    {
                        if (Column_Spliter == 3)                      // Columns Split here.
                        {
                            char ch = '_';
                            string[] str = column.Split(ch);
                            foreach (var item in str)
                            {
                                csv_Data.Columns.Add(item);
                            }
                        }
                        else
                        {
                            ++Column_Spliter;
                            DataColumn datecolumn = new DataColumn(column);
                            csv_Data.Columns.Add(datecolumn);
                        }
                    }

                    int k = 0;
                    ArrayList al = new ArrayList();
                    string[] Adding_Splited_Row_Data;
                    string[] FinalizeData;
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        foreach (var item in fieldData)
                        {
                            if (k == 3)
                            {
                                char ch = '_';
                                Adding_Splited_Row_Data = item.Split(ch);
                                foreach (var item1 in Adding_Splited_Row_Data)
                                {
                                    al.Add(CLeaning_Operation_DLL.Clean_Operation(item1));
                                }

                            }
                            else
                            {
                                al.Add(CLeaning_Operation_DLL.Clean_Operation(item));
                                k++;
                            }
                        } //Making empty value as null
                        FinalizeData = (String[])al.ToArray(typeof(string));
                        csv_Data.Rows.Add(FinalizeData);
                        al.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return csv_Data;
        }


        private static DataTable GDFCSVFile2(string csv_file_path)
        {
            DataTable csv_Data = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFileds = csvReader.ReadFields();

                    foreach (string column in colFileds)
                    {
                        DataColumn dclm = new DataColumn(column);
                        dclm.AllowDBNull = false;
                        csv_Data.Columns.Add(dclm);
                    }

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        fieldData[2] = CLeaning_Operation_DLL.Clean_Operation_Marks(fieldData[2]).ToString();
                        csv_Data.Rows.Add(fieldData);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return csv_Data;
        }


        private static void Coonection_insertion(DateTime Start_Date, DateTime End_Date, string Message, string App_E_M)
        {
            SqlConnection con = new SqlConnection("Data Source=bg-in-dspdb1;User Id=testuser;Password=testuser;DataBase=TESTDB");
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Akhilesh_events(Start_Date,End_date,Message,Application_Error_Message) VALUES(@parmtr1,@parmtr2,@parmtr3,@parmtr4)";

            cmd.Parameters.AddWithValue("@parmtr1", Start_Date);
            cmd.Parameters.AddWithValue("@parmtr2", End_Date);
            cmd.Parameters.AddWithValue("@parmtr3", Message);
            cmd.Parameters.AddWithValue("@parmtr4", App_E_M);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private static void Flushing_DB()
        {
            OracleConnection Con_Flush = new OracleConnection("Data Source=INSTGU01;User Id =akumar;Password=pass2017");
            Con_Flush.Open();
            OracleCommand Cmd_Flush = new OracleCommand("truncate table Akhilesh_Marks1", Con_Flush);
            Cmd_Flush.ExecuteNonQuery();

            Cmd_Flush.CommandText = "truncate table Akhilesh_student";
            Cmd_Flush.ExecuteNonQuery();

            Cmd_Flush.CommandText = "truncate table Akhilesh_stats";
            Cmd_Flush.ExecuteNonQuery();

            Con_Flush.Close();

        }
    }
}






