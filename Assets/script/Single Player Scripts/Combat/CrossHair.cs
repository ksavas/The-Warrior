using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {

    [SerializeField]
    float speed;

    public Transform Reticle;
    Transform crossTop;
    Transform crossBottom;
    Transform crossLeft;
    Transform crossRight;

    float reticleStartPoint;

    void Awake()
    {
        Reticle = GameObject.Find("Canvas").transform.Find("Recticle").transform;
    }
    void Start()
    {
        crossTop = Reticle.Find("Cross").transform.Find("Top");
        crossBottom = Reticle.Find("Cross").transform.Find("Bottom");
        crossLeft = Reticle.Find("Cross").transform.Find("Left");
        crossRight = Reticle.Find("Cross").transform.Find("Right");

        reticleStartPoint = crossTop.localPosition.y;
    }

    void SetVisibility(bool value)
    {
        Reticle.gameObject.SetActive(value);
    }

    void Update()
    {
        SetVisibility(false);
        if (SecondGameManager.Instance.InputController.fire2)
        {
            SetVisibility(true);
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            Reticle.transform.position = Vector3.Lerp(Reticle.transform.position, screenPosition, speed * Time.deltaTime);
        }

     }

    public void ApplyScale(float scale)
    {
        crossTop.localPosition = new Vector3(0, reticleStartPoint + scale, 0);
        crossBottom.localPosition = new Vector3(0, -reticleStartPoint - scale, 0);
        crossLeft.localPosition = new Vector3(-reticleStartPoint - scale, 0, 0);
        crossRight.localPosition = new Vector3(+reticleStartPoint + scale, 0, 0);

    }

}
