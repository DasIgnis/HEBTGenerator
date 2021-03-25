using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT.Nodes
{
    public class SequenceNode : BaseNode
    {
        private List<BaseNode> _children;

        public SequenceNode()
        {
            _children = new List<BaseNode>();
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
                else if (childStatus == BaseNodeExecutionStatus.FAILURE)
                {
                    return BaseNodeExecutionStatus.FAILURE;
                }
            }
            return BaseNodeExecutionStatus.SUCCESS;
        }

        public List<BaseNode> GetChildren()
        {
            return _children;
        }
    }
}
