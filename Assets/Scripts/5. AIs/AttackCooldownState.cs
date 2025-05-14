using UnityEngine;
using System.Collections.Generic;

public class AttackCooldownState : IEnemyState
{
    private float attackCooldown = 1.0f;
    private float cooldownTimer = 0f;
    private bool hasMoved = false;

    public void Enter(EnemyAI ai)
    {
        //Debug.Log("[FSM] Entered AttackCooldown State");
        cooldownTimer = 0f;
        hasMoved = false;
    }

    public void Update(EnemyAI ai)
    {
        cooldownTimer += Time.deltaTime;

        // 한 번만 이동 시도 (50% 확률)
        if (!hasMoved)
        {
            if (Random.value < 0.5f)
            {
                Vector2Int currentCell = GridScanner.Instance.WorldToCell(ai.transform.position);
                List<Vector2Int> neighbours = GridScanner.Instance.GetNeighbours(currentCell);

                if (neighbours.Count > 0)
                {
                    Vector2Int targetCell = neighbours[Random.Range(0, neighbours.Count)];
                    Vector3 worldPos = GridScanner.Instance.CellToWorld(targetCell);
                    ai.MoveTo(worldPos);
                }
            }
            hasMoved = true;
        }

        if (cooldownTimer >= attackCooldown && !ai.IsMoving)
        {
            ai.ChangeState(new ChaseState());
        }
    }

    public void Exit(EnemyAI ai)
    {
        //Debug.Log("[FSM] Exit AttackCooldown State");
    }
}