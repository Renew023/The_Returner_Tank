using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject owner;
    public WeaponController countReturn;
    public Rigidbody2D rb;
    public float damage;
    public Vector2 direction;
    public float time;

    void Start()
    {
    }

    protected virtual void OnEnable()
    {
		StartCoroutine("Shoot");
	}
	protected virtual void OnDisable()
	{
        StopAllCoroutines();
		countReturn.pooling();
	}

	// Update is called once per frame
	public virtual void Update()
    {
        Action();
	}

    public virtual void Action()
    {
		rb.velocity = direction;
	}

    public virtual IEnumerator Shoot()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
