using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDriver : MonoBehaviour
{
	[SerializeField] Transform referencedObject;

	private void Update()
	{
		transform.localEulerAngles = Vector3.up * referencedObject.localEulerAngles.y;
	}
}
