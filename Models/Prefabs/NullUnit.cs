using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewYourCode.Models.Prefabs
{
    internal class NullUnit : BasePreFabsModel
    {
        public NullUnit()
        {
            this.PreFabsId = "0000";
            this.PreFabsName = "Null";
            this.CodePreview = "NULL";
            this.CodeDescription = "null, this null unit";
            this.SelfType = this.GetType();
        }
    }
}
