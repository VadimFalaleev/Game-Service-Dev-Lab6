# Разработка игровых сервисов
Отчет по лабораторной работе #6 выполнил(а):
- Фалалеев Вадим Эдуардович
- РИ-300012

Отметка о выполнении заданий (заполняется студентом):

| Задание | Выполнение | Баллы |
| ------ | ------ | ------ |
| Задание 1 | * | 60 |
| Задание 2 | * | 20 |
| Задание 3 | * | 20 |

знак "*" - задание выполнено; знак "#" - задание не выполнено;

Работу проверили:
- к.т.н., доцент Денисов Д.В.
- к.э.н., доцент Панов М.А.
- ст. преп., Фадеев В.О.

Структура отчета

- Данные о работе: название работы, фио, группа, выполненные задания.
- Цель работы.
- Задание 1.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 2.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 3.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Выводы.

## Цель работы
создание интерактивного приложения с рейтинговой системой пользователя и интеграция игровых сервисов в готовое приложение.

## Задание 1
### Используя видео-материалы практических работ 1-5 повторить реализацию приведенного ниже функционала:

– 1 Практическая работа «Интеграция баннерной рекламы»

– 2 Практическая работа «Интеграция видеорекламы»

– 3 Практическая работа «Показ видеорекламы пользователю за вознаграждение»

– 4 Практическая работа «Создание внутриигрового магазина»

– 5 Практическая работа «Система антиблокировки рекламы»

Ход работы:

- Зайдем на страницу черновика нашей игры и нажмем на кнопку монетизация в верхнем правом углу. После этого зайдем в раздел Реклама на сайтах -> RTB-блоки и нажмем на кнопку Добавить RTB-блок.

