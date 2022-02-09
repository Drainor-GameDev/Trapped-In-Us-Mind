using UnityEngine;

namespace KILLER
{
    public class LinkScript : MonoBehaviour
    {
        public bool isConnect = false, isCo, masterCo, isElec, u,d,l,r;
        public GameObject[] gameO,gameList;
        public int place = 0, connectPlace,order = 0;
        public void OnMouseDown()
        {
            if (!isElec)
            {
                transform.Rotate(Vector3.up * 90);
            }
                
        }
        // Start is called before the first frame update
        void Start()
        {
            if (!isElec)
            {
                transform.Rotate(Vector3.up * Random.Range(0, 4) * 90);
            }
                
        }

    }
}
