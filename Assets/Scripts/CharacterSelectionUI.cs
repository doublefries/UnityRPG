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
                    prevCharacter = selectedCharacter;
                }
                
                selectedCharacter = option.transform; 

            });
        }
    }
}
