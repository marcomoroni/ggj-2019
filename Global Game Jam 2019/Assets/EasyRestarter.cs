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
		if (Input.GetKey("r"))
		{
			timeRestartButtonHasBeenPressed += Time.deltaTime;
		}
		else if (Input.GetKeyUp("r"))
		{
			timeRestartButtonHasBeenPressed = 0;
		}

		if (timeRestartButtonHasBeenPressed > timeNeeded)
		{
			//Application.LoadLevel(Application.loadedLevel);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
