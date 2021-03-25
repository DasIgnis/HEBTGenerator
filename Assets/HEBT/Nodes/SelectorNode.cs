using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT.Nodes
{
    public class NewBehaviourScript : BaseNode
    {
        private List<BaseNode> _children;
        public NewBehaviourScript()
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