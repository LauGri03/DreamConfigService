using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DreamConfigService
{
    public static class Check
    {
        private static string path = @"c:\DreamConfig\";
        public static Process getProcess(EventArrivedEventArgs e)
        {
            Process procToReturn;
            uint pr = Convert.ToUInt32(e.NewEvent["ProcessId"]);
            try
            {
                procToReturn = Process.GetProcessById(Convert.ToInt32(pr));
            }
            catch
            {
                pr = Convert.ToUInt32(e.NewEvent["ParentProcessId"]);
                procToReturn = Process.GetProcessById(Convert.ToInt32(pr));
            }

            return procToReturn;
        }

        public static bool fileExist(string name)
        {
            if (File.Exists(path + name + ".txt"))
                return true;
            else
                return false;
        }

        public static int countLine(string name)
        {
            int count = 0;
            using (StreamReader sr = new StreamReader(path + name + ".txt"))
            {

                string line;
                while ((line = sr.ReadLine()) != null)
                    count++;

            }
            return count;
        }

        public static string[] realAllLines(string name)
        {
            string[] alllines = File.ReadAllLines(path + name + ".txt");
            return alllines;
        }
    }
}
