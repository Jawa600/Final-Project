using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpPower : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject player;
    public Slider jumpSlider;

    // Start is called before the first frame update
    private void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    private float CalculateSliderPercentage(float timePassed){
        return timePassed / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        jumpSlider.value = CalculateSliderPercentage(playerController.timePassed);
    }
}
