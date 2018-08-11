using UnityEngine;
using XboxCtrlrInput;

public class InputManager : MonoBehaviour
{

    public static InputManager instance;
    [SerializeField] private XboxController controller;
    [SerializeField] private bool _IsUsingController;

    //Inputs
    public bool _JumpButtonUp, _JumpButtonDown, _JumpButtonHeld;
    public bool _PunchButtonUp, _PunchButtonDown, _PunchButtonHeld;
    public bool _KickButtonUp, _KickButtonDown, _KickButtonHeld;
    public float _LStickX, _LStickY, _LTrigger;

    private void Awake()
    {
        CreateInstance();
    }

    void Update()
    {
        GetInput();
    }

    private void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void GetInput()
    {
        if (XCI.IsPluggedIn(controller))
        {
            _LStickX = XCI.GetAxisRaw(XboxAxis.LeftStickX, controller);
            _LStickY = XCI.GetAxisRaw(XboxAxis.LeftStickY, controller);

            _LTrigger = XCI.GetAxisRaw(XboxAxis.LeftTrigger, controller);

            _JumpButtonUp = XCI.GetButtonUp(XboxButton.Y, controller);
            _JumpButtonDown = XCI.GetButtonDown(XboxButton.Y, controller);
            _JumpButtonHeld = XCI.GetButton(XboxButton.Y, controller);

            _PunchButtonUp = XCI.GetButtonUp(XboxButton.X, controller);
            _PunchButtonDown = XCI.GetButtonDown(XboxButton.X, controller);
            _PunchButtonHeld = XCI.GetButton(XboxButton.X, controller);

            _KickButtonUp = XCI.GetButtonUp(XboxButton.A, controller);
            _KickButtonDown = XCI.GetButtonDown(XboxButton.A, controller);
            _KickButtonHeld = XCI.GetButton(XboxButton.A, controller);
        }
        else
        {
            _LTrigger = BoolToInt(Input.GetKey(KeyCode.O));

            _LStickX = Input.GetAxisRaw("Horizontal");
            _LStickY = Input.GetAxisRaw("Vertical");

            _JumpButtonUp = Input.GetKeyUp(KeyCode.I);
            _JumpButtonDown = Input.GetKeyDown(KeyCode.I);
            _JumpButtonHeld = Input.GetKey(KeyCode.I);

            _PunchButtonUp = Input.GetKeyUp(KeyCode.J);
            _PunchButtonDown = Input.GetKeyDown(KeyCode.J);
            _PunchButtonHeld = Input.GetKey(KeyCode.J);

            _KickButtonUp = Input.GetKeyUp(KeyCode.K);
            _KickButtonDown = Input.GetKeyDown(KeyCode.K);
            _KickButtonHeld = Input.GetKey(KeyCode.K);
        }
    }

    private int BoolToInt(bool boel)
    {
        if (boel == true)
        {
            return 1;
        }
        if (boel == false)
        {
            return 0;
        }

        return 1;
    }
}
