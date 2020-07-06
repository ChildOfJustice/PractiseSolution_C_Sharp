using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEVENTH_CS
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine(args[1]);
            

            try
            {
                var dirInfoForQuestions = new DirectoryInfo(args[1]);
                var dirInfoForGroups = new DirectoryInfo(args[2]);




            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            //PrepareQuestionsForExam();



            Console.ReadLine();
        }

        private static void PrepareQuestionsForExam()
        {
            throw new NotImplementedException();
        }
    }
}
