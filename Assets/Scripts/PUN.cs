using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KILLER
{
    public class PUN : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI txtstate,txtplayer;
        public Slider slide;
        public TMPro.TMP_InputField Pname, Rname;
        // Start is called before the first frame update
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings("V1");
        }

        // Update is called once per frame
        void Update()
        {
            txtstate.text = PhotonNetwork.connectionStateDetailed.ToString();
            txtplayer.text = slide.value.ToString();
        }
        public void Join()
        {
            PhotonNetwork.playerName = Pname.text;
            RoomOptions opt = new RoomOptions();
            opt.MaxPlayers = (byte)Convert.ToInt32(slide.value);
            opt.IsVisible = true;
            print(opt.MaxPlayers);
            PhotonNetwork.JoinOrCreateRoom(Rname.text, opt, TypedLobby.Default);
        }
        void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("lobby");
            print("yass");
        }
        void OnPhotonJoinRoomFailed()
        {
            print("nope");
        }
    }
}
