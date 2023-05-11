using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>();
	[SerializeField] List<GameObject> spawnPoints = new List<GameObject>();

	[SerializeField] GameObject player;
	[SerializeField] int maxEnemies = -1;
	[SerializeField] float spawnTime = 0.5f;

	int enemyCount = 0;

	Camera mainCam;

	private void Start()
	{
		mainCam = Camera.main;

		new Timer(spawnTime, () => SpawnEnemy(), false, true);
	}

	public void SpawnEnemy()
	{
		if (maxEnemies != -1 && enemyCount >= maxEnemies) return;

		GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], 
			DirectionalObject.GetObjectFrom(mainCam.gameObject, spawnPoints, 120f).transform.position, Quaternion.identity);

		enemy.GetComponent<Enemy>().target = player;
		enemy.GetComponent<Health>().OnDeath.AddListener(() => enemyCount--);

		enemyCount++;
	}
}
