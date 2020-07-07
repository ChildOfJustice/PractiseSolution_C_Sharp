using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEVENTH_CS
{
    class Program
    {
        static void CreateQuestions(DirectoryInfo dirInfoForQuestions, DirectoryInfo dirInfoForGroups, DirectoryInfo destinationDir, string destinationDirName)
        {

            if (!dirInfoForQuestions.Exists)
            {
                throw new FileNotFoundException($"There is no such directory: {dirInfoForQuestions}");
            }
            if (!dirInfoForGroups.Exists)
            {
                throw new FileNotFoundException($"There is no such directory: {dirInfoForGroups}");
            }
            if (!destinationDir.Exists)
            {
                throw new FileNotFoundException($"There is no such directory: {destinationDir}");
            }

            var questionFiles = MakeQuestionFilesArray(dirInfoForQuestions);
            var availableQuestions = new LinkedList<FileInfo>(questionFiles);
            Console.WriteLine($"Available Questions: {availableQuestions.Count}");

            var groupsFiles = MakeGroupFilesArray(dirInfoForGroups);


            foreach (var groupFile in groupsFiles)
            {
                //FileStream sourceStream = File.Open(groupFileName, FileMode.Open);
                int counterForIdenticalFIOs = 0;
                using (StreamReader sr = new StreamReader(File.Open(groupFile.FullName, FileMode.Open)))
                {
                    var allFIOs = new List<List<string>>();
                    string firstLine;
                    Console.WriteLine($"All students from {groupFile.Name}:");
                    while ((firstLine = sr.ReadLine()) != null)
                    {
                        allFIOs.Add(CreateFIO(firstLine));
                        Console.WriteLine($"    {firstLine}");
                    }
                    Console.WriteLine();
                    foreach (var currentFIO in allFIOs)
                    {
                        //Get a random question:
                        Random rnd = new Random(new DateTime().Millisecond);
                        int rand = rnd.Next() % availableQuestions.Count;
                        var item = availableQuestions.First;
                        for (int i = 0; i < rand; i++)
                        {
                            item = item.Next;
                        }
                        var randQuestionFile = item.Value;
                        availableQuestions.Remove(item);
                        //^


                        //creating appropriate result file name 
                        int identicalCounter = 0;
                        var appropriateResultFileName = new StringBuilder();
                        appropriateResultFileName.Append(groupFile.Name);
                        appropriateResultFileName.Append("_");
                        var fullyIdentical = false;
                        var partiallyIdentical = false;

                        List<string> theMostIdenticalFIO = null;
                        int maxIdenticalLetters = 0;
                        foreach (var fio in allFIOs)
                        {

                            if (currentFIO[0] == fio[0] && currentFIO[1] == fio[1] && currentFIO[2] == fio[2])
                            {
                                //fully
                                identicalCounter++;
                                if (identicalCounter != 2)
                                {
                                    continue;
                                }
                                Console.WriteLine("FULLY IDENTICAL <----------(!)");
                                fullyIdentical = true;
                                counterForIdenticalFIOs++;
                                appropriateResultFileName.Append(currentFIO[0]);
                                appropriateResultFileName.Append("_");
                                appropriateResultFileName.Append(currentFIO[1]);
                                appropriateResultFileName.Append(" ");
                                appropriateResultFileName.Append(currentFIO[2]);
                                appropriateResultFileName.Append("_");
                                appropriateResultFileName.Append(counterForIdenticalFIOs);
                                break;
                            }
                            if (currentFIO[0] == fio[0] && currentFIO[1][0] == fio[1][0] && currentFIO[2][0] == fio[2][0])
                            {
                                //partially
                                partiallyIdentical = true;
                                int identicalLetters = 0;

                                ///var nameAndPatronymic = 

                                for (int i = 0; i < Math.Max(currentFIO[1].Length, fio[1].Length); i++)
                                {
                                    if (i == currentFIO[1].Length || i == fio[1].Length || currentFIO[1][i] != fio[1][i])
                                    {
                                        if (identicalLetters > maxIdenticalLetters)
                                        {
                                            theMostIdenticalFIO = fio;
                                        }
                                        break;
                                    }

                                    identicalLetters++;
                                }
                                for (int i = 0; i < Math.Max(currentFIO[2].Length, fio[2].Length); i++)
                                {
                                    if (i == currentFIO[2].Length || i == fio[2].Length || currentFIO[2][i] != fio[2][i])
                                    {
                                        if (identicalLetters > maxIdenticalLetters)
                                        {
                                            theMostIdenticalFIO = fio;
                                        }
                                        break;
                                    }

                                    identicalLetters++;

                                }
                            }
                        }
                        if (!partiallyIdentical && !fullyIdentical)
                        {
                            appropriateResultFileName.Append(currentFIO[0]);
                            appropriateResultFileName.Append("_");
                            appropriateResultFileName.Append(currentFIO[1][0]);
                            appropriateResultFileName.Append(" ");
                            appropriateResultFileName.Append(currentFIO[2][0]);
                        }
                        else
                        if (partiallyIdentical && !fullyIdentical)
                        {
                            if (theMostIdenticalFIO == null)
                            {
                                throw new Exception("very strange error");
                            }
                            appropriateResultFileName.Append(currentFIO[0]);
                            appropriateResultFileName.Append("_");
                            for (int i = 0; i < currentFIO[1].Length; i++)
                            {
                                if (i == currentFIO[1].Length || i == theMostIdenticalFIO[1].Length || currentFIO[1][i] != theMostIdenticalFIO[1][i])
                                {
                                    appropriateResultFileName.Append(currentFIO[1][i]);
                                    appropriateResultFileName.Append(" ");
                                    break;
                                }
                                appropriateResultFileName.Append(currentFIO[1][i]);
                            }
                            for (int i = 0; i < currentFIO[2].Length; i++)
                            {
                                if (i == currentFIO[2].Length || i == theMostIdenticalFIO[2].Length || currentFIO[2][i] != theMostIdenticalFIO[2][i])
                                {
                                    appropriateResultFileName.Append(currentFIO[2][i]);
                                    break;
                                }
                                appropriateResultFileName.Append(currentFIO[2][i]);


                            }
                        }

                        Console.WriteLine(appropriateResultFileName.ToString());

                        File.Copy(randQuestionFile.FullName, Path.Combine(destinationDirName, appropriateResultFileName.ToString() + ".pdf"));
                    }
                }
            }


        }

        static void Main(string[] args)
        {
            
            //Console.WriteLine(args[0]);
            //Console.WriteLine(args[1]);
            
            var destinationDirName = args[2];

            var dirInfoForQuestions = new DirectoryInfo(args[0]);
            var dirInfoForGroups = new DirectoryInfo(args[1]);
            var destinationDir = new DirectoryInfo(destinationDirName);

            try
            {

                CreateQuestions(dirInfoForQuestions, dirInfoForGroups, destinationDir, destinationDirName);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            //PrepareQuestionsForExam();



            Console.ReadLine();
            
            foreach (var file in destinationDir.GetFiles())
            {
                File.Delete(file.FullName);
            }

        }

        private static List<FileInfo> MakeGroupFilesArray(DirectoryInfo dirInfo)
        {
            var files = new List<FileInfo>();
            foreach (var file in dirInfo.GetFiles())
            {
                Regex regex = new Regex(@"(\w*)\.txt$");
                MatchCollection matches = regex.Matches(file.Name);
                if (matches.Count == 1 && file.Name[0] != '0')
                {
                    files.Add(file);
                }
            }
            return files;
        }
        private static List<FileInfo> MakeQuestionFilesArray(DirectoryInfo dirInfo, List<FileInfo> files = null)
        {
            if(files == null)
            {
                files = new List<FileInfo>();
            }
            
            foreach (var file in dirInfo.GetFiles())
            {
                Regex regex = new Regex(@"(\w*)\.pdf$");
                MatchCollection matches = regex.Matches(file.Name);
                if (matches.Count == 1)
                {
                    files.Add(file);                    
                }
            }
            foreach (var dir in dirInfo.GetDirectories())
            {
                files = MakeQuestionFilesArray(dir, files);
            }
            return files;
        }
        private static List<string> CreateFIO(string line)
        {
            var toRet = new List<string>();
            var sb = new StringBuilder();
            var buf = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ')
                {
                    toRet.Add(buf.ToString());
                    buf.Clear();
                    continue;
                }
                buf.Append(line[i]);
            }
            toRet.Add(buf.ToString());

            return toRet;
        }


    }
}
