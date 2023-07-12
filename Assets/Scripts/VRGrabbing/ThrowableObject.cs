using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GrabPoint))]
public class ThrowableObject : MonoBehaviour
{
	List<Vector3> savedPositions = new List<Vector3>();
	[SerializeField, Tooltip("Time in seconds to track data")] float accuracy = 5;

	float trackedTime = 0f;

	public void UpdateDirection()
	{
		savedPositions.Add(transform.position);
		trackedTime	+= Time.deltaTime;

		if(trackedTime >= accuracy)
			savedPositions.RemoveAt(0);

		trackedTime -= Time.deltaTime;
	}

	public void Throw()
	{
		if (savedPositions.Count <= 1) return;

		Vector3 direction = Vector3.zero;
		for (int i = 0; i < savedPositions.Count - 1; i++)
			direction = ((direction * i) + savedPositions[i + 1] - savedPositions[i]) / (i + 1);

		direction = direction.normalized;
		float magnitude = (Vector3.Distance(savedPositions[0], savedPositions[savedPositions.Count / 2])
							+ Vector3.Distance(savedPositions[savedPositions.Count / 2], savedPositions[savedPositions.Count-1]))
								/ accuracy;

		GetComponent<Rigidbody>().AddForce(direction * magnitude, ForceMode.Impulse);
	}
}
