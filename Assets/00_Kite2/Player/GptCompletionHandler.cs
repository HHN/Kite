using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GptCompletionHandler
{
    string ProcessCompletion(string completion);
}
