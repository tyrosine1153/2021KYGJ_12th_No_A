using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxScript : MonoSingleton<TextBoxScript>
{
    private const float TypingTime = 0.1f;

    [SerializeField] private Transform playerTransform;

    private Coroutine _coTypeText;
    private Color _emptyColor;

    private Color _imageColor;

    private Image _imageUI;
    private bool _isFliped;
    private bool _isTyping;
    private Color _textColor;
    private Text _textUI;

    private void Start()
    {
        _imageUI = GetComponent<Image>();
        _textUI = GetComponentInChildren<Text>();

        _imageColor = _imageUI.color;
        _textColor = _textUI.color;
        _emptyColor = new Color(0, 0, 0, 0);

        _isTyping = false;
        _isFliped = false;
        SetTransparent(false);
    }

    private void Update()
    {
        // 타이핑 중에는 따라다니기
        if (_isTyping)
        {
            var playerPos = Camera.main.WorldToScreenPoint(playerTransform.position);
            transform.position = new Vector3(playerPos.x, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.C) && _isTyping)
        {
            if (!_isFliped)
            {
                _isFliped = true;
            }
            else
            {
                _isTyping = false;
                StopCoroutine(_coTypeText);
                SetTransparent(false);
            }
        }
    }

    private void SetTransparent(bool isOpaque)
    {
        if (isOpaque)
        {
            _imageUI.color = _imageColor;
            _textUI.color = _textColor;
        }
        else
        {
            _imageUI.color = _emptyColor;
            _textUI.color = _emptyColor;
        }
    }

    public void TypeText(string text)
    {
        if (_isTyping)
            StopCoroutine(_coTypeText);
        else
            SetTransparent(true);
        _coTypeText = StartCoroutine(CoTypeText(text));
    }

    private IEnumerator CoTypeText(string text)
    {
        _isTyping = true;
        _isFliped = false;

        _textUI.text = "";

        yield return new WaitForSeconds(0.5f);

        foreach (var t in text)
        {
            if (_isFliped) break;

            _textUI.text += t;

            yield return new WaitForSeconds(TypingTime);
        }

        _textUI.text = text;
        _isFliped = true;
        yield return new WaitForSeconds(3f);

        _isTyping = false;
        SetTransparent(false);
    }


    // 나타나기
    // 타이핑 중에는 따라다니기
    // 타이핑 중에 새로운 타이핑이 생기면 원래 코루틴을 중단하고 새로 시작하기
    // 타이핑 중에 ㅁㅁ을 누르면 코루틴을 중단하고 텍스트 나타내기
    // ㄴ 근데 이상태에서 한번 더 누르면 말풍선 사라지기
    // 타이핑이 끝나면 기다렸다가 사라지기
}