![image](https://user-images.githubusercontent.com/54228342/206964938-69fb6721-b8d3-4013-905e-2a252b9389d2.png)
![image](https://user-images.githubusercontent.com/54228342/206964947-c0d6834b-25a5-4aeb-87fc-e3985c9f51ce.png)

- Выбираем нашу игру, назовем блок DragonPickerTest, нажмем продолжить. После этого выбираем десктопную версию и нажимаем продолжить. Затем выбираем основной дизайн и начинаем его настраивать.

![image](https://user-images.githubusercontent.com/54228342/206965581-e8ab8278-4150-4a61-97a7-cf662e6edb4a.png)
![image](https://user-images.githubusercontent.com/54228342/206965594-39a7cca7-b3d0-4764-9605-e4412b6ca699.png)
![image](https://user-images.githubusercontent.com/54228342/206965602-0c9a80b4-2923-4129-85f4-b86eb81ab3a7.png)

- Максимальное количество объявлений ставим на 2 и выходим из редактирования, нажимая кнопку Сохранить дизайн.

![image](https://user-images.githubusercontent.com/54228342/206965861-beb7cb65-ae9b-4130-9a11-ebb1b01b1c89.png)

- В разделе стратегии ничего не меняем, нажимаем сохранить. Теперь наш блок готов. Стоит скопировать номер данного блока, чтобы мы смогли добавить его в игру.
- Следующим шагом надо зайти в проект Unity, нажать на Edit -> Project Settings... -> Player и в окне Resolution and Presentation напротив опции Banner Static 1 вставить номер скопированного блока.

![image](https://user-images.githubusercontent.com/54228342/206966700-b3b75243-ad61-404c-8fdd-7c3244ea472e.png)

- Зайдем в папку YandexGame > WorkingData и нажмем на InfoYG. В инспекторе поставим галочку напротив опции StaticRBT In Game.

![image](https://user-images.githubusercontent.com/54228342/206967554-d1ee3aa8-8712-4bbe-a616-380733f94230.png)

- Теперь нужно собрать сборку и загрузить ее на Яндекс.Игры, чтобы проверить, что реклама работает. Как видим, реклама запускается.

![image](https://user-images.githubusercontent.com/54228342/206970299-e09dc066-882c-4ee7-87b6-8c5bb4598094.png)

- Добавим видеорекламу в игру. Зайдем в скрипт DragonPicker и добавим строчку кода.

```c#

...

public void DragonEggDestroyed()
    {
        GameObject[] tDragonEggArray = GameObject.FindGameObjectsWithTag("Dragon Egg");
        foreach (GameObject tGO in tDragonEggArray)
            Destroy(tGO);

        int shieldIndex = shieldList.Count - 1;
        GameObject tShieldGo = shieldList[shieldIndex];
        shieldList.RemoveAt(shieldIndex);
        Destroy(tShieldGo);

        if (shieldList.Count == 0)
        {
            GameObject scoreGO = GameObject.Find("Score");
            scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
            UserSave(int.Parse(scoreGT.text), YandexGame.savesData.bestScore);

            YandexGame.NewLeaderboardScores("TOPPlayerScore", int.Parse(scoreGT.text));

            YandexGame.RewVideoShow(0); // new
            SceneManager.LoadScene("_0Scene");
            GetLoadSave();
        }
    }
    
...

```

- Добавим такую же строчку в другой скрипт CheckConnectYG.

```c#

...

public void CheckSDK()
    {
        if (YandexGame.auth)
        {
            Debug.Log("User authorization ok");
        }
        else
        {
            Debug.Log("User not authorization");
            YandexGame.AuthDialog();
        }

        YandexGame.RewVideoShow(0); // new

        GameObject scoreBO = GameObject.Find("BestScore");
        scoreBest = scoreBO.GetComponent<TextMeshProUGUI>();
        scoreBest.text = "Best Score: " + YandexGame.savesData.bestScore.ToString();
    }
    
...

```

- Добавим объекту YandexGame на обоих сценах компонент Viewing Ads YG, и настроим его следующим образом.

![image](https://user-images.githubusercontent.com/54228342/206972806-ed8ee250-b22d-4ce2-a943-aae54c1f278a.png)

- Загрузим проект на сервис и убедимся, что реклама работает.

![image](https://user-images.githubusercontent.com/54228342/206977006-4e3d8fa5-8673-4756-9dd8-435f2548c41b.png)

- Теперь будем добавлять видео за вознаграждение. Создадим скрипт AdReward и добавим его к объекту YandexManager.

```c#

using UnityEngine;
using YG;

public class AdReward : MonoBehaviour
{
    private void OnEnable() => YandexGame.CloseVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.CloseVideoEvent -= Rewarded;

    void Rewarded(int id)
    {
        if(id == 1)
        {
            Debug.Log("Пользователь получил награду");
        }
        else
        {
            Debug.Log("Пользователь остался без награды");
        }
    }

    public void OpenAd()
    {
        YandexGame.RewVideoShow(Random.Range(0,2));
    }
}

```

- Добавим кнопку на главный экран, назовем ее Ad и настроим ее так, чтобы она активировала рекламу за вознаграждение при нажатии.

![image](https://user-images.githubusercontent.com/54228342/206981061-e8af1013-b846-40d9-a2ca-7bba312dc561.png)

- Загрузим билд на Яндекс.Игры и проверим, что кнопка со скриптом работает. Видим, что реклама запускается, и если проверить консоль, то нам покажут логи, говорящие о том, что игрок получил или не получил награду.

![image](https://user-images.githubusercontent.com/54228342/206984156-a429946c-fae9-4d21-a1d1-a66c7eb65dd9.png)
![image](https://user-images.githubusercontent.com/54228342/206984168-1275ed02-842b-41f6-a285-7ef1fd8758f0.png)

- Добавим внутриигровые покупки в нашу игру. Для этого зайдем в папку YandexGame > Prefabs > Payments и оттуда на сцену выложим объект OnePurchase.

![image](https://user-images.githubusercontent.com/54228342/207010153-2e064179-1b1b-4e27-92ef-d7873d8397a8.png)

- Перейдем на страницу черновика нашей игры в раздел покупки и добавим к списку логинов для тестовой покупки свой. После чего нажмем на кнопку Добавить покупку.

![image](https://user-images.githubusercontent.com/54228342/207010513-4fb0e6a9-c692-4c33-ad04-348a6900c298.png)

- Запишем id, название и описание покупки на русском и английском, а так же стоимость покупки в янах. Скопируем id товара и нажмем на кнопку добавить.

![image](https://user-images.githubusercontent.com/54228342/207010765-efbb04aa-58cb-4c70-82d3-dc98e667f70e.png)

- Вернемся в Unity и вставим id товара в компонент Payments YG объекта OnePurchase.

![image](https://user-images.githubusercontent.com/54228342/207011108-76409f32-3403-4926-9f17-bcb01e18966d.png)

- Загрузим проект на сервис и проверим, как работает покупка. При нажатии на кнопку появляется окно покупки. Если купить продукт, то выйдет ошибка. Если посмотреть консоль, то покажут ошибку покупки. Дело в том, что это черновик и для рабочей покупки предметов за яны придется писать в поддержку для одобрения.

![image](https://user-images.githubusercontent.com/54228342/207015332-cdbd7ee4-67aa-4036-8e87-ea2300033e85.png)
![image](https://user-images.githubusercontent.com/54228342/207015380-7f2571b5-9137-48a0-a498-79591999b62c.png)
![image](https://user-images.githubusercontent.com/54228342/207015401-0a7a54df-a8de-4cce-92f0-a9307596c065.png)


## Задание 2
### Добавить в приложение интерфейс для вывода статуса наличия игрока в сети (онлайн или офлайн).

Ход работы:



## Задание 3
### Предложить наиболее подходящий на ваш взгляд способ монетизации игры D.Picker. Дать развернутый ответ с комментариями.

Ход работы:



## Выводы

- 
