using UnityEngine;
using System.Collections;

public class Gobbo : MonoBehaviour
{
	static int score = 0;

		void Start ()
		{
		}
		// Update is called once per frame
		void Update ()
		{

		}

		void OnTriggerEnter2D (Collider2D collider)
		{
				score++;
				GameObject.FindGameObjectWithTag ("Score").guiText.text = "Score: " + score.ToString();
				Destroy (gameObject);
		}
}
