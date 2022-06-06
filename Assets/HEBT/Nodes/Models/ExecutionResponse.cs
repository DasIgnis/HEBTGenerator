using HEBT.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.HEBT.Nodes.Models
{
    public class ExecutionResponse
    {
        public BaseNodeExecutionStatus Status { get; set; }
        public string ExecutingActionNodeId { get; set; }
    }
}
