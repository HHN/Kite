public class SceneRouter
{
    public static string GetTargetSceneForBackButton()
    {
        BackStackManager.Instance().Pop();
        return BackStackManager.Instance().Peek();
    }

    public static string GetTargetSceneForCloseButton()
    {
        return SceneNames.MAIN_MENU_SCENE;
    }
}
