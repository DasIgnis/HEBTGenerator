using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEnemyNear : ActionNode
{
    public override ExecutionResponse Execute(IEnvironment args, ref string currentNode)
    {
        Debug.Log("IsNear");
        if (NotCurrent(ref currentNode))
        {
            return new ExecutionResponse { Status = BaseNodeExecutionStatus.SKIP };
        }
        RadiantAIEnvironmentParams env = args.GetEnvironmentVariables() as RadiantAIEnvironmentParams;
        if (env.DistanceToEnemy < 5)
        {
            return new ExecutionResponse { Status = BaseNodeExecutionStatus.SUCCESS };
        } else
        {
            return new ExecutionResponse { Status = BaseNodeExecutionStatus.FAILURE };
        }
    }
}
