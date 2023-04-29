using UnityEngine.SceneManagement;

public class ButtonReload : ButtonProject
{
    protected override void OnButtonClick() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
