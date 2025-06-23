using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;

	[HideInInspector] public GameObject Player;
	[HideInInspector] public PlayerInteraction PInteraction;
	[HideInInspector] public PlayerMovement PMovement;

	private void Awake() {

		// Set instance if it's null
		if (GameManager._instance == null)
			GameManager._instance = this;

		// Find player object
		Player = GameObject.FindWithTag("Player");

		// Get player's scripts
		PInteraction = Player.GetComponent<PlayerInteraction>();
		PMovement = Player.GetComponent<PlayerMovement>();
	}

	public static GameManager getInstance() {
		return GameManager._instance;
	}
}
