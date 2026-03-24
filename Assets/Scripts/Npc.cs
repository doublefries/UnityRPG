using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string dialogue;

    private int index;

    public float wordSpeed;
    public bool playerIsClose;

    void Update()
    {
        if (playerIsClose && Input.GetKeyDown(KeyCode.E))
        {
            dialoguePanel.SetActive(true);
            dialogueText.text = dialogue;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerIsClose =  true;
            Debug.Log("Player entered NPC range");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerIsClose =  false;
            Debug.Log("Player left NPC range");
        }
    }
}