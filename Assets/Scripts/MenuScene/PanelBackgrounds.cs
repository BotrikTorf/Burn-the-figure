using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class PanelBackgrounds : MonoBehaviour
{
    [SerializeField] private ButtonClosePanelBackgrounds _buttonBack;
    [SerializeField] private List<ImageBackground> _backgrounds;
    [SerializeField] private GameObject _verticalScrolView;

    private Sprite _background;
    private float _speed = 3f;
    private float _anchorMaxY;
    private bool _isStateObjects;
    private RectTransform _rectTransform;
    public Sprite Background => _background == null ? _backgrounds[0].GetComponent<Image>().sprite : _background;

    private void Awake() => _rectTransform = GetComponent<RectTransform>();

    private void OnEnable()
    {
        foreach (var background in _backgrounds)
            background.TransferBackground += OnTransferBackground;
    }

    private void OnDisable()
    {
        foreach (var background in _backgrounds)
            background.TransferBackground -= OnTransferBackground;
    }

    public void PullPanel()
    {
        _anchorMaxY = 1;
        _isStateObjects = true;
        StartCoroutine(MovesPanel());
    }

    public void PushInPanel()
    {
        _anchorMaxY = 0;
        _isStateObjects = false;
        StartCoroutine(MovesPanel());
    }

    private void OnTransferBackground(Sprite _sprite) => _background = _sprite;

    private void ChangesStateObjects()
    {
        _buttonBack.gameObject.SetActive(_isStateObjects);
        _verticalScrolView.SetActive(_isStateObjects);
    }
    private IEnumerator MovesPanel()
    {

        Vector2 start = new Vector2(1, _rectTransform.anchorMax.y);
        Vector2 finish = new Vector2(1, _anchorMaxY);
        float delta = 0;

        while (Vector2.Distance(_rectTransform.anchorMax, finish) > 0.01f)
        {
            _rectTransform.anchorMax = Vector2.Lerp(start, finish, delta);
            delta += Time.deltaTime * _speed;
            yield return null;

            if (_anchorMaxY == 1)
            {
                if (Vector2.Distance(_rectTransform.anchorMax, finish) < 0.85f)
                    ChangesStateObjects();
            }
            else
            {
                if (Vector2.Distance(_rectTransform.anchorMax, finish) < 0.15f)
                    ChangesStateObjects();
            }
        }

        _rectTransform.anchorMax = finish;
    }
}
