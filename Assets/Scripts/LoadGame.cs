using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{

    [SerializeField] private ButtonLevelOne _buttonLevelOne;
    [SerializeField] private ButtonLevelTwo _buttonLevelTwo;
    [SerializeField] private ButtonLevelThree _buttonLevelThree;

    private void OnEnable()
    {
        _buttonLevelOne.LevelNumber += OnButtonClick;
        _buttonLevelTwo.LevelNumber += OnButtonClick;
        _buttonLevelThree.LevelNumber += OnButtonClick;
    }

    private void OnDisable()
    {
        _buttonLevelOne.LevelNumber -= OnButtonClick;
        _buttonLevelTwo.LevelNumber -= OnButtonClick;
        _buttonLevelThree.LevelNumber -= OnButtonClick;
    }

    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    private void OnButtonClick(int number)
    {
        LevelGame.SetLevel(number);
        SceneManager.LoadScene(1);
    }
}
