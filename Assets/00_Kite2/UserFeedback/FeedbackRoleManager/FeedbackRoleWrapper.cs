using System;

[Serializable]
public class FeedbackRoleWrapper
{
    public int role;

    // 0 == role01;         no special abilitys

    // 1 == role02;         can give a review to novels

    // 2 == role03;         can give a review to ai reviews

    // 3 == role04;         can give a review to novels and ai reviews

    // 4 == role05;         can give a review to novels and ai reviews + can see all reviews and observers + can add own account as review-observer 

    // 5 == role06;         can give a review to novels and ai reviews + can see all reviews and observers + can add own account as review-observer + can delete reviews
}
