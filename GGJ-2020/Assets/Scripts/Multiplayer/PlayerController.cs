using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    [HideInInspector]
    public int id;

    [Header("Info")]
    public float moveSpeed;
    public float jumpForce;
    public GameObject hatObject;

    [HideInInspector]
    public float curHatTime;

    [Header("Components")]
    public Rigidbody rig;
    public MeshRenderer meshRenderer;
    public PhotonView photonView;
    public Player photonPlayer;

    // called when the player object is instantiated
    [PunRPC]
    public void Initialize (Player player)
    {
        photonPlayer = player;
        id = player.ActorNumber;

        GameManager.instance.players[id - 1] = this;

        if(id == 1)
            GameManager.instance.GiveHat(id, true);

        // if this isn't our local player, disable physics as that's
        // controlled by the user and synced to all other clients
        if(!photonView.IsMine)
            rig.isKinematic = true;
    }

    void Update ()
    {
        // the host will check if the player has won
        if(PhotonNetwork.IsMasterClient)
        {
            if(curHatTime >= GameManager.instance.timeToWin && !GameManager.instance.gameEnded)
            {
                GameManager.instance.gameEnded = true;
                GameManager.instance.photonView.RPC("WinGame", RpcTarget.All, id);
            }
        }

        // only the client of the local player can control it
        if(photonView.IsMine)
        {

            // track the amount of time we're wearing the hat
            //if (hatObject.activeInHierarchy)
            //    curHatTime += Time.deltaTime;
        }
    }

    // sets the player color to default or tagged
    public void SetHat (bool hasHat)
    {
        hatObject.SetActive(hasHat);
    }

    void OnCollisionEnter (Collision col)
    {
        // only the client controlling this player will check for collisions
        // client based collision detection
        if(!photonView.IsMine)
            return;
        
        // did we hit another player?
        if(col.gameObject.CompareTag("Player"))
        {
            // do they have the hat?
            if(GameManager.instance.GetPlayer(col.gameObject).id == GameManager.instance.playerWithHat)
            {
                // can we get the hat?
                if(GameManager.instance.CanGetHat())
                {
                    // give us the hat
                    GameManager.instance.photonView.RPC("GiveHat", RpcTarget.All, id, false);
                }
            }
        }
    }

    public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
    {
        // we want to sync the 'curHatTime' between all clients
        if(stream.IsWriting)
        {
            stream.SendNext(curHatTime);
        }
        else if(stream.IsReading)
        {
            curHatTime = (float)stream.ReceiveNext();
        }
    }

    //#region RayCasting

    //// make a capsule cast to check weather there is an obstavle in front of the player ONLY when jumping.
    //public bool AllowMovement(Vector3 castDirection)
    //{
    //    Vector3 point1;
    //    Vector3 point2;
    //    if (!IsGrounded())
    //    {
    //        // The distance from the bottom and top of the capsule
    //        distanceToPoints = _capsule.height / 2 - _capsule.radius;
    //        /*Top and bottom capsule points respectively, transform.position is used to get points relative to 
    //           local space of the capsule. */
    //        point1 = transform.position + _capsule.center + Vector3.up * distanceToPoints;
    //        point2 = transform.position + _capsule.center + Vector3.down * distanceToPoints;
    //        float radius = _capsule.radius * .95f;
    //        float capsuleCastDist = 0.1f;

    //        if (Physics.CapsuleCast(point1, point2, radius, castDirection, capsuleCastDist))
    //        {
    //            return false;
    //        }
    //    }
    //    if (slopLimitEnabled && IsGrounded())
    //    {
    //        float castDist = _capsule.height;
    //        RaycastHit hit;
    //        if (Physics.Raycast(transform.position + _capsule.center, Vector3.down, out hit, castDist)
    //            && IsGrounded())
    //        {
    //            float currentsSlope = Vector3.Angle(hit.normal, transform.forward) - 90.0f;
    //            if (currentsSlope > slopeLimit)
    //            {
    //                canJump = false;
    //                return false;
    //            }
    //        }
    //    }
    //    canJump = true;
    //    return true;
    //}

    //#endregion
}