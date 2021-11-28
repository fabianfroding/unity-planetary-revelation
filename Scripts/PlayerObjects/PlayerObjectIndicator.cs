using System.Collections;
using UnityEngine;

public class PlayerObjectIndicator : MonoBehaviour
{
    [SerializeField] GameObject radarSignalPrefab;

    void Start()
    {
        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(8f);
            GameObject signal = Instantiate(radarSignalPrefab, transform);
            signal.transform.SetPositionAndRotation(gameObject.transform.position, Quaternion.identity);
            signal.transform.Rotate(90, 0, 0);
            signal.transform.parent = null;
            Destroy(signal, signal.GetComponent<ParticleSystem>().main.duration);
        }
    }
}
