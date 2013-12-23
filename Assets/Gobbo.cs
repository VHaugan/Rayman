using UnityEngine;
using System.Collections;

public class Gobbo : MonoBehaviour
{
	static int score = 0;
	private float x = 0;
	private float initHeight;
		void Start ()
	{initHeight = transform.position.y;
		}
		// Update is called once per frame
		void Update ()
		{
		x++;
		transform.position = new Vector2 (transform.position.x, initHeight + Mathf.Sin (x/30)/8);
		}

		void OnTriggerEnter2D (Collider2D collider)
		{
				score++;
				GameObject.FindGameObjectWithTag ("Score").guiText.text = "Score: " + score.ToString();
				Destroy (gameObject);
		}
}
