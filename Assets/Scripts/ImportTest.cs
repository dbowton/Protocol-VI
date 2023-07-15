using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportTest : MonoBehaviour
{
	public bool ShowFloat = true;
	[ConditionalField("ShowFloat")] public float floatInfo = 5;
}
