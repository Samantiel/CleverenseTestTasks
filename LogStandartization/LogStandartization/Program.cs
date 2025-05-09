using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogStandartization
{
    internal class Program
    {
        static void ErorLog(string log)
        {
            using (StreamWriter writer = new StreamWriter("ErrorLogs.txt", true))
            {
                writer.WriteLine(log);
            }
        }
        static void Main(string[] args)
        {
            List<string> Logs = new List<string>();

            using (StreamReader reader = new StreamReader("LogFile.txt"))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    Logs.Add(line);
                }
            }

            using (StreamWriter writer = new StreamWriter("LogFile.txt", false))
            {
                foreach (string log in Logs)
                {
                    bool error = false;
                    string[] LogBreaked;
                    string StandartisizedLog = "";
                    if (Regex.IsMatch(log, @"^\d\d\.\d\d\.\d\d\d\d"))
                    {
                        LogBreaked = log.Split(' '); 
                        if (LogBreaked.Length < 4)
                        {
                            ErorLog(log);
                            continue;
                        }
                        for (int i = 0; i < LogBreaked.Length; i++)
                        {
                            if (i == 0)
                            {
                                LogBreaked[i] = LogBreaked[i].Replace('.', '-');
                            }

                            if (i == 1)
                            {
                                if (!Regex.IsMatch(LogBreaked[i], @"^\d\d:\d\d:\d\d\.\d+"))
                                {
                                    ErorLog(log);
                                    error = true;
                                    break;
                                }
                            }

                            if (i == 2)
                            {
                                switch (LogBreaked[i])
                                {
                                    case "INFORMATION":
                                        LogBreaked[i] = "INFO"; break;
                                    case "WARNING":
                                        LogBreaked[i] = "WARN"; break;
                                    case "ERROR":
                                        break;
                                    case "DEBUG":
                                        break;
                                     default:
                                        ErorLog(log); continue;
                                }
                            }

                            if (i < 3)
                                StandartisizedLog += $"{LogBreaked[i]}\t";
                            else if (i == 3)
                            {
                                if (!Regex.IsMatch(LogBreaked[i], @"^a-zA-z+"))
                                {
                                    StandartisizedLog += $"DEFAULT \t{LogBreaked[i]} ";
                                }
                                else
                                    StandartisizedLog += $"{LogBreaked[i]}\t";
                            }
                            else
                            {
                                StandartisizedLog += $"{LogBreaked[i]} ";
                            }
                        }
                    }
                    else if (Regex.IsMatch(log, @"^\d\d\d\d-\d\d-\d\d"))
                    {
                        LogBreaked = log.Split('|');
                        if (LogBreaked.Length < 5)
                        {
                            ErorLog(log);
                            continue;
                        }
                        for (int i = 0; i < LogBreaked.Length; i++)
                        {
                            if (i == 0)
                            {
                                string year = LogBreaked[i].Substring(0, 4);
                                string day = LogBreaked[i].Substring(8, 2);
                                string time = LogBreaked[i].Substring(12);
                                if (!Regex.IsMatch(LogBreaked[i], @"^\d\d:\d\d:\d\d\.\d+"))
                                {
                                    ErorLog(log);
                                    error = true;
                                    break;
                                }
                                LogBreaked[i] = LogBreaked[i].Remove(0, 4);
                                LogBreaked[i] = day + LogBreaked[i].Replace(day, year);
                                LogBreaked[i] = LogBreaked[i].Replace(' ', '\t');
                            }

                            if (i == 1)
                            {
                                switch (LogBreaked[i])
                                {
                                    case "INFO":
                                        break;
                                    case "WARN":
                                        break;
                                    case "ERROR":
                                        break;
                                    case "DEBUG":
                                        break;
                                    default:
                                        ErorLog(log);
                                        continue;
                                }
                            }

                            if (i == 2)
                                continue;

                            if (i < 2)
                            {
                                StandartisizedLog += $"{LogBreaked[i]}\t";
                            }
                            else
                            if (i == 3)
                            {
                                if (!Regex.IsMatch(LogBreaked[i], @"[^a-zA-z]+"))
                                {
                                    StandartisizedLog += $"DEFAULT \t{LogBreaked[i]} ";
                                }
                                else
                                    StandartisizedLog += $"{LogBreaked[i]}\t";
                            }
                            else
                            {
                                StandartisizedLog += $"{LogBreaked[i]} ";
                            }
                        }
                    }
                    else
                        ErorLog(log);

                    if (error)
                        continue;

                    writer.WriteLine(StandartisizedLog);
                }
            }

        }
    }
}
