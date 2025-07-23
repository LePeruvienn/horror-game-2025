using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public Vector2 mouse;
	public bool jump;
	public bool sprint;
	public bool crouch;
	public bool interact;
	public bool drop;
	public bool fire;
	public bool drag;
	public bool dragRelease;

	[Header("Movement Settings")]
	public bool analogMovement;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

	public void OnMove(InputValue value) {

		move = value.Get<Vector2>();
	}

	public void OnLook(InputValue value) {

		mouse = value.Get<Vector2>();

		if(cursorInputForLook)
			look = value.Get<Vector2>();
		else
			look = Vector2.zero;
	}

	public void OnJump(InputValue value) {

		jump = value.isPressed;
	}

	public void OnSprint(InputValue value) {

		sprint = value.isPressed;
	}

	public void OnCrouch(InputValue value) {
		
		crouch = value.isPressed;
	}

	public void OnInteract(InputValue value) {

		interact = value.isPressed;
	}

	public void OnDrop(InputValue value) {

		drop = value.isPressed;
	}

	public void OnFire(InputValue value) {
		
		fire = value.isPressed;
		drag = value.isPressed;

		// if we were dragging and now we are not,
		// -> Trigger drag release
		if (drag && !value.isPressed)
			dragRelease = true;
	}

	private void OnApplicationFocus(bool hasFocus) {

		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState) {

		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
}
