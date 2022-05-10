using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToPrayTarget : GoToTargetBaseNode
{
    public override ExecutionResponse Execute(IEnvironment args, ref string currentNode)
    {
        Debug.Log("Go to pray");
        if (NotCurrent(ref currentNode))
        {
            return new ExecutionResponse { Status = BaseNodeExecutionStatus.SKIP };
        }

        var env = args.GetEnvironmentVariables() as RadiantAIEnvironmentParams;
        var result = Go(args, env.PrayTarget.transform.position, 3);
        return new ExecutionResponse { Status = result, ExecutingActionNodeId = _id };
    }

    
}
