using UnityEngine;

public class MonsterExp : MonoBehaviour
{
    public float count = 0.7f;
    public float exp = 30;
    private Home _home;
    private void Awake()
    {
        _home = GameObject.FindWithTag("House").GetComponent<Home>();
    } 

    private void Update(){
        count -= Time.deltaTime;
        if(count <= 0){
            Destroy(gameObject);
            _home.OnGetExp(exp);
        }
    }
}