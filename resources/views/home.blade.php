<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <h1>Главная страница</h1>
    @if(Auth::check())
        <h1>Добро пожаловать, {{ Auth::user()->name }}!</h1>
        <form method="POST" action="{{ route('logout') }}">
            @csrf
            <button type="submit">Выйти</button>
        </form>
        
        @if(Auth::user()->roles_id == 1)
            <div>
                <h2>Панель администратора</h2>
                <a href="{{ route('create.event') }}">Создать событие</a>
                <a href="{{ route('admin.events') }}">Редактировать события</a><br>
                <a href="{{ route('admin.users') }}">Просмотр всех пользователей</a>
            </div>
        @endif
    @else
        <h1>Вы не авторизованы.</h1>
        <div>
            <a href="/register">Зарегистрироваться</a>
            <a href="/login">Войти</a>
        </div>
    @endif
    <p>
        <h1>
            ---
        </h1>
    </p>


    <h2>События</h2>
    @foreach($events as $event)
        <div style="margin: 10px 0px; padding: 0px 10px; border: solid 2px black;">
            <h3>{{ $event->title_event }}</h3>
            <p>{{ $event->description }}</p>
            <p>Адрес: {{ $event->address }}</p>
            <p>Дата и время: {{ $event->date_time_event }}</p>
            <p>Тип события: {{ $event->type->title }}</p>
        </div>
    @endforeach

    
</body>
</html>