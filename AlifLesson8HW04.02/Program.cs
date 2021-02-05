using System;
using System.Data.SqlClient;

namespace AlifLesson8HW04._02
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = @"Data source = DESKTOP-SS5TGJO\SQLEXPRESS; initial catalog = Person; Integrated security = true;";
            SqlConnection connection = new SqlConnection(conString);
            try
            {
                connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Succesfully Connected to Person");
                }                
                Console.WriteLine(@"Please choose action: 
                                 1. Select all
                                 2. Insert
                                 3. Select By Id
                                 4. Update 
                                 5. Delete ");

                SqlCommand command = connection.CreateCommand();
                int action = Convert.ToInt32(Console.ReadLine());
                switch (action)
                {
                    case 1:
                    command.CommandText = $"Select * from Person";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["ID"]}, LastName: {reader["LastName"]}, " +
                            $"FirstName: {reader["FirstName"]}, MiddleName: {reader["MiddleName"]}, BirthDate: {reader["BirthDate"]}, ");
                    }
                    break;
                    case 2:
                        var LastName = ConsoleReadLineWithText("Enter LastName");
                        var FirstName = ConsoleReadLineWithText("Enter FirstName");
                        var MiddleName = ConsoleReadLineWithText("Enter MiddleName");
                        var BirthDate = ConsoleReadLineWithText("Enter BirthDate");

                        command.CommandText = "Insert into Person(" +
                            "LastName," +
                            "FirstName," +
                            "MiddleName," +
                            "BirthDate ) Values(" +
                            $"'{LastName}'," +
                            $"'{FirstName}'," +
                            $"'{MiddleName}'," +
                            $"'{BirthDate}')"
                            ;
                        var result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            
                            Console.WriteLine("Person successfully added!");
                            
                        }
                        break;
                    case 3:
                        int action1 = Convert.ToInt32(Console.ReadLine());
                        command.CommandText = $"select * from person where id = {action1}";
                        var reader2 = command.ExecuteReader();

                        while (reader2.Read())
                        {
                            Console.WriteLine($"ID: {reader2["ID"]}, LastName: {reader2["LastName"]}, " +
                                $"FirstName: {reader2["FirstName"]}, MiddleName: {reader2["MiddleName"]}, BirthDate: {reader2["BirthDate"]},");
                        }
                        break;
                    case 4:                                                
                        var updateLastName = ConsoleReadLineWithText("Enter LastName");
                        var updateFirstName = ConsoleReadLineWithText("Enter FirstName");
                        var updateMiddleName = ConsoleReadLineWithText("Enter MiddleName");
                        var updateBirthDate = ConsoleReadLineWithText("Enter BirthDate");
                        var updateID = ConsoleReadLineWithText("Enter ID");

                        command.CommandText = "update Person set " +
                            "LastName = " + $"'{updateLastName}'," +
                            "FirstName =  " + $"'{updateFirstName}'," +
                            "MiddleName = " + $"'{updateMiddleName}'," +
                            "BirthDate = " + $"'{updateBirthDate}'" +

                            "where id = " + $"'{updateID}'"
                            ;
                        var result1 = command.ExecuteNonQuery();
                        if (result1 > 0)
                        {
                            Console.WriteLine("Person successfully updated!");
                        }
                        break;
                    case 5:
                        var delete = ConsoleReadLineWithText("Delete ID");
                        command.CommandText = $"delete from Person where id = {delete}";
                        var reader4 = command.ExecuteNonQuery();
                        if (reader4 > 0)
                        {
                            Console.WriteLine("Person deleted Succesfully");
                        }
                        break;
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Disconnected");
            }
            Console.ReadKey();
        }
        public static string ConsoleReadLineWithText(string text)
        {
            Console.Write($"{text}:");
            return Console.ReadLine();
        }
    }

}
