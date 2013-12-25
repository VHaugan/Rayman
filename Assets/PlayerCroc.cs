using UnityEngine;
using System.Collections;

public class PlayerCroc : MonoBehaviour
{
	public AudioClip hitground;
	public AudioClip punch;
		public KeyCode moveUp;
		public KeyCode moveDown;
		public KeyCode moveLeft;
		public KeyCode moveRight;
		public KeyCode punchKey;
		private static int LEFT = -1;
		private static int RIGHT = 1;
		public static int dir = LEFT;
		private bool canJump = false;
		private bool onGround = false;
		private bool jumping = false;
		private bool punching = false;
		private int jumpTime = 0;
		private float speed = 5;
		private GameObject fist;
		private float fistCoolDownStart = 0;
		private bool fistCoolDown = false;

		void Start ()
		{
				Physics2D.IgnoreLayerCollision (10, 8);
				fist = GameObject.FindGameObjectWithTag ("Fist");
		}
	
		// Update is called once per frame

		void Update ()
		{
				if (Input.GetKey (moveUp)) {
						if (canJump) {
								rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 6);
								onGround = false;
								canJump = false;
								jumping = true;
						}
						if (jumping) {
								if (jumpTime == 20) {
										jumping = false;
										jumpTime = 0;
								}
								rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 10);
								jumpTime++;
						}
				} else {
						jumping = false;
						jumpTime = 0;
						
				}

				if (Input.GetKey (moveRight)) {

						rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
						if (dir == LEFT) {
								transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
								fist.SendMessage ("Flip");
								dir = RIGHT;
							
						}
				} else if (Input.GetKey (moveLeft)) {
						rigidbody2D.velocity = new Vector2 (speed * -1, rigidbody2D.velocity.y);
						if (dir == RIGHT) {
								fist.SendMessage ("Flip");
								transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
								dir = LEFT;
						}
				} else {
						rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
				}

				if (onGround && !Input.GetKey (moveUp)) {
						canJump = true;
				}
				if (rigidbody2D.velocity.y < -10) {
						rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, -10);
				}

				if (rigidbody2D.velocity.y < 0) {
						canJump = false;
						onGround = false;
				}

				if (Input.GetKey (punchKey) && !punching && !fistCoolDown) {
						fist.SendMessage ("punch");
						punching = true;
				AudioSource.PlayClipAtPoint(punch, Camera.main.transform.position, 0.5f);
				}
				if (!punching) {
						fist.transform.position = transform.position;
						if (fistCoolDown) {
								if (Time.time - fistCoolDownStart > 0.7) {
										fistCoolDown = false;
								}
						}
				}

				RaycastHit2D[] rayCast = Physics2D.RaycastAll (transform.position, new Vector2 (0, -1));

				
				foreach (RaycastHit2D hit in rayCast) {
						if (hit.collider.gameObject.tag == "Ground") {
								if (hit.fraction < 1.7) {
										canJump = true;
								} else {
										canJump = false;
								}
				break;
						}
				}
		}
		
		void OnCollisionEnter2D (Collision2D collision)
		{
				if (collision.gameObject.tag == "Ground") {
						onGround = true;
						AudioSource.PlayClipAtPoint(hitground, Camera.main.transform.position, 0.5f);
				}
				if (collision.gameObject.tag == "Death") {
					transform.position = new Vector2 (2.5f, 2.5f);
				}

	}

		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.gameObject.tag == "Fist" && punching) {
						fistCoolDownStart = Time.time;
						fistCoolDown = true;
						punching = false;
						fist.SendMessage ("reset");
				}
				
		}
}