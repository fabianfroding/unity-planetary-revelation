using System.Collections.Generic;
using UnityEngine;

public class OrbitLineRenderer : MonoBehaviour
{
    [SerializeField] private Material lineRendererMaterial;
    [SerializeField] private float lineRendererWidth = 1;

    private void Start()
    {
        List<GameObject> planets = SolarSystem.Instance.GetPlanets();
        for (int i = 0; i < planets.Count; i++)
        {
            CreateOrbitLine(planets[i]);
        }
    }

    private void CreateOrbitLine(GameObject planet)
    {
        float radius = Vector3.Distance(transform.position, planet.transform.position);
        int segments = (int)radius / 10;
        float angle = 0f;
        float x;
        float z;

        GameObject orbitLine = new GameObject
        {
            name = planet.name + "OrbitLine"
        };
        orbitLine.transform.parent = transform;
        LineRenderer lr = orbitLine.AddComponent<LineRenderer>();
        lr.positionCount = segments + 1;
        lr.useWorldSpace = false;
        lr.material = lineRendererMaterial;
        lr.startWidth = lineRendererWidth;
        lr.startColor = new Color(1, 1, 1, 0.25f);
        lr.endColor = new Color(1, 1, 1, 0.25f);

        for (int i = 0; i < segments + 1; i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lr.SetPosition(i, new Vector3(x, 0, z));

            angle += (360f / segments);
        }
    }
}
