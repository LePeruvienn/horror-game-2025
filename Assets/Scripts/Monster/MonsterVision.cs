using UnityEngine;

public class MonsterVision : MonoBehaviour
{
	[SerializeField] private Transform eyes;
	[SerializeField] private float viewDistance = 15f;
	[SerializeField] private float viewAngle = 60f;

	private GameManager _gameManager;
	private Player _player;
	private Transform _playerTransform;
	private BoxCollider _playerDetectionCollider;

	private bool _isSeeingPlayer = false;

	// All of the point that we want to target on the collider detection
	private Vector3[] _offsets = new Vector3[] {

		new Vector3(-0.5f,  0.5f, 0), // Top Left
		new Vector3( 0f,    0.5f, 0), // Top Middle
		new Vector3( 0.5f,  0.5f, 0), // Top Right
		new Vector3(-0.5f,  0f,   0), // Mid Left
		new Vector3( 0f,    0f,   0), // Center
		new Vector3( 0.5f,  0f,   0), // Mid Right
		new Vector3(-0.5f, -0.5f, 0), // Bottom Left
		new Vector3( 0f,   -0.5f, 0), // Bottom Middle
		new Vector3( 0.5f, -0.5f, 0)  // Bottom Right
	};

	private Vector3[] _targets;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {
		
		// Get Game Manager & Plaer Instance
		_gameManager = GameManager.getInstance();
		_player = _gameManager.player;
		_playerDetectionCollider = _player.DetectionCollider;
		_playerTransform = _player.gameObject.transform;

		_targets = new Vector3[_offsets.Length];
	}

	public bool canSeePlayer() {

		Vector3 direction = (_playerTransform.position - eyes.position).normalized;

		float angleToPlayer = Vector3.Angle(eyes.forward, direction);

		if (angleToPlayer > viewAngle) return false;

		// Compute targets points (in world space)
		for (int i = 0; i < _offsets.Length; i++) {
			Vector3 scaledPoint = Vector3.Scale(_playerDetectionCollider.size, _offsets[i]);
			Vector3 localPoint = _playerDetectionCollider.center + scaledPoint;
			_targets[i] = _playerDetectionCollider.transform.TransformPoint(localPoint);
		}

		// hits array where we save all the hits of the raycasts
		RaycastHit[] hits = new RaycastHit[_offsets.Length];

		for (int i = 0; i < _targets.Length; i++) {

			Vector3 targetDirection = (_targets[i] - eyes.position).normalized;
	
			// Check if RaycastHit something
			if (Physics.Raycast(eyes.position, targetDirection, out hits[i], viewDistance)) {

				// Return true if we have seen the player
				if (hits[i].transform.root.CompareTag ("Player")) return true;
			}
		}

		// Return false if have not seen him
		return false;
	}


	private void OnDrawGizmos() {

		if (eyes == null) return;

		Gizmos.color = Color.yellow;

		// Définir les bords gauche et droit du cône
		Vector3 forward = eyes.forward;
		Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle, 0) * forward;
		Vector3 rightBoundary = Quaternion.Euler(0, viewAngle, 0) * forward;

		// Dessiner les rayons des bords du champ de vision
		Gizmos.DrawRay(eyes.position, leftBoundary * viewDistance);
		Gizmos.DrawRay(eyes.position, rightBoundary * viewDistance);

		// Raycasts
		Gizmos.color = Color.red;

		if (_targets == null) return;

		for (int i = 0; i < _targets.Length; i++) {

			if (_targets[i] == null) continue;

			Vector3 direction = (_targets[i] - eyes.position).normalized;
			Gizmos.DrawRay(eyes.position, direction * viewDistance);
		}
	}
}
