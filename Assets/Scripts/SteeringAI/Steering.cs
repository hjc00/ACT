using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Steering : MonoBehaviour {

    public Vector3 steeringForce;
    public float weight = 1;


	void Start () {
        steeringForce = Vector3.zero;
    }
	

    public virtual Vector3 CalculateForce()
    {
        return steeringForce;
    }
}
