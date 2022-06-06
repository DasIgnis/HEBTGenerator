using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.HEBT.Hints
{
    [Serializable]
    public class BaseHint
    {
        public string Name;

        public List<string> OrderIds;

        public string GetName() {
            return Name;
        }
        public List<string> GetOrderIds()
        {
            return OrderIds;
        }
    }
}
