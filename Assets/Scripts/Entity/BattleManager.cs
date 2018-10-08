using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    [SerializeField]
    protected Collider defenseCol;
    public Collider DefenseCol { get { return defenseCol; } }
    [SerializeField]
    protected int amountToFallDown = 50;

    public virtual void OnTriggerEnter(Collider other)
    {

    }

    private void Awake()
    {
        defenseCol = transform.GetComponent<Collider>();
    }

  
}
