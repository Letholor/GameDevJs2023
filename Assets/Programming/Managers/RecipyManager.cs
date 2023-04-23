using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipyManager : Singleton<RecipyManager>
{
    #region Fields

    [SerializeField] private float speed = 1f; // Speed of the book movement
    [SerializeField] private Vector3 centerPosition; // Center position where the book will move to
    [SerializeField] private GameObject recipeBook;
    [SerializeField] private GameObject pageTemplate;
    [SerializeField] private GameObject ingredientTemplate;
    //[SerializeField] private GameObject recipyReferenceTemplate;
    [SerializeField] private Transform pageLeft;
    [SerializeField] private Transform pageRight;
    [SerializeField] private GameObject pageMoverLeft;
    [SerializeField] private GameObject pageMoverRight;
    [SerializeField] private GameObject firstPage;
    [SerializeField] private List<BookPage> pages;

    private Vector3 originalPosition; // Original position of the book
    private bool isAtCenter;
    public int pageNum = 0;
    private Dictionary<int, BookPage> pageDict = new Dictionary<int, BookPage>();
    private Dictionary<int, GameObject> gameObjectDict = new Dictionary<int, GameObject>();

    #endregion

    #region Properties

    public GameObject buttonPrefab;
    public Transform buttonParent;

    #endregion

    #region Methods

    private void Start()
    {
        originalPosition = recipeBook.transform.position; // Save the original position of the book
        pageMoverLeft.SetActive(false);

        int i = 0;
        foreach (BookPage page in pages)
        {
            pageDict[i] = page;
            i++;
        }
        PageInitiate();
        RenderPages();
    }

    private void Update()
    {
    }

    #endregion

    #region Event Handlers

    // Method to move the book to the center position when the first button is pressed and back
    public void MoveToCenter()
    {
        if (isAtCenter)
        {
            centerPosition = originalPosition;
            isAtCenter = false;
            StartCoroutine(MoveBookToPosition(centerPosition));
        }
        else
        {
            centerPosition = new Vector3(0, 0, 0);
            isAtCenter = true;
            StartCoroutine(MoveBookToPosition(centerPosition));
        }
    }

    private IEnumerator MoveBookToPosition(Vector3 targetPosition)
    {
        while (recipeBook.transform.position != targetPosition)
        {
            recipeBook.transform.position = Vector3.MoveTowards(recipeBook.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void PageInitiate()
    {
        // Initialize the first page
        gameObjectDict[0] = firstPage;

        for (int i = 1; i <= pageDict.Count; i++)
        {
            int pageIndex = i; // Create a new variable to capture the current value of i
            GameObject page = Instantiate(pageTemplate, i % 2 == 0 ? pageLeft : pageRight);
            gameObjectDict[i] = page;

            Transform shelf = page.transform.GetChild(0);
            Transform title = page.transform.GetChild(1);

            BookPage pageDeets = pageDict[i - 1];
            PotionTypeToSprite deets = pageDeets.deets;

            foreach (Ingredient ing in deets.recipy)
            {
                var ingredient = Instantiate(ingredientTemplate, shelf);
                ingredient.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ing.ingredientName;
                ingredient.transform.GetChild(1).GetComponent<Image>().sprite = ing.sprite;
            }

            title.GetChild(0).GetComponent<TextMeshProUGUI>().text = pageDeets.title;

            GameObject newButton = Instantiate(buttonPrefab, buttonParent);
            Button buttonComponent = newButton.GetComponent<Button>();
            newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = new string(pageDeets.title);
            newButton.transform.GetChild(1).GetComponent<Image>().sprite = pageDeets.deets.sprite;
            buttonComponent.onClick.AddListener(() => GoToPage(pageIndex)); // Use the new variable in the lambda
            page.SetActive(false);
        }

        if (pageDict.Count % 2 == 0)
        {
            // Instantiate an extra page
            GameObject extraPage = Instantiate(pageTemplate, pageRight);
            gameObjectDict[pageDict.Count + 1] = extraPage;
            extraPage.SetActive(false);
        }
    }


    public void GoToPage(int page)
    {
        var x = page % 2 == 0 ? page : page - 1;
        pageNum = x;
        
        foreach (GameObject pagei in gameObjectDict.Values)
        {
            pagei.SetActive(false);
        }

        if (gameObjectDict.ContainsKey(pageNum))
        {
            gameObjectDict[pageNum].SetActive(true);
        }
        if (gameObjectDict.ContainsKey(pageNum + 1))
        {
            gameObjectDict[pageNum + 1].SetActive(true);
        }

        if (pageNum == 0)
        {
            pageMoverLeft.SetActive(false);
        }
        else
        {
            pageMoverLeft.SetActive(true);
        }

        int truPage = pageDict.Count % 2 == 0 ? pageDict.Count + 1 : pageDict.Count;

        if (pageNum == truPage - 2 || pageNum == truPage - 1)
        {
            pageMoverRight.SetActive(false);
        }
        else
        {
            pageMoverRight.SetActive(true);
        }
    }

    void RenderPages()
    {
        gameObjectDict[pageNum].SetActive(true);
        if (gameObjectDict.ContainsKey(pageNum + 1))
        {
            gameObjectDict[pageNum + 1].SetActive(true);
        }
    }

    public void PassPageLeft()
    {
        gameObjectDict[pageNum].SetActive(false);
        if (gameObjectDict.TryGetValue(pageNum + 1, out GameObject poge))
        {
            poge.SetActive(false);
        }

        if (gameObjectDict.TryGetValue(pageNum - 2, out GameObject page))
        {
            page.SetActive(true);
        }

        pageNum -= 2;

        RenderPages();

        pageMoverRight.SetActive(true);

        if (pageNum == 0)
        {
            pageMoverLeft.SetActive(false);
        }
    }


    public void PassPageRight()
    {
        gameObjectDict[pageNum].SetActive(false);
        if (gameObjectDict.TryGetValue(pageNum + 2, out GameObject page))
        {
            page.SetActive(false);
        }

        if (gameObjectDict.TryGetValue(pageNum + 1, out GameObject poge))
        {
            poge.SetActive(false);
        }

        pageNum += 2;

        if (gameObjectDict.TryGetValue(pageNum, out GameObject currentPage))
        {
            currentPage.SetActive(true);
        }

        if (gameObjectDict.TryGetValue(pageNum + 1, out GameObject nextPage))
        {
            nextPage.SetActive(true);
        }

        pageMoverLeft.SetActive(true);

        int truPage = pageDict.Count % 2 == 0 ? pageDict.Count + 1 : pageDict.Count;

        if (pageNum == truPage - 2 || pageNum == truPage - 1)
        {
            pageMoverRight.SetActive(false);
        }
    }

    #endregion
}