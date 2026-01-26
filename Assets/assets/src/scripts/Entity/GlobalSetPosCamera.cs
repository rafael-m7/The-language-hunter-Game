using UnityEngine;


/// <summary>
/// Global values to all things/sprites in game(Rotation whatever)
/// </summary>
public struct FrontCameraSetComponent
{
    public Quaternion SetRotationGlobal;
}

public struct CameraSetComponent
{
    public static Quaternion CameraTransformPos;
}

public class GlobalSetPosCamera : MonoBehaviour
{
    private FrontCameraSetComponent frontCaneraSetComponent;

    void OnEnable()
    {
        frontCaneraSetComponent = new FrontCameraSetComponent();
        
        frontCaneraSetComponent.SetRotationGlobal = Quaternion.Euler(new Vector3(15,0,0));

        transform.rotation = frontCaneraSetComponent.SetRotationGlobal;
    }
}
