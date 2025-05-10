using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject owner;

    public WeaponController countReturn;
    [SerializeField] private Rigidbody2D rb;
    public float damage;
    public Vector2 direction;
    public float time;

    void Start()
    {
    }

    void OnEnable()
    {
		StartCoroutine("Shoot");
	}
	void OnDisable()
	{
        StopAllCoroutines();
		countReturn.pooling();
	}

	// Update is called once per frame
	void Update()
    {
        rb.velocity = direction;
	}

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
