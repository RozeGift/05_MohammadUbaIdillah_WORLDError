using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public const float MAX_HEALTH = 100f;

    public static float health = MAX_HEALTH;

    //testing healthui
  
    
    private Image healthbar;
    // Start is called before the first frame update
    void Start()
    {
        
        healthbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = health / MAX_HEALTH;
        
    }
}
