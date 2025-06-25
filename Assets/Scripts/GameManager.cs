using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;

	[HideInInspector] public GameObject playerObject;
	[HideInInspector] public Player player;

	private void Awake() {

		// Set instance if it's null
		if (GameManager._instance == null)
			GameManager._instance = this;

		// Find player object
		playerObject = GameObject.FindWithTag("Player");

		// Get Player's scripts
		player = playerObject.GetComponent<Player>();
	}

	public static GameManager getInstance() {
		return GameManager._instance;
	}
}
