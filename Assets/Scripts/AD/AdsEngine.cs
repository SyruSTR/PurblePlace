using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Нашёл в интеренете сразу говорю)
public class AdsEngine : MonoBehaviour
{
    public bool AdsDisable;

    private bool _personalizationAdsUser_Answer;
    private bool _isResumeGameOnShowingAd;
    
    public bool AppOnenedAdLoad;
    
    private DateTime _appOpenedLoadTime;
    
    
    //test key ca-app-pub-3940256099942544/6300978111
    private const string RewardId = "";

    private RewardedAd _rewardedAd;

    public bool RewardeAdsLoad;
    public Action RewardEvent;
    public Action<bool> RewardeStatus;

    // Start is called before the first frame update
    public void Initializations(bool personalizationAnswer)
    {
        /*
        //Пример Настройки таргетинга. Настройка ориентированная на Детей, необходимо установить настройки до инициализации AdMob SDK
        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
        */

        //Запоминаем ответ пользователя о персонализации рекламы
        _personalizationAdsUser_Answer = personalizationAnswer;

        //Инициализация SDK Рекламы AdMob
        MobileAds.Initialize(initStatus => { LoadAds(); });
    }
    

    private void LoadAds()
    {
        //Загружаем рекламу, если она не отключена
        if (AdsDisable == false)
        {
            ////Загружаем AppOpenedAds рекламу
            //RequestAndLoadAppOpenAd();

            ////Загружае Баннерную рекламу
            //RequestBanner();

            ////Загружаем Интерститиальную рекламу
            //RequestInterstitial();

            ////Загружаем Вознаграждаемую интерститиальную рекламу
            //RequestRewardInterstitial();
        }

        //Загружаем Вознаграждаемую рекламу (не отключается)
        RequestRewardedAd();
    }
    

    

    public void RequestRewardedAd()
    {
        //Создаем объект вознагражденной рекламы
        _rewardedAd = new RewardedAd(RewardId);

        //Добавляем слушателей: Удачная загрузка рекламы, Неудачная загрузка рекламы, Отвечающий за вознаграждение, Закрытие объявления
        _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardedAd.OnAdOpening += HandleRewardAdOpened;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        //Загружаем рекламу
        _rewardedAd.LoadAd(GetAdRequest());
    }
    
    public void ShowRewardAd(Action enterDelegateReward)
    {
        if (_rewardedAd != null && RewardeAdsLoad)
        {
            if (_rewardedAd.IsLoaded())
            {
                //Назначаем делегат вознаграждающий пользователя
                RewardEvent = enterDelegateReward;

                //Инициируем показ рекламы
                _rewardedAd.Show();
            }
        }
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        //Отмечаем удачную загрузку рекламы
        RewardeAdsLoad = true;

        //Вызываем Делегаты подписанные на событие загрузки рекламы, она позволяет кнопкам узначть, что реклама загружена, готова к показу и изменить свое состояние
        RewardeStatus?.Invoke(true);
    }

    //Вызывается  когда по каким то причинам объявление не загружается - передает и перечисляет ошибки
    private void HandleRewardedAdFailedToLoad(object sender, EventArgs args)
    {
        //Отмечаем НЕ удачную загрузку рекламы
        RewardeAdsLoad = false;

        //Вызываем Делегаты подписанные на событие неудачной загрузки рекламы, она позволяет кнопкам узначть, что реклама НЕ была загружена, НЕ готова к показу и изменить свое состояние
        RewardeStatus?.Invoke(false);
    }

    //Вызывается  когда пользователь должен быть вознагражден за просмотр видео.
    private void HandleUserEarnedReward(object sender, Reward args)
    {
        //Вызываем Делегаты отвечающие за Выполнение действий, связанных с вознаграждением, обнуляем слушателя
        RewardEvent?.Invoke();

        //Вызываем событие выгрузки рекламы
        RewardeStatus?.Invoke(false);

        //Отмечаем что реклама выгружена и новая порция не загружена
        RewardeAdsLoad = false;
    }

    //Вызывается когда объявление открывается
    private void HandleRewardAdOpened(object sender, EventArgs args)
    {
        //Отмечаем что начат показ рекламы
        _isResumeGameOnShowingAd = true;
    }

    //Вызывается  когда видеореклама с вознаграждением закрывается из-за того, что пользователь нажимает на значок закрытия или кнопку Назад.
    private void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        //Если пользователь закрыл объявление раньше времени - Загружаем новое объявление
        RequestRewardedAd();
    }

    private AdRequest GetAdRequest()
    {
        //Если пользователь дал согласие на Персонализацию рекламы - создаем простой универсальный запрос по умолчанию, он предоставляет персонализированные объявления
        if (_personalizationAdsUser_Answer)
            return new AdRequest.Builder().Build();

        //Если пользователь отказался от персонализации рекламы - создаем запрос на неперсонифицированную рекламу (npa - Non-personalized ads (NPA) и 1 активна)
        return new AdRequest.Builder().AddExtra("npa", "1").Build();
    }
}

