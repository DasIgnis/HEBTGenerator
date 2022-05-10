using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAroundPoint : ActionNode
{
    private const float WANDER_DISTANCE = 5f;
    private NavMeshAgent _agent;

    private Vector3 _target = Vector3.zero;

    public override ExecutionResponse Execute(IEnvironment args, ref string currentNode)
    {
        if (NotCurrent(ref currentNode)) return new ExecutionResponse { Status = BaseNodeExecutionStatus.SKIP };

        var env = args.GetEnvironmentVariables() as RadiantAIEnvironmentParams;

        if (_target == Vector3.zero) {
            var pos = UnityEngine.Random.insideUnitSphere * WANDER_DISTANCE;
            pos += env.WanderObject.transform.position;

            NavMeshHit navHit;

            NavMesh.SamplePosition(pos, out navHit, WANDER_DISTANCE, -1);

            _target = navHit.position;
            Debug.Log(_target);
        }

        _agent = env.Me.GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = 1;
        _agent.SetDestination(_target);

        if (Vector2.Distance(env.Me.transform.position, _target) < 0.1f)
        {
            _target = Vector3.zero;
            _agent.ResetPath();
            Debug.Log("Path resetted");
        }

        return new ExecutionResponse { Status = BaseNodeExecutionStatus.RUNNING, ExecutingActionNodeId = _id };
    }

    public override void Interrupt()
    {
        if (_agent != null)
        {
            _target = Vector3.zero;
            _agent.ResetPath();
        }
    }
}
