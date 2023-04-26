using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Combustion : Fire
{
    [SerializeField] private Transform _endPosition;
    [SerializeField] private Match _match;

    private Transform _transform;
    private float _speed = 0.0005f;

    private void Awake() => _transform = GetComponent<Transform>();

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Attenuation _) && _match.IsMatchBurned == false)
        {
            Particle.gameObject.SetActive(true);
            Particle.Play();
            StartCoroutine(MovementFire());
        }
    }

    private IEnumerator MovementFire()
    {
        float deltaPosition = 0f;
        Transform startTransform = _transform;

        while (Vector3.Distance(_endPosition.position, _transform.position) > 0.3)
        {
            _transform.position = Vector3.Lerp(
                startTransform.position,
                _endPosition.position, 
                deltaPosition);
            deltaPosition += Time.deltaTime * _speed;
            yield return null;
        }

        Particle.Stop();
        Particle.gameObject.SetActive(false);
        StopCoroutine(MovementFire());
    }
}
