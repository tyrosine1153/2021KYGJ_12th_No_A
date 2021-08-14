using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] private AudioClip[] effectAudioClips;
    private AudioSource _audioSource;
    private Coroutine _coroutine;
    
    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayEffect(int clipNum)
    {
        _audioSource.clip = effectAudioClips[clipNum];
        _audioSource.Play();
    }

    public void FootWalkStart()
    {
        _coroutine = StartCoroutine(CoFootWalk());
    }

    private IEnumerator CoFootWalk()
    {
        while (true)
        {
            _audioSource.clip = effectAudioClips[Random.Range(0, 4)];
            _audioSource.Play();
            
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void FootWalkStop()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
}
