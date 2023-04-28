using UnityEngine;
using UnityEngine.Events;

public class Attenuation : Fire
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private Match _match;

    private int stateBurnt = 2;

    public event UnityAction BurnedMatch;

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Combustion _) && _match.State != stateBurnt)
        {
            Particle.gameObject.SetActive(true);
            Particle.Play();
            _sphereCollider.radius = 3f;
            BurnedMatch?.Invoke();
        }
    }

    public override void Reload()
    {
        Particle.Stop();
        Particle.gameObject.SetActive(false);
        _sphereCollider.radius = 0.5f;
    }
}
