using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerUI : MonoBehaviour {

    public Vector3 charOffset = new Vector3(0.0f, 3.0f, 0.0f);
    public Text nameLabel;
    public Slider playerHP;

    Player target;
    float charHeight;
    Transform targetTramsform;
    Vector3 targetPos;

	void Start () {
        this.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }


		
	}

    void Chese()
    {
        if (targetTramsform != null)
        {
            targetPos = targetTramsform.position;
            targetPos.y += charHeight;
            this.transform.position = Camera.main.WorldToScreenPoint(targetPos) + charOffset;
        }
    }

    public void SetTarget(Player targetPlayer)
    {
        if (target == null)
        {
            Debug.LogError("Error");
            return;
        }
        target = targetPlayer;
        targetTramsform = targetPlayer.GetComponent<Transform>();

        CharacterController cc = target.GetComponent<CharacterController>();

        if (cc != null)
        {
            charHeight = cc.height;
        }
        if (nameLabel != null)
        {
        }
    }

}
