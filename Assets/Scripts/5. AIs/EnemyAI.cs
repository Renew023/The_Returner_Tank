using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    #region EnemyAI 객체 변수 선언
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

    #endregion


    #region Start 메서드
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

    #endregion

    #region Update 메서드
    private void Update()
    {
        currentState?.Update(this);
    }

    #endregion

    #region ChangeState 메서드
    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    #endregion

    #region MoveTo 메서드
    public void MoveTo(Vector3 target)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(MoveToRoutine(target));
    }

    #endregion

    #region MoveToRoutine 메서드
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

    #endregion

    #region PerformAttack
    public void PerformAttack()
    {
        //Debug.Log("[EnemyAI] 공격!");
    }

    #endregion
}