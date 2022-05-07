using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.HEBT.Nodes
{
    public class WanderToEnemy : ActionNode
    {
        public override ExecutionResponse Execute(IEnvironment args, ref string currentNode)
        {
            Debug.Log("WanderTo");
            if (NotCurrent(ref currentNode))
            {
                return new ExecutionResponse { Status = BaseNodeExecutionStatus.SKIP };
            }
            var env = args.GetEnvironmentVariables() as RadiantAIEnvironmentParams;
            env.Me.transform.position = Vector2.MoveTowards(env.Me.transform.position, env.Enemy.transform.position, 0.1f);
            if (Vector2.Distance(env.Me.transform.position, env.Enemy.transform.position) < 5)
            {
                return new ExecutionResponse { Status = BaseNodeExecutionStatus.SUCCESS };
            }
            return new ExecutionResponse { Status = BaseNodeExecutionStatus.RUNNING, ExecutingActionNodeId = _id };
        }
    }
}
