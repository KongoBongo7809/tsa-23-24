using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public Animator animator;
    public DialogueTrigger dialogueTrigger;

    public void Start()
    {
        dialogueTrigger.TriggerDialogue();
    }

    public void TriggerAnimation()
    {
        animator.SetTrigger("Next Animation");
    }
}
