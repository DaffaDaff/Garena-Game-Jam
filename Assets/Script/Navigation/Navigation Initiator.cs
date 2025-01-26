using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationInitiator : MonoBehaviour
{
    public HelpManager managerHelper;
    public List<GameObject> UIElement;

    void Start()
    {        
        NavigationModel menu = new NavigationModel{
            UI = UIElement[0],
            SequenceInput = new List<string> {
                "> > < < > < < > ",
                "> < > < < > > ",
                "> < < > < < < ",
            },
            UIGuide = () => managerHelper.updateState(0)
        };

        NavigationModel game = new NavigationModel{
            UI = UIElement[1],
            SequenceInput = new List<string> {
                //navigation
                "> > > < < > < ",
                "> < > < > < ",
                "> > > > ",
                "> > < < ",
                //movement
                "> < > > ",
                "> < < < ",
                "> < < > ",
                "> < > < ",
                // direction
                "> < < < < > ",
                "> > > < > < < ",
                "> > < > > ",
                "> > < > < < > > ",
                // subject
                "> > > < < ",
                "> > < > < > > > ",
                // to be
                "> > < > ",
                "> > < > < > ",
                // adjective
                "> < > > < < > ",
                "> < < < > < > < <",
                // noun
                "> > < < < > ",
                "> < > < > >"
            },
            UIGuide = () => managerHelper.updateState(1)
        };

        NavigationModel tutor = new NavigationModel{
            UI = UIElement[2],
            SequenceInput = new List<string> {
                "> > > < < > < ",
                "> > > > ",
                "> > < < "
            },
            UIGuide = () => managerHelper.updateState(2)
        };

        NavigationManager.Instance.AddNavigationModel(tutor);
        NavigationManager.Instance.AddNavigationModel(game);
        NavigationManager.Instance.AddNavigationModel(menu);
    }
}
