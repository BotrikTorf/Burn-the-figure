using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ButtonGame _buttonGame;
    [SerializeField] private GameObject _match;

    private int _levelStart;
    private List<int> _lintsNumberMatch;

    private Dictionary<int, float[]> _pointSpawner = new Dictionary<int, float[]>()
    {
        { 1, new float[3] { 0, 0, 90 } },
        { 2, new float[3] { 5, 5.5f, 0 } },
        { 3, new float[3] { 5, 16.5f, 0 } },
        { 4, new float[3] { 0, 22, 90 } },
        { 5, new float[3] { -5, 16.5f, 0 } },
        { 6, new float[3] { -5, 5.5f, 0 } },
        { 7, new float[3] { 0, 11, 90 } }
    };

    private void Awake()
    {
        _levelStart = LevelGame.GetLevel();
        _lintsNumberMatch = (List<int>)LevelGame.GetMatchNumbers(_levelStart);
    }

    public static bool IsPlayGame { get; private set; } = false;

    private void OnEnable() => _buttonGame.PlayGame += OnPlayGame;

    private void OnDisable() => _buttonGame.PlayGame -= OnPlayGame;

    private void Start()
    {
        for (int i = 0; i < _lintsNumberMatch.Count; i++)
        {

            float[] valuePosition = _pointSpawner[_lintsNumberMatch[i]];
            float[] valueRotation = { valuePosition[2], valuePosition[2] - 180 };
            Instantiate(_match,
                new Vector3(valuePosition[0], valuePosition[1], 0),
                Quaternion.Euler(new Vector3(0, 0, valueRotation[Random.Range(0, valueRotation.Length)])));
        }

    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnPlayGame() => IsPlayGame = true;
}