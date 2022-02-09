using UnityEngine;

namespace KILLER
{
    public class LinkConnect : MonoBehaviour
    {
        public bool isConnect, canCo;
        public int place = 0;
        public GameObject go, parent;
        private void OnTriggerStay(Collider other)
        {
            if (other.tag != "Box")
            {
                go = other.gameObject;
                if (other.tag == "Enter")
                {
                    GetComponentInParent<LinkScript>().isCo = true;
                }
                else if (other.tag =="Link")
                {
                    GetComponentInParent<LinkScript>().gameO[place] = go.GetComponent<LinkConnect>().parent;
                }
            }
        }
        public void OnTriggerExit(Collider other)
        {
            GetComponentInParent<LinkScript>().gameO[place] = null;
            go = null;
            if (other.tag == "Enter")
            {
                GetComponentInParent<LinkScript>().isCo = false;
            }
        }
    }
}
