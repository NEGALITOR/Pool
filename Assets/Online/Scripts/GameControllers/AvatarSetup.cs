using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AvatarSetup : MonoBehaviour
{
    private PhotonView PV;
    public int characterValue;
    public GameObject myCharacter;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
        {
            AddCharacter(PlayerInfo.PI.mySelectedCharacter);
        }
    }

    void AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;
        myCharacter = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", PlayerInfo.PI.allCharacters[whichCharacter]), transform.position, transform.rotation);
        myCharacter.transform.parent = gameObject.transform;
        anim = myCharacter.GetComponent<Animator>();
        Debug.Log(myCharacter);

    }
}
