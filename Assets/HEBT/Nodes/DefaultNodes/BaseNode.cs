using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT.Nodes
{
    public interface BaseNode
    {
        List<BaseNode> GetChildren();
        void AddChild(BaseNode node);
        void RemoveChildAt(int index);
        ExecutionResponse Execute(IEnvironment args);

        string GetId();

        void Reorder(List<string> ids);
    }
}