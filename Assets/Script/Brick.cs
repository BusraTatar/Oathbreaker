using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	Vector3 startPos;
	Rigidbody2D rb;

	// Use this for initialization
	void Start()
	{
		startPos = transform.position;
		rb = GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.name.Equals("Player"))
		{
			Invoke("DropPlatform", 0.5f);
			//Destroy(gameObject, 2f);
		}
	}
	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.name.Equals("Player"))
		{
			transform.position = startPos;
			rb.isKinematic = true;
			//Destroy(gameObject, 2f);
		}
	}


	void DropPlatform()
	{
		rb.isKinematic = false;
	}
}
