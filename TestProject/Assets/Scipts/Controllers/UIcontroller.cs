using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour {
    public GameObject spawnController;
    public Sprite[] sprites;
    public Image[] images;

    

    private SpawnController controller;
    private List<obeliskTypes> orderList;

	void Start () {
        controller = spawnController.GetComponent<SpawnController>();
        orderList = controller.getOrderList();
        setImages();
	}

    void setImages()
    {
        for(int i = 0; i < orderList.Count; ++i)
        {
            images[i].sprite = sprites[(int)orderList[i]];
        }
    }
}
