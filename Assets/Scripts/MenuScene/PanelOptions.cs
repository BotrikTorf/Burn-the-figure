using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PanelOptions : MonoBehaviour
{
    [SerializeField] private ButtonClosePanelOptions _buttonClosePanelOptions;
    [SerializeField] private GameObject _textPanelOptions;
    [SerializeField] private GameObject _sound;
    [SerializeField] private GameObject _music;
    [SerializeField] private GameObject _resetProgress;
    [SerializeField] private GameObject _buttonChangeBackground;

    private float _speed = 3f;
    private float _anchorMinX;
    private bool _isStateObjects;
    private RectTransform _rectTransform;

    private void Awake() => _rectTransform = GetComponent<RectTransform>();

    public void PullPanel()
    {
        _anchorMinX = 0;
        _isStateObjects = true;
        StartCoroutine(MovesPanel());
    }

    public void PushInPanel()
    {
        _anchorMinX = 1;
        _isStateObjects = false;
        StartCoroutine(MovesPanel());
    }

    private void ChangesStateObjects()
    {
        _buttonClosePanelOptions.gameObject.SetActive(_isStateObjects);
        _textPanelOptions.SetActive(_isStateObjects);
        _sound.SetActive(_isStateObjects);
        _music.SetActive(_isStateObjects);
        _resetProgress.SetActive(_isStateObjects);
        _buttonChangeBackground.SetActive(_isStateObjects);
    }

    private IEnumerator MovesPanel()
    {
        Vector2 start = new Vector2(_rectTransform.anchorMin.x, 0f);
        Vector2 finish = new Vector2(_anchorMinX, 0);
        float delta = 0;

        while (Vector2.Distance(_rectTransform.anchorMin, finish) > 0.01f)
        {
            _rectTransform.anchorMin = Vector2.Lerp(start, finish, delta);
            delta += Time.deltaTime * _speed;
            yield return null;

            if (_anchorMinX == 0)
            {
                if (Vector2.Distance(_rectTransform.anchorMin, finish) < 0.85f)
                    ChangesStateObjects();
            }
            else
            {
                if (Vector2.Distance(_rectTransform.anchorMin, finish) < 0.15f)
                    ChangesStateObjects();
            }
        }

        _rectTransform.anchorMin = finish;
    }
}
