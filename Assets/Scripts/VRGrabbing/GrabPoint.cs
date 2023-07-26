using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class GrabPoint : MonoBehaviour
{	
	[SerializeField] bool holdable = false;
	[ConditionalField("holdable"), Tooltip("RightHand flips X for LeftHand")] public Vector3 heldPos = Vector3.zero;
	[ConditionalField("holdable")] public Vector3 heldAngle = Vector3.zero;

	public UnityEvent OnGrab;
	public UnityEvent OnHold;
	public UnityEvent OnRelease;

	public void Grab()
	{
		if (holdable)
		{
			Vector3 holdPosition = heldPos;

			if (ControllerManager.rightController.heldPoint != null && ControllerManager.rightController.heldPoint.gameObject == gameObject)
				transform.parent = ControllerManager.rightController.transform.parent;
			else
			{
				transform.parent = ControllerManager.leftController.transform.parent;
				holdPosition.x = -holdPosition.x;
			}

			transform.localPosition = holdPosition;
			transform.localEulerAngles = heldAngle;

			if (transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
			{
				rb.useGravity = false;
				rb.isKinematic = true;
			}
		}

		OnGrab.Invoke();
	}

	public void Hold()
	{
		OnHold.Invoke();
	}

	public void Release()
	{
		if (holdable)
		{
			transform.parent = null;

			if (transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
			{
				rb.useGravity = true;
				rb.isKinematic = false;
			}
		}

		OnRelease.Invoke();
	}
}
