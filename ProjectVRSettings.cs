using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Adjust the render scale for VR.
/// </summary>
public class ProjectVRSettings : MonoBehaviour
{
	[SerializeField, Range(0.5f, 2.0f)]
	private float renderScale = 1.5f;

	private void Awake()
	{
		XRSettings.eyeTextureResolutionScale = renderScale;
	}
}
