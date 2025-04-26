<?php

namespace App\Http\Controllers;

use App\Models\Event;
use App\Models\Record_events;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

class EventController extends Controller
{
    public function index()
    {
        $events = Event::with('type')->get();
        return view('home', compact('events'));
    }

    public function create()
    {
        return view('create_event');
    }

    public function store(Request $request)
    {
        $request->validate([
            'title_event' => 'required|string',
            'date_time_event' => 'required|date',
            'place' => 'required|integer',
            'address' => 'required|string',
            'description' => 'required|string',
            'types_id' => 'required|exists:types,types_id',
        ]);

        Event::create($request->all());

        return redirect()->route('home')->with('success', 'Событие успешно создано!');
    }


    public function show($id)
    {
        $event = Event::with('type')->findOrFail($id);

        $isRecorded = false;
        if (Auth::check()) {
            $isRecorded = Record_events::where('users_id', Auth::id())
                                    ->where('events_id', $id)
                                    ->exists();
        }

        return view('show_events', compact('event', 'isRecorded'));
    }

    public function record($id)
    {
        $userId = Auth::id();
    
        // Проверка, существует ли уже запись
        $exists = Record_events::where('users_id', $userId)
                             ->where('events_id', $id)
                             ->exists();
    
        if (!$exists) {
            Record_events::create([
                'users_id' => $userId,
                'events_id' => $id,
            ]);
        }
    
        return redirect()->route('event.show', $id)->with('success', 'Вы записались на событие.');
    }

    public function unrecord($id)
    {
        if (!Auth::check()) {
            return redirect()->route('login');
        }

        $userId = Auth::id();

        Record_events::where('users_id', $userId)
                ->where('events_id', $id)
                ->delete();

        return redirect()->route('event.show', $id)->with('success', 'Вы отменили запись на событие');
    }

}
