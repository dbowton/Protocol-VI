using System.Collections.Generic;
using UnityEngine;

using VRButton = UnityEngine.XR.CommonUsages;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GrabPoint))]
public class ThrowableObject : MonoBehaviour
{
	List<Vector3> savedPositions = new List<Vector3>();
	[SerializeField, Tooltip("Time in seconds to track data")] float accuracy = 0.5f;
	[SerializeField] float force = 2f;
	float trackedTime = 0f;

	private bool isLeft = true;
	public void SetInput()
	{
		if (ControllerManager.rightController.heldPoint && ControllerManager.rightController.heldPoint.gameObject == gameObject) isLeft = false;
	}

	public void UpdateDirection()
	{
		return;
		savedPositions.Add(transform.position);
		trackedTime	+= Time.deltaTime;

		if(trackedTime >= accuracy)
			savedPositions.RemoveAt(0);

		trackedTime -= Time.deltaTime;
	}

	public void Throw()
	{
		/*
		if (savedPositions.Count <= 1) return;

		Vector3 direction = Vector3.zero;
		for (int i = 0; i < savedPositions.Count - 1; i++)
			direction = ((direction * i) + savedPositions[i + 1] - savedPositions[i]) / (i + 1);

		direction = direction.normalized;
		float magnitude = (Vector3.Distance(savedPositions[0], savedPositions[savedPositions.Count / 2])
							+ Vector3.Distance(savedPositions[savedPositions.Count / 2], savedPositions[savedPositions.Count-1]))
								/ accuracy;
		*/

		Vector3 direction;
		float magnitude = 1f;
		if (isLeft)
			ControllerManager.leftInput.GetControllerPressed(VRButton.deviceVelocity, out direction);
		else
			ControllerManager.rightInput.GetControllerPressed(VRButton.deviceVelocity, out direction);

		GetComponent<Rigidbody>().AddForce(force * magnitude * direction, ForceMode.Impulse);
	}
}
