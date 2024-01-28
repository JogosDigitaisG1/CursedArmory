using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopItemClass 
{
    public ShopItemSO shopItemSO;
    public bool isAvailable = true;

    public ShopItemClass(ShopItemSO item, bool v)
    {
        Item = item;
        V = v;
    }

    public ShopItemSO Item { get; }
    public bool V { get; }
}
