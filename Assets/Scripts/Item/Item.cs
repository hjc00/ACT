using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum ItemType
{
    Unknown = 0,
    Consumable,
    Weapon
}

[CreateAssetMenu(fileName = "new item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int ID = -1;
    public string Name = "";
    public int Value = -1;
    public Sprite sprite = null;
    public GameObject Gobj = null;
    public int maxAmount = 1;  //最大叠加数量
    public string carftStr = "";



    //public Item()
    //{
    //    ID = -1;
    //}

    //public Item(int _id, string _name, int _value, string _path, string _goPath)
    //{
    //    this.ID = _id;
    //    this.Name = _name;
    //    this.Value = _value;
    //    this.sprite = Resources.Load<Sprite>(_path);
    //    this.Gobj = Resources.Load<GameObject>(_goPath);
    //}

    public virtual void Use()
    {

    }
}


