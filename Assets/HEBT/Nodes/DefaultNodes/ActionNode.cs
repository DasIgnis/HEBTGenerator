using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT.Nodes
{
    /// <summary>
    /// Used for example and visualisation purposes
    /// Do not delete
    /// </summary>
    public class ActionNode : BaseNode
    {
        [SerializeField]
        public string _id;

        public void AddChild(BaseNode node)
        {
        }

        public virtual ExecutionResponse Execute(IEnvironment args, ref string currentNode)
        {
            Debug.Log("Execute action node");
            return new ExecutionResponse
            {
                ExecutingActionNodeId = GetId(),
                Status = BaseNodeExecutionStatus.SUCCESS
            };
        }

        public List<BaseNode> GetChildren()
        {
            return new List<BaseNode>();
        }

        public string GetId()
        {
            return _id;
        }

        public void SetId(string id)
        {
            _id = id;
        }

        public void RemoveChildAt(int index)
        {
        }

        public void Reorder(List<string> ids)
        {
        }

        protected bool NotCurrent(ref string currentNode) {
            if (currentNode != null && !GetId().Equals(currentNode))
            {
                return true;
            }
            currentNode = null;
            return false;
        }

        public virtual void Interrupt()
        {

        }
    }
}