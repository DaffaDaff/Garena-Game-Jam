using System;

public interface IInputAdapter
{
    void Listen();
    event Action<InputEvent> OnInputReceived;
}
