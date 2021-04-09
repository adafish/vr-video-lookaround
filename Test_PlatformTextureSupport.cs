using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Test to log supported render texture formats to console
/// </summary>
public class Test_PlatformTextureSupport : MonoBehaviour
{
	[SerializeField]
	private bool printTextureSupportedInfo = false;

	private string support = "Support for ";

	private IEnumerator Start ()
	{
		if (!printTextureSupportedInfo)
			yield break;
		else
		{
			yield return new WaitForSeconds(1.0f);

			Debug.Log(support + " ARGB Half: " + SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf));
			Debug.Log(support + " ARGBFloat: " + SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBFloat));
			Debug.Log(support + " ARGB64: " + SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB64));
		}
	}
}
