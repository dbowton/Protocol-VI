using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using VRButton = UnityEngine.XR.CommonUsages;

public class GrabManager2 : MonoBehaviour
{
	private static GrabManager2 instance;
	public static GrabManager2 Instance { get { return instance; } }

	private void Awake() { if(instance == null) instance = this; }

	private GrabPoint2 leftGrab = null;
	private GrabPoint2 rightGrab = null;

	public float radius = 0.2f;

	private void ManageGrabbedObjects((UnityEngine.XR.InputDevice input, ControllerInitializer controller, Vector3 pos) main, GrabPoint otherGrabbedPoint)
	{
		if (main.input.GetControllerPressed(VRButton.gripButton, out bool grabbed))
		{
			if (grabbed)
			{
				//  Held
				if (main.controller.heldPoint)
				{
					main.controller.heldPoint.Hold();
				}
				else
				{
					//  Grab
					List<Collider> grabbedObjects = Physics.OverlapSphere(main.pos, 0.2f).OrderBy(x => Vector3.Distance(x.transform.position, main.pos)).ToList();
					foreach (var obj in grabbedObjects)
					{
						if (obj.gameObject.TryGetComponent<GrabPoint>(out GrabPoint grabbedPoint) && (otherGrabbedPoint == null || grabbedPoint != otherGrabbedPoint))
						{
							main.controller.heldPoint = grabbedPoint;
							grabbedPoint.Grab();
							return;
						}
					}
				}
			}
			else
			{
				//  release
				if (main.controller.heldPoint)
				{
					main.controller.heldPoint.Release();
					main.controller.heldPoint = null;
				}
			}
		}
	}

	private void Update()
	{
		UpdateRelease(ControllerManager.Left, leftGrab, rightGrab);
		UpdateRelease(ControllerManager.Right, rightGrab, leftGrab);

		UpdateGrab(ControllerManager.Left, leftGrab, rightGrab);
		UpdateGrab(ControllerManager.Right, rightGrab, leftGrab);

		UpdateHold(ControllerManager.Left, leftGrab, rightGrab);
		UpdateHold(ControllerManager.Right, rightGrab, leftGrab);
	}

	void UpdateRelease((UnityEngine.XR.InputDevice input, ControllerInitializer controller, Vector3 pos) main, GrabPoint2 grabPoint, GrabPoint2 other)
	{
		if (grabPoint)
		{
			//	release
			if (main.input.GetControllerPressed(VRButton.gripButton, out bool gripped) && !gripped)
			{
				//	Simplest Release
				if(other == null || grabPoint.transform.root.gameObject != other.transform.root.gameObject) 
				{
					if (grabPoint.transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
					{
						rb.useGravity = true;
						rb.isKinematic = false;
					}

					grabPoint.OnRelease.Invoke();
					grabPoint = null;
				}
				//	Simple Release
				else if(!grabPoint.isPrimary)
				{
					grabPoint.OnRelease.Invoke();
					grabPoint = null;
				}
				//	Hard
				else
				{
					
				}
			}
		}
	}

	void UpdateGrab((UnityEngine.XR.InputDevice input, ControllerInitializer controller, Vector3 pos) main, GrabPoint2 grabPoint, GrabPoint2 other)
	{
		if (!grabPoint)
		{
			//	Grab
			if (main.input.GetControllerPressed(VRButton.gripButton, out bool gripped) && gripped)
			{
				List<Collider> grabbedObjects = Physics.OverlapSphere(main.pos, radius).OrderBy(x => Vector3.Distance(x.transform.position, main.pos)).ToList();
				foreach (var obj in grabbedObjects)
				{
					if (obj.gameObject.TryGetComponent<GrabPoint2>(out GrabPoint2 grabbedPoint) && (other == null || grabbedPoint != other))
					{
						//	Simplest Grab
						if (other == null || grabbedPoint.transform.root.gameObject != other.transform.root.gameObject)
						{

						}
						//	Simple Grab
						else if (grabbedPoint.isPrimary && !grabbedPoint.isPrimary)
						{

						}
						//	Hard
						else
						{

						}
					}
				}
			}
		}
	}

	void UpdateHold((UnityEngine.XR.InputDevice input, ControllerInitializer controller, Vector3 pos) main, GrabPoint2 grabPoint, GrabPoint2 other)
	{
		if (grabPoint)
		{
			//	Simplest
			if (other == null || grabPoint.transform.root.gameObject != other.transform.root.gameObject)
			{

			}
			//	PAIN
			else if (grabPoint.isPrimary != other.isPrimary)
			{

			}
			//	PAIN BUT LESS
			else
			{

			}
		}
	}
}
