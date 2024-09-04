using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class WeaponInputSystem : SystemBase
{
    private PlayerControls PlayerControls;
    private Entity Weapon;

    protected override void OnCreate()
    {
        PlayerControls = new PlayerControls();
        RequireForUpdate<WeaponTag>();
        RequireForUpdate<WeaponRotationInput>();
    }

    protected override void OnStartRunning()
    {
        PlayerControls.Player.Rotate.Enable();
        PlayerControls.Player.Shoot.Enable();
        PlayerControls.Player.Shoot.performed += OnShoot;
        
        Weapon = SystemAPI.GetSingletonEntity<WeaponTag>();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (!SystemAPI.Exists(Weapon)) return;
        SystemAPI.SetComponentEnabled<FireProjectileTag>(Weapon, true);
    }

    protected override void OnUpdate()
    {
        float rotationInput = PlayerControls.Player.Rotate.ReadValue<float>();
        SystemAPI.SetSingleton(new WeaponRotationInput{ Value = rotationInput});
    }

    protected override void OnStopRunning()
    {
        PlayerControls.Disable();
        Weapon = Entity.Null;
    }
}