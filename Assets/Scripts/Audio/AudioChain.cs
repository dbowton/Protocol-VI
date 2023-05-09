using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioChain : MonoBehaviour
{
	public Vector3 position;
	public bool running = false;
    [SerializeField] List<AudioClip> clips= new List<AudioClip>();

	int index = 0;
    AudioSource source;
	Timer chainTimer = null;
	
	private void Start()
	{
		source = GetComponent<AudioSource>();
	}

	public void Trigger()
    {
		running = true;
		if(chainTimer != null) 
		{
			chainTimer.Remove();
			chainTimer = null;
			index = 0;
		}

		chainTimer = new Timer(0.5f,
				() =>
				{
					if (index >= clips.Count)
					{
						index = 0;
						running = false;
						source.clip = null;
						chainTimer.Remove();
						chainTimer = null;
						return;
					}

					if (clips[index])
					{
						transform.position = position;
						source.clip = clips[index];
						source.Play();
						chainTimer.Modify(clips[index].length);
					}
					else
						chainTimer.Modify(0.25f);

					index++;
					chainTimer.Reset();
				}, false, false);
	}
}
