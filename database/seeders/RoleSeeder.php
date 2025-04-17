<?php

namespace Database\Seeders;

use App\Models\Role;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class RoleSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        $roles = [
            ['title_role' => 'Администратор'],
            ['title_role' => 'Модератор'],
            ['title_role' => 'Пользователь'],
        ];

        foreach ($roles as $role) {
            Role::create($role);
        }
    }
}
