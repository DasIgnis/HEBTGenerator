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
                DistanceToEnemy = Vector3.Distance(GameObject.Find("PF Player").transform.position, GameObject.Find("Mob").transform.position)
            };
        }
    }

    public class RadiantAIEnvironmentParams
    {
        public float DistanceToEnemy { get; set; }
    }
}
