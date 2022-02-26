using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT.Nodes
{
    public class SelectorNode : BaseNode
    {
        private List<BaseNode> _children;
        public SelectorNode()
        {
            _children = new List<BaseNode>();
        }

        public SelectorNode(List<BaseNode> children)
        {
            _children = children;
        }

        public BaseNodeExecutionStatus Execute()
        {
            foreach (BaseNode child in _children)
            {
                var childStatus = child.Execute();
                if (childStatus == BaseNodeExecutionStatus.RUNNING)
                {
                    return BaseNodeExecutionStatus.RUNNING;
                }
                else if (childStatus == BaseNodeExecutionStatus.SUCCESS)
                {
                    return BaseNodeExecutionStatus.SUCCESS;
                }
            }
            return BaseNodeExecutionStatus.FAILURE;
        }

        public List<BaseNode> GetChildren()
        {
            return _children;
        }
    }
}