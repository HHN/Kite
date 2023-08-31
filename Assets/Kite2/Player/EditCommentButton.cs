using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditCommentButton : MonoBehaviour, OnSuccessHandler
{
    [SerializeField] private Button button;
    [SerializeField] private Button uploadButon;
    [SerializeField] private GameObject editCommentServerCallPrefab;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject inputFieldWrapper;
    [SerializeField] private TextMeshProUGUI commentTextGameObject;
    [SerializeField] private GameObject parent;

    private long commentId;
    private CommentSectionSceneController commentSectionSceneController;
    private bool isInEditMode;
    private bool isOwnComment;

    private void Start()
    {
        if (GameManager.Instance().applicationMode != ApplicationModes.LOGGED_IN_USER_MODE || !this.isOwnComment)
        {
            this.parent.SetActive(false);
            return;
        }
        this.button.onClick.AddListener(delegate { this.OnClick(); });
        this.uploadButon.onClick.AddListener(delegate { this.OnClickUploadButton(); });
    }

    public void SetOwnComment(bool isOwnComment)
    {
        this.isOwnComment = isOwnComment;
    }

    public void SetCommentId(long id)
    {
        this.commentId = id;
    }

    public void SetInputText(string inputText)
    {
        this.inputField.text = inputText;
    }

    public string GetInputText()
    {
        return this.inputField.text;
    }

    public void SetControllerAndCheckForCommentInEdit(CommentSectionSceneController controller)
    {
        this.commentSectionSceneController = controller;

        if (controller.GetCommentInEdit() == null)
        {
            return;
        }
        if (controller.GetCommentInEdit().Equals(this))
        {
            controller.SetCommentInEdit(this);
        }
    }

    public bool Equals(EditCommentButton instance)
    {
        return (instance.commentId == this.commentId);
    }

    public void OnClick()
    {
        if (this.isInEditMode)
        {
            this.DeactivateEditMode();
        }
        else
        {
            this.ActivateEditMode();
        }
    }

    public void OnClickUploadButton()
    {
        OnChangeButton();
    }

    public void OnChangeButton()
    {
        ChangeCommentServerCall call = Instantiate(this.editCommentServerCallPrefab).GetComponent<ChangeCommentServerCall>();
        call.sceneController = this.commentSectionSceneController;
        call.onSuccessHandler = this;
        call.id = this.commentId;
        call.comment = this.inputField.text.Trim();
        call.SendRequest();
        this.DeactivateEditMode();
    }

    public void ActivateEditMode()
    {
        this.CheckForOtherCommentsInEditModeAndHandleThem();
        this.commentTextGameObject.gameObject.SetActive(false);
        this.inputFieldWrapper.SetActive(true);
        this.isInEditMode = true;
        this.SetInputText(commentTextGameObject.text);
        this.commentSectionSceneController.SetCommentInEdit(this);
    }

    public void DeactivateEditMode()
    {
        this.commentTextGameObject.gameObject.SetActive(true);
        this.inputFieldWrapper.SetActive(false);
        this.isInEditMode = false;
        this.commentSectionSceneController.SetCommentInEdit(null);
    }

    private void CheckForOtherCommentsInEditModeAndHandleThem()
    {
        if (this.commentSectionSceneController.GetCommentInEdit() == null)
        {
            return;
        }

        this.commentSectionSceneController.GetCommentInEdit().DeactivateEditMode();
    }

    public void OnSuccess(Response response)
    {
        List<Comment> comments = response.comments;
        this.commentSectionSceneController.UpdatePage(comments);
    }
}
