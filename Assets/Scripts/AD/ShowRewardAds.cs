using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ShowRewardAds : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AdsEngine _adsEngine;
    [Space(10f)]
    [SerializeField]
    private Image ButtonAdsImage;

    [Space(10f)]
    [SerializeField]
    private UnityEvent _adsRewardComplete;

    private void Awake()
    {
        _adsEngine.RewardeStatus += AdsRewardStatus;

        ButtonAdsImage.color = new Color(1f, 1f, 1f, 0.3f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _adsEngine.ShowRewardAd(GetReward);
    }
    public void GetReward()
    {
        _adsRewardComplete.Invoke();
    }
    public void AdsRewardStatus(bool status)
    {
        if(status)
            ButtonAdsImage.color = new Color(1f, 1f, 1f, 1f);
        else
            ButtonAdsImage.color = new Color(1f, 1f, 1f, 0.3f);
    }
}