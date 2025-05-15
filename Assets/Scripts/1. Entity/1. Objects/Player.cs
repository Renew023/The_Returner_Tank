using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    #region Player 객체 변수 선언

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] private AttackTarget attackTarget;

    [SerializeField] private Camera camera;

    [SerializeField] private Vector2 mousePosition;
    [SerializeField] private Vector2 worldPos;

    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Transform headTransform;

    public PlayerValue playerValue;

    private float demandExp;

    [SerializeField] private Image hpBarFill;

    [SerializeField] public List<Skill> playerSkill = new List<Skill>(5);
    [SerializeField] public GameObject skillSelectUI;

    protected Animator animator;

    #endregion

    #region Awake 메서드
    void Awake()
    {
        rb.freezeRotation = true;
        animator = GetComponentInChildren<Animator>();
	}

    #endregion

    #region OnEnable, OnDisable 메서드

    void OnEnable()
    {
        DataManager.instance.Pick();

        for (int i = 0; i < DataManager.instance.playerValue.playerSkill.Count; i++)
        {
            playerValue.playerSkill.Add(DataManager.instance.playerValue.playerSkill[i]);

            if (playerValue.playerSkill[i].weaponCon != null)
            {
                playerValue.weapons.Add(Instantiate(DataManager.instance.playerValue.playerSkill[i].weaponCon, transform.position, Quaternion.identity, transform));
            }
        }

        for (int j = 0; j < playerValue.weapons.Count; j++)
        {
            playerValue.weapons[j].weapon = DataManager.instance.playerWeapon[j];
        }
        playerValue.playerWeaponStat = DataManager.instance.playerValue.playerWeaponStat;
        playerValue.Exp = DataManager.instance.playerValue.Exp;
        playerValue.Level = DataManager.instance.playerValue.Level;

        DataManager.instance.Pick();

        SetDemandExp(playerValue.Level);
    }

    void OnDisable()
    {
        DataManager.instance.playerValue = playerValue;

        for (int i = 0; i < DataManager.instance.playerWeapon.Count; i++)
        {
            DataManager.instance.playerWeapon[i] = playerValue.weapons[i].weapon;
        }

        for (int j = DataManager.instance.playerWeapon.Count; j < playerValue.weapons.Count; j++)
        {
            DataManager.instance.playerWeapon.Add(playerValue.weapons[j].weapon);
        }
		DataManager.instance.savedPlayerHp = CurHP;
		DataManager.instance.savedPlayerMaxHp = MaxHp;
	}

    #endregion

    #region Start 메서드
    private new void Start()
    {
        base.Start();

        if (DataManager.instance.savedPlayerMaxHp > 0)
        {
            maxHp = DataManager.instance.savedPlayerMaxHp;
            curHp = DataManager.instance.savedPlayerHp;
            hpBarFill.fillAmount = curHp / maxHp;
            UIManager.Instance.uiController.playerLevel.UpdateValue((int)playerValue.Level + 1);
            UIManager.Instance.uiController.playerHP.UpdateValue(curHp, maxHp);
            UIManager.Instance.uiController.playerEXP.UpdateValue(playerValue.Exp, demandExp);
        }
    }

    #endregion

    #region Update 메서드
    void Update()
    {
        mousePosition = Input.mousePosition;
        worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = attackTarget.Searching(lookDirection, transform.position);
        RotateHead();

        for (int i = 0; i < playerValue.weapons.Count; i++)
        {
            playerValue.weapons[i].targetDirect = lookDirection;
            playerValue.weapons[i].playerWeaponStat = playerValue.playerWeaponStat;
        }

        Move();

        Rotate();

        //  테스트용 키 세팅입니다. 튜터님들 편하신대로 사용하셔도 됩니다!!
        if (Input.GetKeyDown(KeyCode.R))
        {
            LevelUp();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.uiController.pauseUI.PauseMenuToggle(); 
        }
    }

    #endregion

    #region Move, Rotate, RotateHead 메서드 → 현재 플레이어 오브젝트가 탱크가 포신 부분, 몸통 부분 2개로 분리하였기에 Rotate를 2개로 구분하여 적용했습니다.
    override protected void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(x, y).normalized;
        rb.velocity = move * moveSpeed;
    }

    override protected void Rotate()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            bodyTransform.rotation = Quaternion.Euler(0, 0, angle + 90f); // ??? +90 ??
        }
    }

    private void RotateHead()
    {
        if (lookDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            headTransform.rotation = Quaternion.Euler(0, 0, angle - 100f);
        }
    }

    #endregion

    #region HpUp, MoveSpeedUp 메서드 → 레벨 업 후 생성되는 선택지 UI에서 플레이어가 체력 증가, 이속 증가를 선택했을 경우를 반영하는 메서드
    public void HpUp(float value = 1)
    {
        maxHp += value;
        curHp += value;
        hpBarFill.fillAmount = curHp / maxHp;
        if (UIManager.Instance != null
        && UIManager.Instance.uiController != null
        && UIManager.Instance.uiController.playerHP != null)
        {
            UIManager.Instance.uiController.playerHP.UpdateValue(curHp, maxHp);
        }
    }

    public void MoveSpeedUp(float value = 1)
    {
        moveSpeed += value;
    }

    #endregion

    #region LevelUp 메서드 → 레벨 업 후 생성되는 선택지 UI와 함께 플레이 타임 정지 및 스탯 상승 기능을 관리하는 메서드
    private void LevelUp()
    {
        playerValue.Level += 1f;
        SetDemandExp(playerValue.Level);

        HpUp(50);
        Time.timeScale = 0.0f;
        skillSelectUI.gameObject.SetActive(true);

        if (curHp + maxHp * .1f < maxHp)
        {
            curHp += maxHp * .1f;
        }
        else
        {
            curHp = maxHp;
        }

        hpBarFill.fillAmount = curHp / maxHp;

        UIManager.Instance.uiController.playerHP.UpdateValue(curHp, maxHp);
        UIManager.Instance.uiController.playerLevel.UpdateValue((int)playerValue.Level + 1);
    }

    #endregion

    #region 투사체와 충돌 시를 처리하는 메서드
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Arrow arrow = collision.gameObject.GetComponent<Arrow>();
            if (arrow.owner == this.gameObject)
                return;

            //Hit(ref curHp, arrow.damage);
            TakeDamage(arrow.damage);

            collision.gameObject.SetActive(false);
        }
    }

    #endregion

    #region TakeDamage 메서드 → 데미지 처리 기능
    public void TakeDamage(float damage)
    {
        curHp -= damage;

        hpBarFill.fillAmount = curHp / maxHp;

        UIManager.Instance.uiController.playerHP.UpdateValue(curHp, maxHp);

        if (curHp <= 0)
        {
            UIManager.Instance.uiController.deathUI.Show(true);
        }
    }

    #endregion

    #region SetDemandExp, AddExp 메서드 → 경험치 획득 시 처리 기능들
    private void SetDemandExp(float lev)
    {
        demandExp = 250 * (lev + 1);
    }

    public void AddExp(int amount)
    {
        playerValue.Exp += amount;
        if (playerValue.Exp >= demandExp)
        {
            playerValue.Exp -= demandExp;
            LevelUp();
        }

        UIManager.Instance.uiController.playerEXP.UpdateValue((float)playerValue.Exp, (float)demandExp);
    }

    #endregion

    #region HealTrigger, LevelUpTrigger 메서드 → 이벤트 씬들 (HealScene, EventScene에 존재하는 오브젝트와 충돌 시를 처리하는 기능
    public void HealTrigger(int healAmount)
    {
        if (healAmount + curHp > maxHp)
        {
            curHp = maxHp;
        }
        else
        {
            curHp = (int)(healAmount + curHp);
        }
        hpBarFill.fillAmount = curHp / maxHp;
        if (UIManager.Instance != null
        && UIManager.Instance.uiController != null
        && UIManager.Instance.uiController.playerHP != null)
        {
            UIManager.Instance.uiController.playerHP.UpdateValue(curHp, maxHp);
        }
    }

    public void LevelUpTrigger(int amount)
    {
        int Expamount = amount;
        Expamount = (int)(demandExp - playerValue.Exp);

        AddExp(Expamount);
    }

    #endregion
}