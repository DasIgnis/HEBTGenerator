using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBehaviour : MonoBehaviour
{
    private Vector3 wanderDot;
    // Start is called before the first frame update
    void Start()
    {
        wanderDot = Random.insideUnitCircle * 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, wanderDot) < Vector3.kEpsilon)
        {
            wanderDot = Random.insideUnitCircle * 5;
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, wanderDot, 0.005f);
        }
    }
}
