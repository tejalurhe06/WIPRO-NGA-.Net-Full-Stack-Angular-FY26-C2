using System;
using Microsoft.Data.SqlClient;
using System.Data;


namespace adodemo1
{
    class StoredProcedure
    {
        // Methods to execute stored procedures will be added here

        public void ExecuteStoredProcedure()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Person;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand to execute the stored procedure
                using (SqlCommand command = new SqlCommand("Person_Ids", connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters if needed
                    command.Parameters.AddWithValue("@personid", 100);


                    //without parameters
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Id: {reader["id"]}, Name: {reader["firstname"]}");
                    }
                }
            }
        }
    }
}