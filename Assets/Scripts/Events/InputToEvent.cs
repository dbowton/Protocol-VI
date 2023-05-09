using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputToEvent : MonoBehaviour
{
	[System.Serializable]
	public class KeyActionPair
	{ 
		public KeyCode keyCode;
		public UnityEvent action;
	}

	[SerializeField] List<KeyActionPair> keyPairs = new List<KeyActionPair>();

	private void Update()
	{
		foreach (var pair in keyPairs)
			if (Input.GetKeyDown(pair.keyCode))
			{
				print("Triggering");
				pair.action.Invoke();
			}
	}
}
