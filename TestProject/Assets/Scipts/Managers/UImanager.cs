using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour {
    public Sprite[] sprites;
    public Image[] images;

    private List<InteractableObjectType> imageOrderList;

	void Start () {
        imageOrderList = GameManager.instance.GetOrderList();
        SetImages();
	}

    void SetImages() //размещение изображений в порядке их активации
    {
        for(int i = 0; i < imageOrderList.Count; ++i)
        {
            images[i].sprite = sprites[(int)imageOrderList[i]];
        }
    }
}
