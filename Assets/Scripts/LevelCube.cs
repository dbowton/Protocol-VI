using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCube : MonoBehaviour
{
	public string levelName;
	public bool isHeld = false;

	public void IsHeld(bool held)
	{
		isHeld = held;
	}
}
