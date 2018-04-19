using UnityEngine;

using TSEntity = TSPE.Entity;
using TSVector = TSPE.Utils.Vector;
using TSQuaternion = TSPE.Utils.Quaternion;

public class TSymmetryRigidbody : MonoBehaviour
{
    private TSEntity entity;

    public float Mass = 1;
    public bool UseGravity = true; // TODO: implement
    public bool IsKinematic; // TODO: implement
    public Vector3 InertiaTensor;
    public Quaternion InertiaTensorRotation;

    public Vector3 Velocity
    {
        get
        {
            return Convert(entity.State.Velocity);
        }
    }

    public Vector3 Position
    {
        get
        {
            return Convert(entity.State.Position);
        }
    }

    public Vector3 AngularVelocity
    {
        get
        {
            return Convert(entity.State.AngularVelocity);
        }
    }

    public Quaternion Rotation
    {
        get
        {
            return Convert(entity.State.Rotation);
        }
    }

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
        entity = new TSEntity(
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
        entity.Simulate();
        // TODO: create an abstraction layer on entity?
        transform.position = Convert(entity.State.Position);
        transform.rotation = Convert(entity.State.Rotation);

        // TODO: switch between states, handle collision, etc.

        entity.Prepare(); // note: for the next frame
    }

    public void Flip()
    {
        entity.State.Flip();
    }

    public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force)
    {
        TSVector vector = Convert(force);

        switch (forceMode)
        {
            case ForceMode.Acceleration:
                entity.Input.AddAcceleration(vector, false, false);
                break;
            case ForceMode.Force:
                entity.Input.AddAcceleration(vector, true, false);
                break;
            case ForceMode.VelocityChange:
                entity.Input.AddVelocity(vector, false, false);
                break;
            case ForceMode.Impulse:
                entity.Input.AddVelocity(vector, true, false);
                break;
        }
    }

    public void AddTorque(Vector3 torque, ForceMode forceMode = ForceMode.Force)
    {
        TSVector vector = Convert(torque);

        switch (forceMode)
        {
            case ForceMode.Acceleration:
                entity.Input.AddAngularAcceleration(vector, false, false);
                break;
            case ForceMode.Force:
                entity.Input.AddAngularAcceleration(vector, true, false);
                break;
            case ForceMode.VelocityChange:
                entity.Input.AddAngularVelocity(vector, false, false);
                break;
            case ForceMode.Impulse:
                entity.Input.AddAngularVelocity(vector, true, false);
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
                entity.Input.AddAccelerationAtPosition(vector, vectorPosition, false, false);
                break;
            case ForceMode.Force:
                entity.Input.AddAccelerationAtPosition(vector, vectorPosition, true, false);
                break;
            case ForceMode.VelocityChange:
                entity.Input.AddVelocityAtPosition(vector, vectorPosition, false, false);
                break;
            case ForceMode.Impulse:
                entity.Input.AddVelocityAtPosition(vector, vectorPosition, true, false);
                break;
        }
    }

    public void AddRelativeForce(Vector3 force, ForceMode forceMode = ForceMode.Force)
    {
        TSVector vector = Convert(force);

        switch (forceMode)
        {
            case ForceMode.Acceleration:
                entity.Input.AddAcceleration(vector, false, true);
                break;
            case ForceMode.Force:
                entity.Input.AddAcceleration(vector, true, true);
                break;
            case ForceMode.VelocityChange:
                entity.Input.AddVelocity(vector, false, true);
                break;
            case ForceMode.Impulse:
                entity.Input.AddVelocity(vector, true, true);
                break;
        }
    }

    public void AddRelativeTorque(Vector3 torque, ForceMode forceMode = ForceMode.Force)
    {
        TSVector vector = Convert(torque);

        switch (forceMode)
        {
            case ForceMode.Acceleration:
                entity.Input.AddAngularAcceleration(vector, false, true);
                break;
            case ForceMode.Force:
                entity.Input.AddAngularAcceleration(vector, true, true);
                break;
            case ForceMode.VelocityChange:
                entity.Input.AddAngularVelocity(vector, false, true);
                break;
            case ForceMode.Impulse:
                entity.Input.AddAngularVelocity(vector, true, true);
                break;
        }
    }

    // public Vector3 GetPointVelocity(Vector3 worldPoint);
    // public Vector3 GetRelativePointVelocity(Vector3 relativePoint);
    // public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);
    // public RaycastHit[] SweepTestAll(Vector3 direction, float maxDistance = Mathf.Infinity, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);
}
