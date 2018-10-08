using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{

    public float radius = 5;
    private Collider[] cols;
    [SerializeField]
    private List<GameObject> neighbors;
    private float timer = 0;
    public float dectectInterval = 0.5f;


    private void Start()
    {
        neighbors = new List<GameObject>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > dectectInterval)
        {
            neighbors.Clear();
            cols = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("AI"));
            if (cols.Length > 0)
            {
                foreach (Collider c in cols)
                {
                    if (c.gameObject != this.gameObject)
                        neighbors.Add(c.gameObject);
                }
            }
            timer = 0;
        }
    }

    public List<GameObject> GetNeighbors()
    {
        return neighbors;
    }
}
