using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Character
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float attackInterval = 2f;
    private float attackerTimer = 0.0f;

    private Player target;

    protected Animator animator;

    protected override void Start()
    {
        base.Start();
        target = FindObjectOfType<Player>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        attackerTimer += Time.deltaTime;

        if(attackerTimer >= attackInterval)
        {
            attackerTimer = 0.0f;

            int attackPattern = Random.Range(0, 3);

            switch(attackPattern)
            {
                case 0: 
                    BasicAttack();
                    break;
                case 1: ConeAttack();
                    break;
                case 2:
                    GroundSlam();
                    break;
            }
        }
    }

    void BasicAttack()
    {
        Vector2 dir = (target.transform.position - transform.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().velocity = dir * 10f; // ���� �ӵ�
    }

    void ConeAttack()
    {
        int bulletCount = 5;
        float angleStep = 15f;
        float angleStart = -((bulletCount - 1) * angleStep) / 2;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = angleStart + angleStep * i;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * (target.transform.position - transform.position).normalized;

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = dir * 8f;
        }
    }

    void GroundSlam()
    {
        // ���⼱ �ܼ��� ���� �ȿ� �ִ� �÷��̾�� ���ظ� �ִ� ������ ó��
        float slamRadius = 3.0f;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, slamRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<Player>().TakeDamage(20); // ���� ������
            }
        }

        // �ִϸ��̼��̳� ����Ʈ�� ���⿡�� ���� ����
        Debug.Log("Boss ��� - ���� ��Ÿ!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;

        animator.SetBool("IsDamage", true);

        //	���� �ð��� ���� ��, Damage �ִϸ��̼� �÷��� �ʱ�ȭ
        StartCoroutine(ResetDamageAnim());

        if (curHp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        gameObject.SetActive(false);

        //	�ش� ���Ͱ� �����ִ� ���� �� ����.
        DungeonManager.instance.OnEnemyDeath();
    }

    private IEnumerator ResetDamageAnim()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("IsDamage", false);
    }
}
