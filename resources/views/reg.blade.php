<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <h1>Страница регистрации</h1>
    <form method="POST" action="{{ route('register') }}">
    @csrf
    <input type="text" name="name" placeholder="Имя" required>
    <input type="text" name="lastname" placeholder="Фамилия" required>
    <input type="email" name="email" placeholder="Почта" required>
    <input type="text" name="login" placeholder="Логин" required>
    <input type="password" name="password" placeholder="Пароль" required>
    <input type="password" name="password_confirmation" placeholder="Подтвердите пароль" required>
    <button type="submit">Зарегистрироваться</button>
    </form>
    <a href="/">Вернуться назад</a>
</body>
</html>