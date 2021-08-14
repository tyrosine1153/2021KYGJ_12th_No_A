using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EffectSoundManager : PersistentSingleton<EffectSoundManager>
{
    [SerializeField] private AudioClip[] effectAudioClips;
    [SerializeField] private AudioSource _walkAudioSource;
    [SerializeField] private AudioSource _effectAudioSource;
    private Coroutine _coroutine;

    public void PlayEffect(int clipNum)
    {
        print("??????????????????????");
        _effectAudioSource.clip = effectAudioClips[clipNum];
        _effectAudioSource.Play();
    }

    public void FootWalkStart()
    {
        _coroutine = StartCoroutine(CoFootWalk());
    }

    private IEnumerator CoFootWalk()
    {
        while (true)
        {
            _walkAudioSource.clip = effectAudioClips[Random.Range(0, 4)];
            _walkAudioSource.Play();
            
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
