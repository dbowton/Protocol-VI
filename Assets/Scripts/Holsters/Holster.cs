using UnityEngine;

public class Holster : MonoBehaviour
{
	[SerializeField] float resetTime = 4f;
	[SerializeField] GunData gunData;

	GameObject spawnedObject;
	Timer reloadTimer;

	private void Start()
	{
		gunData.Enable();
		transform.localPosition = gunData.holsterPosition - (Vector3.up * transform.root.GetComponent<CharacterController>().height);

		reloadTimer = new Timer(resetTime, () =>
		{
			spawnedObject = Instantiate(gunData.GunPrefab, transform);
//			gunData.SetupReciever(spawnedObject.GetComponent<Reciever>());
			spawnedObject.GetComponent<GrabPoint>().OnGrab.AddListener(() => { reloadTimer.Reset(); gunData.SetupReciever(spawnedObject.GetComponent<Reciever>()); });
			spawnedObject.GetComponent<GrabPoint>().OnGrab.AddListener(() => { spawnedObject.GetComponent<GrabPoint>().OnGrab.RemoveListener(() => { reloadTimer.Reset(); gunData.SetupReciever(spawnedObject.GetComponent<Reciever>()); }); });
		});
	}
}
