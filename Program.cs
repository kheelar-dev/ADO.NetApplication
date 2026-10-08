using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NetApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Test Database Connection
            using (SqlConnection con = new SqlConnection(DbConnection.ConnectionString))
            {
                try
                {
                    con.Open();

                    Console.WriteLine("DB connection conected!");

                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("==========================");
                        Console.WriteLine("      STAFF MANAGEMENT");
                        Console.WriteLine("==========================");
                        Console.WriteLine("1. Get Staff List");
                        Console.WriteLine("2. Get Staff Detail");
                        Console.WriteLine("3. New Staff");
                        Console.WriteLine("4. Update Staff");
                        Console.WriteLine("5. Delete Staff");
                        Console.WriteLine("0. Exit");
                        Console.WriteLine("==========================");

                        Console.Write("Choose: ");
                        string choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                GetStaffList();
                                break;

                            case "2":
                                GetStaffDetail();
                                break;

                            case "3":
                                NewStaff();
                                break;

                            case "4":
                                UpdateStaff();
                                break;

                            case "5":
                                DeleteStaff();
                                break;

                            case "0":
                                return;

                            default:
                                Console.WriteLine("Invalid choice!");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database Error: " + ex.Message);
                }
            }
        }


        // =========================================
        // GET STAFF LIST
        // =========================================
        static void GetStaffList()
        {
            DataTable dt = new DataTable();

            string sql = @"
            SELECT
                StaffID,
                StaffName,
                JobTitle,
                Salary
            FROM Staffs
            ORDER BY StaffID";

            using (SqlConnection con = new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, con))
                {
                    adapter.Fill(dt);
                }
            }

            Console.WriteLine();
            Console.WriteLine("========== STAFF LIST ==========");

            if (dt.Rows.Count > 0)
            {
               
                Console.WriteLine("+-------+----------------------+-----------------+--------------+");

                Console.WriteLine(
                    "| " +
                    "ID".PadRight(5) + " | " +
                    "Staff Name".PadRight(20) + " | " +
                    "Job Title".PadRight(15) + " | " +
                    "Salary".PadLeft(12) + " |"
                );

                Console.WriteLine("+-------+----------------------+-----------------+--------------+");

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(
                        "| " +
                        row["StaffID"].ToString().PadRight(5) + " | " +
                        row["StaffName"].ToString().PadRight(20) + " | " +
                        row["JobTitle"].ToString().PadRight(15) + " | " +
                        row["Salary"].ToString().PadLeft(12) + " |"
                    );
                }

                Console.WriteLine("+-------+----------------------+-----------------+--------------+");

            }
            else
            {
                Console.WriteLine("No records to display.");
            }
        }


        // =========================================
        // GET STAFF DETAIL
        // =========================================
        static void GetStaffDetail()
        {
            Console.Write("Enter Staff ID: ");

            int staffId = int.Parse(Console.ReadLine());

            string sql = @"
            SELECT
                StaffID,
                StaffName,
                JobTitle,
                Salary,
                CreatedUser,
                CreatedDate
            FROM Staffs
            WHERE StaffID = @StaffID";

            using (SqlConnection con = new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@StaffID",SqlDbType.Int).Value = staffId;

                    con.Open();

                    using (SqlDataReader reader =cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine();
                            Console.WriteLine("========== STAFF DETAIL ==========");

                            Console.WriteLine($"Staff ID     : {reader["StaffID"]}");

                            Console.WriteLine($"Staff Name   : {reader["StaffName"]}");

                            Console.WriteLine($"Job Title    : {reader["JobTitle"]}");

                            Console.WriteLine($"Salary       : {reader["Salary"]}");

                            Console.WriteLine($"Created Date : {reader["CreatedDate"]}");

                            Console.WriteLine($"Created User : {reader["CreatedUser"]}");
                        }
                        else
                        {
                            Console.WriteLine("Staff not found.");
                        }
                    }
                }
            }
        }


        // =========================================
        // NEW STAFF
        // =========================================
        static void NewStaff()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Staff Name: ");
            string staffName = Console.ReadLine();

            Console.Write("Job Title: ");
            string jobTitle = Console.ReadLine();

            Console.Write("Salary: ");
            int salary = int.Parse(Console.ReadLine());

            Console.Write("Created User: ");
            string createdUser = Console.ReadLine();

            string sql = @"
            INSERT INTO Staffs
            (
                StaffName,
                JobTitle,
                Salary,
                CreatedDate,
                CreatedUser
            )
            VALUES
            (
                @StaffName,
                @JobTitle,
                @Salary,
                @CreatedDate,
                @CreatedUser
            )";

            using (SqlConnection con = new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@StaffName",SqlDbType.NVarChar, 50).Value = staffName;

                    cmd.Parameters.Add("@JobTitle",SqlDbType.VarChar, 50).Value = jobTitle;

                    cmd.Parameters.Add("@Salary",SqlDbType.Int).Value = salary;

                    cmd.Parameters.Add("@CreatedDate",SqlDbType.DateTime).Value = DateTime.Now;

                    cmd.Parameters.Add("@CreatedUser",SqlDbType.VarChar, 50).Value = createdUser;

                    con.Open();

                    int result = cmd.ExecuteNonQuery();

                    Console.WriteLine(
                        result > 0
                            ? "Staff created successfully."
                            : "Insert failed."
                    );
                }
            }
        }


        // =========================================
        // UPDATE STAFF
        // =========================================
        static void UpdateStaff()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Staff ID: ");
            int staffId = int.Parse(Console.ReadLine());

            Console.Write("Staff Name: ");
            string staffName = Console.ReadLine();

            Console.Write("Job Title: ");
            string jobTitle = Console.ReadLine();

            Console.Write("Salary: ");
            int salary = int.Parse(Console.ReadLine());

            Console.Write("Updated User: ");
            string updatedUser = Console.ReadLine();

            string sql = @"UPDATE Staffs
                            SET
                                StaffName = @StaffName,
                                JobTitle = @JobTitle,
                                Salary = @Salary,
                                UpdatedDate = @UpdatedDate,
                                UpdatedUser = @UpdatedUser
                            WHERE StaffID = @id";

            using (SqlConnection con =new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlCommand cmd =new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@id",SqlDbType.Int).Value = staffId;

                    cmd.Parameters.Add("@StaffName",SqlDbType.NVarChar, 50).Value = staffName;

                    cmd.Parameters.Add("@JobTitle",SqlDbType.VarChar, 50).Value = jobTitle;

                    cmd.Parameters.Add("@Salary",SqlDbType.Int).Value = salary;

                    cmd.Parameters.Add("@UpdatedDate",SqlDbType.DateTime).Value = DateTime.Now;

                    cmd.Parameters.Add("@UpdatedUser",SqlDbType.VarChar, 50).Value = updatedUser;

                    con.Open();

                    int result = cmd.ExecuteNonQuery();

                    Console.WriteLine(
                        result > 0
                            ? "Staff updated successfully."
                            : "Staff not found."
                    );
                }
            }
        }


        // =========================================
        // DELETE STAFF
        // =========================================
        static void DeleteStaff()
        {
            Console.Write("Staff ID: ");
            int staffId = int.Parse(Console.ReadLine());

            Console.Write("Are you sure? (Y/N): ");
            string confirm = Console.ReadLine();

            if (confirm.ToUpper() != "Y")
            {
                Console.WriteLine("Delete cancelled.");
                return;
            }

            string sql = @"DELETE FROM Staffs WHERE StaffID = @id";

            using (SqlConnection con = new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@id",SqlDbType.Int).Value = staffId;

                    con.Open();

                    int result = cmd.ExecuteNonQuery();

                    Console.WriteLine(
                        result > 0
                            ? "Staff deleted successfully."
                            : "Staff not found."
                    );
                }
            }
        }
    }
}
