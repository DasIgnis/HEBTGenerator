using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT.Nodes
{
    public class SequenceNode : BaseNode
    {
        [SerializeField, SerializeReference]
        List<BaseNode> _children;

        [SerializeField]
        public string _id;

        public SequenceNode()
        {
            _children = new List<BaseNode>();
        }

        public SequenceNode(List<BaseNode> children, string id)
        {
            _children = children;
            _id = id;
        }

        public void AddChild(BaseNode node)
        {
            _children.Add(node);
        }

        public void RemoveChildAt(int index)
        {
            _children.RemoveAt(index);
        }

        public ExecutionResponse Execute(IEnvironment args, ref string currentNode)
        {
            bool hasUnskipped = false;

            if (currentNode != null && currentNode.Equals(_id))
            {
                currentNode = null;
            }

            foreach (BaseNode child in _children)
            {
                var childStatus = child.Execute(args, ref currentNode);
                if (childStatus.Status == BaseNodeExecutionStatus.SKIP)
                {
                    continue;
                }
                hasUnskipped = true;
                currentNode = null;
                if (childStatus.Status == BaseNodeExecutionStatus.RUNNING)
                {
                    return childStatus;
                }
                else if (childStatus.Status == BaseNodeExecutionStatus.FAILURE)
                {
                    return new ExecutionResponse
                    {
                        ExecutingActionNodeId = _id,
                        Status = BaseNodeExecutionStatus.FAILURE
                    };
                }
            }
            return new ExecutionResponse
            {
                ExecutingActionNodeId = _id,
                Status = hasUnskipped ? BaseNodeExecutionStatus.SUCCESS : BaseNodeExecutionStatus.SKIP
            };
        }

        public List<BaseNode> GetChildren()
        {
            return _children;
        }

        public string GetId()
        {
            return _id;
        }

        public void SetId(string id)
        {
            _id = id;
        }

        public void Reorder(List<string> ids)
        {
            _children.Sort((x, y) => ids.IndexOf(x.GetId()) - ids.IndexOf(y.GetId()));
            _children.ForEach(x => x.Reorder(ids));
        }
    }
}
