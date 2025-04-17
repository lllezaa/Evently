<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <h2>Редактирование событий</h2>
@foreach($events as $event)
    <form method="POST" action="{{ route('admin.events.update', $event->events_id) }}">
        @csrf
        <input type="text" name="title_event" value="{{ $event->title_event }}">
        <input type="text" name="description" value="{{ $event->description }}">
        <input type="text" name="address" value="{{ $event->address }}">
        <input type="datetime-local" name="date_time_event" value="{{ \Carbon\Carbon::parse($event->date_time_event)->format('Y-m-d\TH:i') }}">
        <input type="number" name="types_id" value="{{ $event->types_id }}">
        <button type="submit">Сохранить</button>
    </form>

    <form method="POST" action="{{ route('admin.events.delete', $event->events_id) }}">
        @csrf
        <button type="submit">Удалить</button>
    </form>
    <hr>
@endforeach
<a href="/">Вернуться назад</a>
</body>
</html>