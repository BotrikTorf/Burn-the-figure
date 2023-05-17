using UnityEngine;

public class ButtonOptions : ButtonProject
{
    [SerializeField] private PanelOptions _panelOptions;

    protected override void OnButtonClick() => _panelOptions.PullPanel();
}
