using UnityEngine;
using System.Collections;

public class PlayerCroc : MonoBehaviour
{
		public AudioClip hitground;
		public AudioClip punch;
		public AudioClip death;
		public KeyCode moveUp;
		public KeyCode moveDown;
		public KeyCode moveLeft;
		public KeyCode moveRight;
		public KeyCode punchKey;
		private static int LEFT = -1;
		private static int RIGHT = 1;
		public static int dir = RIGHT;
		private bool canJump = false;
		private bool onGround = false;
		private bool jumping = false;
		private bool punching = false;
		private bool dead = false;
		private int jumpTime = 0;
		private float deathTime;
		private float speed = 5;
		private GameObject fist;
		private GameObject mainMusic;
		private float fistCoolDownStart = 0;
		private bool fistCoolDown = false;
		

		private void playerDie() {
			mainMusic.audio.Stop ();
			AudioSource.PlayClipAtPoint (death, Camera.main.transform.position, 0.5f);
			deathTime = Time.time;
			dead = true;
			
		}

		void Start ()
		{
				Physics2D.IgnoreLayerCollision (10, 8);
				fist = GameObject.FindGameObjectWithTag ("Fist");
				mainMusic = GameObject.FindGameObjectWithTag ("Muzak");
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
				GameObject.FindGameObjectWithTag ("Berry").layer = 12;
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
						AudioSource.PlayClipAtPoint (punch, Camera.main.transform.position, 0.5f);
				}
				if (!punching) {
						fist.transform.position = transform.position;
						if (fistCoolDown) {
								if (Time.time - fistCoolDownStart > 0.3) {
										fistCoolDown = false;
								}
						}
				}

				RaycastHit2D[] rayCast = Physics2D.RaycastAll (transform.position, new Vector2 (0, -1));
		if (GameObject.FindGameObjectWithTag ("Berry").layer == 8) {
						GameObject.FindGameObjectWithTag ("Berry").layer = 12;
				}
				foreach (RaycastHit2D hit in rayCast) {

			//{}
			if (hit.collider.gameObject.tag == "Ground"||hit.collider.gameObject.tag == "Berry") {
				if (rigidbody2D.velocity.y < 0) {
					if (hit.collider.gameObject.tag == "Berry")GameObject.FindGameObjectWithTag ("Berry").layer = 8;}
								if (hit.fraction < 1.5) {
										canJump = true;
								} else {
										canJump = false;
								}
								break;
						}
				}

				if (dead) {
					if ((Time.time - deathTime) > 6) {
						transform.position = new Vector2 (2.5f, 2.5f);
						mainMusic.audio.Play ();
						dead = false;
					}
				}
		}
		
		void OnCollisionEnter2D (Collision2D collision)
		{
				if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Berry") {
						onGround = true;
						AudioSource.PlayClipAtPoint (hitground, Camera.main.transform.position, 0.5f);
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
				if (other.gameObject.tag == "OneWayPlatform") {
						if (rigidbody2D.velocity.y < 0) {
								GameObject.FindGameObjectWithTag ("Berry").layer = 8;
						}
				}
				if (other.gameObject.tag == "Death") {
					if (!dead) playerDie();
				}
	}
}