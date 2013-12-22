using UnityEngine;
using System.Collections;

public class PlayerCroc : MonoBehaviour
{
		void Start ()
		{
		}

		public KeyCode moveUp;
		public KeyCode moveDown;
		public KeyCode moveLeft;
		public KeyCode moveRight;
		private bool canJump = false;
		private bool jumping = false;
		private int jumpTime = 0;
		private float speed = 7;

		// Update is called once per frame
		void Update ()
		{
				if (Input.GetKey (moveUp)) {
						if (canJump) {
								rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 10);
								canJump = false;
								jumping = true;
						}
						if (jumping) {
								if (jumpTime == 12) {
										jumping = false;
										jumpTime = 0;
								}
								rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 15);
								jumpTime++;
						}
				} else {
						jumping = false;
						jumpTime = 0;
						
				}

				if (Input.GetKey (moveRight)) {
						rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
				} else if (Input.GetKey (moveLeft)) {
						rigidbody2D.velocity = new Vector2 (speed * -1, rigidbody2D.velocity.y);
				} else {
						rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
				}	
		}

		void OnCollisionEnter2D (Collision2D collision)
		{
		if (collision.gameObject.tag == "Ground" && !Input.GetKey (moveUp)) {
						canJump = true;
				}
		}
}