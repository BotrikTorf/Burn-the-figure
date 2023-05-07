using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonProject))]
public abstract class ButtonProject : MonoBehaviour
{
    protected Button Button;

    private void Awake() => Button = GetComponent<Button>();

    private void OnEnable() => Button.onClick.AddListener(OnButtonClick);

    private void OnDisable() => Button.onClick.RemoveListener(OnButtonClick);

    abstract protected void OnButtonClick();
}
