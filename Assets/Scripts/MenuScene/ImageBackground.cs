using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class ImageBackground : MonoBehaviour
{
    private Image _image;

    public event UnityAction<Sprite> TransferBackground;

    private void Awake() => _image = GetComponent<Image>();

    private void OnMouseDown() => TransferBackground?.Invoke(_image.sprite);
}
