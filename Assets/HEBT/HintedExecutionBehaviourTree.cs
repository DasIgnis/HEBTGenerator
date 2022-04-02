using Assets.Behaviours;
using Assets.HEBT.Hints;
using HEBT.Nodes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT
{
    [Serializable]
    public class HintedExecutionBehaviourTree: MonoBehaviour
    {
        [SerializeField, SerializeReference]
        public BaseNode tree;

        [SerializeReference]
        public IEnvironment args;

        private List<string> initialOrder = new List<string>();
        private List<BaseHint> appliedHints = new List<BaseHint>();

        public HintedExecutionBehaviourTree()
        {

        }

        public void Execute()
        {
            try
            {
                tree.Execute(args);
            } catch (Exception e)
            {
                Debug.LogError($"Error while performing behaviour: {e.Message}; Stacktrace: {e.StackTrace}");
            }
        }

        public void ApplyHint(BaseHint hint)
        {
            appliedHints.Add(hint);
            tree.Reorder(hint.GetOrderIds());
        }

        public void RemoveHint(BaseHint hint)
        {
            appliedHints.Remove(hint);
            tree.Reorder(initialOrder);
            foreach (BaseHint h in appliedHints)
            {
                tree.Reorder(h.GetOrderIds());
            }
        }
    }
}