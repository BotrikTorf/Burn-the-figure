using UnityEngine.Events;

public class ButtonLevelThree : ButtonGame
{
    public event UnityAction<int> LevelNumber;

    protected override void OnButtonClick() => LevelNumber?.Invoke(2);
}
