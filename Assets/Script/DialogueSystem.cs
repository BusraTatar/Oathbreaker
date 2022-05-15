using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;
    void Start()
    {
        story = new Story(inkJSON.text);
        Debug.Log(LoadStoryChunk());
    }


    void Update()
    {

    }
    string LoadStoryChunk()
    {
        string text = " ";
        if (story.canContinue)
        {
            text = story.ContinueMaximally();
        }
        return text;
    }
}