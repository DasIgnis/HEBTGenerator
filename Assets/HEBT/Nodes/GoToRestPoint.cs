using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToRestPoint : GoToTargetBaseNode
{
    public override ExecutionResponse Execute(IEnvironment args, ref string currentNode)
    {
        if (NotCurrent(ref currentNode))
        {
            Debug.Log("GoToRest skipping");
            return new ExecutionResponse { Status = BaseNodeExecutionStatus.SKIP };
        }

        var env = args.GetEnvironmentVariables() as RadiantAIEnvironmentParams;
        var result = Go(args, env.RestTarget.transform.position, 3);
        return new ExecutionResponse { Status = result, ExecutingActionNodeId = _id };
    }
}
