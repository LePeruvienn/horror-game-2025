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
	private Rigidbody _rigidBody;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {
		
		_gameManager = GameManager.getInstance();
		_agent = GetComponent<NavMeshAgent>();
		_player = _gameManager.Player;
		_rigidBody = GetComponent<Rigidbody>();

		_rigidBody.isKinematic = true;
		_rigidBody.detectCollisions = true;
	}

	// Update is called once per frame
	private void Update() {
		
		// _agent.SetDestination(_player.transform.position);
	}

	private void Patroling() {

	}

	private void Chasing() {

	}
}
