using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(ConfigurableJoint))]
public class DrawerHandle : MonoBehaviour, IDraggable
{
	[SerializeField] private Rigidbody drawerRigidBody;
	[SerializeField] private ConfigurableJoint joint;
	[SerializeField] private float torqueStrength = 0.005f;
	[SerializeField] private float drawerDrag = 5f;
	[SerializeField] private Vector3 direction = new Vector3(1, 0, 0);
	[SerializeField] private bool reverseInput = false;
	[SerializeField] private float max = 1f;
	[SerializeField] private float min = 0f;

	private float _projectedPos;

	private void Start() {

		// Config rigidBody
		drawerRigidBody.useGravity = false;
		drawerRigidBody.interpolation = RigidbodyInterpolation.Interpolate;
		drawerRigidBody.linearDamping = drawerDrag;

		// Lock all axes
		joint.xMotion = ConfigurableJointMotion.Locked;
		joint.yMotion = ConfigurableJointMotion.Locked;
		joint.zMotion = ConfigurableJointMotion.Locked;

		// Allow movement on one axis
		if (Mathf.Abs(direction.x) > 0f)
			joint.xMotion = ConfigurableJointMotion.Limited;
		else if (Mathf.Abs(direction.y) > 0f)
			joint.yMotion = ConfigurableJointMotion.Limited;
		else if (Mathf.Abs(direction.z) > 0f)
			joint.zMotion = ConfigurableJointMotion.Limited;

		// Set linear limit
		SoftJointLimit limit = new SoftJointLimit();
		limit.limit = max;
		joint.linearLimit = limit;

		// Lock rotation
		joint.angularXMotion = ConfigurableJointMotion.Locked;
		joint.angularYMotion = ConfigurableJointMotion.Locked;
		joint.angularZMotion = ConfigurableJointMotion.Locked;

		// Add joint projection to reduce jittering
		joint.projectionMode = JointProjectionMode.PositionAndRotation;
		joint.projectionDistance = 0.01f;
	}

	public void Drag(Vector2 value) {

		int sign = reverseInput ? -1 : 1;
		float input = (value.y - value.x) * sign * torqueStrength;

		// Get local position projected along direction
		Vector3 localPos = transform.localPosition;
		_projectedPos = Vector3.Dot(localPos, direction.normalized);

		// Stop movement if at limit
		if ((_projectedPos >= max && input > 0f) || (_projectedPos <= min && input < 0f))
		{
			drawerRigidBody.linearVelocity = Vector3.zero;
			return;
		}

		drawerRigidBody.linearVelocity = direction.normalized * input;
	}
}

