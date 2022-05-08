using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomComment : ActionNode
{
    private int _showDuration = 1000;
    private int _remainingCount = 0;
    public override ExecutionResponse Execute(IEnvironment args, ref string currentNode)
    {
        if (NotCurrent(ref currentNode)) return new ExecutionResponse { Status = BaseNodeExecutionStatus.SKIP };

        var env = args.GetEnvironmentVariables() as RadiantAIEnvironmentParams;
        var dialogWindow = env.Me.GetComponentInChildren<Text>();

        if (_remainingCount == 1)
        {
            _remainingCount--;
            dialogWindow.text = "";
            Debug.Log("Hide comment");
            return new ExecutionResponse { Status = BaseNodeExecutionStatus.SUCCESS, ExecutingActionNodeId = _id };
        }
        else if (_remainingCount == 0)
        {
            _remainingCount = _showDuration;
            var random = Random.Range(0, _possibleComments.Count - 1);
            dialogWindow.text = _possibleComments[random];
            Debug.Log("Comment");
        }

        _remainingCount--;

        return new ExecutionResponse { Status = BaseNodeExecutionStatus.RUNNING, ExecutingActionNodeId = _id };
    }

    private List<string> _possibleComments = new List<string>
    {
        "� ������ � ��������� ������?",
        "������� � ���� ���������� �������. �� ����� ��� ����������� ������",
        "������ ��� ������ ���� ��� �������� ��� ������...",
        "����� ��� ���������� ��� ����",
        "������� ����� �����", 
        "����� ��������� ��� ����� ������ ��������� � ��� ���������� ������ � �������",
        "����� �� ��� W, A, S ��� D, ������ ��� ������ ������",
        "��� ����� ������ �� �������, ������ ��� ������������ ����������"
    };
}
