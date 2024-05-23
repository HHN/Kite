using UnityEngine;

public class SceneRouter
{
    public static string GetTargetSceneForBackButton()
    {
        BackStackManager.Instance().Pop();
        Debug.Log(BackStackManager.Instance().Peek());
        return BackStackManager.Instance().Peek();
    }

    public static string GetTargetSceneForCloseButton()
    {
        return SceneNames.MAIN_MENU_SCENE;
    }
}
