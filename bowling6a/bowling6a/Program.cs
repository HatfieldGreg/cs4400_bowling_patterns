//Greg Hatfield
//bowling 6 program
//part A read in file portion using the PIPES method
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bowling6a
{
    class Program
    {
        static void Main(string[] args)
        {
            //string fileName = "";
            int curBall;
            List<string> list = new List<string>();
            List<int> numList = new List<int>();
            //read in file FROM INSTRUCTIONS data will be either on ONE line or split on lines so split on space and new line
            // Console.WriteLine("Enter bowling text file please:");
            //fileName = Console.ReadLine();
            string line;
            do
            {
                line = Console.ReadLine();
                if (line != null)
                {
                    string[] numbers = line.Split(' ', '\n', '\r');
                    foreach (string num in numbers)
                    {
                        //if valid, add to numList, not valid ignore
                        bool check = int.TryParse(num, out curBall);
                        if (check == true)
                        {
                            //numList.Add(curBall);
                            Console.WriteLine(curBall);
                            
                        }

                        list.Add(num);
                        // Console.WriteLine("from array " + curBall);
                    }
                    //Console.WriteLine(line);
                }
            } while (line != null);
            Console.WriteLine("END");
        }
    }
}
