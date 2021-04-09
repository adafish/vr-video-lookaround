using UnityEngine;

/// <summary>
/// Singleton that plays sounds.
/// </summary>
public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance = null;

	[SerializeField]
	private AudioSource uiSource;
	[SerializeField]
	private AudioClip selectionClip;
	[SerializeField]
	private AudioClip countdownTickClip;

	[SerializeField]
	private AudioSource backgroundSource;
	[SerializeField]
	private AudioClip backgroundClip;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;

		if (Instance != this)
			enabled = false;

		if (uiSource == null)
		{
			uiSource = CreateSource();
		}

		if (backgroundSource == null)
		{
			backgroundSource = CreateSource(true);
			backgroundSource.clip = backgroundClip;
			backgroundSource.Play();
		}
	}

	private AudioSource CreateSource(bool loop = false)
	{
		AudioSource source = gameObject.AddComponent<AudioSource>();
		source.playOnAwake = false;
		source.loop = loop;

		return source;
	}

	public void PlaySelectionSound()
	{
		uiSource.PlayOneShot(selectionClip);
	}
	
	public void PlayCountDownTick()
	{
		uiSource.PlayOneShot(countdownTickClip);
	}
}
