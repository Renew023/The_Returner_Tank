using UnityEngine;

public class ExpObject : MonoBehaviour
{
    private Transform target;
    private bool isAbsorbing = false;
    
    public int expAmount = 1;
    
    [Header("추적 속도 설정")]
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float maxSpeed = 50f;
    private float currentSpeed = 0f;
    
    private Vector3 prevDirection = Vector3.zero;


    public void StartAbsorb(Transform player)
    {
        target = player;
        isAbsorbing = true;
        currentSpeed = 0f;
        prevDirection = (player.position - transform.position).normalized;
    }

    void Update()
    {
        if (!isAbsorbing || target == null) return;

        // 현재 방향 계산
        Vector3 currentDirection = (target.position - transform.position).normalized;

        // 방향 변화량 측정
        float angleChange = Vector3.Angle(prevDirection, currentDirection);
        float slowdownFactor = Mathf.Clamp01(1f - (angleChange / 180f)); // 방향 꺾일수록 감속

        // 가속도 적용 (감속 포함)
        float effectiveAcceleration = acceleration * slowdownFactor;
        currentSpeed = Mathf.Min(maxSpeed, currentSpeed + effectiveAcceleration * Time.deltaTime);

        // 이동
        transform.position += currentDirection * currentSpeed * Time.deltaTime;

        // 다음 프레임 대비 방향 저장
        prevDirection = currentDirection;
    }
    
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
}