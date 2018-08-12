using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : MonoBehaviour {

    public PlayerBehavior _Player;
    public SmallBlock _Block;
    public float HitlagTime;
    public float _Intensity;

    void Mine()
    {
        _Player.SwitchState(new HitLag(_Player, _Player._State, HitlagTime, _Intensity));
        _Block.MineBlock();
        _Block.SpawnDroppedItem();
    }
}
