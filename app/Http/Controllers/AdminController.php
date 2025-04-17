<?php

namespace App\Http\Controllers;

use App\Models\Event;
use App\Models\User;
use Illuminate\Http\Request;

class AdminController extends Controller
{
    public function editEvents() {
        $events = Event::with('type')->get();
        return view('edit_events', compact('events'));
    }

    public function updateEvent(Request $request, $id) {
        $event = Event::findOrFail($id);
        $event->update($request->only([
            'title_event', 'description', 'address', 'date_time_event', 'types_id'
        ]));
        return back()->with('success', 'Событие обновлено');
    }

    public function deleteEvent($id) {
        Event::destroy($id);
        return back()->with('success', 'Событие удалено');
    }

    public function editUsers() {
        $users = User::orderBy('users_id', 'asc')->get();
        return view('edit_users', compact('users'));
    }

    public function updateUser(Request $request, $id) {
        $user = User::findOrFail($id);
        $user->update($request->only(['name', 'lastname', 'email', 'login', 'roles_id']));
        return back()->with('success', 'Пользователь обновлён');
    }

    public function deleteUser($id) {
        User::destroy($id);
        return back()->with('success', 'Пользователь удалён');
    }
}
