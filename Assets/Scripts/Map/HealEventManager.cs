// Assets/Scripts/Map/HealEventManager.cs
using UnityEngine;

public class HealEventManager : MonoBehaviour
{
    public static HealEventManager Instance { get; private set; }

    void Awake()
    {
        // �̱��� �������� �ν��Ͻ� ����
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    /// �÷��̾ �� �������� �Ծ��� �� ȣ��
    public void OnHealCollected(GameObject healObject)
    {
        // 1) �� ������ �������
        Destroy(healObject);

        // 2) �α׳� ����Ʈ ��� (���߿� HP ȸ�� ���� �߰�)

    }
}
