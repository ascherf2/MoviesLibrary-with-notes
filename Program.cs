using System;
using System.IO;
using System.Collections.Generic;
using NLog.Web;

namespace NewProgram
{
    class Program
    {
        public static void Main(string[] args)
        {
            //The string "path" is grabbing the location for nlog.config
            //var logger creates a new NLogger Object using nlog.config
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

            //Prints "Program started" in log_file.txt (log_file.txt is located at the top of the bin folder)
            logger.Info("Program started");

            string file = "movies.csv";

            //Creates an empty List of strings
            List<string> listOfMovies = new List<string>();

            try {
                //Creates a new StreamReader that reads from movies.csv
                StreamReader movieFile = new StreamReader(file);

                //While there are lines we haven't read from movieFile...
                while (!movieFile.EndOfStream) {
                    //...add the next line to our List of movies
                    listOfMovies.Add(movieFile.ReadLine());
                }

                //Close our movieFile StreamReader since we aren't using it
                movieFile.Close();
            } catch (FileNotFoundException e) {
                System.Console.WriteLine("File not found");
                logger.Error("File not found exception");
            } catch (Exception e) {
                System.Console.WriteLine(e.Message);
                logger.Error(e.Message);
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

                        string movieTitle = "";
                        //If the movie has a " in it, we know there's a comma
                        if (movie.Contains('"')) {
                            //"movie" is the entire line of a movie entry. For example, we have 164983,"Is a movie title, This",Horror|Tragedy
                            //We know the movie title is in between the quotation marks, so if we chop those off, we have our title

                            //First, we need to find the index of the first "
                            //String.IndexOf() returns the index of the first character we passed in
                            //In 164983,"Is a movie title, This",Horror|Tragedy, .IndexOf('"') returns 7
                            //In that case index = 7
                            int index = movie.IndexOf('"');

                            //String.Substring(index) returns everything at and after the index we give it
                            //"164983,"Is a movie title, This",Horror|Tragedy".Substring(index) returns "Is a movie title, This",Horror|Tragedy
                            //We don't want the first ", so we use .Substring(index + 1) to get Is a movie title, This",Horror|Tragedy
                            //movieTitle now equals Is a movie title, This",Horror|Tragedy
                            movieTitle = movie.Substring(index + 1);

                            //Now we need to chop off the other " and everything after it
                            //We can find the " again by using .IndexOf('"')
                            index = movieTitle.IndexOf('"');

                            //String.Substring(index, index) returns everything in a string starting at and including the first index, and everything up to but NOT including the second
                            //"Is a movie title, This",Horror|Tragedy".Substring(0, index) returns Is a movie title, This
                            //That's because it grabs everything from index 0 (The first 'I') up until the index of '"'
                            movieTitle = movieTitle.Substring(0, index);
                        } 
                        //If there isn't a " in the movie line, we don't need to worry about commas
                        else {
                            //The movie title would be at index 1 of our newLine array
                            movieTitle = newLine[1];
                        }
                
                        //Now we can concatenate everything into a string to print
                        //If we put $ before the "", we can use curly braces{} to add in variables
                        System.Console.WriteLine($"MovieID: {newLine[0]}\nTitle: {movieTitle}\nGenres: {String.Join(',', separatedGenres)}");
                        //The movieID is always at index 0 of newLine
                        //String.Join(',', separatedGenres) is saying "Make me a string by joining each item in separatedGenres with ','"
                        //i.e. using separatedGenres for Toy Story would return "Adventure,Animation,Children,Comedy,Fantasy"
                    }
                }

                if (resp == "2"){
                    string newMovieTitle, newMovieGenres;
                    int newMovieID;

                    System.Console.WriteLine("Enter new movie title: ");
                    newMovieTitle = Console.ReadLine();

                    //We make a boolean to keep track of whether the newMovieTitle that the user entered exists or not
                    bool newMovieExists = false;
                    //For each string in listOfMovies...
                    foreach (string movie in listOfMovies) {
                        //...Split the string up by ',' and put the strings into an array called newLine
                        string[] newLine = movie.Split(',');

                        //The same code as above. We're grabbing the movieTitle for each string in listOfMovies
                        string movieTitle;
                        if (movie.Contains('"')) {
                            int index = movie.IndexOf('"');
                            movieTitle = movie.Substring(index + 1);

                            index = movieTitle.IndexOf('"');
                            movieTitle = movieTitle.Substring(0, index);
                        } else {
                            movieTitle = newLine[1];
                        }

                        //Once we have our movie title, we can compare it to the title that the user entered
                        //If the movie title exists already...
                        if (movieTitle == newMovieTitle) {
                            //Tell the user it exists
                            System.Console.WriteLine("Duplicate entry");
                            //Log that a duplicate movie title was entered
                            logger.Info("Duplicate entry for Movie Title");
                            //The movie exists already, so we need to change the boolean we made earlier to "true"
                            newMovieExists = true;
                            //We don't need to keep running this loop, so we can break out of it
                            break;
                        }
                    }

                    //If newMovieExists is false, we know that it never found a match in our foreach loop
                    //Therefore it's safe to add the movie
                    if (!newMovieExists) {
                            //Ask the user for genres
                            System.Console.WriteLine("Enter new movie genres separated by '|': ");
                            newMovieGenres = Console.ReadLine();

                            //We need the newMovieID
                            //The last entry in our listOfMovies has the highest movieID, so if we add 1 to it, we know we have a completely new ID
                            //The index of the last movie in our listOfMovies is the Count minus 1
                            string lastMovie = listOfMovies[listOfMovies.Count - 1];

                            //Splitting the lastMovie up by commas and putting the strings in an array called lastMovieArray
                            string[] lastMovieArray = lastMovie.Split(',');

                            //The lastMovieID is at the first index of our lastMovieArray, since the movieID is always first
                            //Since it's a string, we need to Parse it into an int
                            int lastMovieID = Int32.Parse(lastMovieArray[0]);

                            //Now that we have our lastMovieId, we can add 1 to it for our newMovieID
                            newMovieID = lastMovieID + 1;

                            //If the newMovieTitle contains a comma, we want to surround it with quotation marks
                            if (newMovieTitle.Contains(',')) {
                                //Redeclaring the newMovieTitle by concatenating " around it
                                //Since we're using "'s, we need to use the backslash \ to tell our program that the next character is text
                                newMovieTitle = "\"" + newMovieTitle + "\"";
                            }

                            //A movie line is "movieID,movieTitle,genres"
                            //We can make a whole newMovie line by concatenating
                            string newMovie = newMovieID + "," + newMovieTitle + "," + newMovieGenres;

                            //Add our new movie to our listOfMovies
                            listOfMovies.Add(newMovie);

                            try {
                                //Making a new StreamWriter object called addMovie that writes to movies.csv
                                //We need to pass in "true" so that the StreamWriter appends
                                StreamWriter addMovie = new StreamWriter(file, true);

                                //Using the StreamWriter addMovie, write a new line that contains our newMovie
                                addMovie.WriteLine(newMovie);

                                //Close our StreamWriter
                                addMovie.Close();

                                //Tell the user that their movie was added
                                System.Console.WriteLine("Movie successfully added");
                                //Log that their movie was added
                                logger.Info("Movie added successfully");
                            } catch (FileNotFoundException e) {
                                System.Console.WriteLine("File not found");
                                logger.Error("File not found exception");
                            } catch (Exception e) {
                                System.Console.WriteLine(e.Message);
                                logger.Error(e.Message);
                            }
                        }
                }
            } while (resp == "1" || resp == "2");

            //Prints "Program ended" in log_file.txt
            logger.Info("Program ended");
        }
    }
}
