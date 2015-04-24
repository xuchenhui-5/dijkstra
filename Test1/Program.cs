using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {        
            StreamWriter swWriteFile = File.CreateText("output.txt");
            swWriteFile.Write("1,2,3");
            swWriteFile.Close();
        }
    }
}
