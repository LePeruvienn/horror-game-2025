using UnityEngine;

public enum PropsState {
	PickedUp,
	OnGround,
	Falling,
	Projected
}
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Props : MonoBehaviour, IInteractable
{
	[Header("Properties")]
	[SerializeField] private float Weight = 2f;
	[SerializeField] private float Noise = 1f;

	[Header("Breaking the object")]
	[SerializeField] private bool IsBreakable = false;
	[SerializeField] private GameObject baseObject;
	[SerializeField] private GameObject brokenObject;

	[Header("SFX")]
	[SerializeField] private AudioClip ThrowImpactSFX;
	[SerializeField] private AudioClip DropImpactSFX;
	[SerializeField] private AudioClip PickupSFX;

	private GameManager _gameManager;
	private PlayerInteraction _playerInteraction;

	private Rigidbody _rigidBody;
	private AudioSource _audioSource;

	private PropsState _state = PropsState.OnGround;

	private Quaternion _startRotation;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {

		// Get GameManager instance and Player Interaction if not done yet
		_gameManager = GameManager.getInstance();
		_playerInteraction = _gameManager.player.Interaction;

		// Get object's components
		_rigidBody = GetComponent<Rigidbody>();
		_audioSource = GetComponent<AudioSource>();

		// Save start rotation
		_startRotation = transform.rotation;
	}

	public void Interact() {

		// You cant interact with an object if he is is picked up
		if (_state == PropsState.PickedUp) return;

		// Make Player this Props
		_playerInteraction.Pickup(this);

		// Set Rigidbody to Kinematic
		_rigidBody.isKinematic = true;

		// Set picked up state
		_state = PropsState.PickedUp;
	}

	public void Throw() {

		// You cant throw an object that is not picked up
		if (_state != PropsState.PickedUp) return;

		// Set parent to wolrd space (null)
		transform.SetParent(null);

		// Reset Rigidbody Kinematic status
		_rigidBody.isKinematic = false;

		// Add throw force to object
		Vector3 throwDirection = Camera.main.transform.forward;
		_rigidBody.AddForce(throwDirection * _playerInteraction.GetForce(), ForceMode.Impulse);

		// Set new props state
		_state = PropsState.Projected;
	}

	public void Drop() {

		// You cant drop an object that is not picked up
		if (_state != PropsState.PickedUp) return;

		// Set parent to wolrd space (null)
		transform.SetParent(null);

		// Reset Rigidbody Kinematic status
		_rigidBody.isKinematic = false;

		// Set new props state
		_state = PropsState.Falling;
	}

	public void setStartRotation() {

		transform.rotation = _startRotation;
	}

	private void OnCollisionEnter(Collision other) {

		switch (_state) {
		
			case PropsState.Projected:

				if (ThrowImpactSFX != null)
					_audioSource.PlayOneShot(ThrowImpactSFX);

				if (IsBreakable) {

					brokenObject.SetActive(true);
					baseObject.SetActive(false);
				}
				break;

			case PropsState.Falling:
				if (DropImpactSFX != null)
					_audioSource.PlayOneShot(DropImpactSFX);
				break;
		}

		// Set state on ground
		_state = PropsState.OnGround;
	}
}
