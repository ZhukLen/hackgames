using UnityEngine;

public class SoundDethSpawn : MonoBehaviour
{
    public AudioClip[] Deth, Spawn;
    // Start is called before the first frame update
    AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayDethSound()
    {
        source.clip = Deth[Random.Range(0, Deth.Length)];
        source.Play();
    }

    public void PlaySpawnSound()
    {
        source.clip = Spawn[Random.Range(0, Spawn.Length)];
        source.Play();
    }
}
