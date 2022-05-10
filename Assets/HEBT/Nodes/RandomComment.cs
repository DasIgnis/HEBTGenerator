using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomComment : TellSmthBaseNode
{
    

    public override ExecutionResponse Execute(IEnvironment args, ref string currentNode)
    {
        var random = Random.Range(0, _possibleComments.Count - 1);
        return ShowText(args, ref currentNode, _possibleComments[random]);
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
