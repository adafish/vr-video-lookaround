using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles gaze interaction.
/// </summary>
public class Interact : MonoBehaviour
{
	public static Interact Instance = null;

	[Header("Interaction Settings")]
	[SerializeField]
	private float maxDistance = 30.0f;
	[SerializeField]
	private LayerMask interactionLayers;
	[SerializeField]
	private Transform originTransform;

	[Header("Interaction UI References")]
	[SerializeField]
	private Image reticle;
	[SerializeField]
	private Text countdownText;

	//	Object Tracking	//
	private bool hasObject, hadObject;
	private Transform hitTransform;
	private InteractableObject foundObject, lastFoundObject;

	//	Countdown	//
	private Coroutine countDownRoutine;
	private WaitForSeconds oneSecondWait = new WaitForSeconds(1.0f);

	//	Selection	//
	public bool PauseInteraction { get; private set; }

	private void Awake()
	{
		if (Instance == null)
			Instance = this;

		if (Instance != this)
			enabled = false;

		if (originTransform == null)
			originTransform = Camera.main.transform;

		hasObject = hadObject = false;
		foundObject = lastFoundObject = null;
		PauseInteraction = false;
	}

	private void Update ()
	{
		if (!PauseInteraction)
		{
			DoRaycast();
			CheckFoundObject();
		}
	}

	public void DoRaycast()
	{
		RaycastHit hit;
		hasObject = Physics.Raycast(originTransform.position, originTransform.forward, out hit, maxDistance, interactionLayers);
		hitTransform = (hasObject ? hit.transform : null);
	}

	/// <summary>
	/// Check to see if we've found a new object, or lost the old one.
	/// </summary>
	public void CheckFoundObject()
	{
		if (hasObject)
		{
			foundObject = hitTransform.GetComponentInParent<InteractableObject>();

			if (foundObject != lastFoundObject)
			{
				StopCountDown(); //stop countdown
				if (foundObject) //if we have an object
					ResetCountDown(); //reset it
			}
		}
		else
		{
			foundObject = null;
		}

		if (hasObject && !hadObject) //if we found an object
		{
			OnFindObject();
		}
		else if (hadObject && !hasObject) //if we lost an object
		{
			OnLoseObject();
			StopCountDown();
		}

		hadObject = hasObject;
		lastFoundObject = foundObject;
	}

	public void OnFindObject()
	{
		reticle.enabled = false;
		countdownText.enabled = true;
	}

	public void OnLoseObject()
	{
		reticle.enabled = true;
		countdownText.enabled = false;
	}

	/// <summary>
	/// Stop countdown routine.
	/// </summary>
	public void StopCountDown()
	{
		if (countDownRoutine != null)
			StopCoroutine(countDownRoutine);
	}

	/// <summary>
	/// Stop countdown routine and reset it.
	/// </summary>
	public void ResetCountDown()
	{
		countDownRoutine = StartCoroutine(CountDown());
	}

	private IEnumerator CountDown()
	{
		for (int i = ProjectConstants.NumCountdownLength; i != 0; i--)
		{
			countdownText.text = i.ToString();
			SoundManager.Instance.PlayCountDownTick();
			yield return oneSecondWait;

			if (PauseInteraction)
				yield break;
		}

		Select();
	}

	public void Select()
	{
		reticle.enabled = true;
		countdownText.enabled = false;

		if (foundObject)
		{
			foundObject.Select();
			//Selection
			foundObject.Select();

			SoundManager.Instance.PlaySelectionSound();
		}
	}

	/// <summary>
	/// Pauses interaction to accomodate things like transition times
	/// </summary>
	/// <param name="paused"></param>
	public void SetPauseInteraction(bool paused)
	{
		PauseInteraction = paused;
	}
}
