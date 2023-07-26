using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
	[SerializeField] List<Audio> audioClips = new List<Audio>();

	private void OnValidate()
	{
		foreach (var clip in audioClips)
		{
			clip.Validate();
		}
	}

	public Audio Play()
	{
		if(audioClips.Count == 0) return null;

		Audio clip = audioClips[Random.Range(0, audioClips.Count)];
		AudioManager.Instance.Play(clip);

		return clip;
	}

	public Audio Play(Audio clip)
	{
		AudioManager.Instance.Play(clip);
		return clip;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Play();
		}
	}
}
