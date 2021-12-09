using UnityEngine;

public class SunScript : MonoBehaviour
{
    [SerializeField] private float growIncrementation = 0.02f;
    [SerializeField] private float shrinkDecrementation = 1f;
    private SunState sunState = SunState.GROW;

    private enum SunState
    {
        GROW,
        SHRINK,
        EXPLODE,
        END
    }

    private void FixedUpdate()
    {
        if (sunState == SunState.GROW)
        {
            Grow();
        }
        else if (sunState == SunState.SHRINK)
        {
            Shrink();
        }
        else if (sunState == SunState.EXPLODE)
        {
            Explode();
        }
    }

    private void Grow()
    {
        transform.localScale = new Vector3(
                transform.localScale.x + growIncrementation,
                transform.localScale.y + growIncrementation,
                transform.localScale.z + growIncrementation
                );
        if (transform.localScale.x >= 750f)
        {
            Material mat = GetComponent<Renderer>().material;
            mat.SetVector("_EmissionColor", new Color(0.75f, 0.36f, 0.35f) * 18f);
            sunState = SunState.SHRINK;
        }
    }

    private void Shrink()
    {
        transform.localScale = new Vector3(
                transform.localScale.x - shrinkDecrementation,
                transform.localScale.y - shrinkDecrementation,
                transform.localScale.z - shrinkDecrementation
                );
        if (transform.localScale.x <= 20f)
        {
            Material mat = GetComponent<Renderer>().material;
            mat.SetVector("_EmissionColor", new Color(0.36f, 0.67f, 0.75f) * 18f);
            sunState = SunState.EXPLODE;
        }
    }

    private void Explode()
    {
        sunState = SunState.END;
    }
}
