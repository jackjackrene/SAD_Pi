using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // check that the correct number of input arguments are entered
            if (args.Length > 1)
            {
                System.Console.WriteLine("Too many arguments.");
                return;
            }
            else if (args.Length == 0)
            {
                System.Console.WriteLine("No file name entered.");
                return;
            }

            // initialize values
            string targetFile = @args[0];                   // Target File as read from command line
            List<Target> TargetList = new List<Target>();   // List of targets
            INIReader reader;                               // reader for INI files
            PigLatinWriter writer;                          // writer for Pig Latin files
            string UserPrompt = " ";                        // String variable to store user prompt
            int listSize = 0;                               // size of target list
            char[] delimiter = { ' ' };                     // delimiter for user input
            Target currentTarget = new Target();            // temporary target

            // Test that target file exists
            if (!File.Exists(@targetFile))
            {
                Console.WriteLine("File does not exist");
                return;
            }

            // Initialize reader
            reader = new INIReader(@targetFile);

            // Read file, input to target list
            try
            {
                TargetList = reader.ReadTargets();
            }
            catch (Exception badTargetFile)
            {
                Console.WriteLine("Exception Caught: " + badTargetFile.Message);
                return;
            }

            listSize = TargetList.Count;

            // List command options for user
            Console.WriteLine("Valid Commands: PRINT, PRINT SORT, PRINT <target name>,");
            Console.WriteLine("CONVERT <output file name>, ISFRIEND <target name>, EXIT");

            // Loop until user decides to exit program
            while (UserPrompt.ToUpper() != "EXIT")
            {
                // user prompt
                Console.Write("Enter command: ");
                UserPrompt = Console.ReadLine();

                // split prompt
                string[] words = UserPrompt.Split(delimiter);

                if (words.Length > 2)
                {
                    words[0] = "TOOMANYARGUMENTS";
                }

                // check and process user input
                switch (words[0].ToUpper())
                {
                    case "PRINT":
                        // if user selected "PRINT" only
                        if (words.Length == 1)
                        {
                            // print list of targets
                            for (int i = 0; i < listSize; i++)
                            {
                                Console.WriteLine(TargetList[i].Name);
                            }
                            // if user selected "PRINT SORT"
                        }
                        else if (words[1].ToUpper() == "SORT")
                        {
                            // sort list of targets
                            var sortedList = TargetList.OrderBy(x => x.Name).ToList();
                            // print list of sorted targets
                            for (int i = 0; i < listSize; i++)
                            {
                                Console.WriteLine(sortedList[i].Name);
                            }
                            // if user selected "PRINT" then target name
                        }
                        else
                        {
                            // check if target exists
                            if (TargetList.Exists(x => x.Name.ToUpper() == words[1].ToUpper()))
                            {
                                // grab target and print
                                currentTarget = TargetList.Find(x => x.Name.ToUpper() == words[1].ToUpper());
                                currentTarget.PrintTarget();
                            }
                            else
                            {
                                // if target does not exist
                                Console.WriteLine("Target does not exist");
                            }
                        }
                        break;
                    // if user selects "CONVERT"
                    case "CONVERT":
                        // check if output file was given
                        if (words.Length == 1)
                        {
                            Console.WriteLine("Need an output file name. Try again.");
                        }
                        else
                        {
                            // write Pig Latin file
                            writer = new PigLatinWriter(words[1]);
                            writer.ConvertFile(targetFile);
                        }
                        break;
                    // if user selects "ISFRIEND"
                    case "ISFRIEND":
                        // check to see if user  named a target
                        if (words.Length == 1)
                        {
                            Console.WriteLine("Need a target name. Try again.");
                        }
                        else
                        {
                            // check to see if target exists
                            if (TargetList.Exists(x => x.Name.ToUpper() == words[1].ToUpper()))
                            {
                                // grab target, test if friend or foe
                                currentTarget = TargetList.Find(x => x.Name.ToUpper() == words[1].ToUpper());
                                if (currentTarget.Friend)
                                {
                                    Console.WriteLine("Aye Captain!");
                                }
                                else
                                {
                                    Console.WriteLine("Nay, Scallywag!");
                                }
                            }
                            else
                            {
                                // target is not in list
                                Console.WriteLine("Target does not exist");
                            }
                        }
                        break;
                    // user inputs too many arguments
                    case "TOOMANYARGUMENTS":
                        Console.WriteLine("Too many arguments inputed");
                        break;
                    // user selects "EXIT"
                    case "EXIT":
                        break;
                    // check for if user entered a command that is not listed
                    default:
                        Console.WriteLine("invalid command");
                        break;
                }
            }
        }
    }
}
