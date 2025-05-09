using UnityEditor;
using Unity.Burst;

[InitializeOnLoad]
public static class DisableBurst
{
    static DisableBurst()
    {
        BurstCompiler.Options.EnableBurstCompilation = false;
        BurstCompiler.Options.EnableBurstSafetyChecks = false;
        UnityEngine.Debug.Log(" Burst ��Ȱ��ȭ �Ϸ�");
    }
}