using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class Fuse : MonoBehaviour
{
    [SerializeField] private ButtonGame _buttonGame;

    private const string _nameMethod = "Run";

    private SphereCollider _collider;

    public event UnityAction BurningMatch;

    private void Awake() => _collider = GetComponent<SphereCollider>();

    private void OnEnable() => _buttonGame.PlayGame += OnPlayGame;

    private void OnDisable() => _buttonGame.PlayGame -= OnPlayGame;

    private void OnPlayGame()
    {
        _collider.radius = 1.5f;
        InvokeRepeating(_nameMethod, 1, 0);
    }

    private void Run() => BurningMatch?.Invoke();
}
