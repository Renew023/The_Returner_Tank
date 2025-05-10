using UnityEngine;

public abstract class CharacterUI : MonoBehaviour
{
    protected abstract void Start();

    public abstract void Show(bool show);

    public virtual void UpdateValue(int current)
    {

    }

    public virtual void UpdateValue(float current, float max)
    {

    }
}
