using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level9
{
    public interface ICameraState
    {
        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnExit();
    }
    /// <summary>
    /// Behavours of the camera are accessed here
    /// </summary>
    public class CameraBehaviour : MonoBehaviour
    {
        public static CameraBehaviour instance;
        private ICameraState _State;
        private void Awake()
        {
            _State = new Idle(this);
            CreateInstance();
        }

        //create static instance of class
        void CreateInstance()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        void LateUpdate()
        {
            _State.Update();
            if (Input.GetKeyDown(KeyCode.T))
            {
                ShakeCamera(10, 1);
            }
        }

        //switches camera to rumble state
        public void ShakeCamera(float rumbleintensity, float rumbletime)
        {
            SwitchState(new Rumble(this, rumbleintensity, rumbletime));
        }

        // switches camera's state
        public void SwitchState(ICameraState state)
        {
            _State.OnExit();
            _State = state;
            _State.OnEnter();
        }
    }
    /// <summary>
    /// State that does nothing
    /// </summary>
    public class Idle : ICameraState
    {
        CameraBehaviour _Behaviour;

        public Idle(CameraBehaviour behaviour)
        {

        }
        public void OnEnter()
        {

        }
        public void Update()
        {

        }
        public void FixedUpdate()
        {

        }
        public void OnExit()
        {

        }
    }
    /// <summary>
    /// State that makes the camere rumble
    /// </summary>
    public class Rumble : ICameraState
    {
        CameraBehaviour _Camera;
        float _RumbleTime, _RumbleIntensity, _RumbleTick;
        private float _PreRotation;
        public Rumble(CameraBehaviour camera, float rumbleintensity, float rumbletime)
        {
            _Camera = camera;
            _RumbleTime = rumbletime;
            _RumbleIntensity = rumbleintensity;
        }
        public void OnEnter()
        {

        }
        public void Update()
        {
            _RumbleTime -= Time.deltaTime;
            _RumbleIntensity -= Time.deltaTime;

            float newrotation = _RumbleIntensity;

            if (Mathf.Sign(_PreRotation) == Mathf.Sign(newrotation))
            {
                newrotation = -newrotation;
            }

            _Camera.transform.eulerAngles = Vector3.zero + new Vector3(0, 0, newrotation);

            _PreRotation = newrotation;

            if (_RumbleTime <= 0)
            {
                _Camera.SwitchState(new Idle(_Camera));
            }


        }
        public void FixedUpdate()
        {

        }
        public void OnExit()
        {
            _Camera.transform.eulerAngles = Vector3.zero;
        }

        int RandomSign()
        {
            if (Random.Range(0, 2) == 0)
            {
                return -1;
            }
            return 1;
        }
    }
}

