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

        Vector3 currentDirection = (target.position - transform.position).normalized;

        float angleChange = Vector3.Angle(prevDirection, currentDirection);
        float slowdownFactor = Mathf.Clamp01(1f - (angleChange / 180f));
        float targetSpeed = maxSpeed * slowdownFactor;

        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        transform.position += currentDirection * currentSpeed * Time.deltaTime;

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