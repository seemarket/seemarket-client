using UnityEngine;

namespace test
{
    public class CameraScript : MonoBehaviour
    {
        static WebCamTexture backcam;
        // Start is called before the first frame update
        void Start()
        {
            if(backcam == null) {
                backcam = new WebCamTexture();
            }

            GetComponent<Renderer>().material.mainTexture = backcam;

            if(!backcam.isPlaying){
                backcam.Play();
            }
        }
    }
}
