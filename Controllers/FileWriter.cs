using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using ViewYourCode.Models;
using Path = System.IO.Path;

namespace ViewYourCode.Controllers
{
    public class FileWriter
    {
        //public string path = @" ..\..\Result\build.txt";
        public string path = @"Result\build.txt";

        public void WriteToSkript(ObservableCollection<BasePreFabsModel> temp)
        {
            using (StreamWriter streamWriter = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (var item in temp)
                {
                    streamWriter.WriteLine(item.CodePreview);
                }
                
            }    
        }
    }
}