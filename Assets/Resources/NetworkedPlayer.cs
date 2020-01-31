using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;

public class NetworkedPlayer : MonoBehaviourPunCallbacks
{
    public static GameObject localPlayerInstance;
    //public GameObject playerNamePrebaf;
    public string playerNamePrefab;
    public Rigidbody2D rb;
    public SpriteRenderer playerMesh;
}
