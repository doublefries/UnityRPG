using System.Collections;
using UnityEngine;
using TMPro;

public class Npc : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public string[] dialogue;
    public string npcName;
    public TMP_Text nameText;
    public UnityEngine.UI.Image profileImage;
    public Sprite npcImage;

    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;

    void Update()
    {
        if (playerIsClose && Input.GetKeyDown(KeyCode.E))
        {
            if (dialoguePanel.activeInHierarchy)
            {
                NextLine();
            }
            else
            {
                nameText.text = npcName;
                dialoguePanel.SetActive(true);
                profileImage.sprite = npcImage;
                StartCoroutine(Typing());
            }
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        contButton.SetActive(false);
    }

    IEnumerator Typing()
    {
        dialogueText.text = "";
        contButton.SetActive(false);

        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        contButton.SetActive(true);
    }

    public void NextLine()
    {
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
        if (other.tag == "Player")
        {
            playerIsClose = true;
            Debug.Log("Player entered NPC range");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerIsClose = false;
            Debug.Log("Player left NPC range");
            zeroText();
        }
    }
}