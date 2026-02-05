
using UnityEngine;

public delegate void PlayerEventHandler();

public class Player : BasicEntity
{
    //Basic Entity Set
    private int health = 100;
    // 

    //ID
    [SerializeField] private ID GetIDObject; 
    //

    //Events
    public event PlayerEventHandler OnPlayerMoving;
    public event PlayerEventHandler OnPlayerIdle;
    public event PlayerEventHandler OnPlayerLanguageAttack;
    public event PlayerEventHandler OnPlayerResting;
    //

    //Player Input
    private PlayerAction actions;
    //

    //Get animator component
    [SerializeField] private Animator AnimPlayer;

    //Basic variables
    private GameObject GetCamera;
    protected Vector3 GetCameraPosition = Vector3.zero;
    protected Quaternion GetCameraRotation;
    protected Transform GetPlayerPosition;
    private int ID_Cam;
    protected float T_smooth_follow = 1f;
    protected float OffSetX = 0;
    protected float OffSetY = 1.5f;
    protected float OffSetZ = -5.5f;
    //
    protected Vector3 OffSet()
    {
        return new Vector3(OffSetX, OffSetY, OffSetZ);
    }

    void OnEnable()
    {
        actions.Enable();

        SetCamera();

        print(AnimPlayer);

        GetCameraPosition = new Vector3(-20,2,-20);

        GetCameraRotation = CameraSetComponent.CameraTransformPos;
        GetCameraRotation = Quaternion.Euler(new Vector3(15,0,0));

        if(GetCamera != null)
        {
            GetCamera.transform.position = GetCameraPosition;
            GetCamera.transform.rotation = GetCameraRotation;
        }

        GetPlayerPosition = transform;
        
        OnPlayerMoving += () => SetAnimationWalking(AnimPlayer, true);

        OnPlayerIdle += () => SetAnimationWalking(AnimPlayer, false);
    }

    void OnDisable()
    {
        actions.Disable();
    }

    protected override void Awake()
    {
        base.Awake();

        actions = new PlayerAction();

        ID_ENTITY = GetIDObject.ID_;
    }

    void LateUpdate()
    {
        FollowCamera();
        PlayerMoving(4);        
    }

    #region Camera & Player

    private void SetCamera()
    {
        ID_Cam = Random.Range(0,10000);

        GetCamera = new GameObject("Cam Player: " + ID_Cam.ToString());
        GetCamera.AddComponent<Camera>();

        //disable main camera in the scene
        if(Camera.main.gameObject != null) Camera.main.gameObject.SetActive(false);
        //

        if(GetCamera.GetComponent<Camera>() != null)
        {
            GetCamera.tag = "MainCamera";     
        }
    }

    private void FollowCamera()
    {
        Vector3 desiredPosition = new Vector3(GetPlayerPosition.position.x,transform.position.y, transform.position.z);

        Vector3 smoothMove = Vector3.Lerp(transform.position, desiredPosition, T_smooth_follow * Time.deltaTime);

        GetCamera.transform.position = smoothMove + OffSet();
    }

    private Vector2 PlayerMoving(int speed)
    {
        float MovX = actions.Player_moving.MovementX.ReadValue<float>();
        float MovZ = actions.Player_moving.MovementZ.ReadValue<float>();
        Vector3 Moving = new Vector3(MovX,0,MovZ);

        transform.Translate(Moving.normalized * speed * Time.deltaTime, Space.World);

        if(Moving.sqrMagnitude > 0.01f)
        {
            OnPlayerMoving?.Invoke();
        }
        else
        {
            OnPlayerIdle?.Invoke();
        }

        return Moving;
    }

    #endregion
}