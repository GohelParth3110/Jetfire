using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiLoadIngScreen : MonoBehaviour
{
    [SerializeField] private float flt_LoadingTIme;
    [SerializeField] private GameObject playerone;
    [SerializeField] private GameObject playerTwo;

    [SerializeField] private Slider slider_Loading;



    private void Start()
    {
        
            PlayerAnimation();
         
       

       

    }

   

    private void PlayerAnimation()
    {
        playerone.transform.DOMove(new Vector3(5, -3.8f, 0), flt_LoadingTIme);
        playerTwo.transform.DOMove(new Vector3(18.6f, -3.8f, 0), flt_LoadingTIme);


    }

    private void Update()
    {
        slider_Loading.value -= Time.deltaTime;

        if (slider_Loading.value <= 0)
        {
            LoadScene();
        }
    }
    public    void LoadScene()
    {

        this.gameObject.SetActive(false);

        Destroy(playerone);
        Destroy(playerTwo);
        PlayerManager.Instance.SetPlayerIndex(DataManager.instance.currentPlayerIndex);
        PlayerManager.Instance.StartGameManager();
        UiManager.instance.GetCoinPanel().SetTargetCoin();
    }



    
}
