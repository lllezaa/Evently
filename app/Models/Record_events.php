<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Record_events extends Model
{
    protected $primaryKey = 'record_events_id';
    protected $fillable = ['users_id', 'events_id'];

    public function user()
    {
        return $this->belongsTo(User::class, 'users_id', 'users_id');
    }

    public function event()
    {
        return $this->belongsTo(Event::class, 'events_id', 'events_id');
    }
}
