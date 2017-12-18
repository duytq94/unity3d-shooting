using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

	private Image wrapperJoystick;
	private Image joystick;
	private Vector3 inputVector;

	void Start ()
	{
		wrapperJoystick = GetComponent<Image> ();
		joystick = transform.GetChild (0).GetComponent<Image> ();
	}

	public virtual void OnDrag (PointerEventData ped)
	{
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (wrapperJoystick.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / wrapperJoystick.rectTransform.sizeDelta.x);
			pos.y = (pos.y / wrapperJoystick.rectTransform.sizeDelta.y);

			inputVector = new Vector3 (pos.x * 2 + 1, 0, pos.y * 2 - 1);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			// Move joystick
			joystick.rectTransform.anchoredPosition3D = new Vector3 (inputVector.x * (wrapperJoystick.rectTransform.sizeDelta.x / 3), 
				inputVector.z * (wrapperJoystick.rectTransform.sizeDelta.y / 2));
		}
	}

	public virtual void OnPointerUp (PointerEventData ped)
	{
		inputVector = Vector3.zero;
		joystick.rectTransform.anchoredPosition3D = Vector3.zero;
	}

	public virtual void OnPointerDown (PointerEventData ped)
	{
		OnDrag (ped);
	}

	public float Horizontal ()
	{
		if (inputVector.x != 0) {
			return inputVector.x;
		} else {
			return Input.GetAxis ("Horizontal");
		}
	}

	public float Vertical ()
	{
		if (inputVector.z != 0) {
			return inputVector.z;
		} else {
			return Input.GetAxis ("Vertical");
		}
	}
}
