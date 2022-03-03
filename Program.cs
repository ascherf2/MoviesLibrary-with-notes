using System;
using System.IO;
using System.Collections.Generic;
using NLog.Web;

namespace NewProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            //The string "path" is grabbing the location for nlog.config
            //var logger creates a new NLogger Object using nlog.config
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

            //Prints "Program started" in log_file.txt (log_file.txt is located at the top of the bin folder)
            logger.Info("Program started");

            string file = "movies.csv";
            //Creates a new StreamReader that reads from movies.csv
            StreamReader movieFile = new StreamReader(file);

            //Creates an empty List of strings
            List<string> listOfMovies = new List<string>();

            //While there are lines we haven't read from movieFile...
            while (!movieFile.EndOfStream) {
                //...add the next line to our List of movies
                listOfMovies.Add(movieFile.ReadLine());
            }

            //Practicing with String.Split()
            string fruits = "banana,apple,pear";
            //Splits the "fruits" string up by ',' and puts it in an Array of strings. "threeFruits" holds "banana" "apple" and "pear"
            string[] threeFruits = fruits.Split(',');

            //Prints "banana,apple,pear"
            System.Console.WriteLine(fruits);

            //For each string in threeFruits...
            foreach (string fruit in threeFruits) {
                //...print the string
                System.Console.WriteLine(fruit);
                //Prints out "banana"
                //"apple"
                //"pear"
            }

            //threeFruits.Length returns an int of how many strings are in threeFruits. threeFruits contains "banana" "apple" and "pear" so it returns 3
            System.Console.WriteLine(threeFruits.Length);
            
            
            string resp;
            //A do/while loop runs the block of code at least once, and is used when we don't know how many times it will run
            //A while loop acts like an "if" statement, and is used when we don't know how many times it will run. It may not run at all.
            //A for loop is used when we know how many times we want the code to run, and runs a fixed amount of times unless we tell it to stop
            //A for/each loop is used to iterate through a collection. It runs the block of code on each item in a List/Array/Collection
            do {
                System.Console.WriteLine("Press 1 to read all movies");
                System.Console.WriteLine("Press 2 to add new movies");
                //TODO: Add all movies
                //TODO: Search for movies
                System.Console.WriteLine("Press any other key to exit");
                resp = Console.ReadLine();

                if (resp == "1") {
                    //For each string in listOfMovies...
                    foreach (string movie in listOfMovies) {
                        //...Split the string up by ',' and puts it in an Array of strings called "newLine"
                        string[] newLine = movie.Split(',');
                        //i.e. newLine holds "1", "Toy Story (1995)", and "Adventure|Animation|Children|Comedy|Fantasy"

                        //Grabs the genre(s) and puts it in a string called "genres"
                        string genres = newLine[newLine.Length - 1];
                        //Genre is the last entry in each newLine, so the index is the size (Array.Length) - 1
                        //i.e. the newLine for Toy Story has 3 strings. The genres are at index 2, so newLine.Length (3) - 1 is the index for genres

                        //Split the string "genres" by '|' and puts it in an Array of strings called separatedGenres
                        string[] separatedGenres = genres.Split('|');

                        // TODO: Movie title

                        System.Console.WriteLine($"MovieID: {newLine[0]}\nTitle: {newLine[1]}\nGenres: {String.Join(',', separatedGenres)}");
                        //The movieID is always at index 0 of newLine
                        //String.Join(',', separatedGenres) is saying "Make me a string by joining each item in separatedGenres with ','"
                        //i.e. using separatedGenres for Toy Story would return "Adventure,Animation,Children,Comedy,Fantasy"
                    }
                }
            } while (resp == "1" || resp == "2");

            //Prints "Program ended" in log_file.txt
            logger.Info("Program ended");
        }
    }
}
