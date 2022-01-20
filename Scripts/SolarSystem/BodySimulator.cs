using UnityEngine;

// Simulation script to update all velocities and then all positions.
public class BodySimulator : MonoBehaviour
{
    CelestialBody[] bodies;
    public static CelestialBody[] Bodies
    {
        get
        {
            return Instance.bodies;
        }
    }

    static BodySimulator instance;
    static BodySimulator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BodySimulator>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        bodies = FindObjectsOfType<CelestialBody>();
        Time.fixedDeltaTime = GameConstants.PHYSICS_TIMESTEP;
        Debug.Log("Setting fixedDeltaTime to: " + GameConstants.PHYSICS_TIMESTEP);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            Vector3 acceleration = CalculateAcceleration(bodies[i].Position, bodies[i]);
            bodies[i].UpdateVelocity(acceleration, GameConstants.PHYSICS_TIMESTEP);
        }

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdatePosition(GameConstants.PHYSICS_TIMESTEP);
        }
    }

    public static Vector3 CalculateAcceleration(Vector3 point, CelestialBody ignoreBody = null)
    {
        Vector3 acceleration = Vector3.zero;
        for (int i = 0; i < Instance.bodies.Length; i++)
        {
            if (Instance.bodies[i] != ignoreBody)
            {
                float sqrDist = (Instance.bodies[i].Position - point).sqrMagnitude;
                Vector3 forceDir = (Instance.bodies[i].Position - point).normalized;
                acceleration += forceDir * GameConstants.GRAVITATIONAL_CONSTANT * Instance.bodies[i].mass / sqrDist;
            }
        }
        return acceleration;
    }
}
