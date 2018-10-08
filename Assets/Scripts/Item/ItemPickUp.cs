using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        this.transform.Rotate(new Vector3(0, 1, 0));
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<InventoryManager>().IsFull() == false)
            {
                InventoryManager.Instance.AddItem(item);
                Destroy(gameObject);
            }
            else
            {
                UIManager.Instance.ShowWaring("背包已满");
            }
        }
    }


}
