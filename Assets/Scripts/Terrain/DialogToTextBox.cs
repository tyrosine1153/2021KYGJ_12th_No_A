using UnityEngine;

public class DialogToTextBox : MonoBehaviour
{
    [SerializeField] private bool isDialogueEnd;
    [TextArea] public string dialogueText = "";

    private void Start()
    {
        isDialogueEnd = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDialogueEnd)
        {
            isDialogueEnd = true;
            TextBoxScript.Instance.TypeText(dialogueText);
        }
    }
}