using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicLoopStarter : MonoBehaviour
{
    public AudioClip MusicStart;
    public AudioClip MusicLoop;
    void Start()
    {
        GetComponent<AudioSource>().loop = true;
        StartCoroutine(playEngineSound());
    }

    IEnumerator playEngineSound()
    {
        GetComponent<AudioSource>().clip = MusicStart;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(35);
        GetComponent<AudioSource>().clip = MusicLoop;
        GetComponent<AudioSource>().Play();
    }
}