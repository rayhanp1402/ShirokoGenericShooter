using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float bonusAttackPercent;
    public float baseAttack;

    public float currentAttack;

    private void Awake()
    {
        updateAttack();
    }

    public void AddBonusAttackPercent(float bonusAttackPercent)
    {
        this.bonusAttackPercent += bonusAttackPercent;
        updateAttack();
    }

    public void SubstractBonusAttackPercent(float bonusAttackPercent)
    {
        this.bonusAttackPercent -= bonusAttackPercent;
        updateAttack();
    }

    public void SetBaseAttack(float baseAttack)
    {
        this.baseAttack = baseAttack;
        updateAttack();
    }

    private void updateAttack(){
        currentAttack = baseAttack + (baseAttack * bonusAttackPercent);
    }
}