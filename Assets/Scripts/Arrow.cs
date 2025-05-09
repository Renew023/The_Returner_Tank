using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private WeaponController countReturn;
    [SerializeField] private Rigidbody2D rb;
    public float damage;
    public float speed;
    public Vector2 direction;
    public float time = 3f;

    // Start is called before the first frame update
    void Start()
    {
        countReturn = GameObject.Find("Weapon").GetComponent<WeaponController>();
    }

    void OnEnable()
    {
		StartCoroutine("Shoot");
	}

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * 5f;
	}

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(time);
        countReturn.pooling();
        gameObject.SetActive(false);
    }

	public void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //collision.gameObject.GetComponent<
        }
        //배틀 스크립트 필요 : damage -> Monster(damage);
	}
}
