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
			{
				transform.parent = ControllerManager.rightController.transform.parent;
				ControllerManager.rightController.controllerObject.SetActive(false);
			}
			else
			{
				transform.parent = ControllerManager.leftController.transform.parent;
				ControllerManager.leftController.controllerObject.SetActive(false);
			}

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
		{
			if (ControllerManager.rightController.heldPoint != null && ControllerManager.rightController.heldPoint.gameObject == gameObject)
				ControllerManager.rightController.controllerObject.SetActive(true);
			else
				ControllerManager.leftController.controllerObject.SetActive(true);

			transform.parent = null;
		}

		OnRelease.Invoke();
	}
}
