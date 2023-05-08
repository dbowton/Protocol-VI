using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using VRButton = UnityEngine.XR.CommonUsages;

public class GrabManagment : MonoBehaviour
{
    private void Update()
    {
        ManageGrabbedObjects(ControllerManager.Left, ControllerManager.rightController.heldPoint);
        ManageGrabbedObjects(ControllerManager.Right, ControllerManager.leftController.heldPoint);
    }

    private void ManageGrabbedObjects((UnityEngine.XR.InputDevice input, ControllerInitializer controller, Vector3 pos) main, GrabPoint otherGrabbedPoint)
    {
        if(main.input.GetControllerPressed(VRButton.gripButton, out bool grabbed))
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
}