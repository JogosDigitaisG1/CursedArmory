using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem.asset", menuName = "Shop/New Shop Item")]
public class ShopItemSO : ScriptableObject
{
    public string title;
    public string description;
    public string effectDescription;
    public int cost;
    public ShopItemEffectSO effects;

}
