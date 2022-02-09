using System.Collections;
using System.Collections.Generic;
using System;
using UltimateClean;
using UnityEngine;

public class PersonalizeQuestionsAds : MonoBehaviour
{
    private AdPopupOpen _adPopupOpen; 

    private PersonalizationAdsUserStatement _userPermission = new PersonalizationAdsUserStatement();

    private void Awake()
    {
        //Итак тут все понятно, если ранее пользователь не делал заявление о своем отношении к персонализации данных, то мы забиваем в Плайпрефс
        //соотв переменную (вы можете хранить ответ пользователя по другому, главное спросить и загружать рекламу в соотв с ответом)
        if (PlayerPrefs.HasKey("PersonalizationAdsUser_Statement") == false)
        {
            PlayerPrefs.SetString("PersonalizationAdsUser_Statement", JsonUtility.ToJson(_userPermission));
        }

        _adPopupOpen = FindObjectOfType<AdPopupOpen>();
        //Читаем данные о заявлении пользователя
        _userPermission = JsonUtility.FromJson<PersonalizationAdsUserStatement>(PlayerPrefs.GetString("PersonalizationAdsUser_Statement"));
    }

    

    //Сдесь мы обрабатываем ответ пользователя
    public void UserAnswerProcessing(bool answer)
    {
        //Согласно документации Unity: true - согласен, false - не согласен с персонализацией

        _adPopupOpen.AdsInitializations(answer);

        //Отмечаем что пользователь сделал свое заявление в отношении персонализации рекламы и сохраняем его ответ в Плайпрефс
        _userPermission.PersonalizationAdsUser_Statement = true;
        _userPermission.PersonalizationAdsUser_Answer = answer;
        PlayerPrefs.SetString("PersonalizationAdsUser_Statement", JsonUtility.ToJson(_userPermission));
        
        GetComponent<Popup>().Close();
    }

    //Метод для тестирования экрана Заявления пользователя
    public void CleareAllPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("PersonalizationAdsUser_Statement");
        _userPermission = new PersonalizationAdsUserStatement();
        PlayerPrefs.SetString("PersonalizationAdsUser_Statement", JsonUtility.ToJson(_userPermission));
        
        gameObject.SetActive(true);
    }
    
    
}


//Класс для хранения данных об опросе пользователя
[Serializable]
public class PersonalizationAdsUserStatement
{
    public bool PersonalizationAdsUser_Statement;
    public bool PersonalizationAdsUser_Answer;
}
