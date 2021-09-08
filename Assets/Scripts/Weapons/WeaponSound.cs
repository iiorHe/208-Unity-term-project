using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponSound : MonoBehaviour
{

    public AudioClip fireSound;
    public AudioClip loadBegin;
    public AudioClip loadAction;
    public AudioClip loadEnd;
    private AudioSource _source;
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }
    public void SoundFire(){
        _source.clip = fireSound;
        PlaySound();
    }
    public void SoundLoadBegin(){
        _source.clip = loadBegin;
        PlaySound();
    }
    public void SoundLoadAction(){
        _source.clip = loadAction;
        PlaySound();
    }
    public void SoundLoadEnd(){
        _source.clip = loadEnd;
        PlaySound();
    }
    private void PlaySound(){
        _source.Stop();
        _source.Play();
    }
}
