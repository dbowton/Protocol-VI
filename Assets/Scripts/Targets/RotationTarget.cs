using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotationTarget : Target
{
	[SerializeField] GameObject hinge;

	private void Start()
	{
		start = hinge.transform.localEulerAngles;
	}

	protected override void Opening()
	{
		if (count > 0)
		{
			//			count--;
			count -= Time.deltaTime * speed;
			target = start + Vector3.right * count;
		}
		else
		{
			target = start;
			action = null;
		}
	}
	protected override void Closing()
	{
		if (count < 90)
		{
			//			count = Mathf.Min(90, count + 2);
			count += Time.deltaTime * 2f * speed;
			target = start + Vector3.right * count;
		}
		else
		{
			target = start + Vector3.right * 90;
			action = null;
		}
	}

	protected override void FixedUpdate()
	{
		hinge.transform.localEulerAngles = target;
	}
}
