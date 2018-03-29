using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
	public float speed;

	public float lifetime;

	void Start ()
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		rb.velocity = transform.up * speed;

		Destroy (gameObject, lifetime);
	}

}



