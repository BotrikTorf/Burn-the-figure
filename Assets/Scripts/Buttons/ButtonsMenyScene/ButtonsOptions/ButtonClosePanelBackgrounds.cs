using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClosePanelBackgrounds : ButtonProject
{
    [SerializeField] private PanelBackgrounds _panelBackgrounds;
    protected override void OnButtonClick() => _panelBackgrounds.PushInPanel();
}
