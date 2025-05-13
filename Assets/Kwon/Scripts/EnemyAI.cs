using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackableRange = 5f;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask playerMask;

    private IEnemyState currentState;
    private Coroutine moveRoutine;

    public Transform Player { get; private set; }
    public float AttackableRange => attackableRange;
    public LayerMask ObstacleMask => obstacleMask;
    public LayerMask PlayerMask => playerMask;
    public bool IsMoving => moveRoutine != null;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player")?.transform;

        if (Player == null)
        {
            Debug.LogError("[EnemyAI] Player not found");
            enabled = false;
            return;
        }

        ChangeState(new ChaseState());
    }

    private void Update()
    {
        currentState?.Update(this);
    }

    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    public void MoveTo(Vector3 target)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(MoveToRoutine(target));
    }

    private IEnumerator MoveToRoutine(Vector3 target)
    {
        while (Vector2.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        moveRoutine = null; // ⭐ 이동 완료 후 상태 해제
    }

    public void PerformAttack()
    {
        //Debug.Log("[EnemyAI] 공격!");
        // TODO: 공격 이펙트, 데미지 적용 등 추가
    }
}