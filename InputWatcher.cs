using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Right now we only have two input buttons.
/// This script just checks for escape; if escape has not been pressed,
/// it looks for mouse button zero
/// </summary>
public class InputWatcher : MonoBehaviour
{
	public UnityEvent OnEscape;
	public UnityEvent OnMouseDown;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			OnEscape.Invoke();
		else if (Input.GetMouseButtonDown(0))
			OnMouseDown.Invoke();
	}
}
