using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{
    public Ingredient ingredient;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        //button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        PotionMaster.Instance.AddIngredient(ingredient);
    }
}
