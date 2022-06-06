using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMonitorNode : BaseNode
{
    [SerializeField, SerializeReference]
    List<BaseNode> _children;

    [SerializeField]
    public string _id;

    private string _executingChild;

    public TimeMonitorNode()
    {
        _children = new List<BaseNode>();
    }

    public TimeMonitorNode(List<BaseNode> children, string id)
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
        var env = args.GetEnvironmentVariables() as RadiantAIEnvironmentParams;

        BaseNode mustExecuteNode = _children[env.CurrentTimeGap];
        
        if (mustExecuteNode.GetId() != _executingChild)
        {
            BaseNode childToInterrupt = _children.Find(x => x.GetId() == _executingChild);
            if (childToInterrupt != null)
            {
                childToInterrupt.Interrupt();
            }
            currentNode = null;
            _executingChild = mustExecuteNode.GetId();
        }

        var executionResult = mustExecuteNode.Execute(args, ref currentNode);

        return new ExecutionResponse { ExecutingActionNodeId = executionResult.ExecutingActionNodeId, Status = BaseNodeExecutionStatus.RUNNING };
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

    public void Interrupt()
    {
        _children.ForEach(x => x.Interrupt());
    }
}
