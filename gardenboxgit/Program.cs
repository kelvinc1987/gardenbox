using System;
using System.Data.SqlClient;
// in 4*4 a 16 carrots 1*1 box
// beets 4*4 can fit 9
// 4*4 fit corn 3
namespace gardenboxgit
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kelvin\gitsession13\garden-boxes\gardenboxgit\gardenboxgit\Database1.mdf;Integrated Security=True");
            connection.Open();

            Console.WriteLine("Hello user, Welcome to the garden box!");
            Console.WriteLine("What is your name?");
            string name = Console.ReadLine();

            bool keepgoing = true;

            while (keepgoing == true)
            {
                Console.Clear();
                Console.WriteLine($"Hello {name}! Would you like to a)plant a veggie from our database? b) add a veggie to our database? or c) quit");

                displayPlants(connection);

                string decision = Console.ReadLine().ToLower();

                if (decision == "a")
                {

                    Console.Clear();

                    Console.WriteLine("OK great, What veggie would you like to plant?");
                    displayPlants(connection);
                    string userChoice = Console.ReadLine();

                    

                    
                    SqlCommand command3 = new SqlCommand($"SELECT * from PLANTS WHERE [plant name] = '{userChoice}'", connection);
                    SqlDataReader reader3 = command3.ExecuteReader();
                    if (reader3.HasRows)
                    {
                        while (reader3.Read())
                        {

                            

                            getdimensions(userChoice, reader3);
                            
                        }
                    }
                    else
                    {
                        Console.WriteLine("That veggie is not in our database");
                    }
                    reader3.Close();
                }
                else if (decision == "b")
                {
                    updatePlants(connection);  

                }
                else if (decision == "c")
                {
                    keepgoing = false;
                }
                else
                {
                    Console.WriteLine("Wrong input try again");
                }
            }

            Console.WriteLine();

            connection.Close();
            
            static void displayPlants(SqlConnection connection)
            {
                SqlCommand command = new SqlCommand("SELECT * from PLANTS", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["plant name"]);
                    }
                }
                reader.Close();
            }
            static void getdimensions(string userChoice, SqlDataReader reader)
            {
                Console.WriteLine($"{userChoice}!! Cool!!!");
                Console.WriteLine("Now lets get the dimensions of your garden box");

                Console.WriteLine("What is the length of your garden box in feet ?");
                int length = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("What is the width of your garden box in feet?");
                int width = Convert.ToInt32(Console.ReadLine());
                int area = length * width;
                int howmany = area * Convert.ToInt32(reader["plant size"]) / 16;

                Console.WriteLine($"you can plant {howmany} {userChoice}");
            }
            static void updatePlants(SqlConnection connection)
            {
                Console.WriteLine("What is the plant name?");
                string newPlant = Console.ReadLine();
                Console.WriteLine("How big is the plant?");
                int plantdimension = Convert.ToInt32(Console.ReadLine());
                SqlCommand command2 = new SqlCommand($"INSERT INTO Plants ([plant name], [plant size]) VALUES ('{newPlant}', '{plantdimension}')", connection);
                command2.ExecuteNonQuery();

                Console.WriteLine($"{newPlant} has been uploaded");
            }
        }
    }
}