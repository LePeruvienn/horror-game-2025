using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;

	[HideInInspector] public Player player;
	[HideInInspector] public Monster monster;

	private void Awake() {

		// Set instance if it's null
		if (GameManager._instance == null)
			GameManager._instance = this;

		// Get global entities
		player = FindObjectOfType<Player>();
		monster = FindObjectOfType<Monster>();
	}

	public static GameManager getInstance() {
		return GameManager._instance;
	}
}
