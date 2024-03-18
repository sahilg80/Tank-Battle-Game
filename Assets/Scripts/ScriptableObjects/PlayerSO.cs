using Assets.Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObject/PlayerTank")]
public class PlayerSO : ScriptableObject
{
    public float Damage;
    public float MovementSpeed;
    public float RotationSpeed;
    public TankTypes Type;
    public Material Color;
    public float LaunchForce;
}
