using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace KILLER
{
    public class NetworkManager : MonoBehaviour
    {
        public GameObject Pref;
        public GameObject[] spawn, cams;
        public bool isLobby = false, canStart = false;
        public TMPro.TextMeshProUGUI txtP,txtR;
        // Start is called before the first frame update
        void Start()
        {
            
            if (isLobby)
            {
                Hashtable hash = new Hashtable();
                hash.Add("Ready", false);
                hash.Add("Loaded", false);
                hash.Add("Forced", false);
                hash.Add("ID", PhotonNetwork.room.PlayerCount);
                PhotonNetwork.player.SetCustomProperties(hash);
                GameObject go = PhotonNetwork.Instantiate(Pref.name, spawn[PhotonNetwork.room.PlayerCount - 1].transform.position, spawn[PhotonNetwork.room.PlayerCount - 1].transform.rotation, 0);
                go.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = PhotonNetwork.playerName;
                GameObject.Find("PlayerPrefab 1(Clone)").GetComponent<ReadyCS>().place = PhotonNetwork.room.PlayerCount;
            }
            else
            {
                GameObject go = PhotonNetwork.Instantiate(Pref.name, spawn[(int)PhotonNetwork.player.CustomProperties["ID"] - 1].transform.position, spawn[(int)PhotonNetwork.player.CustomProperties["ID"] - 1].transform.rotation, 0);
                go.GetComponent<Mouvment>().ActiveTrap1();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isLobby)
            {
                txtP.text = PhotonNetwork.room.PlayerCount + "Player on " + PhotonNetwork.room.MaxPlayers;
                txtR.text = PhotonNetwork.room.Name;
                foreach (PhotonPlayer pl in PhotonNetwork.playerList)
                {
                    print(pl.GetScore());
                    if (pl.GetScore() == 0 || PhotonNetwork.room.MaxPlayers != PhotonNetwork.room.PlayerCount)
                    {
                        canStart = false;
                        break;
                    }
                    else
                    {
                        canStart = true;
                    }

                }
                if (canStart)
                {
                    StartGame();
                }
            }
        }
        public void StartGame()
        {
            PhotonNetwork.LoadLevel("1");
        }
    }
}
