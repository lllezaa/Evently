<?php

namespace App\Http\Controllers;

use App\Models\Event;
use Illuminate\Http\Request;

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
}
