﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core
{

    public abstract class FileReader
    {
        public abstract string path
        {
            get;
            set;
        }
        public abstract List<Target> ReadTargets();
    }
    public class ReaderFactory
    {
       public INIReader CreateReader()
        {
            INIReader reader = null;
            reader = new INIReader();
            return reader;
        }
        public static FileReader CreateReader(ReaderType readerType)
        {
            FileReader reader = null;
            switch(readerType)
            {
                case ReaderType.iniReader:
                    reader = new INIReader();
                    break;
            }
            return reader;
        }

    }
    public enum ReaderType
    {

    iniReader,
    }

    public class INIReader : FileReader
    {
        public override string path
        {
            get;
            set;
        }

        public INIReader()
        {
        }

        public override List<Target> ReadTargets()
        {
            Target target;
            string Name = null;
            double x = 0;
            double y = 0;
            double z = 0;
            bool Frd = true;
            int Pts = 0;
            int FR = 0;
            int SR = 0;
            bool CSSWH = true;
            int[] countFields = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string fileLine;
            int counter = 1;
            string tab = "\t";
            char tabDelim = Convert.ToChar(tab);
            char[] delimiter = { '=', ' ', tabDelim };
            int i = 0;

            // create list of targets
            List<Target> TargetList = new List<Target>();

                // read in file to string array
            string[] lines = System.IO.File.ReadAllLines(this.path);

            // loop through file
            for (counter = 1; counter < lines.Length; counter++)
            {
                fileLine = lines[counter];
                string[] words = fileLine.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

                if (words.Length > 2)
                {
                    if (words[2][0] != '#')
                    {
                        throw new CommentException("Extra string is not a comment");
                    }
                }
                if (words.Length > 0)
                {
                    switch (words[0].ToUpper())
                    {
                        case "NAME":
                            Name = words[1];
                            countFields[0]++;
                            break;
                        case "X":
                            x = Convert.ToDouble(words[1]);
                            countFields[1]++;
                            break;
                        case "Y":
                            y = Convert.ToDouble(words[1]);
                            countFields[2]++;
                            break;
                        case "Z":
                            z = Convert.ToDouble(words[1]);
                            countFields[3]++;
                            break;
                        case "FRIEND":
                            Frd = Convert.ToBoolean(words[1]);
                            countFields[4]++;
                            break;
                        case "POINTS":
                            Pts = Convert.ToInt32(words[1]);
                            countFields[5]++;
                            break;
                        case "FLASHRATE":
                            FR = Convert.ToInt32(words[1]);
                            countFields[6]++;
                            break;
                        case "SPAWNRATE":
                            SR = Convert.ToInt32(words[1]);
                            countFields[7]++;
                            break;
                        case "CANSWAPSIDESWHENHIT":
                            CSSWH = Convert.ToBoolean(words[1]);
                            countFields[8]++;
                            break;
                        case "[TARGET]":
                            for (i = 0; i < countFields.Length; i++)
                            {
                                if (countFields[i] != 1)
                                {
                                    throw new CommentException("Invalid number of target data fields");
                                }
                                countFields[i] = 0;
                            }

                            TimeSpan timeSpan = TimeSpan.MinValue;

                            target = new Target(Name, x, y, z, Frd, Pts, FR, SR, CSSWH,0, timeSpan);
                            // SAD.Core.Spherical.ConvertToSphere(target);
                            TargetList.Add(target);
                            break;
                        default:
                            if (words[0][0] != '#')
                            {
                                throw new CommentException("Extra string is not a comment");
                            }
                            break;
                    }
                }
            }

            // add final target
            for (i = 0; i < countFields.Length; i++)
            {
                if (countFields[i] != 1)
                {
                    throw new CommentException("Invalid number of target fields");
                }
                countFields[i] = 0;
            }
            TimeSpan timeSpan1 = TimeSpan.MinValue;
            target = new Target(Name, x, y, z, Frd, Pts, FR, SR, CSSWH, 0, timeSpan1);         
            TargetList.Add(target);

            return TargetList;
        }
    }
}
