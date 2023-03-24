using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TInventoryManager : TSingleton<TInventoryManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

}
