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

    private Text _dialogWindow;

    public override ExecutionResponse Execute(IEnvironment args, ref string currentNode)
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
            var random = Random.Range(0, _possibleComments.Count - 1);
            _dialogWindow.text = _possibleComments[random];
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

    private List<string> _possibleComments = new List<string>
    {
        "И почему я постоянно прыгаю?",
        "Однажды я тоже управлялся игроком. Но потом мне прострелили колено",
        "Стоило мне только один раз пошутить про колено...",
        "Когда уже закончится это лето",
        "Пиксели глаза колют", 
        "Опять сработала эта ветка дерева поведения и мне приходится стоять и болтать",
        "Нажми ты уже W, A, S или D, нечего так близко стоять",
        "Вам стоит отойти от сундука, нечего тут непотребства устраивать"
    };
}
