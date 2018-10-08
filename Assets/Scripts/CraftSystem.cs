using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSystem : MonoBehaviour
{
    public List<Item> craftItems = new List<Item>();

    public InputField[] inputFields;
    public Button carftBtn;

    private string curCraftStr = "";
    // Use this for initialization
    void Start()
    {
        carftBtn.GetComponent<Button>().onClick.AddListener(TryCraft);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TryCraft()
    {
        curCraftStr = "";
        if (inputFields[0].text != "" && inputFields[1].text != "")
        {
            for (int i = 0; i < inputFields.Length; i++)
            {
                curCraftStr += inputFields[i].text;
            }

            if (FetchCraftItem(curCraftStr))
            {
                Debug.Log("can craft!" + curCraftStr);
            }
            else
            {
                Debug.Log("can not craft!" + curCraftStr);
            }
        }
        else
        {
            Debug.Log("plse fill the inputfield!");
        }
    }

    public Item FetchCraftItem(string str)
    {
        for (int i = 0; i < craftItems.Count; i++)
        {
            if (craftItems[i].carftStr == str)
            {
                Debug.Log(craftItems[i].name);
                return craftItems[i];
            }
        }
        return null;
    }
}
