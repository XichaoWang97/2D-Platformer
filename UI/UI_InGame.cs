using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Player player;
    protected Rigidbody2D rb;
    public TextMeshProUGUI VelocityX; //监控玩家速度
    public TextMeshProUGUI VelocityY; //监控玩家速度
    public Slider staminaSlider; //体力条
    public TextMeshProUGUI staminasliderValue;

    private void Start()
    {
        rb = _player.GetComponent<Rigidbody2D>();
        player = _player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        VelocityX.text = rb.velocity.x.ToString();
        VelocityY.text = rb.velocity.y.ToString();

        staminaSlider.value = player.curStamina;
        staminasliderValue.text = player.curStamina.ToString();
    }
}
