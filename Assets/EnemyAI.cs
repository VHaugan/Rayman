using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
		private bool onGround = false;
		private float speed = 4;

		void Start ()
		{
				print ("hiihi");
		}

		void Update ()
		{
				//if (onGround)
				rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
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
}

