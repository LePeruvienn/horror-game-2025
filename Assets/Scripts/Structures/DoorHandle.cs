using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(HingeJoint))]
public class DoorHandle : MonoBehaviour, IDraggable
{

	[SerializeField] private Rigidbody doorRigidBody;
	[SerializeField] private float torqueStrength = 3f;
	[SerializeField] private float doorDrag = 5f;
	[SerializeField] private bool reverseInput = false;

	private GameManager _gameManager;
	private Player _player;
	private InputManager _input;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {

		// Set door drag
		doorRigidBody.linearDamping = doorDrag;
	}

	// Update is called once per frame
	private void Update() {

	}

	public void Drag(Vector2 value) {

		// Handle reverse input
		int sign = (reverseInput) ? -1 : 1;

		// We want to make that we can move door my look horizontally of verticaly
		float input = (value.x - value.y) * sign;

		// Compute new velocity
		Vector3 newVelocity = new Vector3(0f, -input * torqueStrength, 0f);

		// Set new velocity
		doorRigidBody.angularVelocity = newVelocity;
	}

	public void Push (Transform transform) {

	}
}
