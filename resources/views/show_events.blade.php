<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>

<h1>{{ $event->title_event }}</h1>
<p>{{ $event->description }}</p>
<p>Адрес: {{ $event->address }}</p>
<p>Дата и время: {{ $event->date_time_event }}</p>
<p>Тип события: {{ $event->type->types_id ?? '—' }}</p>

@if(Auth::check())
    @if($isRecorded)
        <form method="POST" action="{{ route('event.unrecord', $event->events_id) }}">
            @csrf
            @method('DELETE')
            <button type="submit" style="background-color: red; color: white;">Отменить запись</button>
        </form>
    @else
        <form method="POST" action="{{ route('event.record', $event->events_id) }}">
            @csrf
            <button type="submit">Записаться на событие</button>
        </form>
    @endif
@else
    <p><a href="{{ route('login') }}">Войдите, чтобы записаться</a></p>
@endif
    
    <a href="/">Вернуться назад</a>
</body>
</html>