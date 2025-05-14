public interface IEnemyState
{
    void Enter(EnemyAI ai);
    void Update(EnemyAI ai);
    void Exit(EnemyAI ai);
}