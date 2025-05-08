using UnityEngine;

public abstract class BaseBattleUI : MonoBehaviour
{
    public virtual void UpdatePlayerLevel(int level)
    {

    }

    public virtual void UpdatePlayerEXP(float current, float max)
    {

    }

    public virtual void UpdatePlayerHP(float current, float max)
    {

    }

    public virtual void ShowDamageToPlayer(float damage)
    {

    }

    public virtual void ShowDamageToMonster(float damage)
    {

    }

    public virtual void UpdateNormalMonsterHP(float current, float max)
    {

    }

    public virtual void TogglePauseMenu(bool isActive)
    {

    }
}
