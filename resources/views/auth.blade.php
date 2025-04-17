<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <h1>Страница авторизации</h1>
    <form method="POST" action="{{ route('login') }}">
    @csrf
    <input type="text" name="login" placeholder="Логин" required>
    <input type="password" name="password" placeholder="Пароль" required>
    <button type="submit">Войти</button>
    </form>
    <a href="/">Вернуться назад</a>
</body>
</html>