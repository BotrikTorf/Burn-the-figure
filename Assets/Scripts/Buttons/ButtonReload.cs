using UnityEngine;

public class ButtonReload : ButtonProject
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Fuse _fuse;

    protected override void OnButtonClick()
    {
        _fuse.Reload();
        _spawner.Reload();
    }
}
