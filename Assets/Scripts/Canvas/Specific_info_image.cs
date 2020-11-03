
using UnityEngine;
public Button pb;
public Sprite newSprite;

public class Specific_info_image : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        pb.image.sprite = newSprite;
    }

    void Update()
    {
        
    }
}
