using UnityEngine;
using System.Collections;

public class BounceObject : MonoBehaviour {
	
	private float bounceSpeed = 15;
	private float xSpeed = 0;
	private float punchForce = 2;
	private GameObject fist;

	// Use this for initialization
	void Start () {
		fist = GameObject.FindGameObjectWithTag ("Fist");
	}

	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Ground") {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, bounceSpeed);
		}
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.gameObject.tag == "Fist" && Fist.punching) {
						if (rigidbody2D.IsSleeping ()) {
								rigidbody2D.WakeUp ();
						renderer.sortingOrder = 1;
				}
						else {
								rigidbody2D.velocity = new Vector2 (Mathf.Sign (collider.rigidbody2D.velocity.x) * 5, 0);
						}
				}
	}
}