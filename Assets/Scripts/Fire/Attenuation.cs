using UnityEngine;
using UnityEngine.Events;

public class Attenuation : Fire
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private Match _match;

    public event UnityAction BurnedMatch;

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Combustion _) && _match.IsMatchBurned == false)
        {
            Particle.gameObject.SetActive(true);
            Particle.Play();
            _sphereCollider.radius = 3f;
            BurnedMatch?.Invoke();
        }
    }
}
