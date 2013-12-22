using UnityEngine;
using System.Collections;

public class Gobbo : MonoBehaviour
{

		void Start ()
		{
		}
		// Update is called once per frame
		void Update ()
		{

		}

		void OnTriggerEnter2D (Collider2D collider)
		{
				Destroy (gameObject);
		}
}
