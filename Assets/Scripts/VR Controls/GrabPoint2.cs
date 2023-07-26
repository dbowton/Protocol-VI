using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class GrabPoint2 : MonoBehaviour
{
	public bool holdable = false;
	[ConditionalField("holdable")] public bool isPrimary = true;
	[ConditionalField("holdable"), Tooltip("RightHand flips X for LeftHand")] public Vector3 heldPos = Vector3.zero;
	[ConditionalField("holdable")] public Vector3 heldAngle = Vector3.zero;

	public UnityEvent OnGrab;
	public UnityEvent OnHold;
	public UnityEvent OnRelease;
}
