using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewYourCode.Models;
using ViewYourCode.Models.Prefabs;
using ViewYourCode.Models.TestPreFabs;

namespace ViewYourCode.Controllers
{
    public class ModelSeparator
    {
        public dynamic SeparateModels(BasePreFabsModel model) 
        {
            switch (model.PreFabsId)
            {
                case ("0000"):
                    return new NullUnit();
                case ("0255"):
                    return new TestUnit();
                default:
                    break;
            }

            return null;
        }
    }
}
