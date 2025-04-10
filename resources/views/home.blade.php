<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <h1>Главная страница</h1>
    <div>
        <a href="/register">Зарегистрироваться</a>
        <a href="/login">Войти</a>
    </div>
    <p>
        <h1>
            ---
        </h1>
    </p>
    @if(Auth::check())
    <h1>Добро пожаловать, {{ Auth::user()->name }}!</h1>
    <form method="POST" action="{{ route('logout') }}">
        @csrf
        <button type="submit">Выйти</button>
    </form>
    @else
        <h1>Вы не авторизованы.</h1>
    @endif
</body>
</html>