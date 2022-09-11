using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Transform levelContainer;
    public RectTransform menuContainer;
    public float transitionTime = 1f;
    private int screenWidth;

    private void Start()
    {
        InitLevelButtons();
        screenWidth = Screen.width;
    }
    private void InitLevelButtons()
    {
        int i = 0;
        foreach(Transform t in levelContainer)
        {
            int currentIndex = i;
            Button button = t.GetComponent<Button>();
            button.onClick.AddListener(() => OnLevelSelect(currentIndex));
        }

    }

    private void ChangeMenu(MenuType menuType)
    {
        Vector3 newPos;

        if(menuType == MenuType.Map1Menu)
        {
            newPos = new Vector3(-1*screenWidth, 0f, 0f);
        }

        else
        {
            newPos = Vector3.zero;
        }

        StopAllCoroutines();
        StartCoroutine(ChangeMenuAnimation(newPos));

    }

    private IEnumerator ChangeMenuAnimation(Vector3 newPos)
    {
        float elapsed = 0f;
        Vector3 oldPos = menuContainer.anchoredPosition3D;

        while(elapsed <= transitionTime)
        {
            elapsed += Time.deltaTime;
            Vector3 currentPos = Vector3.Lerp(oldPos, newPos, elapsed / transitionTime);
            menuContainer.anchoredPosition3D = currentPos;
            yield return null;
        }
    }
    private void OnLevelSelect(int idx)
    {
        Debug.Log("we press the level button " + idx);
        SceneManager.LoadScene("Level1");
    }
    public void OnPlayButtonClicked()
    {
        Debug.Log("play button clicked");
        ChangeMenu(MenuType.Map1Menu);
    }

    public void OnMainMenuButtonClicked()
    {
        Debug.Log("Clicked main button");
        ChangeMenu(MenuType.MainMenu);
    }

    public void OnNextMapButtonClicked()
    {
        Debug.Log("Next map clicked");
    }

    private enum MenuType
    {
        MainMenu, 
        Map1Menu
    }
}
