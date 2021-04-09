using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles fading between scenes.
/// </summary>
public class SceneTransition : MonoBehaviour
{
	[Header("Scene Transition Settings")]
	[SerializeField]
	private bool fadeInOnStart;
	[SerializeField]
	private bool fadeInOnLoad = false;
	[SerializeField]
	private CanvasGroup fadeGroup;
	[SerializeField]
	private bool overrideTransitionTime = false; //one of the videos looks like garbage on startup
	[SerializeField, Range(0.0f, 5.0f)]	
	private float customTransitionTime = 1.5f; //...so we're making it take longer to load

	private Coroutine fadeRoutine;
	private WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();

	private void Start ()
	{
		if (fadeInOnStart && !fadeInOnLoad)
		{
			if (!overrideTransitionTime)
				StartCoroutine(FadeRoutine(0.0f, true));
			else
				StartCoroutine(FadeRoutine(0.0f, customTransitionTime, true)); //this is our custom transition time for garbage video
		}
	}

	private void OnEnable()
	{
		if (fadeInOnLoad)
			VideoPlayURL.OnVideoLoaded += StartFadeIn;
	}

	private void OnDisable()
	{
		if (fadeInOnLoad)
			VideoPlayURL.OnVideoLoaded -= StartFadeIn;
	}

	public void StartFadeIn()
	{
		StartCoroutine(FadeRoutine(0.0f, true));
	}

	public void StartFadeOut()
	{
		if (fadeRoutine != null)
			StopCoroutine(fadeRoutine);

		fadeRoutine = StartCoroutine(FadeRoutine(1.0f));
	}

	private IEnumerator FadeRoutine(float targetAlpha, bool pauseInteraction = false)
	{
		float startAlpha = fadeGroup.alpha;
		float timer = 0.0f;

		if (pauseInteraction && Interact.Instance != null)
			Interact.Instance.SetPauseInteraction(true);

		while (timer <= ProjectConstants.NumSceneTransitionTime)
		{
			fadeGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / ProjectConstants.NumSceneTransitionTime);

			yield return waitFrame;

			timer += Time.deltaTime;
		}

		fadeGroup.alpha = targetAlpha;

		if (pauseInteraction && Interact.Instance != null)
			Interact.Instance.SetPauseInteraction(false);
	}

	private IEnumerator FadeRoutine(float targetAlpha, float overrideTime, bool pauseInteraction = false)
	{
		float startAlpha = fadeGroup.alpha;
		float timer = 0.0f;

		if (pauseInteraction && Interact.Instance != null)
			Interact.Instance.SetPauseInteraction(true);

		while (timer <= ProjectConstants.NumSceneTransitionTime)
		{
			fadeGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / overrideTime);

			yield return waitFrame;

			timer += Time.deltaTime;
		}

		fadeGroup.alpha = targetAlpha;

		if (pauseInteraction && Interact.Instance != null)
			Interact.Instance.SetPauseInteraction(false);
	}
}
