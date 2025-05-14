using UnityEngine;
using System.Collections.Generic;

public class ChaseState : IEnemyState
{
    private float randomMoveCooldown = 1.5f;
    private float timeSinceLastRandomMove = 0f;

    public void Enter(EnemyAI ai)
    {
        //Debug.Log("[FSM] Entered Chase State");
        timeSinceLastRandomMove = 0f;
    }

    public void Update(EnemyAI ai)
    {
        float distance = Vector2.Distance(ai.transform.position, ai.Player.position);

        // 공격 조건 만족 시 바로 전환
        if (distance <= ai.AttackableRange && CanSeePlayer(ai))
        {
            ai.ChangeState(new AttackState());
            return;
        }

        Vector2Int currentCell = GridScanner.Instance.WorldToCell(ai.transform.position);

        timeSinceLastRandomMove += Time.deltaTime;
        if (timeSinceLastRandomMove >= randomMoveCooldown)
        {
            if (Random.value < 0.3f)
            {
                List<Vector2Int> neighbours = GridScanner.Instance.GetNeighbours(currentCell);
                if (neighbours.Count > 0)
                {
                    Vector2Int randomTarget = neighbours[Random.Range(0, neighbours.Count)];
                    Vector3 worldTarget = GridScanner.Instance.CellToWorld(randomTarget);
                    ai.MoveTo(worldTarget);
                    timeSinceLastRandomMove = 0f;
                    return; // 여기서 끝내야 아래 A* 추적과 중복되지 않음
                }
            }

            timeSinceLastRandomMove = 0f;
        }

        // 기본 A* 추적
        List<Vector2Int> path = AStarPathfinder.Instance.FindPath(ai.transform.position, ai.Player.position);
        if (path.Count > 1)
        {
            Vector3 nextPos = GridScanner.Instance.CellToWorld(path[1]);
            ai.MoveTo(nextPos);
        }
    }

    public void Exit(EnemyAI ai)
    {
        //Debug.Log("[FSM] Exit Chase State");
    }

    private bool CanSeePlayer(EnemyAI ai)
    {
        Vector2 origin = ai.transform.position;
        Vector2 direction = (ai.Player.position - ai.transform.position).normalized;
        float distance = Vector2.Distance(origin, ai.Player.position);

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, ai.ObstacleMask | ai.PlayerMask);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }
}