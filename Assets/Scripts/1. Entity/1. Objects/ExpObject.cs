using UnityEngine;

public class ExpObject : MonoBehaviour
{
    #region ExpObject 객체 변수 선언
    private Transform target;
    private bool isAbsorbing = false;
    
    public int expAmount = 1;
    
    [Header("추적 속도 설정")]
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float maxSpeed = 50f;
    private float currentSpeed = 0f;
    
    private Vector3 prevDirection = Vector3.zero;

    #endregion

    #region StartAbsorb 메서드 → 경험치 오브젝트가 플레이어를 추적하도록 설정하고 초기 방향과 속도를 설정하는 기능
    public void StartAbsorb(Transform player)
    {
        target = player;
        isAbsorbing = true;
        currentSpeed = 0f;
        prevDirection = (player.position - transform.position).normalized;
    }

    #endregion

    #region Update 메서드 → 매 프레임마다 플레이어를 향해 가속하며 추적하고, 방향 변화에 따라 속도를 조절하는 기능 추가
    void Update()
    {
        if (!isAbsorbing || target == null) return;

        Vector3 currentDirection = (target.position - transform.position).normalized;

        float angleChange = Vector3.Angle(prevDirection, currentDirection);
        float slowdownFactor = Mathf.Clamp01(1f - (angleChange / 180f));
        float targetSpeed = maxSpeed * slowdownFactor;

        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        transform.position += currentDirection * currentSpeed * Time.deltaTime;

        prevDirection = currentDirection;
    }

    #endregion

    #region 경험치 오브젝트가 플레이어와 충돌 시 처리하는 기능
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.AddExp(expAmount);
            }
            Destroy(gameObject);
        }
    }

    #endregion
}