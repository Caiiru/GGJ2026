using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputActionAsset actionAsset;
    

    public InputActionAsset GetInputActionAsset()
    {
        return actionAsset;
    }
}