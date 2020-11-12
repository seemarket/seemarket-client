using UnityEngine.UI;

namespace DefaultNamespace.ARImage
{
    public class ARImageControl: UICanvasBase
    {

        public Text imageText;

        private ARImageLinker _linker;
        void Start()
        {
            _linker = new ARImageLinker();
            _linker.setupDatabase();
        }


        public void setUpId(string id)
        {
            string name = _linker.getname(id);
            imageText.text = name;
        }
            
    }
}