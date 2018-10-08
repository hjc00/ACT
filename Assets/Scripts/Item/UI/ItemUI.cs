using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler,
    IDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item itemData;  //保存的物品信息
    public int gridID;  //表示该物品放在哪个格子的ID

    private GameObject canvas;
    private GameObject itemTip;  //显示物品信息的物体

    private Transform currentParent;  //当前的父transform

    public GameObject goInScene;
    public int existAmount = 0; //当前存在的数量

    void Start()
    {
        currentParent = this.transform.parent;
        canvas = GameObject.Find("Canvas");
        itemTip = canvas.transform.Find("ItemTip").gameObject;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemData != null)
        {
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position;
            this.GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemData != null)
        {
            this.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemData != null)
        {
            GameObject endDragGameObject = eventData.pointerCurrentRaycast.gameObject;
            if (endDragGameObject == null)  //丢弃物品
            {
                GameObject go = itemData.Gobj;
                Instantiate(go,
                    InventoryManager.Instance.transform.position + InventoryManager.Instance.transform.forward * 1.5f + new Vector3(0, 0.5f, 0),
                    Quaternion.identity);
                InventoryManager.Instance.RemoveItem(itemData);
                Destroy(this.gameObject);
            }
            else
            {
                if (endDragGameObject.transform.tag == "GridUI" && endDragGameObject.transform.childCount == 0) //格子为空
                {
                    //获取该格子的ID
                    int tempGridId = endDragGameObject.transform.GetComponent<GridUI>().gridId;
                    //改变该物品所在的格子ID
                    this.gridID = tempGridId;
                    this.transform.SetParent(InventoryManager.Instance.grids[tempGridId].transform);
                    this.transform.localPosition = Vector3.zero;
                    currentParent = this.transform.parent;

                }
                else if (endDragGameObject.transform.tag == "ItemUI")
                {
                    //获取endDragGameObject的所在格子ID
                    int tempGridId = endDragGameObject.transform.GetComponent<ItemUI>().gridID;
                    //交换位置
                    this.transform.SetParent(InventoryManager.Instance.grids[tempGridId].transform);
                    endDragGameObject.transform.SetParent(InventoryManager.Instance.grids[gridID].transform);

                    this.transform.localPosition = Vector3.zero;
                    endDragGameObject.transform.localPosition = Vector3.zero;
                    //交换格子ID
                    endDragGameObject.transform.GetComponent<ItemUI>().gridID = this.gridID;
                    this.gridID = tempGridId;
                }
                else
                {
                    this.transform.SetParent(InventoryManager.Instance.grids[this.gridID].transform);
                    this.transform.localPosition = Vector3.zero;

                }
            }
        }
        this.transform.GetComponent<Image>().raycastTarget = true;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            existAmount--;
            transform.GetComponentInChildren<Text>().text = existAmount.ToString();
            InventoryManager.Instance.RemoveItem(itemData);
            if (existAmount <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemTip.SetActive(true);
        itemTip.transform.position = eventData.pointerEnter.gameObject.transform.parent.position;
        itemTip.transform.SetAsLastSibling();
        itemTip.GetComponentInChildren<Text>().text = itemData.Name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemTip.SetActive(false);
    }
}
