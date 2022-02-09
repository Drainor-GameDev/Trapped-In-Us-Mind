using UnityEngine;
using System.Collections;

namespace KILLER
{
    public class finish : MonoBehaviour
    {
        public bool a = true;
        void Start()
        {
            StartCoroutine(check());
        }
        public IEnumerator check()
        {
            yield return new WaitForSeconds(0.5f);
            if(GameObject.Find("c").GetComponent<checker>().End && GameObject.Find("c1").GetComponent<checker>().End2 && GameObject.Find("c2").GetComponent<checker>().End3 && a)
            {
                a = false;
                GameObject.Find("Player" + PhotonNetwork.player.GetScore()).GetComponent<Mouvment>().off();
                GameObject.Find("Door" + PhotonNetwork.player.GetScore()).transform.rotation = new Quaternion(0, 180, 0, 0);

            }
            StartCoroutine(check());
        }
    }
}
