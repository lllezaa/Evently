<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <h2>Пользователи</h2>
@foreach($users as $user)
    <form method="POST" action="{{ route('admin.users.update', $user->users_id) }}">
        @csrf
        <input type="text" name="name" value="{{ $user->name }}">
        <input type="text" name="lastname" value="{{ $user->lastname }}">
        <input type="email" name="email" value="{{ $user->email }}">
        <input type="text" name="login" value="{{ $user->login }}">
        <input type="number" name="roles_id" value="{{ $user->roles_id }}">
        <button type="submit">Сохранить</button>
    </form>

    <form method="POST" action="{{ route('admin.users.delete', $user->users_id) }}">
        @csrf
        <button type="submit">Удалить</button>
    </form>
    <hr>
@endforeach
<a href="/">Вернуться назад</a>
</body>
</html>