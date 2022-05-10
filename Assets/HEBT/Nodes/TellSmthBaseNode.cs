using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TellSmthBaseNode : ActionNode
{
    protected int _showDuration = 1000;
    protected int _remainingCount = 0;

    protected Text _dialogWindow;

    protected ExecutionResponse ShowText(IEnvironment args, ref string currentNode, string text)
    {
        if (NotCurrent(ref currentNode)) return new ExecutionResponse { Status = BaseNodeExecutionStatus.SKIP };

        var env = args.GetEnvironmentVariables() as RadiantAIEnvironmentParams;
        _dialogWindow = env.Me.GetComponentInChildren<Text>();

        if (_remainingCount == 1)
        {
            _remainingCount--;
            _dialogWindow.text = "";
            Debug.Log("Hide comment");
            return new ExecutionResponse { Status = BaseNodeExecutionStatus.SUCCESS, ExecutingActionNodeId = _id };
        }
        else if (_remainingCount == 0)
        {
            _remainingCount = _showDuration;
            _dialogWindow.text = text;
            Debug.Log("Comment");
        }

        _remainingCount--;

        return new ExecutionResponse { Status = BaseNodeExecutionStatus.RUNNING, ExecutingActionNodeId = _id };
    }

    public override void Interrupt()
    {
        _remainingCount = 0;
        if (_dialogWindow != null)
        {
            _dialogWindow.text = "";
        }
    }
}
