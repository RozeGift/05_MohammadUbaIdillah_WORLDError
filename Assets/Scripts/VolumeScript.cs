using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audsource;

    private float musicvolume = 1f;
    void Start()
    {
        audsource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        audsource.volume = musicvolume;
    }

    public void VolumeUpdate(float volume)
    {
        musicvolume = volume;
    }
}
