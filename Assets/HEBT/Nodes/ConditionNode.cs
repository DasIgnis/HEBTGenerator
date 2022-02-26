using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEBT.Nodes
{
    class ConditionNode<T> : BaseNode
    {
        private Predicate<T> _predicate;
        private T _args;
        public ConditionNode(T args, Predicate<T> predicate)
        {
            _args = args;
            _predicate = predicate;
        }
        public BaseNodeExecutionStatus Execute()
        {
            return _predicate(_args) ? BaseNodeExecutionStatus.SUCCESS : BaseNodeExecutionStatus.FAILURE;
        }

        public List<BaseNode> GetChildren()
        {
            return new List<BaseNode>();
        }
    }
}
