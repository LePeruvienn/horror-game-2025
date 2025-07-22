using UnityEngine;

public class DoorHandle : MonoBehaviour, IDraggable
{

	[SerializeField] private Rigidbody doorRigidBody;
	[SerializeField] private float torqueStrength = 3f;
	[SerializeField] private bool reverseInput = false;

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

		// Handle reverse input
		int sign = (reverseInput) ? -1 : 1;

		// We want to make that we can move door my look horizontally of verticaly
		float input = (value.x - value.y) * sign;

		// Compute new velocity
		Vector3 newVelocity = new Vector3(0f, -input * torqueStrength, 0f);

		// Set new velocity
		doorRigidBody.angularVelocity = newVelocity;
	}

	public void DragRelease() {

		// TODO: !!!
	}
}
