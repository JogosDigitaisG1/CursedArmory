using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemEffect.asset", menuName = "Shop/New Shop Item Effect")]
public class ShopItemEffectSO : ScriptableObject
{
    public int healthIncrease;
    public int capacityIncrease;
    public int damageIncrease;
}
