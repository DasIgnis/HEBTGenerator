using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Behaviours
{
    public class RadiantAIEnvironment : IEnvironment
    {
        private int _currentTimeGap;
        private GameObject _me;
        private GameObject _wanderObject;
        private GameObject _prayTarget;
        private GameObject _restTarget;

        public RadiantAIEnvironment(GameObject me, GameObject wanderObject, GameObject prayTarget, GameObject restTarget, int currentTimeGap)
        {
            _me = me;
            _wanderObject = wanderObject;
            _prayTarget = prayTarget;
            _restTarget = restTarget;
            _currentTimeGap = currentTimeGap;
        }

        public object GetEnvironmentVariables()
        {
            return new RadiantAIEnvironmentParams
            {
                Me = _me,
                Enemy = GameObject.Find("PF Player"),
                DistanceToEnemy = Vector3.Distance(GameObject.Find("PF Player").transform.position, GameObject.Find("Mob").transform.position),
                Chest = GameObject.Find("PF Props Chest"),
                WanderObject = _wanderObject,
                PrayTarget = _prayTarget,
                RestTarget = _restTarget,
                CurrentTimeGap = _currentTimeGap
            };
        }
    }

    public class RadiantAIEnvironmentParams
    {
        public GameObject Me { get; set; }
        public GameObject Enemy { get; set; }
        public float DistanceToEnemy { get; set; }
        public GameObject Chest { get; set; }
        public GameObject WanderObject { get; set; }
        public GameObject PrayTarget { get; set; }
        public GameObject RestTarget { get; set; }
        public int CurrentTimeGap { get; set; }
    }
}
