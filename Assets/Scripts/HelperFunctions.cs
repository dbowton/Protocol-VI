using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HelperFunctions : MonoBehaviour
{
	int index = 0;
	Material tempMaterial = null;

	public void SetIndex(int index)
	{
		this.index = index;
	}

	public void SetMaterial(Material material)
	{
		this.tempMaterial = material;
	}

	public void SetMaterial(Renderer renderer)
	{
		if (tempMaterial == null) return;

		List<Material> mats = renderer.materials.ToList();
		mats[index] = tempMaterial;

		renderer.SetMaterials(mats);
	}

	public void DestroyGameObject(GameObject go)
	{
		Destroy(go);
	}


	List<Material> fadeMaterials = new List<Material>();

	public void SetFadeMaterials(GameObject go)
	{
		fadeMaterials = go.GetComponent<Renderer>().materials.ToList();
	}

	Timer fadeTimer;
	float fadeTime = 0;
	float fadeAccumulator = 0;
	public void FadeMaterials(float time)
	{
		fadeTime = time;
		fadeAccumulator = time;
		fadeTimer = new Timer(Time.deltaTime, () =>
		{
			fadeAccumulator -= Time.deltaTime;

			foreach(Material mat in fadeMaterials) 
			{
				Color temp = mat.color;
				temp.a = (fadeAccumulator < 0 ? 0 : fadeAccumulator) / fadeTime;
				mat.color = temp;
			}

			if (fadeAccumulator <= 0)
			{
				fadeTimer.Remove();
				fadeTimer = null;
			}


		}, false, true);
	}


	public void InstantiateGameObject(GameObject go)
	{
		Instantiate(go, transform.position, transform.rotation);
	}

	public void StopLoopingParticleEffect(ParticleSystem ps)
	{
		ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
	}
}
