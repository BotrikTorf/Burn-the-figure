using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Transform))]
public class Combustion : Fire
{
    [SerializeField] private Transform _endTransform;
    [SerializeField] private Match _match;

    private Transform _transform;
    private float _speed = 0.005f;
    private int _stateBurnt = 2;

    public event UnityAction BurningMatch;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Attenuation _)
            || collider.TryGetComponent(out Fuse _))
        {
            if (_match.State != _stateBurnt)
            {
                BurningMatch?.Invoke();
                Particle.gameObject.SetActive(true);
                Particle.Play();
                StartCoroutine(MovementFire());
            }
        }
    }

    private IEnumerator MovementFire()
    {
        float deltaPosition = 0f;
        Transform startTransform = _transform;

        while (Vector3.Distance(_endTransform.position, _transform.position) > 0.3)
        {
            _transform.position = Vector3.Lerp(
                startTransform.position,
                _endTransform.position,
                deltaPosition);
            deltaPosition += Time.deltaTime * _speed;
            yield return null;
        }

        Particle.Stop();
        Particle.gameObject.SetActive(false);
        StopCoroutine(MovementFire());
    }
}
