using UnityEngine;
using UnityEngine.AI;

public enum MonsterState {

	Patroling,
	Chasing,
	LoosingTarget,
}

public class MonsterAI : MonoBehaviour
{
	[SerializeField] private float timeBeforeLooseTarget = 2.5f;

	private GameManager _gameManager;
	private Player _player;
	private NavMeshAgent _agent;
	private MonsterVision _vision;
	private Rigidbody _rigidBody;

	private float _looseTargetTime = 0f;

	private MonsterState _state = MonsterState.Patroling;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {
		
		_gameManager = GameManager.getInstance();
		_agent = GetComponent<NavMeshAgent>();
		_player = _gameManager.player;
		_rigidBody = GetComponent<Rigidbody>();
		_vision = GetComponent<MonsterVision>();

		_rigidBody.isKinematic = true;
		_rigidBody.detectCollisions = true;
	}

	// Update is called once per frame
	private void Update() {
	
		bool canSeePlayer = _vision.canSeePlayer();

		if (canSeePlayer && _state != MonsterState.Chasing) {

			_state = MonsterState.Chasing;

		} else if (!canSeePlayer && _state == MonsterState.Chasing) {

			_state = MonsterState.LoosingTarget;
			_looseTargetTime = timeBeforeLooseTarget;

		} else if (_state == MonsterState.LoosingTarget) {

			_looseTargetTime -= Time.deltaTime;

			if (_looseTargetTime <= 0)
				_state = MonsterState.Patroling;
		}

		if (_state == MonsterState.Chasing || _state == MonsterState.LoosingTarget)
			_agent.SetDestination(_player.gameObject.transform.position);
	}

	private void patroling() {

	}

	private void chasing() {
	}
}
