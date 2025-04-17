<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up()
    {
        Schema::create('users', function (Blueprint $table) {
            $table->id('users_id');
            $table->string('name');
            $table->string('lastname');
            $table->string('login')->unique();
            $table->string('password');
            $table->string('email')->unique();
            $table->unsignedBigInteger('roles_id')->default(3);
            $table->foreign('roles_id')->references('roles_id')->on('roles');
            $table->timestamps();
        });
    }

    public function down()
    {
        Schema::dropIfExists('users');
    }
};
