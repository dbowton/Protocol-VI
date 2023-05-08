using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	[SerializeField] string sceneName;

	public void LoadScene()
	{
		SceneManager.LoadScene(sceneName);
//		SceneManagement.instance.LoadScene(sceneName, fadeOut, fadeIn, fadescreen, minTime);
	}
}
