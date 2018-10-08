using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreeingForSepration : Steering
{
    public float comforDist = 1f;
    public float multiplierNearby = 2;//在旁边的惩罚因子
    private Radar radar;

    private void Start()
    {
        radar = GetComponent<Radar>();
    }

    public override Vector3 CalculateForce()
    {
        Vector3 steeringForce = Vector3.zero;
        foreach (GameObject g in radar.GetNeighbors())
        {
            if (g != null && g != this.gameObject)
            {
                Vector3 toNeighbor = transform.position - g.transform.position;
                float length = toNeighbor.magnitude;
                steeringForce += toNeighbor.normalized / length;
                if (length < comforDist)
                    steeringForce *= multiplierNearby;
            }
        }
        return steeringForce;
    }
}
