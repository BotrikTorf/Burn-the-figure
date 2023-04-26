using UnityEngine.Events;

public class ButtonLevelTwo : ButtonGame
{
    public event UnityAction<int> LevelNumber;

    protected override void OnButtonClick() => LevelNumber?.Invoke(1);
}