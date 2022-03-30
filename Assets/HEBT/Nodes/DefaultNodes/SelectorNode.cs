using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT.Nodes
{
    public class SelectorNode : BaseNode
    {
        [SerializeField, SerializeReference]
        List<BaseNode> _children;

        public SelectorNode()
        {
            _children = new List<BaseNode>();
        }

        public SelectorNode(List<BaseNode> children)
        {
            _children = children;
        }

        public void AddChild(BaseNode node)
        {
            _children.Add(node);
        }

        public void RemoveChildAt(int index)
        {
            _children.RemoveAt(index);
        }

        public new ExecutionResponse Execute(IEnvironment args)
        {
            foreach (BaseNode child in _children)
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

        public new List<BaseNode> GetChildren()
        {
            return _children;
        }

        public new string GetId()
        {
            return "";
        }

        public void Reorder(List<string> ids)
        {
            _children.Sort((x, y) => ids.IndexOf(x.GetId()) - ids.IndexOf(y.GetId()));
            _children.ForEach(x => x.Reorder(ids));
        }
    }
}