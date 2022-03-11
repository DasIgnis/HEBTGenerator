using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.HEBT.Hints
{
    public interface BaseHint
    {
        string GetName();
        List<string> GetOrderIds();
    }
}
