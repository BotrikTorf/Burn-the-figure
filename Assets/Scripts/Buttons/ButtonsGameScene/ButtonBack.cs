using UnityEngine.SceneManagement;

public class ButtonBack : ButtonProject
{
    protected override void OnButtonClick() => SceneManager.LoadScene(0);
}
