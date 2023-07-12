using UnityEngine;
using UnityEngine.Events;

public class GrabPoint : MonoBehaviour
{	
	[SerializeField] bool holdable = false;

	public UnityEvent OnGrab;
	public UnityEvent OnHold;
	public UnityEvent OnRelease;

	public void Grab()
	{
		if (holdable)
		{
			if (ControllerManager.rightController.heldPoint != null && ControllerManager.rightController.heldPoint.gameObject == gameObject)
				transform.parent = ControllerManager.rightController.transform.parent;
			else
				transform.parent = ControllerManager.leftController.transform.parent;

			transform.localPosition = Vector3.zero;
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
			transform.parent = null;

		OnRelease.Invoke();
	}
}
