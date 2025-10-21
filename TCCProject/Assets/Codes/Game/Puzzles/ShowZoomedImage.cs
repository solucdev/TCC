using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowZoomedImage : MonoBehaviour
{
    public Camera playerCamera;
    public float maxDistance = 5f;
    public GameObject imagePanel; 
    public Sprite zoomedSprite;   
    public GameObject texto;
    public GameObject fechar;

    private Image imageComponent;

    void Start()
    {
        imageComponent = imagePanel.GetComponentInChildren<Image>();
        imagePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        texto.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        texto.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            //imagePanel.SetActive(!isActive);

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                
                if (hit.transform.CompareTag("Zoomable")) 
                {
                    bool isActive = imagePanel.activeSelf;

                    if (!isActive)
                    {
                    Objetivos.Instance.SetObjective("Siga as orientações da carta");
                    imageComponent.sprite = zoomedSprite;
                    imagePanel.SetActive(true);
                    fechar.SetActive(true);
                    }
                    else
                    {
                    imagePanel.SetActive(false);
                    fechar.SetActive(false);
                    }
                    
                }
            }
        }
    }
}
