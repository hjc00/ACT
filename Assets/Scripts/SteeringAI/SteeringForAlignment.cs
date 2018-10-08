using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForAlignment : Steering
{

    private Radar radar;
    // Use this for initialization
    void Start()
    {
        radar = GetComponent<Radar>();
    }

    public override Vector3 CalculateForce()
    {
        Vector3 steeringForce = Vector3.zero;
        Vector3 averageForward = Vector3.zero;
        int count = 0;
        foreach (GameObject g in radar.GetNeighbors())
        {
            if (g != null && g != this.gameObject)
            {
                averageForward += g.transform.forward;
                count++;
            }

        }
        if (count > 0)
        {
            averageForward /= count;
            steeringForce = averageForward - transform.forward;
        }
        return steeringForce;
    }
}
