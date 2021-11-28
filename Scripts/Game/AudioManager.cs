using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject planetScannedSoundPrefab;
    [SerializeField] private GameObject playerObjectDestroyedSoundPrefab;

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag(EditorConstants.TAG_AUDIO_MANAGER).GetComponent<AudioManager>();
            }
            return instance;
        }
    }

    public void PlayPlayerObjectDestroyedSound(GameObject source)
    {
        PlaySound(playerObjectDestroyedSoundPrefab, source.transform.position);
    }

    public void PlayPlanetScannedSound(GameObject source)
    {
        PlaySound(planetScannedSoundPrefab, source.transform.position);
    }

    private void PlaySound(GameObject sound, Vector3 position)
    {
        GameObject s = Instantiate(sound);
        s.transform.position = position;
        s.transform.parent = null;
        Destroy(s, s.GetComponent<AudioSource>().clip.length);
    }
}
