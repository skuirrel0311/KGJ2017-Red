using UnityEngine;
using UnityEngine.UI;

public class BarGauge : BaseGauge
{
	[SerializeField]
	Image pointImage = null;

    protected override void SetGaugeImage()
    {
		pointImage.fillAmount = (float)Value / maxValue;
    }
}
