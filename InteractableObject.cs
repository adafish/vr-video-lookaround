using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
	public UnityEvent OnSelect;

	public void Select()
	{
		OnSelect.Invoke();
	}
}
