using MyBox;
using UnityEngine;

[System.Serializable]
public class Audio
{
	[SerializeField] public bool useRangeVolume = false;
	[HideInInspector] public bool useSetVolume = false;
	
	[ConditionalField("useRangeVolume"), Range(0, 1)] public float minVolume = 0f;
	[ConditionalField("useRangeVolume"), Range(0, 1)] public float maxVolume = 1f;
	[ConditionalField("useSetVolume"), Range(0, 1)] public float volume = 0.5f;

	[SerializeField] public bool useRangePitch = false;
	[HideInInspector] public bool useSetPitch = false;

	[ConditionalField("useRangePitch"), Range(-1, 1)] public float minPitch = 0f;
	[ConditionalField("useRangePitch"), Range(-1, 1)] public float maxPitch = 1f;
	[ConditionalField("useSetPitch"), Range(-1, 1)] public float pitch = 0f;

	[SerializeField, Range(-1,1), Tooltip("-1 Left, 1 Right")] float channelFavor = 0f;

	[SerializeField] public AudioClip clip = null;

	public float Volume
	{
		get 
		{ 
			if(useSetVolume)
				return volume;
			else
				return Random.Range(minVolume, maxVolume);
		}
	}

	public float Pitch
	{
		get 
		{ 
			if(useSetPitch)
				return pitch * 3f;
			else
				return Random.Range(minPitch, maxPitch) * 3f;
		}
	}

	public float ChannelFavor { get { return channelFavor; } }


	public void Validate()
	{
		useSetVolume = !useRangeVolume;
		useSetPitch = !useRangePitch;
	}
}
