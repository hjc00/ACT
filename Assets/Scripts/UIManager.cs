using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Text warningText;
    public GameObject inventoryPanel;

    private static UIManager instance;
    private Tweener tweener;

    bool showInventory = false;


    public static UIManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            else
                return null;
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        instance = this;
    }

    public void UpdateHealthBar(float amount)
    {
        healthBar.value = amount;
    }

    public void ShowInventory()
    {
        showInventory = !showInventory;
        inventoryPanel.SetActive(showInventory);
    }


    public void ShowWaring(string s)
    {
        warningText.gameObject.SetActive(true);
        warningText.text = s;
        tweener = warningText.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f);

        tweener.OnComplete(HideWaring);

    }

    private void HideWaring()
    {
        warningText.gameObject.SetActive(false);
        warningText.transform.DOScale(new Vector3(0, 0f, 0f), 0.5f);
    }
}
