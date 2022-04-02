using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEnemyNear : BaseNode
{
    [SerializeField]
    public string _id;

    public IsEnemyNear()
    {
    }

    public void AddChild(BaseNode node) { }

    public ExecutionResponse Execute(IEnvironment args)
    {
        RadiantAIEnvironmentParams env = args as RadiantAIEnvironmentParams;
        Debug.Log("IsNear");
        if (env.DistanceToEnemy < 5)
        {

            return new ExecutionResponse { Status = BaseNodeExecutionStatus.SUCCESS };
        } else
        {
            return new ExecutionResponse { Status = BaseNodeExecutionStatus.FAILURE };
        }
    }

    public List<BaseNode> GetChildren()
    {
        return new List<BaseNode> { };
    }

    public string GetId()
    {
        return _id;
    }

    public void SetId(string id)
    {
        _id = id;
    }

    public void RemoveChildAt(int index) {  }

    public void Reorder(List<string> ids) {  }
}
