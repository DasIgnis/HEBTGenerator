using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.HEBT.Nodes
{
    public class WanderToPoint : GoToTargetBaseNode
    {
        public override ExecutionResponse Execute(IEnvironment args, ref string currentNode)
        {
            if (NotCurrent(ref currentNode))
            {
                Debug.Log("WanderTo skipping");
                return new ExecutionResponse { Status = BaseNodeExecutionStatus.SKIP };
            }

            var env = args.GetEnvironmentVariables() as RadiantAIEnvironmentParams;
            var result = Go(args, env.WanderObject.transform.position, 3);
            return new ExecutionResponse { Status = result, ExecutingActionNodeId = _id };
        }
    }
}
