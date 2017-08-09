using UnityEngine;

public class BaseGauge : MonoBehaviour
{
    [SerializeField]
    protected int maxValue = 10;
	int gaugeValue;
	public int Value{ 
		get {
			return gaugeValue;
		}
		set
		{
			gaugeValue = Mathf.Clamp (value, 0, maxValue);
			SetGaugeImage ();
		}
	}
	

    bool isCalc = false;

    public virtual void Start()
    {
		gaugeValue = maxValue;
    }

    public virtual void Update()
    {
    }

    protected virtual void SetGaugeImage()
    {

    }
}
