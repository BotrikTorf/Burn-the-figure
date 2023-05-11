using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Ignition : Fire
{
    [SerializeField] private Match _match;

    private BoxCollider _boxCollider;
    private int _stateBurnt = 2;

    private void Awake() => _boxCollider = GetComponent<BoxCollider>();

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Attenuation _)
            || collider.TryGetComponent(out Fuse _))
        {
            if (_match.State != _stateBurnt)
            {
                Particle.gameObject.SetActive(true);
                Particle.Play();
            }
        }
    }

    public void ControlsStateCollider()
    {
        if (_boxCollider.enabled)
            _boxCollider.enabled = false;
        else
            _boxCollider.enabled = true;
    }
}
