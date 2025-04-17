<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Type extends Model
{
    protected $primaryKey = 'types_id';
    protected $fillable = [];

    public function events()
    {
        return $this->hasMany(Event::class, 'types_id', 'types_id');
    }
}
