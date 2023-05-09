using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvent : MonoBehaviour
{
	int index = 0;
	[SerializeField] List<UnityEvent> actions;

	public void SetIndex(int index)
	{
		this.index = index;
	}

	public void Invoke(float delay)
	{
		int x = index;
		new Timer(delay, () => actions[x].Invoke(), true);
	}
}
