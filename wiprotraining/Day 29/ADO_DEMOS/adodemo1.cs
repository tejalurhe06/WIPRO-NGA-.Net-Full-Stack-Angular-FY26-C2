using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace adodemo1
{
    class Program
    {
        /*static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Person;Integrated Security=True";

            //SQLConnection
            // Establishing a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Perform database operations
                Console.WriteLine("Connection to the database established successfully.");

                connection.Close();
                Console.WriteLine("Connection to the database closed.");
            }

            //SQLDataReader
            // Using SqlDataReader to read data from the database

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connection to the database established successfully for data reading.");

                using (SqlCommand command = new SqlCommand("SELECT * FROM Person", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id"]}, Name: {reader["firstname"]}");
                        }
                    }
                }

                connection.Close();
                Console.WriteLine("Connection to the database closed after reading data.");
            }

            //SQLDataAdapter
            // Using SqlDataAdapter to fill a DataTable 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connection to the database established successfully for data adapter.");

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Person", connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine($"ID: {row["id"]}, Name: {row["firstname"]} , LastName: {row["lastname"]}, Age: {row["age"]}");
                }

                connection.Close();
                Console.WriteLine("Connection to the database closed after using data adapter.");
            }

            //SQLCommand
            // Using SqlCommand to execute a query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connection to the database established successfully for command execution.");

                //insert record
                // using (SqlCommand command = new SqlCommand("INSERT INTO Person (id,firstname,lastname,age) VALUES (400, 'Kanishka', 'Borole', 25)", connection))
                // {
                //     int rowsAffected = command.ExecuteNonQuery();
                //     Console.WriteLine($"{rowsAffected} row(s) inserted.");
                // }

                //update record
                using (SqlCommand command = new SqlCommand("UPDATE Person SET lastname = 'Unknown' WHERE id = 100", connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) updated.");
                }

                //delete record 
                using (SqlCommand command = new SqlCommand("DELETE FROM Person WHERE id in (1,5)", connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) deleted.");
                }

                connection.Close();
                Console.WriteLine("Connection to the database closed after executing command.");
            }
        }*/
    }
}