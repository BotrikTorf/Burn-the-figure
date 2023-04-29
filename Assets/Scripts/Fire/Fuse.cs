using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class Fuse : MonoBehaviour
{
    [SerializeField] private ButtonGame _buttonGame;

    private SphereCollider _collider;

    public event UnityAction BurningMatch;

    private void Awake() => _collider = GetComponent<SphereCollider>();

    private void OnEnable() => _buttonGame.PlayGame += OnPlayGame;

    private void OnDisable() => _buttonGame.PlayGame -= OnPlayGame;

    private void OnPlayGame()
    {
        _collider.radius = 1f;
        InvokeRepeating("Run", 1, 0);
    }

    private void Run() => BurningMatch?.Invoke();
}
