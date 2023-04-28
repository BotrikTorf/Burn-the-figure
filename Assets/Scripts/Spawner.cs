using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ButtonGame _buttonGame;
    [SerializeField] private GameObject _match;

    private int _levelStart;
    private List<int> _lintsNumberMatch;
    private List<GameObject> _matchs;

    private Dictionary<int, float[]> _pointSpawner = new Dictionary<int, float[]>
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
        _matchs = new List<GameObject>(_lintsNumberMatch.Count);
        CreateMatches();
    }

    public static bool IsPlayGame { get; private set; } = false;

    private void OnEnable()
    {
        _buttonGame.PlayGame += OnPlayGame;

        foreach (var match in _matchs)
            match.GetComponentInChildren<Match>().BurnedDown += OnBurnedDown;
    }

    private void OnDisable()
    {
        _buttonGame.PlayGame -= OnPlayGame;

        foreach (var match in _matchs)
            match.GetComponentInChildren<Match>().BurnedDown -= OnBurnedDown;
    }

    public void Reload()
    {
        IsPlayGame = false;

        foreach (var match in _matchs)
            match.GetComponentInChildren<Match>().Reload();

        for (int i = 0; i < _matchs.Count; i++)
        {
            float[] valuePosition = _pointSpawner[_lintsNumberMatch[i]];
            float[] valueRotation = { valuePosition[2], valuePosition[2] - 180 };
            _matchs[i].transform.rotation =
                Quaternion.Euler(new Vector3(0, 0, valueRotation[Random.Range(0, valueRotation.Length)]));
        }
    }


    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnPlayGame() => IsPlayGame = true;

    private void OnBurnedDown()
    {
        int matchBurned = 0;
        bool haveBurningMatches = false;

        foreach (var match in _matchs)
        {
            if (match.GetComponentInChildren<Match>().State == 1)
            {
                haveBurningMatches = true;
                break;
            }

            if (match.GetComponentInChildren<Match>().State == 2)
                matchBurned++;
        }

        if (matchBurned == _matchs.Count)
        {
            Debug.Log("Win");
        }
        else
        {
            if (haveBurningMatches == false && matchBurned < _matchs.Count)
            {
                Debug.Log("Loss");
            }
        }
    }

    private void CreateMatches()
    {
        for (int i = 0; i < _lintsNumberMatch.Count; i++)
        {
            float[] valuePosition = _pointSpawner[_lintsNumberMatch[i]];
            float[] valueRotation = { valuePosition[2], valuePosition[2] - 180 };
            GameObject clonMatct = Instantiate(_match,
                new Vector3(valuePosition[0], valuePosition[1], 0),
                Quaternion.Euler(new Vector3(0, 0, valueRotation[Random.Range(0, valueRotation.Length)])));
            _matchs.Add(clonMatct);
        }
    }
}