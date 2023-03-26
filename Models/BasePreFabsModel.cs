using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewYourCode.Models
{
    public class BasePreFabsModel
    {
        public string PreFabsId { get; set; }
        public string PreFabsName { get; set; }
        public string CodePreview { get; set; }
        public string CodeDescription { get; set; }
        public Type SelfType { get; set; }

    }
}
