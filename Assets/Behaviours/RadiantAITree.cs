using Assets.HEBT.Hints;
using HEBT;
using HEBT.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours
{
    class RadiantAITree: MonoBehaviour
    {
        public int Energy = 10;

        //[SerializeField]
        //public HintedExecutionBehaviourTree tree;
        [SerializeField, SerializeReference]
        public HintedExecutionBehaviourTree tree;

        [SerializeField]
        public List<BaseHint> hints;

        private void Update()
        {
            
        }
    }
}
