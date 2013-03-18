using UnityEngine;
using System.Collections;
public enum ESpawnType
{
    Coin,
    Enemy
}

public abstract class SpawnInterface : MonoBehaviour
{
    public int Id = 0;
    public ESpawnType SpawnType;

}
