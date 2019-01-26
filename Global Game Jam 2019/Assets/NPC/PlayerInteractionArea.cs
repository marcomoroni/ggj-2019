using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractionArea : MonoBehaviour
{
	public GameObject player;
	public DialogueID dialogue;

	public class InteractionEvent : UnityEvent<DialogueID> { }
	public static InteractionEvent triggerDialogueAreaEnter = new InteractionEvent();
	public static InteractionEvent triggerDialogueAreaExit = new InteractionEvent();

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject == player)
		{
			triggerDialogueAreaEnter.Invoke(dialogue);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject == player)
		{
			triggerDialogueAreaExit.Invoke(dialogue);
		}
	}
}
