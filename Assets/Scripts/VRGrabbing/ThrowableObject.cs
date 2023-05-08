using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GrabPoint))]
public class ThrowableObject : MonoBehaviour
{
	List<Vector3> savedPositions = new List<Vector3>();
	[SerializeField] int accuracy = 5;

	public void UpdateDirection()
	{
		savedPositions.Add(transform.position);

		if(savedPositions.Count > accuracy)
			savedPositions.RemoveAt(0);
	}

	public void Throw()
	{
		if (savedPositions.Count == 0 || savedPositions.Count == 1) return;

		Vector3 direction = Vector3.zero;
		for (int i = 0; i < savedPositions.Count - 1; i++)
			direction = ((direction * i) + savedPositions[i + 1] - savedPositions[i]) / (i + 1);

		GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
	}
}
