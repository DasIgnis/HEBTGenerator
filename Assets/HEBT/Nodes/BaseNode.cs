using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT.Nodes
{
    public interface BaseNode
    {
        List<BaseNode> GetChildren();

        BaseNodeExecutionStatus Execute();
    }
}