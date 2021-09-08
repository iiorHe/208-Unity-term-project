using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipList : MonoBehaviour
{
    public AudioSource source;
    public List<AudioClip> clips;
    private void Start() {
        source = GetComponent<AudioSource>();
        source.clip = clips[Random.Range(0,clips.Count-1)];
        source.Play();
    }
}
