using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace KILLER
{
    public class ReadyCS : MonoBehaviour
    {
        public PhotonView pv;
        public Button bt;
        public int place;
        // Start is called before the first frame update
        void Start()
        {
            
            pv = GetComponent<PhotonView>();
            if (pv.isMine)
            {
                bt = GameObject.Find("BT").GetComponent<Button>();
                bt.onClick.AddListener(Ready);
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        void Ready()
        {
            if (pv.isMine)
            {
                if(PhotonNetwork.player.GetScore() == 0)
                {
                    PhotonNetwork.player.SetScore(place);
                }
                else
                {
                    PhotonNetwork.player.SetScore(0);
                }
                print(PhotonNetwork.player.GetScore());
            }
        }
    }
}
