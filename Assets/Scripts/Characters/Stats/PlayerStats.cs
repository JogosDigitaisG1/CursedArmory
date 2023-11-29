using UnityEngine;

[System.Serializable]
public class PlayerStats : CharacterBaseStats
{
    [SerializeField]
    private int swordSpirits;
    [SerializeField]
    private int shieldSpirits;
    [SerializeField]
    private int bowSpirits;
    [SerializeField]
    private int staffSpirits;
    [SerializeField]
    private int gold;
    [SerializeField]
    private int bagCapacity;

    public PlayerStats(int hp, int attackPower, int attackSpeed, int moveSpeed, int swordSpirits, 
        int shieldSpirits, int bowSpirits, int staffSpirits, int gold, int bagCapacity) : base(hp, attackPower, attackSpeed, moveSpeed)
    {

        this.swordSpirits = swordSpirits;
        this.shieldSpirits = shieldSpirits;
        this.bowSpirits = bowSpirits;
        this.staffSpirits = staffSpirits;
        this.gold = gold;
        this.bagCapacity = bagCapacity;
    }

    public PlayerStats(PlayerStats playerStats) : base(playerStats.Hp, playerStats.AttackPower, playerStats.AttackSpeed, playerStats.MoveSpeed)
    {
        swordSpirits = playerStats.swordSpirits;
        shieldSpirits = playerStats.shieldSpirits;
        bowSpirits = playerStats.bowSpirits;
        staffSpirits = playerStats.staffSpirits;
        gold = playerStats.gold;    
        bagCapacity = playerStats.bagCapacity;
    }

    public void AddSword()
    {
        SwordSpirits++;
        AddToBag();
    }

    public void AddShield()
    {
        ShieldSpirits++;
        AddToBag();
    }

    public void AddBow()
    {
        BowSpirits++;
        AddToBag();
    }

    public void AddStaff()
    {
        StaffSpirits++;
        AddToBag();
    }

    public void AddGold()
    {
        Gold++;
    }

    public void AddToBag()
    {
        BagCapacity++;
    }

    public void RefreshBag()
    {
        BagCapacity = 0;
    }

    public void RefreshAll()
    {
        BagCapacity = 0;
        SwordSpirits = 0;
        ShieldSpirits = 0;
        BowSpirits = 0;
        StaffSpirits = 0;
        Gold = 0;
    }

    public void DecreaseFromBag(int num)
    {
        bagCapacity = bagCapacity - num;
    }

    public int SwordSpirits { get => swordSpirits; set => swordSpirits = value; }
    public int ShieldSpirits { get => shieldSpirits; set => shieldSpirits = value; }
    public int BowSpirits { get => bowSpirits; set => bowSpirits = value; }
    public int StaffSpirits { get => staffSpirits; set => staffSpirits = value; }
    public int Gold { get => gold; set => gold = value; }
    public int BagCapacity { get => bagCapacity; set => bagCapacity = value; }
}

