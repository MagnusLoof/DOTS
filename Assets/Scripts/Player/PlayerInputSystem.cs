using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class PlayerInputSystem : SystemBase
{
    private PlayerControls PlayerControls;
    private Entity Player;

    protected override void OnCreate()
    {
        PlayerControls = new PlayerControls();
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();
    }

    protected override void OnStartRunning()
    {
        PlayerControls.Player.Enable();
        PlayerControls.Player.Shoot.performed += OnShoot;
        
        Player = SystemAPI.GetSingletonEntity<PlayerTag>();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (!SystemAPI.Exists(Player)) return;
        SystemAPI.SetComponentEnabled<FireProjectileTag>(Player, true);
    }

    protected override void OnUpdate()
    {
        Vector2 moveInput = PlayerControls.Player.Movement.ReadValue<Vector2>();
        SystemAPI.SetSingleton(new PlayerMoveInput{ Value = moveInput});
        float rotationInput = PlayerControls.Player.Rotate.ReadValue<float>();
        SystemAPI.SetSingleton(new PlayerRotationInput{ Value = rotationInput});
    }

    protected override void OnStopRunning()
    {
        PlayerControls.Disable();
        Player = Entity.Null;
    }
}
