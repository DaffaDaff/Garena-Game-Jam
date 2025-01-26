using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance;
    private List<NavigationModel> navigationRoute = new List<NavigationModel>();
    public NavigationModel currentNavigation = new NavigationModel{SequenceInput = new List<string> {}};

    // Start is called before the first frame update
    void Awake()
    {
        if ( Instance == null ){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        } 
    }

    private void redraw(){
        foreach (var model in navigationRoute){
            if(model.UI){
                model.UI.SetActive(false);
            }
        }
    }
    // TODO : Make navigation logic
    public void NavigateToMenu() 
    {
        if (currentNavigation.UI == navigationRoute[1].UI) 
        {
            NavigateBackFromGame();
            return;
        }
        redraw();
        currentNavigation = navigationRoute[2];
        currentNavigation.UI.SetActive(true);
        currentNavigation.UIGuide.Invoke();
    }
    
    public void NavigateToTutor() 
    {
        redraw();
        currentNavigation = navigationRoute[0];
        currentNavigation.UI.SetActive(true);
        currentNavigation.UIGuide.Invoke();
    }

    public void NavigateToGame() 
    {
        redraw();
        currentNavigation = navigationRoute[1];
        currentNavigation.UIGuide.Invoke();
        SceneManager.LoadScene("LevelTest");
    }

    public void NavigateBackFromGame()
    {
        GameManager.Instance.GameOverUI.SetActive(false);
        SceneManager.LoadScene("UI Scene");
        currentNavigation = navigationRoute[2];
        redraw();
        currentNavigation.UIGuide.Invoke();
        currentNavigation.UI.SetActive(true);
    }

    public void NavigationNext()
    {
        if (currentNavigation.UI == navigationRoute[1].UI)
        {
            HelpManager.Instance.NextGameGuide();
        }

        else if (currentNavigation.UI == navigationRoute[2].UI)
        {

        }
    }

    public void NavigationPrevious()
    {
        if (currentNavigation.UI == navigationRoute[1].UI)
        {
            HelpManager.Instance.PreviousGameGuide();
        }

        else if (currentNavigation.UI == navigationRoute[2].UI)
        {

        }
    }

    public void AddNavigationModel(NavigationModel newModel){
        navigationRoute.Add(newModel);
        currentNavigation = newModel;
    }
}
