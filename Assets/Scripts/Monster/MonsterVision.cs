using UnityEngine;

public class MonsterVision : MonoBehaviour
{
	[SerializeField] private Transform eyes;
	[SerializeField] private float viewDistance = 15f;
	[SerializeField] private float viewAngle = 60f;

	private GameManager _gameManager;
	private GameObject _target;

	private Vector3 _dirToPlayer;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		
		_gameManager = GameManager.getInstance();
		_player = _gameManager.PlayerTarget;
		_dirToPlayer = eyes.forward;
	}

	// Update is called once per frame
	void Update() {
		
		canSeePlayer();
	}

	private bool canSeePlayer() {

		 _dirToPlayer = (_player.transform.position - eyes.position).normalized;
		float angleToPlayer = Vector3.Angle(eyes.forward, _dirToPlayer);

		if (angleToPlayer > viewAngle) return false;

		// Un seul Raycast
		if (Physics.Raycast(eyes.position, _dirToPlayer, out RaycastHit hit, viewDistance))
		{

			Debug.Log (hit.transform.root);
			return hit.transform.parent.CompareTag("Player");
		}

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

		// Rayon central (avant)
		Gizmos.color = Color.red;
		Gizmos.DrawRay(eyes.position, _dirToPlayer * viewDistance);
	}
}
