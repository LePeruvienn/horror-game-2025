using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	[Header("Player's References")]
	[Tooltip("Where is object go when the player hold it")]
	[SerializeField] private Transform PropsHoldOrigin;

	[Header("Props Properties")]
	[SerializeField] private float Range = 2.0f;
	[SerializeField] private float Force = 2.0f;

	private bool _isHoldingProps = false;
	private Props _heldProps;
	private InputManager _input;
	private GameObject _mainCamera;

	private void Awake() {

		// Get a reference to our main camera if it is not defined
		if (_mainCamera == null)
			_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {
		
		_input = GetComponent<InputManager>();
	}

	// Update is called once per frame
	private void Update() {
		
		if (_input.interact)
			Interact();

		else if (_input.drop)
			Drop();

		else if (_input.fire)
			Throw();
	}

	private void Interact() {

		// Reset interact input status
		_input.interact = false;

		// Ray cast the object
		GameObject obj = InteractionRaycast();

		// If we dont hit an object stop here
		if (obj == null) return;

		// Get IInteractable Interface
		IInteractable interactable = obj.GetComponent<IInteractable>();

		// If we can interact with the object, well call the interact method
		if (interactable != null)
			interactable.Interact();
	}

	private void Throw() {

		// Reset input state
		_input.fire = false;

		// Dont do anything if we are not holding a props
		if (!_isHoldingProps) return;

		Debug.Log ("THROW");

		// Throw held props
		_heldProps.Throw();

		// Reset _isHoldingProps status
		_isHoldingProps = false;
	}

	private void Drop() {

		// Reset input state
		_input.drop = false;

		// Dont do anything if we are not holding a props
		if (!_isHoldingProps) return;

		// Drop held props
		_heldProps.Drop();

		// Reset _isHoldingProps status
		_isHoldingProps = false;
	}

	private GameObject InteractionRaycast() {

		// Setting up raycast variables
		Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
		
		// Doing the raycast !
		Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
		
		// Setting raycast output variable
		RaycastHit hit;
		
		// Setting obj output variable
		GameObject lastHit = null;

		// If raycast hit
		if (Physics.Raycast(ray, out hit, Range))
			lastHit = hit.transform.gameObject; // Set the target point to the point hit by the raycast

		return lastHit;
	}

	public void Pickup(Props props) {

		// Get props's gameObject
		GameObject obj = props.gameObject;
		
		// TODO REMETTRE
		// if (!_isHoldingProps) return;

		// Set parent to PropsHoldOrigin
		obj.transform.SetParent(PropsHoldOrigin);

		// Reset Object position & rotation
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localRotation = Quaternion.identity;

		// vvv Maybe useless ?
		// obj.transform.localScale = obj.transform.lossyScale;

		// Set _isHoldingProps status to true & set _heldObject
		_isHoldingProps = true;
		_heldProps = props;
	}

	public float GetForce() {

		return Force;
	}

	// FOR DEBUG
	private void OnDrawGizmos() {

		if (_mainCamera == null) return;

		// Draw the ray in the Scene view from the camera's position
		Gizmos.color = Color.red;
		Gizmos.DrawLine(_mainCamera.transform.position, _mainCamera.transform.position + _mainCamera.transform.forward * Range);
	}
}
