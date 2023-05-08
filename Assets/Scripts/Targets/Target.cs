using System;
using UnityEngine;

public abstract class Target : MonoBehaviour
{
	[SerializeField] TargetManager manager;
	[SerializeField] protected float speed = 4f;
	protected Action action;
	protected Vector3 start;
	protected Vector3 target;

	protected float count = 0;
	bool open = false;

	private void OnValidate()
	{
		if (transform.parent && transform.parent.TryGetComponent(out TargetManager targetManager))
		{
			manager = targetManager;
			manager.AddTarget(this);
		}
	}

	private void Update()
	{
		action?.Invoke();
	}

	public void Hit(int val)
	{
		if(open)
			manager?.AddScore(val);

		Close();
	}

	public void Open()
	{
		if (action != Closing)
		{
			action = Opening;
			open = true;
		}
	}

	public void Close()
	{
		if (action != Closing)
		{
			open = false;
			action = Closing;
		}
	}

	protected abstract void Opening();
	protected abstract void Closing();
	protected abstract void FixedUpdate();
}
