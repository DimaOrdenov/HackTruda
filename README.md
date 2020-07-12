# HackTruda

## МигРу - прототип социальной сети для мигрантов
Помимо базового функционала соцсетей (лента, сториз, профиль, сообщения, уведомления) в прототип заложены сервисы для повседневных нужд,
социальные сервисы (поиск соотечественников, места и мероприятия) и развлечения (игры, подкасты).
__Прототип кроссплатформенный и работает сразу на платформах Android и iOS.__ 
__Все решение написано на одном языке (C#) - это упрощает разработку и поддержку, а так же позволяет разработчикам в команде быть взаимозаменяемыми.__

### Frontend
Для мобильного приложения используется Xamarin Forms - __кроссплатформенный фреймворк для нативных приложений__. 
Паттерн __MVVM__ упрощает поддержку и развитие кодовой базы, что будет полезно для развития сервисов и интеграций.

__Проект HackTruda - общая часть для всех платформ, HackTruda.Android и HackTruda.iOS - проекты для конкретных платформ__.
В репозитории разделы представлены VM в директории ViewModels и страницами/представлениями в директории Views
Взаимодействие с бэкендом представлена в директории BusinessLogic в виде Rest-запросов

### Backend
Серверная часть выполнена в виде __REST API на базе Net Core__.
Для чатов используется __real-time взаимодействие с помощью SignalR__. __Документация ведется по стандарту Open API (Swagger)__.

Проект HackTruda.API содержит контроллеры в директории Controllers соответствующие разделам и функционалу приложения
Во время разработки серверная часть хостилась на AWS в рамках бесплатного периода, а значит на этом этапе __затраты на инфраструктуру отсутствуют__.
