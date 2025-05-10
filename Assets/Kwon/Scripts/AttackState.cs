using UnityEngine;

public class AttackState : IEnemyState
{
    public void Enter(EnemyAI ai)
    {
        Debug.Log("[FSM] Entered Attack State");

        // 즉시 공격
        ai.PerformAttack();

        // 공격 후 쿨다운 상태로 전환
        ai.ChangeState(new AttackCooldownState());
    }

    public void Update(EnemyAI ai)
    {
        // 공격은 Enter에서 수행하므로, Update는 필요 없음
    }

    public void Exit(EnemyAI ai)
    {
        Debug.Log("[FSM] Exit Attack State");
    }
}