using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OutroTrigger : MonoBehaviour
{
	public static UnityEvent outroTriggered = new UnityEvent();

	public GameObject player;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject == player)
		{
			outroTriggered.Invoke();
		}

		// Deactivate this
		gameObject.SetActive(false);
	}
}
