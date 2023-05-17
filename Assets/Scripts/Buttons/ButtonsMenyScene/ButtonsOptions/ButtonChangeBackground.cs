using UnityEngine;

public class ButtonChangeBackground : ButtonProject
{
    [SerializeField] private PanelBackgrounds _panelBackgrounds;

    protected override void OnButtonClick() => _panelBackgrounds.PullPanel();
}
