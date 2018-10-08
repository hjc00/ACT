using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instacne;
    public static InventoryManager Instance
    {
        get
        {
            if (instacne != null)
                return instacne;
            else
                return null;
        }
    }

    public GameObject itemUIPrefab;

    public List<GameObject> grids = new List<GameObject>();
    private List<Item> items = new List<Item>();

    public List<Item> Items { get { return items; } }
    public int capicity;
    public int curCount = 0;

    private void Start()
    {
        capicity = grids.Count;
        instacne = this;
        for (int i = 0; i < grids.Count; i++)
        {
            grids[i].GetComponent<GridUI>().gridId = i;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            UIManager.Instance.ShowInventory();
        }
    }

    public void AddItem(Item item)
    {
        if (item.maxAmount > 1)  //可叠加
        {
            if (!FindExistItem(item, 0)) //第一次添加
            {
                AddItemUI(item);
            }
            else   //非第一次添加
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].transform.childCount > 0
                        && grids[i].transform.GetChild(0).GetComponent<ItemUI>().itemData.ID == item.ID) //找到叠加的地方
                    {
                        ItemUI itemInGrid = grids[i].transform.GetChild(0).GetComponent<ItemUI>();
                        if (itemInGrid.existAmount < item.maxAmount)
                        {
                            curCount++;
                            items.Add(item);
                            itemInGrid.existAmount++;
                            itemInGrid.transform.GetComponentInChildren<Text>().text = itemInGrid.existAmount.ToString();
                        }
                        else if (!FindExistItem(item, i + 1))
                        {
                           AddItemUI(item);
                            break;
                        }
                    }
                }
            }
        }
        else   //该物品不可叠加
        {
            //创建新的itemUI
            AddItemUI(item);
        }
        // Debug.Log("curCount:" + curCount);
    }

    bool FindExistItem(Item item, int _i)
    {
        for (int i = _i; i < grids.Count; i++)
        {
            if (grids[i].transform.childCount == 0)
            {
                return false;
            }
            if (grids[i].transform.GetChild(0).GetComponent<ItemUI>().itemData.ID == item.ID)
                return true;
        }
        return false;
    }

    void AddItemUI(Item item)
    {
        curCount++;
        items.Add(item);
        GameObject itemUIToAdd = Instantiate(itemUIPrefab);
        for (int i = 0; i < grids.Count; i++)
        {
            if (grids[i].transform.childCount == 0)
            {
                itemUIToAdd.GetComponent<Image>().sprite = item.sprite;

                itemUIToAdd.transform.SetParent(grids[i].transform, true);
                itemUIToAdd.transform.localPosition = Vector2.zero;
                itemUIToAdd.GetComponent<ItemUI>().itemData = item;
                itemUIToAdd.GetComponent<ItemUI>().goInScene = item.Gobj;
                itemUIToAdd.GetComponent<ItemUI>().gridID = i;
                itemUIToAdd.GetComponent<ItemUI>().existAmount++;
                // itemUIToAdd.GetComponentInChildren<Text>().text = item.existAmount.ToString();
                break;
            }

        }
    }


    public void RemoveItem(Item item)
    {
        curCount--;
        items.Remove(item);
        //  Debug.Log("curCount:" + curCount);
    }

    public void UseItem()
    {

    }

    public bool IsFull()
    {
        if (curCount >= capicity)
            return true;
        else
            return false;
    }
}
