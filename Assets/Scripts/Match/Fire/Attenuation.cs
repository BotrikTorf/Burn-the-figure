using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class Attenuation : Fire
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private Match _match;
    [SerializeField] private Material _material;

    private MeshRenderer _meshRenderer;

    private int stateBurnt = 2;

    public event UnityAction BurnedMatch;

    private void Awake() => _meshRenderer = GetComponent<MeshRenderer>();

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Combustion _) && _match.State != stateBurnt)
        {
            Particle.gameObject.SetActive(true);
            Particle.Play();
            _sphereCollider.radius = 3f;
            _meshRenderer.material = _material;
            BurnedMatch?.Invoke();
        }
    }
}
