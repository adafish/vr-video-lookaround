using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using UnityEngine.Serialization;

/// <summary>
/// Utility for finding a video from streaming assets and playing that by URL
/// </summary>
public class VideoPlayURL : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField, FormerlySerializedAs("videoName")]
	private string androidVideoName = "corp_androidVideoName.mp4";
	[SerializeField]
	private string editorVideoName = "corp_editorVideoName.mp4";

	[Header("References")]
	[SerializeField]
	private VideoPlayer player;

	public UnityEvent OnEndReached;

	public delegate void LoadAction();
	public static event LoadAction OnVideoLoaded;

	private IEnumerator Start()
	{
		if (!player)
		{
			player = Camera.main.gameObject.AddComponent<VideoPlayer>();
			player.playOnAwake = false;
			player.renderMode = VideoRenderMode.CameraNearPlane; //this would look wacky
		}

		//in 2017 Unity does not support H.265 on PC but Android does. So
		//we have "editor" and "android" versions of each video. If you are building for android
		//and don't want the extra videos in Streaming Assets, taking them out of the project is fine
#if UNITY_ANDROID && !UNITY_EDITOR
		player.url = Application.streamingAssetsPath + @"/" + androidVideoName;
#else
		Debug.Log("Playing editor version");
		player.url = System.IO.Path.Combine(Application.streamingAssetsPath, editorVideoName);
#endif
		player.loopPointReached += EndReached;

		//set up
		player.frame = 0;
		player.Prepare();

		yield return new WaitUntil(() => player.isPrepared);

		if (OnVideoLoaded != null)
			OnVideoLoaded();

		player.Play();
	}

	public void EndReached(VideoPlayer vp)
	{
		vp.playbackSpeed = vp.playbackSpeed / 10.0f;
		OnEndReached.Invoke();
	}
}
