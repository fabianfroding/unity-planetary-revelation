using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public PlayerObjectData playerObjectData;
    [SerializeField] private GameObject castPoint;
    [SerializeField] private SphereCollider scanCollider;
    
    private List<GameObject> detectedPlanets;
    private string lineRendererSuffix = "_LineRenderer";

    private void Start()
    {
        detectedPlanets = new List<GameObject>();
        scanCollider.radius = playerObjectData.scanRange;
        StartCoroutine(Tick());
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < detectedPlanets.Count; i++)
        {
            // TODO: If planet is fully scanned, remove LR and remove from detected planets.
            // But then it still wont update for new planets already inside the range...
            LineRenderer lr = transform.Find(detectedPlanets[i].name + lineRendererSuffix).gameObject.GetComponent<LineRenderer>();
            lr.SetPosition(0, castPoint.transform.position);
            lr.SetPosition(1, detectedPlanets[i].transform.position);
        }

        if (scanCollider.radius != playerObjectData.scanRange)
        {
            scanCollider.radius = playerObjectData.scanRange;
        }
    }

    public int GetResourceCost()
    {
        return playerObjectData.resourceCost;
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
            AudioManager.Instance.PlayPlayerObjectDestroyedSound(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (detectedPlanets.Count < playerObjectData.maxTargets &&
            other.gameObject.layer == LayerMask.NameToLayer(EditorConstants.LAYER_PLANET) &&
            other.GetComponent<PlanetScript>().planetData.percentScanned < 100)
        {
            detectedPlanets.Add(other.gameObject);

            GameObject line = new GameObject(other.gameObject.name + lineRendererSuffix);
            line.transform.parent = gameObject.transform;
            line.AddComponent<LineRenderer>();
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.material = playerObjectData.lineRendererMaterial;
            lineRenderer.SetPosition(0, castPoint.transform.position);
            lineRenderer.SetPosition(1, other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(EditorConstants.LAYER_PLANET))
        {
            detectedPlanets.Remove(other.gameObject);
            Transform lrChild = transform.Find(other.gameObject.name + lineRendererSuffix);
            if (lrChild != null)
            {
                Destroy(lrChild.gameObject);
            }
        }
    }
}
