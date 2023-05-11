using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ButtonGame _buttonGame;
    [SerializeField] private GameObject _match;
    [SerializeField] private Fuse _fuse;
    [SerializeField] private GameObject _panelVictory;
    [SerializeField] private GameObject _panelLoss;

    private int _levelStart;
    private List<int> _lintsNumberMatch;
    private List<Match> _matchs;

    private Dictionary<int, float[]> _pointsSpawner = new Dictionary<int, float[]>
    {
        { 1, new float[3] { 0, 0, 90 } },
        { 2, new float[3] { 3.8f, 3.8f, 0 } },
        { 3, new float[3] { 3.8f, 11.3f, 0 } },
        { 4, new float[3] { 0, 15.1f, 90 } },
        { 5, new float[3] { -3.8f, 11.3f, 0 } },
        { 6, new float[3] { -3.8f, 3.8f, 0 } },
        { 7, new float[3] { 0, 7.5f, 90 } }
    };

    public static bool IsPlayGame { get; private set; }

    private void Awake()
    {
        _levelStart = LevelGame.GetLevel();
        _lintsNumberMatch = (List<int>)LevelGame.GetMatchNumbers(_levelStart);
        _matchs = new List<Match>(_lintsNumberMatch.Count);
        CreateMatches();
        IsPlayGame = false;
    }

    private void OnEnable()
    {
        _buttonGame.PlayGame += OnPlayGame;
        _fuse.BurningMatch += OnBurnedDown;

        foreach (var match in _matchs)
            match.BurnedDown += OnBurnedDown;
    }

    private void OnDisable()
    {
        _buttonGame.PlayGame -= OnPlayGame;
        _fuse.BurningMatch -= OnBurnedDown;

        foreach (var match in _matchs)
            match.BurnedDown -= OnBurnedDown;
    }

    private void OnPlayGame() => IsPlayGame = true;

    private void OnBurnedDown()
    {
        int matchBurned = 0;
        bool haveBurningMatches = false;

        foreach (var match in _matchs)
        {
            if (match.State == 1)
            {
                haveBurningMatches = true;
                break;
            }

            if (match.State == 2)
                matchBurned++;
        }

        if (matchBurned == _matchs.Count)
        {
            RemovesMatches();
            ShowVictoryPanel();
        }

        else if (haveBurningMatches == false && matchBurned < _matchs.Count)
        {
            RemovesMatches();
            ShowLossPanel();
        }


    }

    private void CreateMatches()
    {
        for (int i = 0; i < _lintsNumberMatch.Count; i++)
        {
            float[] valuePosition = _pointsSpawner[_lintsNumberMatch[i]];
            float[] valueRotation = { valuePosition[2], valuePosition[2] - 180 };
            GameObject cloneMatch = Instantiate(_match,
                new Vector3(valuePosition[0], valuePosition[1], 0),
                Quaternion.Euler(new Vector3(0, 0, valueRotation[Random.Range(0, valueRotation.Length)])));
            _matchs.Add(cloneMatch.GetComponent<Match>());
        }
    }

    private void ShowLossPanel() => _panelLoss.SetActive(true);

    private void ShowVictoryPanel()
    {
        _panelVictory.SetActive(true);
        InvokeRepeating("ExitMenuScene", 2, 0);
    }

    private void ExitMenuScene() => SceneManager.LoadScene(0);

    private void RemovesMatches()
    {
        foreach (var match in _matchs)
            match.gameObject.SetActive(false);
    }
}
