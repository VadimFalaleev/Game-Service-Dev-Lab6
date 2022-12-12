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



## Задание 2
### Добавить в приложение интерфейс для вывода статуса наличия игрока в сети (онлайн или офлайн).

Ход работы:



## Задание 3
### Предложить наиболее подходящий на ваш взгляд способ монетизации игры D.Picker. Дать развернутый ответ с комментариями.

Ход работы:



## Выводы

- 
