using UnityEngine;
using UnityEngine.AI;

public enum MonsterState {

	Patroling,
	Chasing,
}

public class MonsterAI : MonoBehaviour
{

	private GameManager _gameManager;

	private GameObject _player;
	private NavMeshAgent _agent;
	private MonsterVision _vision;
	private Rigidbody _rigidBody;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {
		
		_gameManager = GameManager.getInstance();
		_agent = GetComponent<NavMeshAgent>();
		_player = _gameManager.playerObject;
		_rigidBody = GetComponent<Rigidbody>();
		_vision = GetComponent<MonsterVision>();

		_rigidBody.isKinematic = true;
		_rigidBody.detectCollisions = true;
	}

	// Update is called once per frame
	private void FixedUpdate() {
	
		if (_vision.canSeePlayer())
			_agent.SetDestination(_player.transform.position);
	}

	private void Patroling() {

	}

	private void Chasing() {

	}
}
