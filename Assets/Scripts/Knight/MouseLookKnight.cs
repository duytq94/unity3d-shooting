using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookKnight : MonoBehaviour
{
	Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 5f;
	public float smoothing = 2f;

	private GameObject character;

	// Use this for initialization
	void Start ()
	{
		character = this.transform.parent.gameObject;
	}

	// Update is called once per frame
	void Update ()
	{
		if (GetComponent<Camera> ().isActiveAndEnabled) {
			// Lấy tọa độ rê chuột
			Vector2 md = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

			// Giúp di chuyển mượt
			md = Vector2.Scale (md, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
			smoothV.x = Mathf.Lerp (smoothV.x, md.x, 1f / smoothing);
			smoothV.y = Mathf.Lerp (smoothV.y, md.y, 1f / smoothing);
			mouseLook += smoothV;

			transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
			character.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, character.transform.up);

		}
	}
}
