<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <h2>Создать событие</h2>
    <form method="POST" action="{{ route('store.event') }}">
        @csrf
        <input type="text" name="title_event" placeholder="Название" required><br>
        <input type="datetime-local" name="date_time_event" required><br>
        <input type="number" name="place" placeholder="Места" required><br>
        <input type="text" name="address" placeholder="Адрес" required><br>
        <textarea name="description" placeholder="Описание" required></textarea><br>
        <input type="number" name="types_id" placeholder="ID типа" required><br>
        <button type="submit">Создать</button>
    </form>
    <a href="/">Вернуться назад</a>
</body>
</html>