using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
	[SerializeField] List<AudioClip> clips = new List<AudioClip>();

	public void PlayRandom()
	{
		AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Count)], transform.position);
	}
}
