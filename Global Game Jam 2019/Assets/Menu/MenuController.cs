using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public GameObject titleText;
	public GameObject ggjText;
	public GameObject mmText;
	public GameObject pressAnyKetText;

	private float timeBetweenTexts = 0.18f;

	private float timeQuitButtonHasBeenPressed = 0;
	private float timeNeeded = 1f;

	private void Start()
	{
		titleText.SetActive(false);
		ggjText.SetActive(false);
		mmText.SetActive(false);
		pressAnyKetText.SetActive(false);
	}

	private void Update()
	{
		StartCoroutine(ShowLabels());

		if (Time.timeSinceLevelLoad > 1f)
		{
			if (Input.GetKeyDown("space"))
				SceneManager.LoadScene(1);
		}

		if (Input.GetKey(KeyCode.Escape))
		{
			timeQuitButtonHasBeenPressed += Time.deltaTime;
		}
		else if (Input.GetKeyUp(KeyCode.Escape))
		{
			timeQuitButtonHasBeenPressed = 0;
		}

		if (timeQuitButtonHasBeenPressed > timeNeeded)
		{
			Debug.Log("Quitting...");
			Application.Quit();
		}
	}

	private IEnumerator ShowLabels()
	{
		yield return new WaitForSeconds(0.8f);

		titleText.SetActive(true);

		yield return new WaitForSeconds(timeBetweenTexts);

		ggjText.SetActive(true);

		yield return new WaitForSeconds(timeBetweenTexts);

		mmText.SetActive(true);

		yield return new WaitForSeconds(1.6f);

		pressAnyKetText.SetActive(true);
	}
}
