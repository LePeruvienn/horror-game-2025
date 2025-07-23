using UnityEngine;

public class DrawerHandle : MonoBehaviour, IDraggable
{

	[SerializeField] private Transform drawer;
	[SerializeField] private float torqueStrength = 0.005f;
	[SerializeField] private Vector3 direction = new Vector3(1, 0, 0);
	[SerializeField] private bool reverseInput = false;
	[SerializeField] private float max = 1f;
	[SerializeField] private float min = 0f;

	private Vector3 _pmax;
	private Vector3 _pmin;

	private float offset = 0f;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {

		_pmax = (direction * max) + drawer.position;
		_pmin = (direction * min) + drawer.position;
	}

	// Update is called once per frame
	private void Update() {

	}

	public void Drag(Vector2 value)
	{
		// Handle reverse input
		int sign = (reverseInput) ? -1 : 1;

		// Input handling
		float input = (value.y - value.x) * sign;

		// Compute new position
		Vector3 newPosition = drawer.position + direction * input * torqueStrength;

		// Project the movement onto the direction axis
		Vector3 localOffset = newPosition - _pmin;
		float projectedDistance = Vector3.Dot(localOffset, direction.normalized);

		// Clamp the movement between min and max
		projectedDistance = Mathf.Clamp(projectedDistance, 0f, max - min);

		// Apply the clamped position
		drawer.position = _pmin + direction.normalized * projectedDistance;
	}
}
