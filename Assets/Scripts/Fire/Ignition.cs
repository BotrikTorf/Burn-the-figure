using UnityEngine;

public class Ignition : Fire
{
    [SerializeField] private Match _match;

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Attenuation _) && _match.IsMatchBurned == false)
        {
            Particle.gameObject.SetActive(true);
            Particle.Play();
        }
    }
}
