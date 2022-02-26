using HEBT;
using HEBT.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletorBehaviorScript : MonoBehaviour
{
    [SerializeField]
    public GameObject flag;

    private HintedExecutionBehaviourTree _behaviourTree;
    private ActionNodeParams _params;

    private Vector3? _wanderPoint;

    private class ActionNodeParams
    {
        public Vector3 FlagPosition { get; set; }
        public Vector3 CurrentPosition { get; set; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _params = new ActionNodeParams
        {
            FlagPosition = flag.transform.position,
            CurrentPosition = transform.position
        };

        SelectorNode root = new SelectorNode(new List<BaseNode>
        {
            new SequenceNode(new List<BaseNode>
            {
                new ConditionNode<ActionNodeParams>(_params, FlagIsFar),
                new ActionNode<ActionNodeParams>(_params, MoveTowardsFlag)
            }),
            new ActionNode<ActionNodeParams>(_params, Wander)
        });

        _behaviourTree = new HintedExecutionBehaviourTree(root);
    }

    // Update is called once per frame
    void Update()
    {
        _behaviourTree.Execute();
        transform.position = _params.CurrentPosition;
        UpdateWordParams();
    }

    void UpdateWordParams()
    {
        _params.FlagPosition = flag.transform.position;
    }

    void MoveTowardsFlag(ActionNodeParams nodeParams)
    {
        if (_wanderPoint.HasValue)
        {
            _wanderPoint = null;
        }

        nodeParams.CurrentPosition = Vector3.MoveTowards(
                    nodeParams.CurrentPosition,
                    nodeParams.FlagPosition,
                    0.01f);
    }

    void Wander(ActionNodeParams nodeParams)
    {
        if (_wanderPoint.HasValue)
        {
            nodeParams.CurrentPosition = Vector3.MoveTowards(
                nodeParams.CurrentPosition,
                _wanderPoint.Value,
                0.01f);
        } else
        {
            System.Random random = new System.Random();
            double theta = random.NextDouble() * 2 * Mathf.PI;
            double radius = random.Next() * 6;
            _wanderPoint = new Vector3(Convert.ToSingle(radius * Math.Cos(theta)), Convert.ToSingle(radius * Math.Sin(theta)));
        }
    }

    bool FlagIsFar(ActionNodeParams nodeParams)
    {
        return Vector3.Distance(nodeParams.CurrentPosition, nodeParams.FlagPosition) > 3f;
    }
}
