using UnityEngine.Events;


public abstract class ButtonLevel : ButtonProject
{
    private protected int NumberLevel;

    public event UnityAction<int> LevelNumber;
    protected override void OnButtonClick() => LevelNumber?.Invoke(NumberLevel);
}
