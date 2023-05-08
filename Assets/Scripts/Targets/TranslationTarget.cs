using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationTarget : Target
{
	[SerializeField][Tooltip("x - right | y - up | z - forward")] Vector3 moveMaxDir = Vector3.back;

	private void Start()
	{
		target = transform.position;
		start = transform.position;

		Vector3 temp = Vector3.zero;
		temp += moveMaxDir.x * transform.right;
		temp += moveMaxDir.y * transform.up;
		temp += moveMaxDir.z * transform.forward;

		moveMaxDir = temp;
	}

	protected override void Opening()
	{
		if (count > 0)
		{
			//			count--;
			count -= Time.deltaTime * speed;
			target = start + moveMaxDir * (count / 90f);
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
			target = start + moveMaxDir * (count / 90f);
		}
		else
		{
			target = start + moveMaxDir;
			action = null;
		}
	}

	protected override void FixedUpdate()
	{
		transform.position = target;
	}
}
