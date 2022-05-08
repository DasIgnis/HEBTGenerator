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
        public object GetEnvironmentVariables()
        {
            return new RadiantAIEnvironmentParams
            {
                Me = GameObject.Find("Mob"),
                Enemy = GameObject.Find("PF Player"),
                DistanceToEnemy = Vector3.Distance(GameObject.Find("PF Player").transform.position, GameObject.Find("Mob").transform.position),
                Chest = GameObject.Find("PF Props Chest")
            };
        }
    }

    public class RadiantAIEnvironmentParams
    {
        public GameObject Me { get; set; }
        public GameObject Enemy { get; set; }
        public float DistanceToEnemy { get; set; }
        public GameObject Chest { get; set; }
    }
}
