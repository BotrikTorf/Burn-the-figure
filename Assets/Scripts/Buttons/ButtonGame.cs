using UnityEngine.Events;

public class ButtonGame : ButtonProject
{
    public event UnityAction PlayGame;

    protected override void OnButtonClick() => PlayGame?.Invoke();
}
