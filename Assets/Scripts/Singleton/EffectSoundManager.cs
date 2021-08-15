using System.Collections;
using UnityEngine;

public class EffectSoundManager : MonoSingleton<EffectSoundManager>
{
    [SerializeField] private AudioClip[] effectAudioClips;
    [SerializeField] private AudioSource walkAudioSource;
    [SerializeField] private AudioSource effectAudioSource;
    private Coroutine _coroutine;

    public void PlayEffect(int clipNum, bool isLoop = false)
    {
        effectAudioSource.loop = isLoop;
        effectAudioSource.clip = effectAudioClips[clipNum];
        effectAudioSource.Play();
    }

    public void FootWalkStart()
    {
        _coroutine = StartCoroutine(CoFootWalk());
    }

    private IEnumerator CoFootWalk()
    {
        while (true)
        {
            walkAudioSource.clip = effectAudioClips[Random.Range(0, 4)];
            walkAudioSource.Play();

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void FootWalkStop()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
    }
}