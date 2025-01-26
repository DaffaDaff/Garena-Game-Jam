using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public SequenceInput sequence;
    private List<IInputAdapter> inputAdapters = new List<IInputAdapter>();
    public int state = 0;

    void Start()
    {
        sequence.inputManager = this;
    }

    public void RegisterAdapter(IInputAdapter adapter)
    {
        inputAdapters.Add(adapter);
        adapter.OnInputReceived += HandleInputEvent;
    }

    public void UnregisterAdapter(IInputAdapter adapter)
    {
        inputAdapters.Remove(adapter);
        adapter.OnInputReceived -= HandleInputEvent;
    }

    private void Update()
    {
        foreach (var adapter in inputAdapters)
        {
            adapter.Listen();
        }
    }

    private void HandleInputEvent(InputEvent inputEvent)
    {
        if (inputEvent.InputName == "Left"){
            if (state == 0){
                // Implement back with this
            }else if (state == 1) {
                state = 0;
                sequence.Reset();
            }else {
                sequence.UpdateString("<");
            }
        }
        else if (inputEvent.InputName == "Right"){
            if (state == 0){
                state = 1;
                sequence.Chant();
            } else if (state == 1){
                sequence.UpdateString(">");
                state = 2;
            } 
            else {
                sequence.UpdateString(">");
            }
        }
        // Dispatch input event to relevant components or systems
        // Debug.Log($"Input Received: {inputEvent.InputName} from {inputEvent.AdapterName}");
    }
}
