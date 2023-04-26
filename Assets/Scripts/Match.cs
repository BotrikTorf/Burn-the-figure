using System.Collections;
using UnityEngine;

public class Match : MonoBehaviour
{
    [SerializeField] private Attenuation _attenuation;

    private float _speed = 1f;

    public bool IsMatchBurned { get; private set; } = false;

    private void OnEnable() => _attenuation.BurnedMatch += OnatchBurned;

    private void OnDisable() => _attenuation.BurnedMatch -= OnatchBurned;

    private void OnMouseDown()
    {
        if (Spawner.IsPlayGame == false)
            StartCoroutine(Flips());
    }

    private void OnatchBurned() => IsMatchBurned = true;

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
