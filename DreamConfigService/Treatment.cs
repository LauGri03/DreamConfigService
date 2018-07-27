using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DreamConfigService
{
   public static class Treatment
    {
        public static void doTreatment(EventArrivedEventArgs e)
        {
            try
            {
                if(Check.fileExist(Check.getProcess(e).MainModule.FileVersionInfo.FileDescription))
                {
                    string name = Check.getProcess(e).MainModule.FileVersionInfo.FileDescription;
                    int count = Check.countLine(name);
                    string[] line = Check.realAllLines(name);

                    if(count == 3)  
                        ChangeConfig.changeConfig(Convert.ToInt32(line[0]), line[2], Convert.ToInt32(line[1]));
                    else if(count == 2)
                        ChangeConfig.changeConfig(Convert.ToInt32(line[0]), line[2]);
                }

            }
            catch { }
        }
  
    }
}
