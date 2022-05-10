using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pray : TellSmthBaseNode
{
    private const string PRAY_TEXT = "*неразборчивое бормотание*";

    public override ExecutionResponse Execute(IEnvironment args, ref string currentNode)
    {
        return ShowText(args, ref currentNode, PRAY_TEXT);
    }

}
