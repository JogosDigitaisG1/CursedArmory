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

    public PlayerStats(int hp, int attackPower, int attackSpeed, int moveSpeed, int swordSpirits, 
        int shieldSpirits, int bowSpirits, int staffSpirits) : base(hp, attackPower, attackSpeed, moveSpeed)
    {

        this.swordSpirits = swordSpirits;
        this.shieldSpirits = shieldSpirits;
        this.bowSpirits = bowSpirits;
        this.staffSpirits = staffSpirits;
    }

    public int SwordSpirits { get => swordSpirits; set => swordSpirits = value; }
    public int ShieldSpirits { get => shieldSpirits; set => shieldSpirits = value; }
    public int BowSpirits { get => bowSpirits; set => bowSpirits = value; }
    public int StaffSpirits { get => staffSpirits; set => staffSpirits = value; }
}

