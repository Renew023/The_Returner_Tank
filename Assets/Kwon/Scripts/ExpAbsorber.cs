using UnityEngine;

public class ExpAbsorber : MonoBehaviour
{
    [SerializeField] private Transform expParent; // ExpObjects 오브젝트
    [SerializeField] private Transform player;    // 플레이어 Transform

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // 필요한 경우에만 Find 호출
            if (expParent == null)
                expParent = GameObject.Find("ExpObjects")?.transform;

            if (player == null)
                player = GameObject.Find("Player")?.transform;

            if (expParent == null || player == null) return;

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