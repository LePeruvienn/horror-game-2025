using UnityEngine;

public enum DoorType {
	Slide,
	Rotation
}

public class Door : MonoBehaviour, IInteractable
{
	[Header("Door Configuration")]
	[SerializeField] private Transform Pivot;
	[SerializeField] private bool IsOpen = false;
	[SerializeField] private DoorType Type = DoorType.Rotation;
	[SerializeField] private float TransitionDuration = 1.5f;

	[Header("End Position")]
	[SerializeField] private float RotationAmount = 90f;
	[SerializeField] private float SlideAmount = 1.2f;
	[SerializeField] private Vector3 RotationAxis = Vector3.up;
	[SerializeField] private Vector3 SlideDirection = Vector3.right;

	private bool _isTransitioning = false;
	private float _transitionTime = 0f;

	private Quaternion _startRotation;
	private Quaternion _endRotation;

	private Vector3 _startPosition;
	private Vector3 _endPosition;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {

		// Save Pivot start Position & rotation
		_startRotation = Pivot.rotation;
		_startPosition = Pivot.position;
	}

	// Update is called once per frame
	private void Update() {

		// If not transitioning there is noting to do
		if (!_isTransitioning) return;

		// Add elapsed time to transition
		_transitionTime += Time.deltaTime;

		// Get transition ratio
		float t = Mathf.Clamp01(_transitionTime / TransitionDuration);

		// If door is a rotation, rotate door
		if (Type == DoorType.Rotation)
			Pivot.rotation = Quaternion.Slerp(_startRotation, _endRotation, t);

		// If door is a slide, slide door
		else if (Type == DoorType.Slide)
			Pivot.position = Vector3.Lerp(_startPosition, _endPosition, t);

		// When elapsed ratio has finished, set transitioning to false
		if (t >= 1f)
			_isTransitioning = false;
	}

	public void Interact() {

		// if door is already transitioning do do anything
		if (_isTransitioning) return;

		// set transitioning to true, reset transition elapsed time to 0
		_isTransitioning = true;
		_transitionTime = 0f;

		// Compute start & end rotation for all door types

		// If door is a rotation
		if (Type == DoorType.Rotation) {
			_startRotation = Pivot.rotation;
			float sign = IsOpen ? -1f : 1f;
			Vector3 direction = transform.TransformDirection(RotationAxis.normalized);
			_endRotation = _startRotation * Quaternion.AngleAxis(sign * RotationAmount, direction);

		// If door is a rotation
		} else if (Type == DoorType.Slide) {

			_startPosition = Pivot.position;
			float sign = IsOpen ? -1 : 1;
			Vector3 worldSlide = transform.TransformDirection(SlideDirection.normalized);
			_endPosition = _startPosition + worldSlide * sign * SlideAmount;
		}

		// Update IsOpen status
		IsOpen = !IsOpen;
	}
}
