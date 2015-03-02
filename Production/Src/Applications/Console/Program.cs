using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAD.Core;
using BuildDefender;
using SAD.Core.Devices;
using SAD.Core.Devices.Missile_Launcher.Subclasses;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize values
            string targetFile;
            List<SAD.Core.Target> TargetList = new List<SAD.Core.Target>();
            string UserPrompt = " ";
            int listSize = 0;
            char[] delimiter = { ' ' };
            SAD.Core.Target currentTarget = new SAD.Core.Target();
            double phi = 0;
            double theta = 0;

            // initialize objects
            ReaderType readerType = ReaderType.iniReader;
            SAD.Core.FileReader reader = null;
            reader = SAD.Core.ReaderFactory.CreateReader(readerType);
            SAD.Core.Devices.SADMissileLauncherFactory missileLauncherFactory = SAD.Core.Devices.SADMissileLauncherFactory.GetInstance();
            SAD.Core.TargetManager targetManager = SAD.Core.TargetManager.GetInstance();

            // Create a SADDreamCheekyMissileLauncher
            SADMissileLauncher missileLauncher = missileLauncherFactory.CreateSADMissileLauncher(SAD.Core.Devices.SADMissileLauncher.SADMissileType.DreamCheekyMissileLauncher);

            // List command options for user
            System.Console.WriteLine("System Loaded");
            System.Console.WriteLine("Ready to Fire Ze Missiles");
            System.Console.WriteLine("Valid Commands: FIRE, MOVE <phi theta>, MOVEBY <phi theta>,");
            System.Console.WriteLine("RELOAD, LOAD <filepath>, SCOUNDRELS, FRIENDS, KILL <targetname>");
            System.Console.WriteLine("STATUS, EXIT");

            // Loop until user decides to exit program
            while (UserPrompt.ToUpper() != "EXIT")
            {
                // user prompt
                System.Console.Write("Enter command: ");
                UserPrompt = System.Console.ReadLine();

                // split prompt
                string[] words = UserPrompt.Split(delimiter);

                if (words.Length > 3)
                {
                    words[0] = "TOOMANYARGUMENTS";
                }

                // check and process user input
                switch (words[0].ToUpper())
                {
                    // if user selects "FIRE"
                    case "FIRE":
                        missileLauncher.Fire();
                        break;
                    // if user selects "MOVE"
                    case "MOVE":
                        if (words.Length != 3)
                        {
                            System.Console.WriteLine("Incorrect number of arguments");
                        } else
                        {
                            phi = Convert.ToDouble(words[1]);
                            theta = Convert.ToDouble(words[2]);
                            missileLauncher.Move(phi, theta);
                        }
                        break;
                    // if user selects "MOVEBY"
                    case "MOVEBY":
                        if (words.Length != 3)
                        {
                            System.Console.WriteLine("Incorrect number of arguments");
                        } else
                        {
                            phi = Convert.ToDouble(words[1]);
                            theta = Convert.ToDouble(words[2]);
                            missileLauncher.MoveBy(phi, theta);
                        }
                        break;
                    // if user selects "RELOAD"
                    case "RELOAD":
                        missileLauncher.Reload();
                        break;
                    // if user selects "LOAD"
                    case "LOAD":
                        targetFile = words[1];
                        // Test that target file exists
                        if (!File.Exists(@targetFile))
                        {
                            System.Console.WriteLine("File does not exist");
                        }

                        // Initialize reader
                        reader.path = @targetFile;

                        // Read file, input to target list
                        try
                        {
                            TargetList = reader.ReadTargets();
                        }
                        catch (Exception badTargetFile)
                        {
                            System.Console.WriteLine("Exception Caught: " + badTargetFile.Message);
                            return;
                        }
                        targetManager.TargetList = TargetList;
                        break;
                    // if user selects "SCOUNDRELS"
                    case "SCOUNDRELS":
                        TargetList = targetManager.GetEnemies.ToList();
                        listSize = TargetList.Count;
                        for (int i = 0; i < listSize; i++)
                        {
                            System.Console.WriteLine("Target: {0}", TargetList[i].Name);
                            System.Console.WriteLine("Friend: Not anymore.");
                            System.Console.WriteLine("Position: x={0}, y={1}, z={2}", TargetList[i].X, TargetList[i].Y, TargetList[i].Z);
                            System.Console.WriteLine("Points: {0}", TargetList[i].Points);
                            System.Console.WriteLine("Status: At Large");
                        }
                        break;
                    // if user selects "FRIENDS"
                    case "FRIENDS":
                        TargetList = targetManager.GetFriends.ToList();
                        listSize = TargetList.Count;
                        for (int i = 0; i < listSize; i++)
                        {
                            System.Console.WriteLine("Target: {0}", TargetList[i].Name);
                            System.Console.WriteLine("Friend: For now.");
                            System.Console.WriteLine("Position: x={0}, y={1}, z={2}", TargetList[i].X, TargetList[i].Y, TargetList[i].Z);
                            System.Console.WriteLine("Points: {0}", TargetList[i].Points);
                            System.Console.WriteLine("Status: At Large");
                        }
                            break;
                    // if user selects "KILL"
                    case "KILL":
                        if (words.Length != 2)
                        {
                            System.Console.WriteLine("Incorrect number of arguments");
                        }
                        else
                        {
                            if (TargetList.Exists(x => x.Name.ToUpper() == words[1].ToUpper()))
                            {
                                // grab target
                                currentTarget = TargetList.Find(x => x.Name.ToUpper() == words[1].ToUpper());
                                SAD.Core.Spherical.ConvertToSphere(currentTarget);
                                missileLauncher.Kill(currentTarget.Phi, currentTarget.Theta);
                            } else
                            {
                                // if target does not exist
                                System.Console.WriteLine("Target does not exist");
                            }
                        }
                        break;
                    // if user selects "STATUS"
                    case "STATUS":
                        System.Console.WriteLine("Launcher: DreamCheeky");
                        System.Console.WriteLine("Missiles: {0} of 4 remain", missileLauncher.CurrentMissileCount);
                        break;
                    // if user selects "EXIT"
                    case "EXIT":
                        break;
                    // user inputs too many arguments
                    case "TOOMANYARGUMENTS":
                        System.Console.WriteLine("Too many arguments inputed");
                        break;
                    // check for if user entered a command that is not listed
                    default:
                        System.Console.WriteLine("invalid command");
                        break;
                }
            }
        }
    }
}
