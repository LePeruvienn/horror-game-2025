using UnityEngine;

public enum DoorType {
	Slide,
	Rotation
}

public class Door : MonoBehaviour, IInteractable
{

	[Header("Door Configuration")]
	[SerializeField] private bool IsOpen;
	[SerializeField] private DoorType Type;
	[SerializeField] private float transitionDuration;

	[Header("End Position")]
	[SerializeField] private float RotationAmount = 90f;
	[SerializeField] private float SlideAmount = 1.2f;

	private bool _isTransitioning = false;
	private float transitionTime = 0;

	private Quaternion _startRotation;
	private Vector3 _startPosition;

	private Quaternion _endRotation;
	private Vector3 _endPosition;
    private Vector3 Forward;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {
	
		// Save Start Rotation & Position
		_startPosition = transform.position;
		_startRotation = transform.rotation;

		// Since "Forward" actually is pointing into the door frame, choose a direction to think about as "forward" 
		Forward = transform.right;
	}

	// Update is called once per frame
	private void Update() {
	
		// If Door is not transitioning stop here
		if (!_isTransitioning) return;

		// Remove elapsed time
		transitionTime -= Time.deltaTime;

		if (transitionTime > 0) {

			transform.rotation = Quaternion.Slerp(_startRotation, _endRotation, transitionTime);
		}
	}

	public void Interact() {

		// If door is already transitioning stop here
		if (_isTransitioning) return;

		// Else set _isTransitioning to true
		_isTransitioning = true;
	}
}
