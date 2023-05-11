using UnityEngine;
using UnityEngine.Events;

public class RepeatEvent : MonoBehaviour
{
	[SerializeField] float delay;
	[SerializeField] UnityEvent action;
	[SerializeField] UnityEvent finalAction;

	Timer repeatTimer;

	int count = 0;

	public void Trigger(int count)
	{
		if (repeatTimer != null)
		{
			repeatTimer.Remove();
			repeatTimer = null;
		}

		action.Invoke();

		this.count = count - 1;
		repeatTimer = new Timer(delay,
			() =>
			{
				this.count--;

				action.Invoke();
				if (this.count <= 0)
				{
					finalAction.Invoke();
					repeatTimer.Remove();
					repeatTimer = null;
				}
			}, false, true);
	}
}
