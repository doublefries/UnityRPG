using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    public GameObject optionPrefab;
    public Transform prevCharacter;
    public Transform selectedCharacter;

    [SerializeField] private Button startButton; //dragging start button here

    private void Start()
    {
        //DIsable start until a character is chosen
        if (startButton != null)
        {
            startButton.interactable = false;
        }
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
                
                //Enable once character is picked
                if (startButton != null)
                {
                    startButton.interactable = true;
                }
            });
            
            Text text =  option.GetComponentInChildren<Text>();
            text.text = c.name; //set text to that characters name
            
            Image image = option.GetComponentInChildren<Image>();
            image.sprite = c.icon;
        }
        //Wire to game manager
        if (startButton != null){
            startButton.onClick.AddListener(() => {
            GameManager.instance.StartGame(); });
        }
    }
}
