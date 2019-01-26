using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSnap : MonoBehaviour
{
	public GameObject player;
	public GameObject snapCamera;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject == player)
		{
			snapCamera.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject == player)
		{
			snapCamera.SetActive(false);
		}
	}
}
