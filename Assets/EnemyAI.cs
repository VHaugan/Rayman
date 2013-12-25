using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
		private bool onGround = false;
		private float speed = 4;

		void Start ()
		{
		}

		void Update ()
		{
				if (onGround)
						rigidbody2D.velocity = new Vector2 (-speed, rigidbody2D.velocity.y);
				if (rigidbody2D.velocity.y < 0) {
						onGround = false;
				}
		}

		void OnCollisionEnter2D (Collision2D collision)
		{
				if (collision.gameObject.tag == "Ground") {
						onGround = true;
				}
		}

		void OnTriggerEnter2D (Collider2D collider)
		{
				if (collider.tag == "Reverse") {
						speed *= -1;
						transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
						transform.position = new Vector2 (transform.position.x - 53 / 100, transform.position.y);
				}
				if (collider.gameObject.tag == "Fist") {
						Destroy (gameObject);	
				}
		}
}