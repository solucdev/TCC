using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowZoomedImage : MonoBehaviour
{
    public Camera playerCamera;
    public float maxDistance = 5f;
    public GameObject imagePanel; // Painel com a imagem ampliada
    public Sprite zoomedSprite;   // Imagem que será exibida
    public GameObject texto;

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.transform.CompareTag("Zoomable")) // Use uma tag para identificar objetos válidos
                {
                    imageComponent.sprite = zoomedSprite;
                    imagePanel.SetActive(true);
                }
            }
        }

        // Pressione ESC para fechar a imagem
        if (Input.GetKeyDown(KeyCode.G))
        {
            imagePanel.SetActive(false);
        }
    }
}
