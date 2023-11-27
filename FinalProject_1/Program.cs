using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;
namespace FinalProject_1
{
    class Program
    {
        String connectionString = @"Data Source=DESKTOP-ITM63MP\SQLEXPRESS01;Initial Catalog=Employees;Integrated Security=True";

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Connect_data();
            new Program().Connect_data_2();
            p.Delete_Data();
            Console.ReadKey();
        }
        /// ket noi DataBase
        public void Connect_data()
        {
            var sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                if (sqlConnection.State == ConnectionState.Open)
                {
                    Console.WriteLine("Ket noi thanh cong DataBase bang cach 1");
                }

            }
            catch (Exception e)
            {
                if (sqlConnection.State != ConnectionState.Open)
                {
                    Console.WriteLine("Ket noi khong thanh cong!!!");
                }
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                sqlConnection.Dispose();
            }
        }
        // su dung khoi lenh using ket noi khong can dong
        public void Connect_data_2()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        Console.WriteLine("KET NOI THANH CONG DATABASE BANG CACH 2");
                    }
                }
                catch (Exception e)
                {
                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        Console.WriteLine("DA CO LOI XAY RA!!" + e);
                    }
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }
                    sqlConnection.Dispose();
                }
            }
        }
        // them du lieu vao Bang
        public void Create_data_Table()
        {
            var sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand insert_into_Employee = new SqlCommand
                ("insert into EmployeeTable(EmployeeId,TitleOfCourtesy,FistName,LastName,HireDate) values('102','chaomungbanT','PHAM','MT','3/1/2023'),('103','chaomungbH','HUYNH','TRANHAU','3/12/2023');", sqlConnection);
                if (sqlConnection.State == ConnectionState.Open)
                {
                    insert_into_Employee.BeginExecuteNonQuery();
                    Console.WriteLine("Them du lieu thanh cong!!!");
                }
            }
            catch (Exception e)
            {
                if (sqlConnection.State != ConnectionState.Open)
                {
                    Console.WriteLine("Da co loi xay ra" + e);
                }
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                sqlConnection.Dispose();
            }
        }
        // truy van du lieu trong bang 
        public void Read_Data()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand Cmd_truyvan1 = new SqlCommand("select *from EmployeeTable;", sqlConnection);
                    SqlCommand command_count = new SqlCommand("Select count(EmployeeId) from EmployeeTable;", sqlConnection);
                    sqlConnection.Open();
                    int totalRows = (int)command_count.ExecuteScalar();
                    Console.WriteLine("Tong so NV:" + totalRows);
                    //                     // su dung sqlReadata
                    SqlDataReader sqlDataReader = Cmd_truyvan1.ExecuteReader();
                    Console.WriteLine("Doc bang thanh cong!!!");
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine(sqlDataReader["EmployeeId"] + " " + sqlDataReader["TitleOfCourtesy"] + " " + sqlDataReader["FistName"] + " " + sqlDataReader["LastName"] + " " + sqlDataReader["HireDate"]);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Da co loi xay ra!!" + e);
                }
            }
        }
        public void Insert_Update_data()
        {
            var sqlConnection = new SqlConnection(connectionString);
            try
            {
                SqlCommand command_1 = new SqlCommand("insert into EmployeeTable(EmployeeId,TitleOfCourtesy,FistName,LastName,HireDate) values('104','mungbanK','Nguyen','VanK','2/9/2023')", sqlConnection);
                sqlConnection.Open();
                int count = command_1.ExecuteNonQuery();
                Console.WriteLine("Them cot:" + count);
                Read_Data();
            }
            catch (Exception e)
            {
                Console.WriteLine("Da co loi xay ra!!" + e);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        // xoa du lieu
        public void Delete_Data(){
            var  sqlConnection = new SqlConnection(connectionString);
            try{
                SqlCommand command_delete= new SqlCommand("delete from EmployeeTable where EmployeeId=104;",sqlConnection);
                sqlConnection.Open();
                command_delete.ExecuteNonQuery();
                Console.WriteLine("Xoa du lieu thanh cong!!");
                // doc lai
                Read_Data();
            }catch(Exception e){
                Console.WriteLine("Da co loi xay ra!!" +e);
            }
            finally{
                sqlConnection.Close();
            }
        }
        // update dung sqlCommand("update EmployeeTable set LastName="vothanhdat" where EmployeeId=4);
    }
}