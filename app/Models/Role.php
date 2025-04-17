<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Role extends Model
{
    protected $primaryKey = 'roles_id';
    protected $fillable = ['title_role'];

    public function users()
    {
        return $this->hasMany(User::class, 'roles_id', 'roles_id');
    }
}
