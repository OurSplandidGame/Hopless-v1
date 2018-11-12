using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    public GameObject front;
    public GameObject back;
    public float duration = 60;

    private ItemData item;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 1, 0), 360*Time.deltaTime);
        duration -= Time.deltaTime;
        if(duration <= 0)
        {
            duration = 0;
            Destroy(gameObject);
        }
	}

    public void updateItemInfo(ItemData newItem)
    {
        item = newItem;
        front.GetComponent<Renderer>().material.mainTexture = item.ItemIcon.texture;
        back.GetComponent<Renderer>().material.mainTexture = item.ItemIcon.texture;
        front.GetComponent<Renderer>().material.SetTexture("_EmissionMap", item.ItemIcon.texture);
        back.GetComponent<Renderer>().material.SetTexture("_EmissionMap", item.ItemIcon.texture);
    }

    public ItemData getItem()
    {
        return this.item;
    }


}
