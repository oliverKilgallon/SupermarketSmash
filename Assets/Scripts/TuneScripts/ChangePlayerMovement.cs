using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerMovement : MonoBehaviour
{
    public InputField baseMovementInput;
    public InputField baseTurnInput;
    public InputField angularMaxInput;
    public InputField angularDecayRate;
    public InputField linearDragInput;
    public InputField brakeStrength;
    public InputField cameraRotSpeed;

    public Rigidbody playerRigidbody;
    public CameraFollow playerCamera;

    MovementTest playerMoveScript;
    // Start is called before the first frame update
    void Start()
    {
        playerMoveScript = GetComponent<MovementTest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBaseMoveMagnitude()
    {
        playerMoveScript.baseMoveMagnitude = float.Parse(baseMovementInput.text);
    }

    public void ChangeTurnMovement()
    {
        playerMoveScript.baseTurnMagnitude = float.Parse(baseTurnInput.text);
    }

    public void ChangeMaxAngularVel()
    {
        playerMoveScript.angularVelocityMaxMagnitude = float.Parse(angularMaxInput.text);
    }

    public void ChangeLinearDrag()
    {
        playerRigidbody.drag = float.Parse(linearDragInput.text);
    }

    public void ChangeAngularDecay()
    {
        playerMoveScript.angularVelocityDecayRate = float.Parse(angularDecayRate.text);
    }

    public void ChangeBrakeStrength()
    {
        playerMoveScript.baseBrakeMagnitude = float.Parse(brakeStrength.text);
    }

    public void ChangeCameraRotSpeed()
    {
        playerCamera.rotSpeed = float.Parse(cameraRotSpeed.text);
    }
}
