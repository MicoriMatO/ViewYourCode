using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewYourCode.Models;
using ViewYourCode.Models.Prefabs;

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
            this.SelfType = this.GetType();

            this.dicttionaryParams = new Dictionary<string, BasePreFabsModel>();
            this.dicttionaryParams["param1"] = param1;
            this.dicttionaryParams["param2"] = param2;
        }
        public readonly string startCode = "start{";
        public readonly string midCode = " + (mid) + ";
        public readonly string endCode = "}end";
        public BasePreFabsModel nextUnit = new NullUnit();
        public BasePreFabsModel param1 = new NullUnit();
        public BasePreFabsModel param2 = new NullUnit();
        public Dictionary<string, BasePreFabsModel> dicttionaryParams;
    }
}
