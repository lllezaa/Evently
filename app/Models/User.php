<?php

namespace App\Models;

use Illuminate\Foundation\Auth\User as Authenticatable;

class User extends Authenticatable
{
    protected $primaryKey = 'users_id';
    protected $fillable = [
        'name', 'lastname', 'login', 'password', 'email', 'roles_id'
    ];

    public function role()
    {
        return $this->belongsTo(Role::class, 'roles_id', 'roles_id');
    }

    public function records()
    {
        return $this->hasMany(Record_events::class, 'users_id', 'users_id');
    }

}