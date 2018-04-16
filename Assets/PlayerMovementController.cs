using UnityEngine;
using UnityEngine.Networking;

// Based off of Unity Networking tutorial (so far)
public class PlayerMovementController : NetworkBehaviour {

	public float rotationSpeed = 150.0f;
	public float movementSpeed = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}

		float yRotation = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
		float zMovement = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

		transform.Rotate(0, yRotation, 0);
		transform.Translate(0, 0, zMovement);
	}
}
