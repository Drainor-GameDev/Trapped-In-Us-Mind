using System.Collections;
using UnityEngine;

namespace KILLER
{
    public class Mouvment : MonoBehaviour
    {
        public GameObject camO,camT, camP, camP2, camP3, trap1, au1, au2, au3, au4, bloodEffect, killCam,killcam1,killcam2,killcam3, animcam1,animcam2,elec, elec2,elec3;
        public CharacterController controller;
        PhotonView pv;
        public int speed,bspeed, place;
        public Transform cam;
        public Animator anim, helanim;
        public float turnSmoothTime = 0.1f;
        public bool canMove = true,win = false;

        float turnSmoothVelocity;
        private Vector3 velocity;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.visible = false;
            pv = GetComponent<PhotonView>();
            controller = GetComponent<CharacterController>();
            cam = GameObject.Find("Main Camera").GetComponent<Transform>();
            anim = GetComponent<Animator>();
            if (pv.isMine)
            {
                camO.SetActive(true);
                gameObject.name = "Player" + PhotonNetwork.player.GetScore();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (pv.isMine)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = bspeed * 2;
                    anim.SetBool("run", true);
                }
                else
                {
                    speed = bspeed;
                    anim.SetBool("run", false);
                }
                if (Input.GetKey(KeyCode.Z) && canMove)
                {
                    //transform.Translate(Vector3.forward * Time.deltaTime * speed);
                    anim.SetBool("walk", true);
                }
                else if (Input.GetKey(KeyCode.S) && canMove)
                {
                    //transform.Translate(-Vector3.forward * Time.deltaTime * speed);
                    anim.SetBool("walk", true);
                }
                else if (Input.GetKey(KeyCode.Q) && canMove)
                {
                    //transform.Translate(-Vector3.right * Time.deltaTime * speed);
                    anim.SetBool("walk", true);
                }
                else if (Input.GetKey(KeyCode.D) && canMove)
                {
                    //transform.Translate(Vector3.right * Time.deltaTime * speed);
                    anim.SetBool("walk", true);
                }
                else
                {
                    anim.SetBool("walk", false);
                }
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                if (canMove)
                {
                    velocity.y += -5.81f * Time.deltaTime;
                    controller.Move(velocity * Time.deltaTime);
                }
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
                if (direction.magnitude >= 0.1f && canMove)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);
                }
            }
            
        }
        public void OnTriggerStay(Collider other)
        {
            if (other.transform.tag == "Door")
            {
                if (Input.GetKeyDown(KeyCode.E) && pv.isMine)
                {
                    anim.SetBool("open", true);
                    pv.RPC("Open", PhotonTargets.AllBuffered, other.name);
                }
            }
            if (other.transform.tag == "Box")
            {
                if (Input.GetKeyDown(KeyCode.E) && pv.isMine)
                {

                    canMove = false;
                    Cursor.visible = true;
                    if(other.name == elec.name)
                    {
                        transform.position = GameObject.Find("NewPos3" + (int)PhotonNetwork.player.CustomProperties["ID"]).transform.position;
                        camP.SetActive(true);
                    }
                    else if (other.name == elec2.name)
                    {
                        transform.position = GameObject.Find("NewPos1" + (int)PhotonNetwork.player.CustomProperties["ID"]).transform.position;
                        camP2.SetActive(true);
                    }
                    else if (other.name == elec3.name)
                    {
                        transform.position = GameObject.Find("NewPos2" + (int)PhotonNetwork.player.CustomProperties["ID"]).transform.position;
                        camP3.SetActive(true);
                    }

                    camO.SetActive(false);
                }
            }
        }

        [PunRPC]
        public void Open(string nm)
        {
            GameObject.Find(nm).GetComponent<OpenDoor>().Open();
        }
        public void CantMove()
        {
            canMove = false;
        }
        public void CanMove()
        {
            canMove = true;
            anim.SetBool("open", false);
        }
        public void off()
        {
            win = true;
            anim.SetBool("off", true);
            canMove = false;
            transform.position = GameObject.Find("Manager").GetComponent<NetworkManager>().spawn[PhotonNetwork.player.GetScore() - 1].transform.position;
            killcam1.SetActive(true);
        }
        public void ActiveTrap1()
        {
            trap1.SetActive(true);
            elec = GameObject.Find("ElectricBox" + PhotonNetwork.player.GetScore());
            elec2 = GameObject.Find("ElectricBox1" + PhotonNetwork.player.GetScore());
            elec3 = GameObject.Find("ElectricBox222" + PhotonNetwork.player.GetScore());
            camP = GameObject.Find("CamPanel" + PhotonNetwork.player.GetScore());
            camP2 = GameObject.Find("CamPanel1" + PhotonNetwork.player.GetScore());
            camP3 = GameObject.Find("CamPanel2" + PhotonNetwork.player.GetScore());
            animcam1 = GameObject.Find("anim1" + PhotonNetwork.player.GetScore());
            animcam2 = GameObject.Find("anim2" + PhotonNetwork.player.GetScore());
            foreach(GameObject go in GameObject.Find("Manager").GetComponent<NetworkManager>().cams)
            {
                go.SetActive(false);
            }
            StartCoroutine(Cine());
        }
        public IEnumerator Cine()
        {
            
            elec2.SetActive(true);
            elec.SetActive(true);
            elec3.SetActive(true);
            anim.SetBool("panic", true);
            canMove = false;
            animcam1.SetActive(true);
            animcam2.SetActive(false);
            yield return new WaitForSeconds(15f);
            animcam1.SetActive(false);
            animcam2.SetActive(true);
            yield return new WaitForSeconds(4f);
            animcam2.SetActive(false);
            killCam.SetActive(true);
            yield return new WaitForSeconds(3f);
            killCam.SetActive(false);
            animcam1.SetActive(true);
            yield return new WaitForSeconds(10f);
            animcam1.SetActive(false);
            anim.SetBool("panic", false);
            StartCoroutine(Explose());
            canMove = true;
        }
        public IEnumerator Explose()
        {

            au1.SetActive(true);
            yield return new WaitForSeconds(60f);
            if (!win)
            {
                canMove = false;
                anim.SetBool("panic", true);
                au1.SetActive(false);
                au2.SetActive(true);
                camO.SetActive(false);
                killCam.SetActive(true);
                yield return new WaitForSeconds(3f);
                canMove = false;
                killCam.SetActive(false);
                killcam1.SetActive(true);
                yield return new WaitForSeconds(1f);
                canMove = false;
                killcam1.SetActive(false);
                killcam2.SetActive(true);
                yield return new WaitForSeconds(1f);
                canMove = false;
                killcam2.SetActive(false);
                killcam3.SetActive(true);
                yield return new WaitForSeconds(2f);
                canMove = false;
                au4.SetActive(false);
                anim.SetBool("act", true);
                bloodEffect.SetActive(true);
                anim.SetBool("die", true);
                au3.SetActive(true);
                yield return new WaitForSeconds(2f);
                au2.SetActive(false);
                au3.SetActive(false);
            }
        }
        public void Back()
        {
            camP.SetActive(false);
            camO.SetActive(true);
            canMove = true;
            Cursor.visible = false;
        }
        public void Back2()
        {
            camP2.SetActive(false);
            camO.SetActive(true);
            canMove = true;
            Cursor.visible = false;
        }
        public void Back3()
        {
            camP3.SetActive(false);
            camO.SetActive(true);
            canMove = true;
            Cursor.visible = false;
        }
        public void ActBear()
        {
            anim.SetBool("act", true);
            au1.SetActive(false);
            anim.SetBool("off", false);
        }
        public void DisBear()
        {
            trap1.SetActive(false);
            canMove = true;
            killcam1.SetActive(false);
        }
    }
}
