using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class CelestialBody : GravityObject
{
    public float radius;
    public float surfaceGravity;
    public Vector3 initialVelocity;
    public string bodyName = "Unknown";
    Transform meshHolder;

    public Vector3 velocity { get; private set; }
    public float mass { get; private set; }
    Rigidbody rb;

    // F = G * ((m1 * m2) / (r * r))
    // F: Force that two bodies attract one another
    // G: Gravitional constant
    // mN: Mass of (each) body
    // r: Radius

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        Initialize();

        rb.mass = mass;
        velocity = initialVelocity;
    }

    public void UpdateVelocity(CelestialBody[] bodies, float timestep)
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            if (bodies[i] != this)
            {
                float sqrDist = (bodies[i].rb.position - rb.position).sqrMagnitude; // Squared length of a vector.
                Vector3 forceDir = (bodies[i].rb.position - rb.position).normalized;

                Vector3 acceleration = forceDir * GameConstants.GRAVITATIONAL_CONSTANT * bodies[i].mass / sqrDist;
                velocity += acceleration * timestep;
            }
        }
    }

    public void UpdateVelocity(Vector3 acceleration, float timestep)
    {
        velocity += acceleration * timestep;
    }

    // Moves a body using it's velocity.
    public void UpdatePosition(float timestep)
    {
        rb.MovePosition(rb.position + velocity * timestep);
    }

    // NOTE: Does not run in build mode.
    private void OnValidate()
    {
        Initialize();
    }

    private void Initialize()
    {
        mass = surfaceGravity * radius * radius / GameConstants.GRAVITATIONAL_CONSTANT;
        //meshHolder = transform.GetChild(0);
        transform.localScale = Vector3.one * radius; //meshHolder.localScale = Vector3.one * radius;
        gameObject.name = bodyName;
    }

    public Rigidbody Rigidbody
    {
        get { return rb; }
    }

    public Vector3 Position
    {
        get { return rb.position; }
    }
}
