using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Match : MonoBehaviour
{
    [SerializeField] private Attenuation _attenuation;
    [SerializeField] private Combustion _combustion;
    [SerializeField] private Ignition _ignition;

    private float _speed = 1f;
    private int stateBurning = 1;
    private int stateBurnt = 2;

    public int State { get; private set; } = 0;

    public event UnityAction BurnedDown;

    private void OnEnable()
    {
        _attenuation.BurnedMatch += OnBurnedMatch;
        _combustion.BurningMatch += OnBurningMatch;
    }

    private void OnDisable()
    {
        _attenuation.BurnedMatch -= OnBurnedMatch;
        _combustion.BurningMatch -= OnBurningMatch;
    }

    private void OnMouseDown()
    {
        if (Spawner.IsPlayGame == false)
            StartCoroutine(Flips());
    }

    public void Reload()
    {
        _combustion.Reload();
        _attenuation.Reload();
        _ignition.Reload();

    }

    private void OnBurnedMatch()
    {
        State = stateBurnt;
        BurnedDown?.Invoke();
    }

    private void OnBurningMatch() => State = stateBurning;

    private IEnumerator Flips()
    {
        float deltaPosition = 0f;
        float liftingHeight = -5;
        Vector3 startPosition = gameObject.transform.position;
        Vector3 endPosition = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            gameObject.transform.position.z + liftingHeight);

        while (Vector3.Distance(gameObject.transform.position, endPosition) > 0.01)
        {
            gameObject.transform.position = Vector3.Lerp(
                startPosition, endPosition, deltaPosition);
            deltaPosition += Time.deltaTime * _speed;
            yield return null;
        }

        gameObject.transform.position = endPosition;
        float stepTurning = 0;

        while (stepTurning < 180f)
        {
            gameObject.transform.Rotate(0, 0, 1f, Space.Self);
            stepTurning += 1;
            yield return new WaitForSeconds(0.01f);
        }

        deltaPosition = 0f;

        while (Vector3.Distance(gameObject.transform.position, startPosition) > 0.01)
        {
            gameObject.transform.position = Vector3.Lerp(
                endPosition, startPosition, deltaPosition);
            deltaPosition += Time.deltaTime * _speed;
            yield return null;
        }

        gameObject.transform.position = startPosition;
    }
}
