using Assets.HEBT.Nodes.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT.Nodes
{
    public class ExasmpleActionNode : BaseNode<string>
    {
        public ExecutionResponse Execute(string args)
        {
            return new ExecutionResponse
            {
                ExecutingActionNodeId = GetId(),
                Status = BaseNodeExecutionStatus.SUCCESS
            };
        }

        public List<BaseNode<string>> GetChildren()
        {
            return new List<BaseNode<string>>();
        }

        public string GetId()
        {
            return "";
        }
    }
}