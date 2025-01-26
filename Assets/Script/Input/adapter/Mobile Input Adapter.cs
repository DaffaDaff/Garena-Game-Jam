using UnityEngine;
using System;

public class MobileInputAdapter : MonoBehaviour, IInputAdapter
{
    public event Action<InputEvent> OnInputReceived;

    private float screenWidth;

    void Start()
    {
        // Cache the screen width
        screenWidth = Screen.width;

        gameObject.GetComponent<InputManager>().RegisterAdapter(this);
    }

    public void Listen()
    {
        // Check if there is at least one touch on the screen
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float touchPositionX = touch.position.x;

            // Determine the side of the screen and trigger the appropriate event
            if (touchPositionX < screenWidth / 2)
            {
                // Trigger a left-side touch event
                OnInputReceived?.Invoke(new InputEvent
                {
                    InputName = "Left",
                    AdapterName = "Mobile",
                });
            }
            else
            {
                // Trigger a right-side touch event
                OnInputReceived?.Invoke(new InputEvent
                {
                    InputName = "Right",
                    AdapterName = "Mobile",
                });
            }
        }
    }
}
