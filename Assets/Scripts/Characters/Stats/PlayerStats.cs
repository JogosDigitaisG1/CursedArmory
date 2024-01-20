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
    private int bagMaxCapacity;
    [SerializeField]
    private int bagActualCapacity;

    public PlayerStats(int hp, int attackPower, int attackSpeed, int moveSpeed, int swordSpirits, 
        int shieldSpirits, int bowSpirits, int staffSpirits, int gold, int bagMaxCapacity, int bagActualCapacity) : base(hp, attackPower, attackSpeed, moveSpeed)
    {
        this.swordSpirits = swordSpirits;
        this.shieldSpirits = shieldSpirits;
        this.bowSpirits = bowSpirits;
        this.staffSpirits = staffSpirits;
        this.gold = gold;
        this.bagMaxCapacity = bagMaxCapacity;
        this.bagActualCapacity = bagActualCapacity;
    }

    public PlayerStats(PlayerStats playerStats) : base(playerStats.Hp, playerStats.AttackPower, playerStats.AttackSpeed, playerStats.MoveSpeed)
    {
        swordSpirits = playerStats.swordSpirits;
        shieldSpirits = playerStats.shieldSpirits;
        bowSpirits = playerStats.bowSpirits;
        staffSpirits = playerStats.staffSpirits;
        gold = playerStats.gold;    
        bagMaxCapacity = playerStats.bagMaxCapacity;
        bagActualCapacity = playerStats.bagActualCapacity;
    }

    public bool BagNotFull()
    {
        return bagMaxCapacity > bagActualCapacity;
    }
    public void AddSword()
    {
        if (BagNotFull())
        {
            SwordSpirits++;
            AddToBag();
        }

    }

    public void AddShield()
    {

        if (BagNotFull())
        {
            ShieldSpirits++;
            AddToBag();
        }

    }

    public void AddBow()
    {

        if (BagNotFull())
        {
            BowSpirits++;
            AddToBag();
        }

    }

    public void AddStaff()
    {
        if (BagNotFull())
        {
            StaffSpirits++;
            AddToBag();
        }

    }

    public void AddGold()
    {
        gold++;
    }

    public void DecreaseGold()
    {
        gold--;
    }

    public void AddToBag()
    {
        BagActualCapacity++;
    }

    public void RefreshBag()
    {
        BagActualCapacity = 0;
    }

    public void RefreshAll()
    {
        RefreshAllButGold();
        Gold = 0;
    }

    public void RefreshAllButGold()
    {
        BagActualCapacity = 0;
        SwordSpirits = 0;
        ShieldSpirits = 0;
        BowSpirits = 0;
        StaffSpirits = 0;
    }


    public void DecreaseFromBag(int num)
    {
        bagActualCapacity = bagActualCapacity - num;
    }

    public void IncreaseBagCapacity(int num)
    {
        bagMaxCapacity = bagMaxCapacity + num;
    }

    public void decreaseBagCapacity(int num)
    {
        bagMaxCapacity = bagMaxCapacity - num;
    }

    public int SwordSpirits { get => swordSpirits; set => swordSpirits = value; }
    public int ShieldSpirits { get => shieldSpirits; set => shieldSpirits = value; }
    public int BowSpirits { get => bowSpirits; set => bowSpirits = value; }
    public int StaffSpirits { get => staffSpirits; set => staffSpirits = value; }
    public int Gold { get => gold; set => gold = value; }
    public int BagActualCapacity { get => bagActualCapacity; set => bagActualCapacity = value; }
    public int BagMaxCapacity { get => BagMaxCapacity; set => BagMaxCapacity = value; }
}

    