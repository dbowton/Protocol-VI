using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using VRButton = UnityEngine.XR.CommonUsages;

public class UIManager : MonoBehaviour
{
	[SerializeField] List<GameObject> images = new List<GameObject>();

	[SerializeField] Material default_mat;
	[SerializeField] Material selected_mat;
	[SerializeField] Material clicked_mat;

	int index;
	bool ready = true;
	bool released = false;

	private void Start()
	{
		index = 0;
		images[index].GetComponent<Image>().material = selected_mat;
	}

	private void Update()
	{
		//	manual joystick scroll toggle 
		if (!released)
		{
			ControllerManager.rightInput.GetControllerPressed(VRButton.primaryButton, out bool startedPressed);
			released = !startedPressed;
		}
		//	manual primary button select
		else if (ControllerManager.rightInput.GetControllerPressed(VRButton.primaryButton, out bool pressed) && pressed)
		{
			images[index].GetComponent<Image>().material = clicked_mat;
			images[index].GetComponent<EventHolder>().action.Invoke();
		}
		//	manual joystick scroll
		else if (ControllerManager.rightInput.GetControllerPressed(VRButton.primary2DAxis, out Vector2 dir))
		{
			if (dir.y > 0.7f || dir.y < -0.7f)
			{
				if (ready)
				{
					if (dir.y > 0.7f)
					{
						images[index].GetComponent<Image>().material = default_mat;
						index--;
						if (index < 0) index += images.Count;
						images[index].GetComponent<Image>().material = selected_mat;
					}
					if (dir.y < -0.7f)
					{
						images[index].GetComponent<Image>().material = default_mat;
						index++;
						if (index >= images.Count) index -= images.Count;
						images[index].GetComponent<Image>().material = selected_mat;
					}

					ready = false;
				}
			}
			else
			{
				ready = true;
			}
		}

		//	raycast from input controllers
		//	if hit selectable object set index
		//	check triggers of selected controllers

		//	check Left
		Ray leftRay = new Ray(ControllerManager.leftController.transform.position, ControllerManager.leftController.transform.forward);
		if (Physics.Raycast(leftRay, out RaycastHit leftHitinfo))
		{
			if(images.Contains(leftHitinfo.collider.gameObject))
			{
				images[index].GetComponent<Image>().material = default_mat;
				for (index = 0; images[index] != leftHitinfo.collider.gameObject; index++) ;

				images[index].GetComponent<Image>().material = selected_mat;

				if(ControllerManager.leftInput.GetControllerPressed(VRButton.triggerButton, out bool Leftpressed) && Leftpressed)
				{
					images[index].GetComponent<Image>().material = clicked_mat;
					images[index].GetComponent<EventHolder>().action.Invoke();
				}
			}
		}

		//	check right
		Ray rightRay = new Ray(ControllerManager.rightController.transform.position, ControllerManager.rightController.transform.forward);
		if (Physics.Raycast(rightRay, out RaycastHit rightHitinfo))
		{
			if (images.Contains(rightHitinfo.collider.gameObject))
			{
				images[index].GetComponent<Image>().material = default_mat;
				for (index = 0; images[index] != rightHitinfo.collider.gameObject; index++) ;

				images[index].GetComponent<Image>().material = selected_mat;

				if (ControllerManager.leftInput.GetControllerPressed(VRButton.triggerButton, out bool rightPressed) && rightPressed)
				{
					images[index].GetComponent<Image>().material = clicked_mat;
					images[index].GetComponent<EventHolder>().action.Invoke();
				}
			}
		}
	}
}
