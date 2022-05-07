using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEBT.Nodes
{
    public enum BaseNodeExecutionStatus
    {
        SUCCESS = 0,
        FAILURE = 1,
        RUNNING,
        SKIP
    }
}
