using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewYourCode.Models;

namespace ViewYourCode.Models.TestPreFabs
{
    public class TestUnit : BasePreFabsModel
    {
        public TestUnit()
        {
            this.PreFabsId = "0255";
            this.PreFabsName = "TestUnit";
            this.CodePreview = "none code, this test unit";
            this.CodeDescription = "none code, this test unit";
        }
    }
}
