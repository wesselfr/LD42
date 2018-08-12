using UnityEngine;


public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    //Inputs
    public bool _JumpButtonUp, _JumpButtonDown, _JumpButtonHeld;
    public bool _UpButtonUp, _UpButtonDown, _UpButtonHeld;
    public bool _DownButtonUp, _DownButtonDown, _DownButtonHeld;
    public bool _LeftButtonUp, _LeftButtonDown, _LeftButtonHeld;
    public bool _RightButtonUp, _RightButtonDown, _RightButtonHeld;
    public bool _MineButtonUp, _MineButtonDown, _MineButtonHeld;

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

    public int HorizontalDirection()
    {
        int dir = 0;

        if (_LeftButtonDown || _LeftButtonHeld)
        {
            if (dir != -1)
                dir -= 1;
        }
        if (_RightButtonDown || _RightButtonHeld)
        {
            if (dir != 1)
                dir += 1;
        }
        if (_LeftButtonUp)
        {
            if (dir == -1)
                dir += 1;
        }
        if (_RightButtonUp)
        {
            if (dir == 1)
                dir -= 1;
        }

        return dir;
    }

    public int VericalDirection()
    {
        int dir = 0;

        if (_DownButtonDown || _DownButtonHeld)
        {
            if (dir != -1)
                dir -= 1;
        }
        else if (_UpButtonDown || _UpButtonHeld)
        {
            if (dir != 1)
                dir += 1;
        }
        else if (_DownButtonUp)
        {
            if (dir == -1)
                dir += 1;
        }
        else if (_UpButtonUp)
        {
            if (dir == 1)
                dir -= 1;
        }

        return dir;
    }


    private void GetInput()
    {
            //jump
            _JumpButtonUp = Input.GetKeyUp(KeyCode.Space);
            _JumpButtonDown = Input.GetKeyDown(KeyCode.Space);
            _JumpButtonHeld = Input.GetKey(KeyCode.Space);

            // up/jump
            _UpButtonUp = Input.GetKeyUp(KeyCode.W);
            _UpButtonDown = Input.GetKeyDown(KeyCode.W);
            _UpButtonHeld = Input.GetKey(KeyCode.W);

            //down
            _DownButtonUp = Input.GetKeyUp(KeyCode.S);
            _DownButtonDown = Input.GetKeyDown(KeyCode.S);
            _DownButtonHeld = Input.GetKey(KeyCode.S);

            //left
            _LeftButtonUp = Input.GetKeyUp(KeyCode.A);
            _LeftButtonDown = Input.GetKeyDown(KeyCode.A);
            _LeftButtonHeld = Input.GetKey(KeyCode.A);

            //left
            _LeftButtonUp = Input.GetKeyUp(KeyCode.A);
            _LeftButtonDown = Input.GetKeyDown(KeyCode.A);
            _LeftButtonHeld = Input.GetKey(KeyCode.A);

            //right
            _RightButtonUp = Input.GetKeyUp(KeyCode.D);
            _RightButtonDown = Input.GetKeyDown(KeyCode.D);
            _RightButtonHeld = Input.GetKey(KeyCode.D);

            //mine
            _MineButtonUp = Input.GetMouseButtonUp(0);
            _MineButtonDown = Input.GetMouseButtonDown(0);
            _MineButtonHeld = Input.GetMouseButton(0);
    }

    public Vector3 GetMousePosition2DWorldSpace()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        return mousePosition;
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
