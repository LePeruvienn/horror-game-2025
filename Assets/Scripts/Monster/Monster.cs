using UnityEngine;

public class Monster : MonoBehaviour
{
	[HideInInspector] public MonsterAI AI;
	[HideInInspector] public MonsterVision Vision;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {

		AI = GetComponent<MonsterAI>();
		Vision = GetComponent<MonsterVision>();
	}
}
