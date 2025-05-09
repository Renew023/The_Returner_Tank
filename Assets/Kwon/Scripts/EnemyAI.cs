using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("설정")]
    public float chaseableRange = 5f;
    public float attackableRange = 1.5f;
    public float moveSpeed = 2f;
    public float repathInterval = 0.5f;
    public LayerMask obstacleLayer;
    public Transform player;

    private enum State { Patrol, Chase, AttackMove, Standby }
    private State currentState;

    private Vector2Int lastPlayerCell = new Vector2Int(int.MinValue, int.MinValue);
    private List<Vector2Int> path = new List<Vector2Int>();
    private int pathIndex = 0;
    private bool isMoving = false;

    private int aiGroupIndex;

    private void Start()
    {
        aiGroupIndex = Random.Range(0, 5); // 그룹별로 분산 처리
        StartCoroutine(EnemyLoop());
    }

    private IEnumerator EnemyLoop()
    {
        yield return new WaitForSeconds(0.1f * aiGroupIndex); // 초기 분산

        WaitForSeconds wait = new WaitForSeconds(repathInterval);

        while (true)
        {
            EvaluateState();
            if (!isMoving)
            {
                HandleState();
            }

            yield return wait;
        }
    }

    private void EvaluateState()
    {
        float distSqr = (player.position - transform.position).sqrMagnitude;
        float attackRangeSqr = attackableRange * attackableRange;
        float chaseRangeSqr = chaseableRange * chaseableRange;

        if (distSqr <= attackRangeSqr)
        {
            if (HasLineOfSightToPlayer())
                currentState = State.Standby;
            else
                currentState = State.AttackMove;
        }
        else if (distSqr <= chaseRangeSqr)
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Patrol;
        }
    }

    private void HandleState()
    {
        switch (currentState)
        {
            case State.Patrol:
            case State.Standby:
                Vector2Int currentCell = GridScanner.Instance.WorldToCell(transform.position);
                Vector2Int offset = new Vector2Int(Random.Range(-1, 2), Random.Range(-1, 2));
                Vector2Int target = currentCell + offset;

                if (GridScanner.Instance.IsWalkable(target))
                {
                    MoveTo(GridScanner.Instance.CellToWorld(target));
                }
                break;

            case State.Chase:
            case State.AttackMove:
                Vector2Int chaseCell = GridScanner.Instance.WorldToCell(transform.position);
                Vector2Int targetCell = GridScanner.Instance.WorldToCell(player.position);

                if (targetCell != lastPlayerCell || path.Count == 0)
                {
                    path = AStarPathfinder.Instance.FindPath(transform.position, player.position);
                    lastPlayerCell = targetCell;
                    pathIndex = 1;
                }

                if (path.Count <= 1 || chaseCell == targetCell) return;

                if (pathIndex < path.Count)
                {
                    MoveTo(GridScanner.Instance.CellToWorld(path[pathIndex]));
                }
                break;
        }
    }

    private void MoveTo(Vector3 targetPos)
    {
        if (isMoving) return;
        if ((targetPos - transform.position).sqrMagnitude < 0.01f * 0.01f) return;

        StartCoroutine(MoveToRoutine(targetPos));
    }

    private IEnumerator MoveToRoutine(Vector3 targetPos)
    {
        isMoving = true;

        float elapsed = 0f;
        Vector3 startPos = transform.position;
        float totalDist = Vector3.Distance(startPos, targetPos);
        float duration = totalDist / moveSpeed;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;

        if ((currentState == State.Chase || currentState == State.AttackMove) && pathIndex < path.Count)
        {
            MoveTo(GridScanner.Instance.CellToWorld(path[pathIndex]));
            pathIndex++;
        }
    }

    private bool HasLineOfSightToPlayer()
    {
        float distSqr = (player.position - transform.position).sqrMagnitude;
        float maxCheckSqr = (attackableRange + 0.5f) * (attackableRange + 0.5f);

        if (distSqr > maxCheckSqr) return false; // 일정 거리 이상은 확인 안 함

        return !Physics2D.Linecast(transform.position, player.position, obstacleLayer);
    }
}
