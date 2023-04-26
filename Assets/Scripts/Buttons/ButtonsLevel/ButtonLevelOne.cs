using UnityEngine.Events;

public class ButtonLevelOne : ButtonGame
{
    public event UnityAction<int> LevelNumber;

    protected override void OnButtonClick() => LevelNumber?.Invoke(0);
}
