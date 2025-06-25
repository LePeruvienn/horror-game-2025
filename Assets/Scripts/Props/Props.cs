using UnityEngine;

public class Props : MonoBehaviour, IInteractable
{
	private static GameManager _gameManager;
	private static PlayerInteraction _playerInteraction;

	[SerializeField] private float Weight = 2f;
	[SerializeField] private float Noise = 1f;
	[SerializeField] private bool IsBreakable = false;

	private bool _isPickedUp = false;
	private Rigidbody _rigidBody;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {

		// Get GameManager instance and Player Interaction if not done yet
		if (Props._gameManager == null) {
			_gameManager = GameManager.getInstance();
			_playerInteraction = _gameManager.player.Interaction;
		}

		// Get object's components
		_rigidBody = GetComponent<Rigidbody>();
	}

	public void Interact() {

		// You cant interact with an object if he is is picked up
		if (_isPickedUp) return;

		// Make Player this Props
		_playerInteraction.Pickup(this);

		// Set Rigidbody to Kinematic
		_rigidBody.isKinematic = true;

		// Set picked up state
		_isPickedUp = true;
	}

	public void Throw() {

		// You cant throw an object that is not picked up
		if (!_isPickedUp) return;

		// Set parent to wolrd space (null)
		transform.SetParent(null);

		// Reset Rigidbody Kinematic status
		_rigidBody.isKinematic = false;

		// Add throw force to object
		Vector3 throwDirection = Camera.main.transform.forward;
		_rigidBody.AddForce(throwDirection * _playerInteraction.GetForce(), ForceMode.Impulse);

		// Reset picked up state
		_isPickedUp = false;
	}

	public void Drop() {

		// You cant drop an object that is not picked up
		if (!_isPickedUp) return;

		// Set parent to wolrd space (null)
		transform.SetParent(null);

		// Reset Rigidbody Kinematic status
		_rigidBody.isKinematic = false;

		// Reset picked up state
		_isPickedUp = false;
	}
}
