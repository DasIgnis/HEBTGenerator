using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : TellSmthBaseNode
{
    public override ExecutionResponse Execute(IEnvironment args, ref string currentNode)
    {
        return ShowText(args, ref currentNode, "Zzzzz");
    }
}
