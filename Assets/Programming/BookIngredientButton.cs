using UnityEngine;
using UnityEngine.UI;

public class BookIngredientButton : MonoBehaviour
{
    public GameObject targetObject;
    public string targetFunction;
    public int parameter;

    private Button button;

    void Start()
    {
        // Get the Button component of the attached button
        //button = GetComponent<Button>();

        // Add a listener to the button to call the target function when it is clicked
        //button.onClick.AddListener(() => targetObject.SendMessage(targetFunction, parameter));
    }
}
