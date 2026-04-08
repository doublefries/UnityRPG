using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public string[] dialogue;
    public string npcName;
    public TMP_Text nameText;
    public Image profileImage;
    public Sprite npcImage;

    private int index;

    public Button contButton;
    public float wordSpeed;
    public bool playerIsClose;

    private bool isTyping;
    private bool dialogueOpen;

    void Update()
    {
        if (playerIsClose && Input.GetKeyDown(KeyCode.E) && !dialogueOpen)
        {
            if (dialogue == null || dialogue.Length == 0)
                return;

            dialogueOpen = true;
            index = 0;

            dialoguePanel.SetActive(true);
            nameText.text = npcName;
            profileImage.sprite = npcImage;

            contButton.onClick.RemoveAllListeners();
            contButton.onClick.AddListener(NextLine);

            StopAllCoroutines();
            StartCoroutine(Typing());
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialogueOpen = false;
        isTyping = false;
        dialoguePanel.SetActive(false);
        contButton.gameObject.SetActive(false);
    }

    IEnumerator Typing()
    {
        if (dialogue == null || dialogue.Length == 0)
            yield break;

        if (index < 0 || index >= dialogue.Length)
            yield break;

        isTyping = true;
        dialogueText.text = "";
        contButton.gameObject.SetActive(false);

        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        isTyping = false;
        contButton.gameObject.SetActive(true);
    }

    public void NextLine()
    {
        if (!dialogueOpen || isTyping)
            return;

        if (index < dialogue.Length - 1)
        {
            index++;
            StopAllCoroutines();
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            Debug.Log("Player entered NPC range");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            Debug.Log("Player left NPC range");
            zeroText();
        }
    }
}