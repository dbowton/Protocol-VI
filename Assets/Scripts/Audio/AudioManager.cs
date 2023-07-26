using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private static AudioManager instance = null;
	public static AudioManager Instance
	{
		get 
		{ 
			if (instance == null)
			{
				GameObject go = new GameObject();
				go.name = "AudioManager";
				instance = go.AddComponent<AudioManager>();
			}

			return instance;
		}
	}

	private List<AudioSource> sources = new List<AudioSource>();

	private void Awake()
	{
		if (instance == null) instance = this;
	}

	private void Start()
	{
		
	}

	public void Play(Audio clip, Transform parent = null)
	{
		if (instance != this)
		{
			Instance.Play(clip);
		}
		else
		{
			GameObject go = new GameObject();
			go.transform.parent = parent;
			go.name = clip.clip.name;
			AudioSource source = go.AddComponent<AudioSource>();
			source.clip = clip.clip;
			source.volume = clip.Volume;
			source.pitch = clip.Pitch;
			source.panStereo = clip.ChannelFavor;

			source.Play();
			Destroy(go, source.clip.length + Time.deltaTime * 2f);
		}
	}
}
