
using Assets.Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "ScriptableObject/Enemy")]
public class EnemySO : ScriptableObject
{
    public float Damage;
    public float FireRate;
    public float LaunchForce;
    public float MovementSpeed;
    public float RotationSpeed;
    public EnemyTank Type;
    public Material Color;
}
