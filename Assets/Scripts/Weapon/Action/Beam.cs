using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : Arrow
{

    void Start()
    {
    }

    void OnEnable()
    {
		StartCoroutine("Beam");
	}
	void OnDisable()
	{
        StopAllCoroutines();
		countReturn.pooling();
	}

	// Update is called once per frame
	void Update()
    {
		Action();
	}

	public override void Action()
	{
		rb.velocity = direction;
	}

	public override IEnumerator Shoot()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
