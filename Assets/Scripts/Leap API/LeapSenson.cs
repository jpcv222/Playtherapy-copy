using UnityEngine;
using UnityEngine.UI;
using LeapAPI;



public class LeapSenson : MonoBehaviour
{
    public Text angle;
        
    void Start()
    {
        //new LeapService().CreateService();
        LeapService.CreateController();
    }

    // Update is called once per frame
    void Update()
    {
        //angle.text = "" + new Movements().FlexoExtenIF(Leap.Finger.FingerType.TYPE_INDEX);
		angle.text = "" + (int)new Movements().UlnarRadial();
    }

    private void OnDestroy()
    {
        LeapService.DestroyController();
    }

    private void OnApplicationQuit()
    {
        LeapService.DestroyController();
    }
}


