using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;

	public Player player;
	public Monster monster;

	private void Awake() {

		// Set instance if it's null
		if (GameManager._instance == null)
			GameManager._instance = this;
	}

	public static GameManager getInstance() {
		return GameManager._instance;
	}
}
