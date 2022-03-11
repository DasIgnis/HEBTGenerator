using HEBT;
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
        [SerializeField]
        public HintedExecutionBehaviourTree<RadiantAITree> tree;
    }
}
