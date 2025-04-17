<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Event extends Model
{
    protected $primaryKey = 'events_id';
    protected $fillable = ['title_event', 'date_time_event', 'place', 'address', 'description', 'types_id'];

    public function type()
    {
        return $this->belongsTo(Type::class, 'types_id', 'types_id');
    }

    public function records()
    {
        return $this->hasMany(Record_events::class, 'events_id', 'events_id');
    }
}
