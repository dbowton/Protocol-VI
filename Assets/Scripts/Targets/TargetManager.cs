using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
	[SerializeField] float gameTime = 60f;
	[SerializeField] int startNumber = 4;
	[SerializeField] float interval = 0.5f;
	[SerializeField] int chance = 3;

	[SerializeField] List<Target> targets;
	[SerializeField] TMPro.TMP_Text timerText;
	[SerializeField] TMPro.TMP_Text scoreText;
	
	Timer gameTimer;
	Timer targetTimer;

	private int score = 0;

	private void Start()
	{
		foreach (var t in targets) t.Close();

		if (gameTimer != null) gameTimer.Remove();
		if (targetTimer != null) targetTimer.Remove();

		score = 0;

		scoreText.text = score.ToString("D2");
	}

	public void ResetTargets()
	{
		foreach (var t in targets) t.Close();

		if (gameTimer != null) gameTimer.Remove();
		if (targetTimer != null) targetTimer.Remove();

		score = 0;

		scoreText.text = score.ToString("D2");

		for (int i = 0; i < startNumber && i < targets.Count; i++)
		{
			int targetNum = Random.Range(0, targets.Count);
			print(targetNum);

			targets[i].Open();
		}

		gameTimer = new Timer(gameTime, () => { foreach (var t in targets) t.Close(); targetTimer.Remove(); });
		targetTimer = new Timer(interval, () =>
		{
			if (!gameTimer.IsOver && Random.Range(0, chance) == 0)
			{
				targets[Random.Range(0, targets.Count)].Open();
			}
		}, false, true);

	}

	public void AddTarget(Target target)
	{
		if(targets.Contains(target)) return;
		targets.Add(target);
	}

	private void Update()
	{
		if (gameTimer != null)
			timerText.text = (gameTimer.GetTime() - gameTimer.GetElapsed).ToString("F1");
		else timerText.text = "0.0";
	}

	public void AddScore(int val)
	{
		if (!gameTimer.IsOver)
		{
			score += val;
			scoreText.text = score.ToString("D2");
		}
	}
}
