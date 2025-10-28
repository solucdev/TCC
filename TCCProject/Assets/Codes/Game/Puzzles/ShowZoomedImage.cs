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
        texto.SetActive(false);
    }


    void Update()
    {
        
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

        //imagePanel.SetActive(!isActive);


        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // Verifica se o jogador está olhando para o objeto com a tag "Zoomable"
            if (hit.transform.CompareTag("Zoomable"))
            {
                texto.SetActive(true); // Ativa o texto

                if (Input.GetKeyDown(KeyCode.E))
                {
                    bool isActive = imagePanel.activeSelf;

                    if (!isActive && UIManager.Instance != null && UIManager.Instance.IsAnyUIOpen && !UIManager.Instance.isCartaOpen)
                        return;

                    if (!isActive)
                    {
                        Objetivos.Instance.SetObjective("Siga as orientações da carta");
                        imageComponent.sprite = zoomedSprite;
                        imagePanel.SetActive(true);
                        fechar.SetActive(true);
                        Time.timeScale = 0f;
                        UIManager.Instance.isCartaOpen = true;
                        UIManager.Instance.UpdateTimeScale();
                    }
                    else
                    {
                        imagePanel.SetActive(false);
                        fechar.SetActive(false);
                        Time.timeScale = 1f;
                        UIManager.Instance.isCartaOpen = false;
                        UIManager.Instance.UpdateTimeScale();
                    }
                }
            }
            else
            {
                texto.SetActive(false); // Desativa se não estiver olhando para o objeto
            }
        }
        else
        {
            texto.SetActive(false); // Desativa se não houver hit
        }
    }
}

