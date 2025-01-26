using UnityEngine;
using System;

public class LaptopInputAdapter : MonoBehaviour, IInputAdapter
{
    public event Action<InputEvent> OnInputReceived;

    void Start() {
        gameObject.GetComponent<InputManager>().RegisterAdapter(this);
    }

    public void Listen()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnInputReceived?.Invoke(new InputEvent
            {
                InputName = "Left",
                AdapterName = "Laptop",
            });
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnInputReceived?.Invoke(new InputEvent
            {
                InputName = "Right",
                AdapterName = "Laptop",
            });
        }
    }
}
