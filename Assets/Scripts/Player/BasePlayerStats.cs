using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Player/BaseStats", order = 1)]
public class BasePlayerStats : ScriptableObject
{
    public float _AccelerationMultiplier, _FrictionMultiplier;
    public float _FallForceMultiplier;
    public float _JumpForce, _AirDodgeForce;
    public Vector2 _Velocity, _PreVelocity, _MaxVelocity;
}
