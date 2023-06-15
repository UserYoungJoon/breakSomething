public struct Buff
{
    public float duration;
    public int damageMultiplier;
    public int criticalChanceBonus;
    public FloatUpper0point1 shieldCoolTime;
    public int coinBonus;

    public Buff(float duration, int damageMultiplier, int criticalChanceBonus, FloatUpper0point1 shieldCoolTime, int coinBonus)
    {
        this.duration = duration;
        this.damageMultiplier = damageMultiplier;
        this.criticalChanceBonus = criticalChanceBonus;
        this.shieldCoolTime = shieldCoolTime;
        this.coinBonus = coinBonus;
    }
}