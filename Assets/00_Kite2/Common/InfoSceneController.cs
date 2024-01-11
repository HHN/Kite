public class InfoSceneController : SceneController
{
    private int clickCounter = 0;

    public void OnHiddenButtonPressed()
    {
        clickCounter++;
        if (clickCounter == 10)
        {
            clickCounter = 0;
            LoadFeedbackRoleManagementScene();
        }
    }

    private void LoadFeedbackRoleManagementScene()
    {
        SceneLoader.LoadFeedbackRoleManagementScene();
    }
}
