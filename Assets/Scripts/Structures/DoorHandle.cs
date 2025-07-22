using UnityEngine;

public class DoorHandle : MonoBehaviour, IDraggable
{

	[SerializeField] private Rigidbody doorRigidBody;
	[SerializeField] private float torqueStrength = 10f;

	private GameManager _gameManager;
	private Transform _playerTransform;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {

		// Get GameManager instance and Player Interaction if not done yet
		_gameManager = GameManager.getInstance();
		_playerTransform = _gameManager.player.transform;
	}

	// Update is called once per frame
	private void Update() {

	}

	public void Drag(Vector2 value) {

		// Get input horizontal axis
		float input = value.x;

		// Compute torque value, we add the force to the Y rotation axis
		Vector3 torque = new Vector3(0f, -input * torqueStrength, 0f);

		// Add torque to door Rigidbody
		doorRigidBody.AddTorque(torque, ForceMode.Acceleration);
	}

	public void DragRelease() {

		// TODO: !!!
	}
}
