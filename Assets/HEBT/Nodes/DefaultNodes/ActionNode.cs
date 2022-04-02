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
        public void AddChild(BaseNode node)
        {
            throw new NotImplementedException();
        }

        public ExecutionResponse Execute(IEnvironment args)
        {
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
            return "";
        }

        public void SetId(string id)
        {

        }

        public void RemoveChildAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Reorder(List<string> ids)
        {
        }
    }
}