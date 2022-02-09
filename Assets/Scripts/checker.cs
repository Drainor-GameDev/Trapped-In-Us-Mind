using System;
using System.Collections;
using UnityEngine;

namespace KILLER
{
    public class checker : MonoBehaviour
    {
        public int block = 0;
        public GameObject go;
        public bool End, End2,End3,a,b,c;
        public int check;
        public string search;
        // Start is called before the first frame update
        void Start()
        {
            End = false;
            StartCoroutine(Test());
        }
        public IEnumerator Test()
        {
            yield return new WaitForSeconds(1f);
            Search(GameObject.Find(search), GameObject.Find(search));
            StartCoroutine(Test());
            if (End && a)
            {
                a = false;
                GameObject.Find("Player" + PhotonNetwork.player.GetScore()).GetComponent<Mouvment>().Back();
                GameObject.Find("Player" + PhotonNetwork.player.GetScore()).GetComponent<Mouvment>().elec.GetComponent<BoxCollider>().enabled = true;
                print("ayé");
            }
            if (End2 && b)
            {
                b = false;
                GameObject.Find("Player" + PhotonNetwork.player.GetScore()).GetComponent<Mouvment>().Back2();
                GameObject.Find("Player" + PhotonNetwork.player.GetScore()).GetComponent<Mouvment>().elec2.GetComponent<BoxCollider>().enabled = true;
                print("ayé");
            }
            if (End3 && c)
            {
                c = false;
                GameObject.Find("Player" + PhotonNetwork.player.GetScore()).GetComponent<Mouvment>().Back3();
                GameObject.Find("Player" + PhotonNetwork.player.GetScore()).GetComponent<Mouvment>().elec3.GetComponent<BoxCollider>().enabled = true;
                print("ayé");
            }

        }
        public void Search(GameObject GameObj, GameObject Parent)
        {
            foreach (GameObject gameObj in GameObj.GetComponent<LinkScript>().gameO)
            {
                if (gameObj != null &&  gameObj.name != Parent.name && GameObject.Find(search).GetComponent<LinkScript>().isCo)
                {
                    if (Int32.Parse(gameObj.name) == 16)
                    {
                        if (search == "p")
                        {
                            End2 = true;
                        }
                        else if(search == "s")
                        {
                            End3 = true;
                        }
                        else if(search == "1")
                        {
                            End = true;
                        }
                    }
                    else
                    {
                        Search(gameObj, GameObj);
                    }
                }  
            }
        }
    }
}
