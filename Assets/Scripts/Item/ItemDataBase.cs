using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemDataBase : MonoBehaviour
{
    private static ItemDataBase instance;
    private JsonData itemData;
    private Dictionary<int, Item> itemDict = new Dictionary<int, Item>();

    public static ItemDataBase Instacne
    {
        get
        {
            if (instance == null)
                return null;
            else
                return instance;
        }
    }
    // Use this for initialization
    void Start()
    {
        instance = this;
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/ItemDataBase.json"));
        ConstructDataBase();
    }

    void ConstructDataBase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            //itemDict.Add((int)itemData[i]["id"], new Item((int)itemData[i]["id"],
            //    itemData[i]["name"].ToString(),
            //    (int)itemData[i]["value"], itemData[i]["path"].ToString()));

            int id = (int)itemData[i]["id"];
            string name = itemData[i]["name"].ToString();
            int value = (int)itemData[i]["value"];
            string spritePath = itemData[i]["path"].ToString();
            string goPath = itemData[i]["objPath"].ToString();
        }
    }

    //public T GetItemById<T>(int id)
    //{
    //    return itemData[id];
    //}

    public string GetName(int id)
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            if (id == (int)itemData[i]["id"])
                return itemData[i]["name"].ToString();
        }
        return null;
    }


    public Item FetchItemById(int id)
    {
        if (itemDict.ContainsKey(id))
        {
            return itemDict[id];
        }
        return null;
    }
}
