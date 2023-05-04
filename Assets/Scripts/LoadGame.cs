using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private List<ButtonLevel> _buttonsLevel;

    private int _numberScene = 1;

    private void OnEnable()
    {
        foreach (var button in _buttonsLevel)
            button.LevelNumber += OnButtonClick;
    }


    private void OnDisable()
    {
        foreach (var button in _buttonsLevel)
            button.LevelNumber -= OnButtonClick;
    }

    private void OnButtonClick(int number)
    {
        LevelGame.SetLevel(number);
        SceneManager.LoadScene(_numberScene);
    }
}
