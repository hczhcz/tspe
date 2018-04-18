using UnityEngine;

using TSRigidbody = TSPE.Rigidbody;
using TSVector = TSPE.Utils.Vector;
using TSQuaternion = TSPE.Utils.Quaternion;

public class TSymmetryRigidbody : MonoBehaviour
{
    private TSRigidbody tsRigidbody;

    public float Mass = 1;
    public bool UseGravity = true; // TODO: implement
    public bool IsKinematic; // TODO: implement
    public Vector3 InertiaTensor;
    public Quaternion InertiaTensorRotation;

    private TSVector Convert(Vector3 vector)
    {
        return new TSVector(vector.x, vector.y, vector.z);
    }

    private Vector3 Convert(TSVector vector)
    {
        return new Vector3(
            (float) vector.X,
            (float) vector.Y,
            (float) vector.Z
        );
    }

    private TSQuaternion Convert(Quaternion quaternion)
    {
        return new TSQuaternion(
            quaternion.w,
            new TSVector(quaternion.x, quaternion.y, quaternion.z)
        );
    }

    private Quaternion Convert(TSQuaternion quaternion)
    {
        return new Quaternion(
            (float) quaternion.Direction.X,
            (float) quaternion.Direction.Y,
            (float) quaternion.Direction.Z,
            (float) quaternion.W
        );
    }

    void Reset()
    {
        tsRigidbody = new TSRigidbody(
            null, // TODO
            Mass,
            Convert(InertiaTensor),
            Convert(InertiaTensorRotation),
            Convert(Vector3.zero),
            Convert(transform.position),
            Convert(Vector3.zero),
            Convert(transform.rotation)
        );
    }

    void Start()
    {
        // TODO: manually calculate the inertia tensor?
        Rigidbody rb = GetComponent<Rigidbody>();
        Mass = rb.mass;
        UseGravity = rb.useGravity;
        IsKinematic = rb.isKinematic;
        InertiaTensor = rb.inertiaTensor;
        InertiaTensorRotation = rb.inertiaTensorRotation;
        rb.Sleep();

        Reset();
    }

    void FixedUpdate()
    {
        tsRigidbody.Simulate();
        // TODO: create an abstraction layer on tsRigidbody?
        transform.position = Convert(tsRigidbody.PhysicsState.Position);
        transform.rotation = Convert(tsRigidbody.PhysicsState.Rotation);
    }

    public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force)
    {
        TSVector vector = Convert(force);

        switch (forceMode)
        {
            case ForceMode.Acceleration:
                tsRigidbody.PhysicsInput.AddAcceleration(vector, false, false);
                break;
            case ForceMode.Force:
                tsRigidbody.PhysicsInput.AddAcceleration(vector, true, false);
                break;
            case ForceMode.VelocityChange:
                tsRigidbody.PhysicsInput.AddVelocity(vector, false, false);
                break;
            case ForceMode.Impulse:
                tsRigidbody.PhysicsInput.AddVelocity(vector, true, false);
                break;
        }
    }

    public void AddTorque(Vector3 torque, ForceMode forceMode = ForceMode.Force)
    {
        TSVector vector = Convert(torque);

        switch (forceMode)
        {
            case ForceMode.Acceleration:
                tsRigidbody.PhysicsInput.AddAngularAcceleration(vector, false, false);
                break;
            case ForceMode.Force:
                tsRigidbody.PhysicsInput.AddAngularAcceleration(vector, true, false);
                break;
            case ForceMode.VelocityChange:
                tsRigidbody.PhysicsInput.AddAngularVelocity(vector, false, false);
                break;
            case ForceMode.Impulse:
                tsRigidbody.PhysicsInput.AddAngularVelocity(vector, true, false);
                break;
        }
    }

    public void AddForceAtPosition(Vector3 force, Vector3 position, ForceMode forceMode = ForceMode.Force)
    {
        TSVector vector = Convert(force);
        TSVector vectorPosition = Convert(position);

        switch (forceMode)
        {
            case ForceMode.Acceleration:
                tsRigidbody.PhysicsInput.AddAccelerationAtPosition(vector, vectorPosition, false, false);
                break;
            case ForceMode.Force:
                tsRigidbody.PhysicsInput.AddAccelerationAtPosition(vector, vectorPosition, true, false);
                break;
            case ForceMode.VelocityChange:
                tsRigidbody.PhysicsInput.AddVelocityAtPosition(vector, vectorPosition, false, false);
                break;
            case ForceMode.Impulse:
                tsRigidbody.PhysicsInput.AddVelocityAtPosition(vector, vectorPosition, true, false);
                break;
        }
    }

    public void AddRelativeForce(Vector3 force, ForceMode forceMode = ForceMode.Force)
    {
        TSVector vector = Convert(force);

        switch (forceMode)
        {
            case ForceMode.Acceleration:
                tsRigidbody.PhysicsInput.AddAcceleration(vector, false, true);
                break;
            case ForceMode.Force:
                tsRigidbody.PhysicsInput.AddAcceleration(vector, true, true);
                break;
            case ForceMode.VelocityChange:
                tsRigidbody.PhysicsInput.AddVelocity(vector, false, true);
                break;
            case ForceMode.Impulse:
                tsRigidbody.PhysicsInput.AddVelocity(vector, true, true);
                break;
        }
    }

    public void AddRelativeTorque(Vector3 torque, ForceMode forceMode = ForceMode.Force)
    {
        TSVector vector = Convert(torque);

        switch (forceMode)
        {
            case ForceMode.Acceleration:
                tsRigidbody.PhysicsInput.AddAngularAcceleration(vector, false, true);
                break;
            case ForceMode.Force:
                tsRigidbody.PhysicsInput.AddAngularAcceleration(vector, true, true);
                break;
            case ForceMode.VelocityChange:
                tsRigidbody.PhysicsInput.AddAngularVelocity(vector, false, true);
                break;
            case ForceMode.Impulse:
                tsRigidbody.PhysicsInput.AddAngularVelocity(vector, true, true);
                break;
        }
    }

    // public Vector3 GetPointVelocity(Vector3 worldPoint);
    // public Vector3 GetRelativePointVelocity(Vector3 relativePoint);
    // public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);
    // public RaycastHit[] SweepTestAll(Vector3 direction, float maxDistance = Mathf.Infinity, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);
}
