using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentSectionInputArea : MonoBehaviour, OnSuccessHandler
{
    public Button postButton;
    public TMP_InputField inputField;
    public GameObject postCommentServerCallPrefab;
    public CommentSectionSceneController commentSectionSceneController;

    // Start is called before the first frame update
    void Start()
    {
        postButton.onClick.AddListener(delegate { OnPostButton(); });
    }

    public void OnPostButton()
    {
        commentSectionSceneController.DisplayInfoMessage(InfoMessages.WAIT_FOR_UPLOAD_COMMENT);
        PostCommentServerCall call = Instantiate(postCommentServerCallPrefab).GetComponent<PostCommentServerCall>();
        call.sceneController = commentSectionSceneController;
        call.onSuccessHandler = this;
        call.visualNovelId = PlayManager.Instance().GetVisualNovelToPlay().id;
        call.comment = inputField.text.Trim();
        call.SendRequest();
        inputField.text = string.Empty;
    }

    public void OnSuccess(Response response)
    {
        commentSectionSceneController.messageObject.CloseMessageBox();
        List<Comment> comments = response.comments;
        commentSectionSceneController.SetComments(comments);
    }
}
