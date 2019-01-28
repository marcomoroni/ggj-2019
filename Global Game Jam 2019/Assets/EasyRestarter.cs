using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyRestarter : MonoBehaviour
{
	private float timeRestartButtonHasBeenPressed = 0;
	public float timeNeeded = 3.0f;

	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			timeRestartButtonHasBeenPressed += Time.deltaTime;
		}
		else if (Input.GetKeyUp(KeyCode.Escape))
		{
			timeRestartButtonHasBeenPressed = 0;
		}

		if (timeRestartButtonHasBeenPressed > timeNeeded)
		{
			//Application.LoadLevel(Application.loadedLevel);
			SceneManager.LoadScene(0);
		}
	}
}
