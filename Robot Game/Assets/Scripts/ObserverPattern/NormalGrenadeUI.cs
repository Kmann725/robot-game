using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NormalGrenadeUI : MonoBehaviour, IPlayerObserver
{
    private TextMeshProUGUI counter;

    void Awake()
    {
        counter = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.RegisterPlayerObserver(this);
    }

    public void UpdateData(PlayerData data)
    {
        counter.text = "" + data.NormalGrenadeCount;
    }
}
