using Assets.CodeBase.Infrastructure.Services;
using UnityEngine;

public class StandaloneInputService : InputService
{
    public override bool JumpButtonPressed() =>
                        Input.GetKeyDown(KeyCode.Space);

        
    

    public override void Move(Transform transform, float speedModifier)
    {
       float _horizontal = Input.GetAxis("Horizontal");
       speedModifier = 30f;
       transform.Translate(new Vector3(1,0,0) * _horizontal * speedModifier * Time.deltaTime); 

    }

    public override bool SlideButtonPressed() =>
        Input.GetKeyDown(KeyCode.S);

    public override bool StartButtonPressed() =>
        Input.GetKeyDown(KeyCode.W);
    
    
}
