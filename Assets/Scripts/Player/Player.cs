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

	private void Awake () {

		// Get Player's components
		Interaction = GetComponent<PlayerInteraction>();
		Movement = GetComponent<PlayerMovement>();
	}
}
