using UnityEngine;

public class ExpObject : MonoBehaviour
{
    private Transform target;
    private Vector3 startPos;
    
    private bool isAbsorbing = false;
    private float absorbTimer = 0f;
    private float absorbDuration = 2f; //흡수까지 걸리는 시간

    private float turningPoint = 0.3f; //0점 도달시간
    private float speedScale = 10f;
    private Vector3 moveDir;

    public void StartAbsorb(Transform player)
    {
        target = player;
        isAbsorbing = true;
        startPos = transform.position;

        moveDir = (target.position - transform.position).normalized;
        absorbTimer = 0f;
    }

    void Update()
    {
        if (!isAbsorbing || target == null) return;

        absorbTimer += Time.deltaTime;
        float t = absorbTimer;
        
        if (absorbTimer >= absorbDuration)
        {
            transform.position = target.position;
            return;
        }
        
        float velocity = t * t * (t - turningPoint);
        if (t < turningPoint) velocity *= 20f;
        
        float adjustedSpeed = velocity * speedScale;
        transform.position += moveDir * adjustedSpeed * Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                //플레이어 경험치 증가
            }
            Destroy(gameObject);
        }
    }
}