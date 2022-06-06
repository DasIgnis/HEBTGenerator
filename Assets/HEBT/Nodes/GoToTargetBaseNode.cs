using Assets.Behaviours;
using HEBT.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToTargetBaseNode : ActionNode
{
    private NavMeshAgent _agent;

    protected BaseNodeExecutionStatus Go(IEnvironment args, Vector2 target, float speed)
    {
        var env = args.GetEnvironmentVariables() as RadiantAIEnvironmentParams;

        _agent = env.Me.GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = speed;
        _agent.SetDestination(target);

        if (Vector2.Distance(env.Me.transform.position, target) < 1)
        {
            _agent.ResetPath();
            return BaseNodeExecutionStatus.SUCCESS ;
        }
        return BaseNodeExecutionStatus.RUNNING;
    }

    public override void Interrupt()
    {
        if (_agent != null)
        {
            _agent.ResetPath();
        }
    }
}
