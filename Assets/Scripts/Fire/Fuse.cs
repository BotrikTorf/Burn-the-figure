using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Fuse : MonoBehaviour
{
    [SerializeField] private ButtonGame _buttonGame;

    private SphereCollider _collider;

    private void Awake() => _collider = GetComponent<SphereCollider>();

    private void OnEnable() => _buttonGame.PlayGame += OnPlayGame;

    private void OnDisable() => _buttonGame.PlayGame -= OnPlayGame;

    public void Reload() => _collider.radius = 0.1f;

    private void OnPlayGame() => _collider.radius = 1f;
}
