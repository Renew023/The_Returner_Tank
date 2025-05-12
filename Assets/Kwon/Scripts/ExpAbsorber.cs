using UnityEngine;

public class ExpAbsorber : MonoBehaviour
{
    public Transform expParent; // ExpObjects 오브젝트
    public Transform player;    // 플레이어 Transform

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (Transform exp in expParent)
            {
                ExpObject expObj = exp.GetComponent<ExpObject>();
                if (expObj != null)
                {
                    expObj.StartAbsorb(player);
                }
            }
        }
    }
}