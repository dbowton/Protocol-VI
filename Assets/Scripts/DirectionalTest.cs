using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalTest : MonoBehaviour
{
	[SerializeField] List<GameObject> positions = new List<GameObject>();
	[SerializeField] AudioChain chain;

	public void Test()
	{
		chain.Trigger();
	}

	private void Update()
	{
		if (chain.running)
		{
			chain.position = DirectionalObject.GetObjectBehind(gameObject, positions).transform.position;
		}
	}
}
