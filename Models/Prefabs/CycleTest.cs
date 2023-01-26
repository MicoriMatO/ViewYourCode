using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewYourCode.Models.Prefabs
{
    public class CycleTest : BasePreFabsModel
    {
        public CycleTest()
        {
            this.PreFabsId = "0224";
            this.PreFabsName = "CycleTest";
            this.CodePreview = "Cycle()" +
                "{" +
                "}" +
                "";
            this.CodeDescription = "Make one function again and again";
        }
    }
}
