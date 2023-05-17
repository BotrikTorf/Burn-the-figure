using UnityEngine;

public class ButtonClosePanelOptions : ButtonProject
{
    [SerializeField] private PanelOptions _panelOptions;

    protected override void OnButtonClick() => _panelOptions.PushInPanel();
}
