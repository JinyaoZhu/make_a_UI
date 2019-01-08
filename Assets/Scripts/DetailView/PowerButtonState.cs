using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerButtonState : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

     public bool buttonPressed = false;

	public void OnPointerDown (PointerEventData eventData) {
		buttonPressed = true;
		// Debug.Log("buttonPressed");
	}

	public void OnPointerUp (PointerEventData eventData) {
		buttonPressed = false;
		// Debug.Log("buttonReleased");
	}
}