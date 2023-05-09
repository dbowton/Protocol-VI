using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
	[SerializeField] List<AudioClip> clips = new List<AudioClip>();
	[SerializeField] AudioSource source;


	public void PlayRandomClip()
	{
		if (clips.Count == 0) return;
		AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Count)], transform.position);
	}

	public void PlayRandom()
	{
		if (clips.Count == 0) return;
		if (source != null && !source.isPlaying)
		{
			source.clip = clips[Random.Range(0, clips.Count)];
			source.Play();
		}
	}
}
