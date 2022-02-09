using System.Collections;
using System.Collections.Generic;
using UltimateClean;
using UnityEngine;

public class AdPopupOpen : MonoBehaviour
{
    // Start is called before the first frame update
    
    private PersonalizationAdsUserStatement _userPermission = new PersonalizationAdsUserStatement();
    private AdsEngine _adsEngine;

    private void Awake()
    {
        //Итак тут все понятно, если ранее пользователь не делал заявление о своем отношении к персонализации данных, то мы забиваем в Плайпрефс
        //соотв переменную (вы можете хранить ответ пользователя по другому, главное спросить и загружать рекламу в соотв с ответом)
        if (PlayerPrefs.HasKey("PersonalizationAdsUser_Statement") == false)
        {
            PlayerPrefs.SetString("PersonalizationAdsUser_Statement", JsonUtility.ToJson(_userPermission));
        }

        _adsEngine = GetComponent<AdsEngine>();

        //Читаем данные о заявлении пользователя
        _userPermission = JsonUtility.FromJson<PersonalizationAdsUserStatement>(PlayerPrefs.GetString("PersonalizationAdsUser_Statement"));
    }
    private void Start()
    {
        PlayerPrefs.DeleteKey("PersonalizationAdsUser_Statement");
        GetComponent<PopupOpener>().OpenPopup();
        //Если игрок ранее прошол опрос, то в соотв с ответом инициируем рекламу и закрываем меню Опроса
        if (_userPermission.PersonalizationAdsUser_Statement)
        {
            //Обратите внимание - Инициализация происходит в методе Start, т.е. после выполнения методов Awake, так как в
            //методах Aweke происходит подписка на события загрузки рекламы
            AdsInitializations(_userPermission.PersonalizationAdsUser_Answer);
            
        }
    }

    public void AdsInitializations(bool answer)
    {
        _adsEngine.Initializations(answer);
        //gameObject.SetActive(false);
    }
    
}
