using UnityEngine;

public abstract class BaseBattleUI : MonoBehaviour
{
    private int playerLevel;
    protected int PlayerLevel => playerLevel;
    public void SetPlayerLevel(int _playerLevel)
    {
        playerLevel = _playerLevel;
    }

    private float playerCurrentEXP;
    protected float PlayerCurrentEXP => playerCurrentEXP;
    public void SetPlayerCurrentEXP(float _playerCurrentEXP)
    {
        playerCurrentEXP = _playerCurrentEXP;
    }

    private float playerMaxEXP;
    protected float PlayerMaxEXP => playerMaxEXP;
    public void SetPlayerMaxEXP(float _playerMaxEXP)
    {
        playerMaxEXP = _playerMaxEXP;
    }

    private float playerCurrentHP;
    protected float PlayerCurrentHP => playerCurrentHP;
    public void SetPlayerCurrentHP(float _playerCurrentHP)
    {
        playerCurrentHP = _playerCurrentHP;
    }

    private float playerMaxHP;
    protected float PlayerMaxHP => playerMaxHP;
    public void SetPlayerMaxHP(float _playerMaxHP)
    {
        playerMaxHP = _playerMaxHP;
    }

    // 사망했을 때 실행될 함수
    // ESC키 눌렀을 때 실행될 함수
}
