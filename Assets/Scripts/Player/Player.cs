using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Anatomy")]
	public GameObject Head; 
	public GameObject Hips; 

	[Header("Detection")]
	public BoxCollider DetectionCollider;

	[HideInInspector] public PlayerInteraction Interaction;
	[HideInInspector] public PlayerMovement Movement;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {
		// Get Player's components
		Interaction = GetComponent<PlayerInteraction> ();
		Movement = GetComponent<PlayerMovement> ();
	}
}
