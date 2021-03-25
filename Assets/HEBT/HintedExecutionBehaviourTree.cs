using HEBT.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT
{
    public class HintedExecutionBehaviourTree
    {
        private BaseNode _root;

        public HintedExecutionBehaviourTree(BaseNode root)
        {
            _root = root;
        }

        public void Execute()
        {
            _root.Execute();
        }
    }
}