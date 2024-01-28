using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

public class ShopManagerScript : MonoBehaviour
{
    public TMP_Text CoinUi;
    private List<ShopItemClass> shopItemClassList;
    [SerializeField]
    private List<ShopTemplate> shopPanels;
    public GameObject itemTemplate;
    public GameObject contents;
    // Start is called before the first frame update
    void Start()
    {
        shopItemClassList = GameManager.Instance.shopItemClassList;
        shopPanels = new List<ShopTemplate>();
        CoinUi.text = "Gold:" + GameManager.Instance.GetGold();
        LoadPanels();


       // CheckPurchasable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemClassList.Count; i++) 
        {

            if (shopItemClassList[i].isAvailable)
            {
                var shopPanel = Instantiate(itemTemplate, new Vector3(0, 0, 0), Quaternion.identity);
                shopPanel.transform.SetParent(contents.transform, false);

                shopPanel.GetComponent<ShopTemplate>().titleTxt.text = shopItemClassList[i].shopItemSO.title;
                shopPanel.GetComponent<ShopTemplate>().descriptionTxt.text = shopItemClassList[i].shopItemSO.description;
                shopPanel.GetComponent<ShopTemplate>().effectTxt.text = shopItemClassList[i].shopItemSO.effectDescription;
                shopPanel.GetComponent<ShopTemplate>().costTxt.text = "Gold: " + shopItemClassList[i].shopItemSO.cost.ToString();
                shopPanel.GetComponent<ShopTemplate>().shopItemSO = shopItemClassList[i].shopItemSO;

                int index = i; // Capture the current value of i
                shopPanel.GetComponent<ShopTemplate>().buyButton.onClick.RemoveAllListeners();
                shopPanel.GetComponent<ShopTemplate>().buyButton.onClick.AddListener(() => BuyItem(index));

                shopPanels.Add(shopPanel.GetComponent<ShopTemplate>());

                shopPanel.SetActive(true);

            }

        }
    }


    public void CheckPurchasable()
    {
        for (int i = 0; i < shopPanels.Count; i++)
        {

            shopPanels[i].buyButton.interactable = GameManager.Instance.GetGold() >= shopPanels[i].shopItemSO.cost;

        }
    }

    public void BuyItem(int num)
    {
        Debug.Log("Num " + num);
        if (shopPanels[num].isActiveAndEnabled && GameManager.Instance.GetGold() >= shopPanels[num].shopItemSO.cost)
        {
            
            GameManager.Instance.DecreaseGold(shopPanels[num].shopItemSO.cost);
            GameManager.Instance.GetUpgrades(shopPanels[num].shopItemSO.effects);
            shopPanels[num].gameObject.SetActive(false);


            foreach (var item in GameManager.Instance.shopItemClassList)
            {
                if (item.shopItemSO == shopPanels[num].shopItemSO)
                {
                    item.isAvailable = false;
                }
            }

            CheckPurchasable();
            CoinUi.text = "Gold:" + GameManager.Instance.GetGold();
        }
    }
}
