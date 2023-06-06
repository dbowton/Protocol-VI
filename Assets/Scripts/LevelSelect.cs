using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
	[SerializeField] TMPro.TMP_Text text;
	[SerializeField] string levelName = "";
	GameObject currentCube;

	public void LoadLevel()
	{
		if(levelName != "")
			SceneManager.LoadScene(levelName);
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.transform.root.TryGetComponent<LevelCube>(out LevelCube cube))
		{
			if (!cube.isHeld && cube.gameObject != currentCube)
			{
				if (currentCube)
					currentCube.GetComponent<Rigidbody>().useGravity = true;

				levelName = cube.levelName;
				text.text = levelName;
				cube.GetComponent<Rigidbody>().useGravity = false;
				cube.GetComponent<Rigidbody>().velocity = Vector3.zero;

				cube.transform.position = transform.position;

				currentCube = cube.gameObject;
			}
			else if (cube.isHeld && cube.gameObject == currentCube)
			{
				levelName = "";
				text.text = "";
				currentCube.GetComponent<Rigidbody>().useGravity = true;
				currentCube = null;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.root.TryGetComponent<LevelCube>(out LevelCube cube) && cube == currentCube)
		{
			levelName = "";
			text.text = "";
			currentCube.GetComponent<Rigidbody>().useGravity = true;
			currentCube = null;
		}
	}
}
