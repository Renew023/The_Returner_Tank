public class BossBattleUI : BaseBattleUI
{
    private float bossCurrentHP;
    private float BossCurrentHP => bossCurrentHP;
    public void SetBossCurrentHP(float _bossCurrentHP)
    {
        bossCurrentHP = _bossCurrentHP;
    }

    private float bossMaxHP;
    private float BossMaxHP => bossMaxHP;
    public void SetBossMaxHP(float _bossMaxHP)
    {
        bossMaxHP = _bossMaxHP;
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}
