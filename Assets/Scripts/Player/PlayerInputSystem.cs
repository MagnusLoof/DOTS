using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class PlayerInputSystem : SystemBase
{
    private PlayerControls PlayerControls;
    public Entity Player;

    protected override void OnCreate()
    {
        PlayerControls = new PlayerControls();
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();
    }

    protected override void OnStartRunning()
    {
        PlayerControls.Player.Movement.Enable();
        
        Player = SystemAPI.GetSingletonEntity<PlayerTag>();
    }

    protected override void OnUpdate()
    {
        Vector2 moveInput = PlayerControls.Player.Movement.ReadValue<Vector2>();
        SystemAPI.SetSingleton(new PlayerMoveInput{ Value = moveInput});
    }

    protected override void OnStopRunning()
    {
        PlayerControls.Disable();
        Player = Entity.Null;
    }
}
