using Assets.HEBT.Hints;
using Assets.Models;
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
        [SerializeField, SerializeReference]
        public HintedExecutionBehaviourTree tree;

        [SerializeField]
        public List<BaseHint> hints;

        [SerializeField]
        public List<TimeRange> timeRanges;

        [SerializeField]
        public TimeMonitorScript timeMonitor;

        [SerializeField]
        public GameObject wanderObject;

        [SerializeField]
        public GameObject prayObject;

        [SerializeField]
        public GameObject restObject;

        public void Update()
        {
            int timeGap = timeRanges.FindIndex(x => x.Start <= timeMonitor.GetHours() && x.End > timeMonitor.GetHours());
            tree.args = new RadiantAIEnvironment(this.gameObject, wanderObject, prayObject, restObject, timeGap);
            tree.Execute();
        }
    }
}
