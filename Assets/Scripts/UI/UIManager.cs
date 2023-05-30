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
		if (!released)
		{
			ControllerManager.rightInput.GetControllerPressed(VRButton.primaryButton, out bool startedPressed);
			released = !startedPressed;
		}
		else if (ControllerManager.rightInput.GetControllerPressed(VRButton.primaryButton, out bool pressed) && pressed)
		{
			images[index].GetComponent<Image>().material = clicked_mat;
			images[index].GetComponent<EventHolder>().action.Invoke();
		}
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
	}
}
