using UnityEngine;

public abstract class BaseBattleUI : MonoBehaviour
{
    private int currentValue;
    protected int CurrentValue => currentValue;
    public void SetCurrentValue(int _currentValue)
    {
        currentValue = _currentValue;
    }
    public abstract void UpdateValue(float current, float max);
}
