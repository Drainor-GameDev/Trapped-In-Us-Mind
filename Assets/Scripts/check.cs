using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KILLER
{
    public class check : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(checke());
        }
        public IEnumerator checke()
        {
            yield return new WaitForSeconds(0.5f);
            if (GameObject.Find("c").GetComponent<checker>().End && GameObject.Find("c1").GetComponent<checker>().End2 && GameObject.Find("c2").GetComponent<checker>().End3)
            {
                print("win");
            }
        }
    }
}
