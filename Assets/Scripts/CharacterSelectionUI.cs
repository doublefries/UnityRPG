using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    public GameObject optionPrefab;
    public Transform prevCharacter;
    public Transform selectedCharacter;

    private void Start()
    {
        foreach (Character c in GameManager.instance.characters)
        {
            GameObject option = Instantiate(optionPrefab, transform);
            Button button = option.GetComponent<Button>(); //Button on each character option
            
            button.onClick.AddListener(() =>
            {
                GameManager.instance.SetCharacter(c);
                if (selectedCharacter != null)
                {
                    prevCharacter = selectedCharacter; //For Animation
                }
                
                selectedCharacter = option.transform; 
            });
            
            Text text =  option.GetComponentInChildren<Text>();
            text.text = c.name; //set text to that characters name
            
            Image image = option.GetComponentInChildren<Image>();
            image.sprite = c.icon;

        }
    }
}
