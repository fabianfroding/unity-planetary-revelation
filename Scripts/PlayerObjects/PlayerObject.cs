using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    [SerializeField] private Material lineRendererMaterial;
    [SerializeField] private GameObject castPoint;
    [SerializeField] private float scanRange = 40f;
    [SerializeField] private SphereCollider scanCollider;

    private List<GameObject> detectedPlanets;
    private string lineRendererSuffix = "_LineRenderer";

    private void Start()
    {
        detectedPlanets = new List<GameObject>();
        scanCollider.radius = scanRange;
        StartCoroutine(Tick());
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < detectedPlanets.Count; i++)
        {
            LineRenderer lr = transform.Find(detectedPlanets[i].name + lineRendererSuffix).gameObject.GetComponent<LineRenderer>();
            lr.SetPosition(0, castPoint.transform.position);
            lr.SetPosition(1, detectedPlanets[i].transform.position);
        }
        if (scanCollider.radius != scanRange)
        {
            scanCollider.radius = scanRange;
        }
    }

    private IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < detectedPlanets.Count; i++)
            {
                detectedPlanets[i].GetComponent<PlanetScript>().Scan();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(EditorConstants.TAG_HARMFUL))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(EditorConstants.LAYER_PLANET))
        {
            detectedPlanets.Add(other.gameObject);

            GameObject line = new GameObject(other.gameObject.name + lineRendererSuffix);
            line.transform.parent = gameObject.transform;
            line.AddComponent<LineRenderer>();
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.material = lineRendererMaterial;
            lineRenderer.SetPosition(0, castPoint.transform.position);
            lineRenderer.SetPosition(1, other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(EditorConstants.LAYER_PLANET))
        {
            detectedPlanets.Remove(other.gameObject);
            Destroy(transform.Find(other.gameObject.name + lineRendererSuffix).gameObject);
        }
    }

    private void OnDestroy()
    {
        AudioManager.Instance.PlayPlayerObjectDestroyedSound(gameObject);
    }
}
