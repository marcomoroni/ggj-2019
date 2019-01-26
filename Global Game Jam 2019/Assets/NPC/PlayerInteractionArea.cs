using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionArea : MonoBehaviour
{
	public GameObject player;
	public DialogueID dialogueToStart;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject == player)
		{

		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject == player)
		{

		}
	}
}
