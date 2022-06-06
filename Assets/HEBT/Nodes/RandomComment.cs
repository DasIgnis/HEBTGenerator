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
