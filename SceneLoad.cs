using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles scene loading and transitions.
/// </summary>
public class SceneLoad : MonoBehaviour
{
	public static bool SceneLoading = false;

	[Header("Scene Load Settings")]
	[SerializeField]
	private ProjectConstants.Scenes sceneToLoad = ProjectConstants.Scenes.Scene_Intro;

	public UnityEvent OnSceneTransitionStart;

	private Coroutine sceneLoadRoutine;
	private WaitForSeconds sceneTransitionTime;

	private string sceneLoadInProgressWarning = "Scene load already in progress.";

	public void Awake()
	{
		sceneTransitionTime = new WaitForSeconds(ProjectConstants.NumSceneTransitionTime);
	}

	public void StartSceneLoad()
	{
		if (!SceneLoading)
		{
			sceneLoadRoutine = StartCoroutine(LoadScene());
			SceneLoading = true;
		}
		else
		{
			Debug.LogWarning(sceneLoadInProgressWarning);
		}
	}

	private IEnumerator LoadScene()
	{
		OnSceneTransitionStart.Invoke();

		yield return sceneTransitionTime;

		SceneManager.LoadScene((int)sceneToLoad);
		SceneLoading = false;
	}
}
