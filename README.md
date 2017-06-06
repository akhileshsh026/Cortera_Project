# Cortera_Project
Mini Project 

Create a Console application to do the following tasks.

Summary:
There will be 2 CSV files containing information about Students & their marks in several subjects. These information needs to be extracted from CSV files and loaded into Oracle tables after some cleaning. A set of statistics needs to be calculated and populated in another Oracle table.
Additionally, each run of the program should insert a Event record in a SQL Server table with all Event related information.
Some of the steps needs to be coded into a separate DLL and used in the main EXE.
Detailed specifications:

1.	First CSV file with file name <YourName>_Student.csv contains the following fields.
Student_ID, Name, Address, City_State_Postalcode
Note that city,state & postalcode info are joined into the same field with ‘_’ between them.
Example: Bangalore_KA_560001
Create a minimum of 10 records in this file.

2.	Second CSV file with file name <YourName>_Marks.csv contains the following fields – 
Student_ID, Sub_Code, marks
Have at-least 3 records in this CSV for each student row.

3.	The field delimiter is comma for the above files. Enclose each field data within double quotes. 

4.	The data from these 2 CSV files needs to be read into respective DataTable

5.	Perform data cleaning as follows – 
a.	Convert all string Data to capitals.
b.	Strip any spaces before & after the data.
c.	Any double spaces/tabs inside the data needs to be converted to single space.
d.	Remove all junk characters from the data except the following – Alphabets, Numbers, Comma, Hash(#), Period(.)
e.	Trim the data to fit into individual DB column as per their lengths.
f.	Any non-numerical data in Marks field should be interpreted as Zero.

All the cleaning logic in Step 5 should be coded & compiled into a separate DLL. Link this DLL to your main EXE and call the respective function to do cleaning task.

6.	Load the Student data into the following Oracle table – 
<YourName>_Student (Student_Id varchar2(5), Name varchar2(100), Address varchar2(200), City varchar2(50), State varchar2(2), Postalcode varchar2(6))
Student Id should be the primary key. Name should be NOT NULL
The joined City_State_Postalcode data present in CSV should be separated and populated into individual fields.

7.	Load the Student marks data into the following Oracle table – 
<YourName>_Marks (Student_Id varchar2(5), Sub_code number(2), Marks number(5))
Student_Id should be a foreign key and refer Student_id field in student table.
Student_Id+Sub_code should be unique.

8.	Calculate the statistics and load them into the following Oracle table –
<YourName>_stats (Stats_desc varchar2(100), stats_value number(5), student_id varchar2(5), Sub_code number(2))
Statistics to be calculated:
a.	Student with the highest & lowest average marks. (sub_code will be NULL)
b.	Student with the highest & lowest marks in each subject.
c.	Average marks of all students in each subject. (student_id will be NULL)
d.	Average marks of all students in all subjects. (student_id & sub_code will be NULL)

9.	Every time the application is run, you need to populate a new record in the following table in SQL Server database– 
<YourName>_events (Event_Id number, Start_Date date, End_Date date, Message varchar(200))
Event_Id should be a sequence number and get auto-generated & populated for every run.
Start_date= Date & Time when the program starts.
End_date= Date & Time when the program completes all steps.
Message=SUCCESS if no errors. If any error occurs, populate appropriate error message here.


10.	Eventually, output the statistics into a CSV file by name <YourName>_stats.csv. The file should have the following fields – 
Event_Id, Stats_desc, stats_value, student_id, sub_code

11.	Additional points:
a.	Prepare the data in input CSV files manually and include a variety of data that covers all scenarios. While testing, we will try giving different values in CSV and the application should handle them appropriately.
b.	The CSV files should be present & read from the same directory where EXE/DLL file is present.
c.	Handle any error that occurs during program execution & put appropriate error messages. Capture this error in SQL Server events table. Some of the possible handled errors – CSV file not found, no data in CSV, wrong number of fields in CSV etc. You can have more if required.
d.	Use ADO.NET wherever possible.

