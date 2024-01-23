

using UnityEngine;

[System.Serializable]
public class CharacterBaseStats 
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private int attackPower;
    [SerializeField]
    private int attackSpeed;
    [SerializeField]
    private int moveSpeed;


    public CharacterBaseStats() { }
    public CharacterBaseStats(int hp, int attackPower, int attackSpeed, int moveSpeed)
    {
        this.hp = hp;
        this.attackPower = attackPower;
        this.attackSpeed = attackSpeed;
        this.moveSpeed = moveSpeed;
    }



    public int Hp { get => hp; set => hp = value; }
    public int AttackPower { get => attackPower; set => attackPower = value; }
    public int AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public int MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
}

