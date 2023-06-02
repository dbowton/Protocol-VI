using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
	[SerializeField] string levelName = "";
	private GameObject currentCube;

	List<string> currentCubes = new List<string>();

	public void LoadLevel()
	{
		if(levelName != "")
			SceneManager.LoadScene(levelName);
	}

	private void OnTriggerEnter(Collider other)
	{
		print("entered " + other.name);
		if(other.transform.root.TryGetComponent<LevelCube>(out LevelCube cube))
		{
			if (currentCube)
				currentCube.GetComponent<Rigidbody>().useGravity = true;

			levelName = cube.levelName;
			cube.GetComponent<Rigidbody>().useGravity = false;

			cube.transform.position = transform.position;

			currentCube = cube.gameObject;
		}
	}
}
