using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TSceneManager : TSingleton<TSceneManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

}
