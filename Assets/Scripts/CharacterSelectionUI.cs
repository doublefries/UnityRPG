using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private Character[] characters;
    [SerializeField] private Button startButton; //dragging start button here

    private void Start()
    {
        //DIsable start until a character is chosen
        if (startButton != null)
        {
            startButton.interactable = false;
        }
        foreach (Character c in characters)
        {
            Character localChar = c;
            GameObject option = Instantiate(optionPrefab, transform);
            Button button = option.GetComponent<Button>(); //Button on each character option
            
            button.onClick.AddListener(() =>
            {
                GameManager.instance.SelectCharacter(localChar);
                
                //Enable once character is picked
                if (startButton != null)
                {
                    startButton.interactable = true;
                }
            });
            
            Text text =  option.GetComponentInChildren<Text>();
            text.text = localChar.name; //set text to that characters name
            
            Image image = option.GetComponentInChildren<Image>();
            image.sprite = localChar.icon;
        }
        //Wire to game manager
        if (startButton != null){
            startButton.onClick.AddListener(() => {
            GameManager.instance.StartGame(); });
        }
    }
}
