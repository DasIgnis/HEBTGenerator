using Assets.HEBT.Nodes.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT.Nodes
{
    public class SelectorNode<T> : BaseNode<T>
    {
        public SelectorNode()
        {
            _children = new List<BaseNode<T>>();
        }

        public SelectorNode(List<BaseNode<T>> children)
        {
            _children = children;
        }

        public new ExecutionResponse Execute(T args)
        {
            foreach (BaseNode<T> child in _children)
            {
                var childStatus = child.Execute(args);
                if (childStatus.Status == BaseNodeExecutionStatus.RUNNING)
                {
                    return childStatus;
                }
                else if (childStatus.Status == BaseNodeExecutionStatus.SUCCESS)
                {
                    return new ExecutionResponse
                    {
                        ExecutingActionNodeId = "",
                        Status = BaseNodeExecutionStatus.SUCCESS
                    };
                }
            }
            return new ExecutionResponse
            {
                ExecutingActionNodeId = "",
                Status = BaseNodeExecutionStatus.FAILURE
            };
        }

        public new List<BaseNode<T>> GetChildren()
        {
            return _children;
        }

        public new string GetId()
        {
            return "";
        }
    }
}