using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVcam : MonoBehaviour
{
    [field: SerializeField] private float threshold;
    [field: SerializeField] private float centerAnchor = 0.5f;

    private Player player { get; set; }
    private GameManager gameManager { get; set; }

    private void Start()
    {
        player = Player.instance;
        gameManager = GameManager.instance;
    }

    private void LateUpdate()
    {
        Vector3 mousePos = gameManager.mousePos;

        mousePos.x = Mathf.Clamp(mousePos.x, -threshold + player.transform.position.x, threshold + player.transform.position.x);
        mousePos.y = Mathf.Clamp(mousePos.y, -threshold + player.transform.position.y + centerAnchor, threshold + player.transform.position.y + centerAnchor);

        transform.position = Vector3.Lerp(transform.localPosition, mousePos, Time.deltaTime * 3f);
    }
}
