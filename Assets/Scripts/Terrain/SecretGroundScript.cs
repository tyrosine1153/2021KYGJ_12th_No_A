using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretGroundScript : MonoBehaviour
{
    private Tilemap _tilemap;
    
    private Color _opaqueColor;
    private Color _transparentColor;
    
    private bool _isCoroutine;
    private Coroutine _coroutine;
    
    private void Start()
    {
        _tilemap = GetComponent<Tilemap>();
        
        _opaqueColor = _tilemap.color;
        _transparentColor = _opaqueColor;
        _transparentColor.a = 0;
        
        _isCoroutine = false;
    }

    private IEnumerator SetTransparent(bool isOpaque)
    {
        _isCoroutine = true;
        
        var startColor = _tilemap.color;
        var destColor = isOpaque ? _opaqueColor : _transparentColor;
        var curT = isOpaque ? startColor.a : (1 - startColor.a);

        // 나타나기
        for (var t = curT; t <= 1; t += 0.1f)
        {
            _tilemap.color = Color.Lerp(startColor, destColor, t);
            yield return new WaitForSeconds(0.1f);
        }

        _isCoroutine = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_isCoroutine)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(SetTransparent(false));
        }

        print($"Enter {gameObject.name} : {other.name}");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_isCoroutine)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(SetTransparent(true));
        }
        print($"Exit {gameObject.name} : {other.name}");
    }
}
