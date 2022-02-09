using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KILLER
{
    public class OpenDoor : MonoBehaviour
    {
        public void Open()
        {
            GetComponent<Animator>().SetBool("Open", true);
        }
    }
}
