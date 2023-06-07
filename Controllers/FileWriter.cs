using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using VPMSerialezator.Models;
using Path = System.IO.Path;
using VPMSerialezator;

namespace ViewYourCode.Controllers
{
    public class FileWriter
    {
        //public string path = @" ..\..\Result\build.txt";
        public string Path = @"Result\";

        public void WriteToSkript(VPMmodel vPMmodel, string name, string path)
        {
            using (StreamWriter streamWriter = new StreamWriter(path + name + vPMmodel.FileType, false, System.Text.Encoding.Default))
            {
                streamWriter.WriteLine(vPMmodel.OutPutCode);
 
            }    
        }
    }
}